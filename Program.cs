/* TODO:
 * ? do not snap via to via
 *
 * - locking doesn't work for bendable components (select attached doesn't check lock?)
 * - context menu option to detach individual connection points and all connection points for selected parts
 * - Ctrl-Shift-A and Ctrl-Alt-A - select all parts with the same (or inherited) type (and value) as the selected ones (can be multiple)
 * - per-document grid settings, save & load
 * - wire & track midpoints, split and join
 * - support to convert built-in tracks to dynamic
 * - cut tracks (convert to dynamic 1st)
 * - shift while dragging to snap direction to 45 degrees
 * - drag (rectangle) selection
 * - proper part snapping (including bendable pins), when snapping multiple parts do not snap individual elements
 *   - check if it fixes the bug with moving bendable components
 * - solve the problem where rotation causes part to "drift"
 * - rotate tracks & wires with one point attached properly (around the attached point)
 * - groups
 * - auto vias
 * - make dragging of bendable component legs less annoying (when u want to drag the part)
 * ? wire and track labels
 * - label part (comments, notes, etc.)
 
 * - custom draw from lua (provide DrawLine, Rect, Arc, etc. in part local coords)
 * - custom right-click menu options from lua (e.g. flip a switch)
 * 
 * - enable / disable console autoscroll 
 * - flash console tab on new output (if not active)
 * - recursive *.lua load
 * - reload .lua and refresh parts list and placed parts (make refresh available as a separate command)
 * - xpcall part inits, do not add part on errors
 *
 * - trace connections
 * - option to delete board but keep used connections - convert to dynamic
 * - auto wire & track colors
 * 
 * - parts by category and recently used, favorites?
 * - right click in part trees -> option to select all of this type (or inherited)
 * - parts filter
 * 
 * - properties panel
 *   - via colors (ring and connection)
 *   - pin properties
 *   - double clic on properties title to set selection to corresponding parts
 *   - rememer changed properties
 *   - display changed property labels in blue
 *   - reset changed properties on label doubleclick
 *   - save & load
 *   
 * - SMD (pads and legs)
 * 
 * - double layers support
 * 
 * - simulation
*/


using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

using NLua;

namespace ProtoBoard
{
    public class AABB
    {
        public float min_x = 0;
        public float min_y = 0;
        public float max_x = -1;
        public float max_y = -1;

        public AABB() { }

        public bool Empty { get { return max_x < min_x || max_y < min_y; } }

        public float Width { get { return max_x - min_x; } }
        public float Height { get { return max_y - min_y; } }

        public PointF Min { get { return new PointF(min_x, min_y); } }
        public PointF Max { get { return new PointF(max_x, max_y); } }
        public PointF Center { get { return new PointF((min_x + max_x) / 2, (min_y + max_y) / 2); } }

        public RectangleF Rect { get { return new RectangleF(min_x, min_y, Width, Height); } }

        public void Add(float x, float y)
        {
            if (Empty)
            {
                min_x = max_x = x;
                min_y = max_y = y;
                return;
            }
            if (x < min_x) min_x = x;
            if (y < min_y) min_y = y;
            if (x > max_x) max_x = x;
            if (y > max_y) max_y = y;
        }

        public void Add(PointF pt) { Add(pt.X, pt.Y); }

        public void Add(AABB aabb)
        {
            if (aabb.Empty) return;
            Add(aabb.min_x, aabb.min_y);
            Add(aabb.max_x, aabb.max_y);
        }

        public void Grow(float g)
        {
            if (max_x < min_x) max_x = min_x + g; else max_x += g;
            if (max_y < min_y) max_y = min_y + g; else max_y += g;
            min_x -= g;
            min_y -= g;
        }

        public bool Contains(float x, float y) { return x >= min_x && x <= max_x && y >= min_y && y <= max_y; }
        public bool Contains(PointF pt) { return Contains(pt.X, pt.Y); }
    }

    public class Geom
    {
        public static PointF Add(PointF pt1, PointF pt2) { return new PointF(pt1.X + pt2.X, pt1.Y + pt2.Y); }
        public static PointF Sub(PointF pt1, PointF pt2) { return new PointF(pt1.X - pt2.X, pt1.Y - pt2.Y); }
        public static PointF Mul(PointF pt, float f) { return new PointF(pt.X * f, pt.Y * f); }
        public static PointF Div(PointF pt, float f) { return new PointF(pt.X / f, pt.Y / f); }
        
        public static float Dot(PointF pt1, PointF pt2) { return pt1.X * pt2.X + pt1.Y * pt2.Y; }

        public static float MapPtToLine(PointF pt, PointF pt1, PointF pt2)
        {
            PointF v = Sub(pt2, pt1);
            PointF p = Sub(pt, pt1);
            float t = Dot(p, v);
            return t;
        }

        public static PointF SnapPtToLineSeg(PointF pt, PointF pt1, PointF pt2)
        {
            PointF v = Sub(pt2, pt1);
            PointF p = Sub(pt, pt1);
            float t = Dot(p, v);
            if (t <= 0)
                return pt1;
            float len2 = SqrLen(v);
            if (t >= len2)
                return pt2;
            return Add(pt1, Mul(v, t / len2));
        }

        public static float DistPtToLineSeg(PointF pt, PointF pt1, PointF pt2)
        {
            PointF pts = SnapPtToLineSeg(pt, pt1, pt2);
            return Dist(pt, pts);
        }


        public static PointF Rotate(PointF pt, float rot)
        {
            double rad = (Math.PI / 180) * rot;
            double sin = Math.Sin(rad);
            double cos = Math.Cos(rad);
            float x = Convert.ToSingle(pt.X * cos - pt.Y * sin);
            float y = Convert.ToSingle(pt.X * sin + pt.Y * cos);
            return new PointF(x, y);
        }

        public static PointF Rotate(PointF pt, float rot, PointF ptOrigin)
        {
            return Add(ptOrigin, Rotate(Sub(pt, ptOrigin), rot));
        }

        public static float SqrLen(PointF pt)
        {
            return pt.X * pt.X + pt.Y * pt.Y;
        }

        public static float Length(PointF pt)
        {
            return (float) Math.Sqrt(SqrLen(pt));
        }

        public static float Dist(PointF pt1, PointF pt2)
        {
            return Length(Sub(pt2, pt1));
        }

        public static bool Eq(PointF pt1, PointF pt2)
        {
            const float eps = 0.01f;
            float dx = pt2.X - pt1.X;
            float dy = pt2.Y - pt1.Y;
            return (dx > -eps) && (dx < eps) && (dy > -eps) && (dy < eps);
        }

        public static int CalcIntersection(PointF pt1, PointF v1, PointF pt2, PointF v2, ref float t1, ref float t2)
        {
            float dx = pt1.X - pt2.X;
            float dy = pt1.Y - pt2.Y;
            float mul1 = dy * v2.X - dx * v2.Y;
            float mul2 = dy * v1.X - dx * v1.Y;
            float div = v1.X * v2.Y - v1.Y * v2.X;
            //!!! floating points, use epsilons for equality checks?
            if (div != 0) { // lines intersect
                t1 = mul1 / div;
                t2 = mul2 / div;
                return 0;
            }
            if (mul1 == 0 && mul2 == 0) return 2; // incident
            return 1; // parallel
        }

        public static float DistLineSegToLineSeg(PointF pt11, PointF pt12, PointF pt21, PointF pt22)
        {
            PointF v1 = Geom.Sub(pt12, pt11);
            PointF v2 = Geom.Sub(pt22, pt21);
            float t1 = 0, t2 = 0;
            int res = CalcIntersection(pt11, v1, pt21, v2, ref t1, ref t2);
            if (res == 0 && t1 >= 0 && t1 <= 1 && t2 >= 0 && t2 <= 1)
                return 0;
            float d11 = Geom.DistPtToLineSeg(pt11, pt21, pt22);
            float d12 = Geom.DistPtToLineSeg(pt12, pt21, pt22);
            float d21 = Geom.DistPtToLineSeg(pt21, pt11, pt12);
            float d22 = Geom.DistPtToLineSeg(pt22, pt11, pt12);
            float d = Math.Min(Math.Min(d11, d12), Math.Min(d21, d22));
            return d;
        }

        public static float Angle(PointF pt)
        {
            double rad = Math.Atan2(pt.Y, pt.X);
            return Convert.ToSingle(rad * 180 / Math.PI);
        }

        public static float Angle(PointF pt0, PointF pt)
        {
            return Angle(Sub(pt, pt0));
        }

        public static float Snap(float val, float origin, float step)
        {
            if (step == 0) return val;
            float d = val - origin;
            float div = d / step;
            float rounded_div = Convert.ToSingle(Math.Round(div));
            float rounded_d = rounded_div * step;
            float res = origin + rounded_d;
            return res;
        }

        public static PointF Snap(PointF pt, PointF origin, float step)
        {
            return new PointF(Snap(pt.X, origin.X, step), Snap(pt.Y, origin.Y, step));
        }
    }

    public class Contact
    {
        public ContactsList own_contacts;
        public ContactsList direct_contacts;
        public bool trace_open = false;
        public bool trace_closed = false;

        public Contact()
        {
            own_contacts = new ContactsList(this);
            direct_contacts = new ContactsList(this);
        }

        public static bool Check_Pin_Pin(Pin c1, Pin c2) { return c1 != c2 && Geom.Eq(c1.pos, c2.pos); }
        public static bool Check_Pin_Via(Pin c1, Via c2) { return Geom.Eq(c1.pos, c2.pos); }
        public static bool Check_Pin_Wire(Pin c1, Connection c2)
        {
            return c2.cp1.valid_contact && Geom.Eq(c1.pos, c2.cp1.pos) || Geom.Eq(c1.pos, c2.cp2.pos);
        }
        public static bool Check_Pin_Track(Pin c1, Connection c2)
        {
            return Geom.DistPtToLineSeg(c1.pos, c2.cp1.pos, c2.cp2.pos) <= c1.radius + c2.width / 2;
        }

        public static bool Check_Via_Via(Via c1, Via c2) { return c1 != c2 && Geom.Eq(c1.pos, c2.pos); }
        public static bool Check_Via_Wire(Via c1, Connection c2)
        {
            return c2.cp1.valid_contact && Geom.Eq(c1.pos, c2.cp1.pos) || Geom.Eq(c1.pos, c2.cp2.pos);
        }
        public static bool Check_Via_Track(Via c1, Connection c2)
        {
            return Geom.DistPtToLineSeg(c1.pos, c2.cp1.pos, c2.cp2.pos) <= c1.ring_radius + c2.width / 2;
        }

        public static bool Check_Wire_Wire(Connection c1, Connection c2)
        {
            if (c1 == c2) return false;
            if (c1.cp1.valid_contact)
            {
                if (c2.cp1.valid_contact && Geom.Eq(c1.cp1.pos, c2.cp1.pos)) return true;
                if (Geom.Eq(c1.cp1.pos, c2.cp2.pos)) return true;
            }
            if (c2.cp1.valid_contact && Geom.Eq(c1.cp2.pos, c2.cp1.pos)) return true;
            if (Geom.Eq(c1.cp2.pos, c2.cp2.pos)) return true;
            return false;
        }
        public static bool Check_Wire_Track(Connection c1, Connection c2)
        {
            if (c1.cp1.valid_contact && Geom.DistPtToLineSeg(c1.cp1.pos, c2.cp1.pos, c2.cp2.pos) <= c2.width / 2) return true;
            if (Geom.DistPtToLineSeg(c1.cp2.pos, c2.cp1.pos, c2.cp2.pos) <= c2.width / 2) return true;
            return false;
        }

        public static bool Check_Track_Track(Connection c1, Connection c2)
        {
            if (c1 == c2) return false;
            // not implemented
            PointF pt11 = c1.cp1.pos;
            PointF pt12 = c1.cp2.pos;
            PointF pt21 = c2.cp1.pos;
            PointF pt22 = c2.cp2.pos;
            float dist = Geom.DistLineSegToLineSeg(pt11, pt12, pt21, pt22);
            if (dist > (c1.width + c2.width) / 2) return false;
            return true;
        }

        public static bool Check(Contact c1, Contact c2)
        {
            Pin pin1 = c1 as Pin;
            if (pin1 != null)
            {
                Pin pin2 = c2 as Pin;
                if (pin2 != null) return Check_Pin_Pin(pin1, pin2);
                Via via2 = c2 as Via;
                if (via2 != null) return Check_Pin_Via(pin1, via2);
                Connection con2 = c2 as Connection;
                if (con2 != null)
                {
                    if (con2.type == "Wire") return Check_Pin_Wire(pin1, con2);
                    if (con2.type == "Track") return Check_Pin_Track(pin1, con2);
                    throw new InvalidOperationException();
                    //return false;
                }
                throw new InvalidOperationException();
                //return false;
            }

            Via via1 = c1 as Via;
            if (via1 != null)
            {
                Pin pin2 = c2 as Pin;
                if (pin2 != null) return Check_Pin_Via(pin2, via1);
                Via via2 = c2 as Via;
                if (via2 != null) return Check_Via_Via(via1, via2);
                Connection con2 = c2 as Connection;
                if (con2 != null)
                {
                    if (con2.type == "Wire") return Check_Via_Wire(via1, con2);
                    if (con2.type == "Track") return Check_Via_Track(via1, con2);
                    throw new InvalidOperationException();
                    //return false;
                }
                throw new InvalidOperationException();
                //return false;
            }

            Connection con1 = c1 as Connection;
            if (con1 != null)
            {
                if (con1.type == "Wire")
                {
                    Pin pin2 = c2 as Pin;
                    if (pin2 != null) return Check_Pin_Wire(pin2, con1);
                    Via via2 = c2 as Via;
                    if (via2 != null) return Check_Via_Wire(via2, con1);
                    Connection con2 = c2 as Connection;
                    if (con2 != null)
                    {
                        if (con2.type == "Wire") return Check_Wire_Wire(con1, con2);
                        if (con2.type == "Track") return Check_Wire_Track(con1, con2);
                        throw new InvalidOperationException();
                        //return false;
                    }
                    throw new InvalidOperationException();
                    //return false;
                }
                if (con1.type == "Track")
                {
                    Pin pin2 = c2 as Pin;
                    if (pin2 != null) return Check_Pin_Track(pin2, con1);
                    Via via2 = c2 as Via;
                    if (via2 != null) return Check_Via_Track(via2, con1);
                    Connection con2 = c2 as Connection;
                    if (con2 != null)
                    {
                        if (con2.type == "Wire") return Check_Wire_Track(con2, con1);
                        if (con2.type == "Track") return Check_Track_Track(con1, con2);
                        throw new InvalidOperationException();
                        //return false;
                    }
                    throw new InvalidOperationException();
                    //return false;
                }
                throw new InvalidOperationException();
                //return false;
            }

            throw new InvalidOperationException();
            //return false;
        }

        public void TraceContacts(ContactsList lst)
        {
            if (trace_closed) return;
            trace_closed = true;
            
            if (!trace_open)
            {
                trace_open = true;
                lst.Add(this);
            }

            foreach (Contact c in own_contacts.all)
            {
                if (!c.trace_open)
                {
                    c.trace_open = true;
                    lst.Add(c);
                }
            }
            foreach (Contact c in direct_contacts.all)
            {
                if (!c.trace_open)
                {
                    c.trace_open = true;
                    lst.Add(c);
                }
            }
        }
    }

    public class ContactsList
    {
        public int uid = ++Program.uid;

        public Contact owner = null;

        public List<Contact> all = new List<Contact>();
        public List<Pin> pins = new List<Pin>();
        public List<Via> vias = new List<Via>();
        public List<Connection> wires = new List<Connection>();
        public List<Connection> tracks = new List<Connection>();

        public ContactsList(Contact obj = null) { owner = obj; }

        public void Add(Contact obj)
        {
            all.Add(obj);
            
            Pin pin = obj as Pin;
            if (pin != null) pins.Add(pin);

            Via via = obj as Via;
            if (via != null) vias.Add(via);

            Connection c = obj as Connection;
            if (c != null)
            {
                if (c.type == "Wire") wires.Add(c);
                else if (c.type == "Track") tracks.Add(c);
            }
        }

        public void Clear()
        {
            all.Clear();
            pins.Clear();
            vias.Clear();
            wires.Clear();
            tracks.Clear();
        }

    }

    public class ConnectionPoint
    {
        public int uid = ++Program.uid;

        public Connection connection;
        public PointF ofs = new PointF(0, 0); // relative to owner
        public Pin pin = null;
        public Via via = null;

        public ConnectionPoint original = null;
        public ConnectionPoint copy = null;

        public ConnectionPoint(Connection c) { connection = c;  }
        
        public ConnectionPoint(Connection c, ConnectionPoint cp)
        {
            connection = c;
            ofs = cp.ofs;
            pin = cp.pin;
            via = cp.via;
        }

        public ConnectionPoint(Connection c, object obj)
        {
            connection = c;
            pin = obj as Pin;
            via = obj as Via;
        }

        public ConnectionPoint(Connection c, float x, float y)
        {
            connection = c;
            ofs = new PointF(x, y);
        }

        public ConnectionPoint Copy(Connection c, bool bAlways)
        {
            if (pin != null && pin.copy != null)
            {
                copy = new ConnectionPoint(c, pin.copy);
                copy.ofs = ofs;
                copy.original = this;
                return copy;
            }
            if (via != null && via.copy != null)
            {
                copy = new ConnectionPoint(c, via.copy);
                copy.ofs = ofs;
                copy.original = this;
                return copy;
            }
            //if (!bAlways && (pin != null || via != null)) return null;
            if (bAlways)
                copy = new ConnectionPoint(c, this);
            else
                copy = new ConnectionPoint(c, ofs.X, ofs.Y);
            copy.original = this;
            return copy;
        }

        public void ClearCopy() { copy = null; }
        public void ClearOriginal() { original = null; }

        public bool printed { get { return pin != null && pin.printed || via != null && via.printed; } }

        public override string ToString()
        {
            //string s = uid + ": ";
            if (pin != null) return /*s + */pin.ToString();
            if (via != null) return /*s + */via.ToString();
            return "(" + pos.X.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture) + ", " + pos.Y.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture) + ")";
        }

        public bool dynamic { get { return (pin != null) || (via != null); } }
      
        public Part dest_part { get {
            if (pin != null) return pin.part;
            if (via != null) return via.part;
            return null;
        } }

        public int index { get { return connection.cp1 == this ? 0 : connection.cp2 == this ? 1 : -1; } }

        public PointF pos { get { return connection.part.LocalToMM(ofs); } }

        public bool valid_contact { get { return connection.part.bendable_pin == null || index != 0; } }

        public bool dragable { 
            get 
            {
                if (!connection.dynamic) return false;
                if (connection.part.bendable_pin == null) return true;
                return index != 0; 
            } 
        }

        public bool Matches(ConnectionPoint cp)
        {
            return via == cp.via && pin == cp.pin && (via != null || pin != null || Geom.Eq(pos, cp.pos));
        }

        public void Link()
        {
            Part part = dest_part;
            if (part != null)
            {
                part.direct_connections.Add(connection);
            }
        }

        public void Unlink()
        {
            Part part = dest_part;
            if (part != null)
                part.direct_connections.Remove(connection);
        }

        public void Detach()
        {
            if (connection.part.bendable_pin != null && connection == connection.part.own_connections[0] && index == 0)
                return;
            Unlink();
            pin = null;
            via = null;
        }

        public void Update()
        {
            if (pin != null)
            {
                if (connection.part.bendable_pin == pin && index == 0)
                    ofs = connection.part.MMToLocal(pin.org_pos);
                else
                    ofs = connection.part.MMToLocal(pin.pos);
            }
            else if (via != null)
                ofs = connection.part.MMToLocal(via.pos);
        }

        public bool IsMoveable()
        { 
            if (pin != null) return pin.part.selected || pin.part.original != null && pin.part.original.selected;
            if (via != null) return via.part.selected || via.part.original != null && via.part.original.selected;
            return true;
        }

        public string GetSaveString()
        {
            if (pin != null)
                return "pin = part" + pin.part.save_index + ".pins[" + pin.index + "]";
            if (via != null)
                return "via = part" + via.part.save_index + ".vias[" + via.index + "]";
            PointF pt = pos;
            return "x = " + pt.X.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture) + ", y = " + pt.Y.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture);
        }
    }

    public class Connection : Contact
    {
        public int uid = ++Program.uid;

        public Part part;
        public LuaTable tbl;
        public string name;
        public string type;
        public float width;
        public Color color;

        public ConnectionPoint cp1;
        public ConnectionPoint cp2;

        public Connection original = null;
        public Connection copy = null;

        public bool selected = false;
        public bool highlighted = false;
        public bool hovered = false;
        public bool printed = false;

        public int dragged_cp = -1;

        public Connection(Part p, Connection c)
        {
            part = p;
            tbl = c.tbl;
            name = c.name;
            type = c.type;
            color = c.color;
            width = c.width;
            cp1 = new ConnectionPoint(this, c.cp1);
            cp2 = new ConnectionPoint(this, c.cp2);
        }

        public Connection(Part p, LuaTable t)
        {
            part = p;
            tbl = t;
            name = tbl["name"] as string;
            type = t["connection_type"] as string;
            width = Convert.ToSingle(tbl["width"]);
            color = Program.lua.ReadColor(tbl["color"] as LuaTable);

            cp1 = new ConnectionPoint(this);
            cp1.ofs.X = Convert.ToSingle(tbl["x1"]);
            cp1.ofs.Y = Convert.ToSingle(tbl["y1"]);
            cp2 = new ConnectionPoint(this);
            cp2.ofs.X = Convert.ToSingle(tbl["x2"]);
            cp2.ofs.Y = Convert.ToSingle(tbl["y2"]);
        }

        public Connection Copy(Part part, bool bAlways)
        {
            copy = new Connection(part, this);
            copy.cp1 = copy.cp1.Copy(copy, bAlways);
            copy.cp2 = copy.cp2.Copy(copy, bAlways);
            if (copy.cp1 == null)
            {
                ClearCopy();
                return null;
            }
            if (copy.cp2 == null)
            {
                if (part.bendable_pin != null)
                {
                    copy.cp2 = new ConnectionPoint(copy, part.bendable_pin);
                }
                else
                {
                    ClearCopy();
                    return null;
                }
            }
            copy.original = this;
            copy.Link();
            return copy;
        }

        public void ClearCopy() 
        {
            if (cp1 != null) cp1.ClearCopy();
            if (cp2 != null) cp2.ClearCopy();
            copy = null;
        }

        public void ClearOriginal()
        {
            if (cp1 != null) cp1.ClearOriginal();
            if (cp2 != null) cp2.ClearOriginal();
            original = null;
        }

        public static Part Create(string part_name, object obj1, object obj2)
        {
            Part part = Part.Get(part_name);
            if (part == null)
                return null;
            if (part.type != "Connection")
                throw new InvalidCastException();
            part = new Part(part);
            Connection c = new Connection(part, part.tbl);
            c.cp1 = new ConnectionPoint(c, obj1);
            c.cp2 = new ConnectionPoint(c, obj2);
            
            part.own_connections.Add(c);
            c.Link();            
            c.Update();

            return part;
        }

        public static Part Create(string part_name, object obj1, PointF ofs2)
        {
            Part part = Part.Get(part_name);
            if (part == null)
                return null;
            if (part.type != "Connection")
                throw new InvalidCastException();
            part = new Part(part);
            Connection c = new Connection(part, part.tbl);
            c.cp1 = new ConnectionPoint(c, obj1);
            c.cp2 = new ConnectionPoint(c, ofs2.X, ofs2.Y);

            part.own_connections.Add(c);
            c.Link();
            c.Update();

            return part;
        }

        public static Part CreateBendableLeg(Pin pin)
        {
            Part part = Part.Get("Leg");
            if (part == null)
                return null;
            if (part.type != "Connection")
                throw new InvalidCastException();
            part = new Part(part);
            part.bendable_pin = pin;
            Connection c = new Connection(part, part.tbl);
            c.cp1 = new ConnectionPoint(c, pin);
            PointF pos = pin.pos;
            c.cp2 = new ConnectionPoint(c, pos.X, pos.Y);

            part.own_connections.Add(c);
            c.Link();
            c.Update();

            return part;
        }

        public static Part Create(LuaTable tbl)
        {
            string name = tbl["name"] as string;
            Part part = Part.Get(name);
            if (part == null)
                return null;
            if (part.type != "Connection")
                return null;
            LuaTable points = tbl["points"] as LuaTable;
            if (points == null)
                return null;
            part = new Part(part);
            part.bendable_pin = tbl["bendable_pin"] as Pin;
            
            Connection c = new Connection(part, part.tbl);
            int cnt = points.Values.Count;
            for (int i = 1; i <= cnt; ++i)
            {
                LuaTable tcp = points[i] as LuaTable;
                Pin pin = tcp["pin"] as Pin;
                Via via = tcp["via"] as Via;
                float x = Convert.ToSingle(tcp["x"]);
                float y = Convert.ToSingle(tcp["y"]);
                ConnectionPoint cp;
                if (pin != null)
                    cp = new ConnectionPoint(c, pin);
                else if (via != null)
                    cp = new ConnectionPoint(c, via);
                else
                    cp = new ConnectionPoint(c, x, y);
                if (i == 1)
                    c.cp1 = cp;
                else if (i == 2)
                {
                    c.cp2 = cp;
                    part.own_connections.Add(c);
                }
                else
                {
                    string n = tcp["name"] as string;
                    LuaTable t;
                    if (n == null)
                    {
                        n = name;
                        t = part.tbl;
                    }
                    else
                    {
                        Part p = Part.Get(n);
                        if (p == null || p.type != "Connection")
                            return null;
                        t = p.tbl;
                    }
                    Connection c2 = new Connection(part, t);
                    c2.cp1 = new ConnectionPoint(c2, c.cp2);
                    c2.cp2 = new ConnectionPoint(c2, cp);
                    part.own_connections.Add(c2);
                    c = c2;
                }
            }

            if (part.own_connections.Count == 0)
                return null;

            foreach (Connection con in part.own_connections) con.Link();
            part.UpdateConnections();
            return part;
        }

        public override string ToString()
        {
            return name + /*"(" + uid + ")" +*/ " from " + cp1.ToString() + " to " + cp2.ToString();
        }

        public bool dynamic { get { return type == "Wire" || part.type == "Connection"; } }

        public void Link()
        {
            cp1.Link();
            cp2.Link();
        }

        public void Unlink()
        {
            cp1.Unlink();
            cp2.Unlink();
        }

        public void Update()
        {
            cp1.Update();
            cp2.Update();
        }

        public bool IsMoveable() { return cp1.IsMoveable() && cp2.IsMoveable(); }

        public bool Contains(PointF pt)
        {
            PointF ptl = part.MMToLocal(pt);;
            float rot = Geom.Angle(cp1.ofs, cp2.ofs);
            ptl = Geom.Rotate(Geom.Sub(ptl, cp1.ofs), -rot);
            return ptl.X >= -width / 2 && ptl.X <= Geom.Dist(cp1.ofs, cp2.ofs) + width / 2 && ptl.Y >= -width / 2 && ptl.Y <= width / 2;
        }
    };

    public class Via : Contact
    {
        public int uid = ++Program.uid;

        public Part part;
        public int index;
        public LuaTable tbl;
        public string name;
        public PointF ofs = new PointF();
        public float hole_radius;
        public float ring_radius;
        public string form;
        public Color color_hole;
        public Color color_ring;

        public Via original = null;
        public Via copy = null;

        public bool selected = false;
        public bool highlighted = false;
        public bool hovered = false;
        public bool printed = false;

        public Via(Part p, Via via)
        {
            part = p;
            index = via.index;
            tbl = via.tbl;
            name = via.name;
            ofs = via.ofs;
            hole_radius = via.hole_radius;
            ring_radius = via.ring_radius;
            form = via.form;
            color_hole = via.color_hole;
            color_ring = via.color_ring;
        }

        public Via(Part p, int idx, LuaTable t)
        {
            part = p;
            index = idx;
            tbl = t;
            name = tbl["name"] as string;
            ofs.X = Convert.ToSingle(tbl["x"]);
            ofs.Y = Convert.ToSingle(tbl["y"]);
            hole_radius = Convert.ToSingle(tbl["hole_radius"]);
            ring_radius = Convert.ToSingle(tbl["outer_radius"]);
            form = tbl["form"] as string;
            color_hole = Program.lua.ReadColor(tbl["color_hole"] as LuaTable);
            color_ring = Program.lua.ReadColor(tbl["color_ring"] as LuaTable);
        }

        public Via Copy(Part part, bool bAlways)
        {
            copy = new Via(part, this);
            copy.original = this;
            return copy;
        }

        public void ClearCopy()
        {
            copy = null;
        }

        public void ClearOriginal()
        {
            original = null;
        }

        public override string ToString() {
            string s = name;
            if (s != "Via")
                return s;
            if (direct_contacts.pins.Count > 0)
                return s + " (" + direct_contacts.pins[0].ToString() + ")";
            return s + " (" + pos.X.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture) + ", " + pos.Y.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture) + ")";
        }

        public bool Contains(PointF pt)
        {
            PointF ptl = part.MMToLocal(pt);
            return Geom.Dist(ptl, ofs) <= ring_radius;
        }

        public PointF pos { get { return part.LocalToMM(ofs); } }
    };

    public class Pin : Contact
    {
        public int uid = ++Program.uid;

        public Part part;
        public int index;
        public LuaTable tbl;
        public string name;
        public PointF ofs = new PointF();
        public float radius;
        public string form;
        public Color color;
        public bool bendable;
        public PointF org_ofs;

        public Pin original = null;
        public Pin copy = null;

        public bool selected = false;
        public bool highlighted = false;
        public bool hovered = false;
        public bool printed = false;

        public Pin(Part p, Pin pin)
        {
            part = p;
            index = pin.index;
            tbl = pin.tbl;
            name = pin.name;
            ofs = pin.ofs;
            radius = pin.radius;
            form = pin.form;
            color = pin.color;
            bendable = pin.bendable;
            org_ofs = pin.org_ofs;
        }

        public Pin(Part p, int idx, LuaTable t)
        {
            part = p;
            index = idx;
            tbl = t;
            name = tbl["name"] as string;
            ofs.X = Convert.ToSingle(tbl["x"]);
            ofs.Y = Convert.ToSingle(tbl["y"]);
            radius = Convert.ToSingle(tbl["radius"]);
            form = tbl["form"] as string;
            color = Program.lua.ReadColor(tbl["color"] as LuaTable);
            bendable = Convert.ToBoolean(tbl["bendable"]);
            org_ofs = ofs;
            if (bendable)
            {
                LuaTable tOfs = tbl["bendable_ofs"] as LuaTable;
                if (tOfs != null)
                {
                    ofs.X += Convert.ToSingle(tOfs["x"]);
                    ofs.Y += Convert.ToSingle(tOfs["y"]);
                }
            }
        }

        public Pin Copy(Part part, bool bAlways)
        {
            copy = new Pin(part, this);
            copy.original = this;
            return copy;
        }

        public void ClearCopy()
        {
            copy = null;
        }

        public void ClearOriginal()
        {
            original = null;
        }

        public override string ToString() { return part.ToString() + " " + name /*+ "(" + uid + ")"*/; }

        public bool Contains(PointF pt)
        {
            PointF ptl = part.MMToLocal(pt);
            return Geom.Dist(ptl, ofs) <= radius;
        }

        public PointF pos { get { return part.LocalToMM(ofs); } }
        public PointF org_pos { get { return part.LocalToMM(org_ofs); } }

        public Part BendablePart { get {
          foreach (Connection c in part.direct_connections)
          {
              if (c.part.bendable_pin == this)
                  return c.part;
          }
          return null;
        } }

        public bool dragged { get {
            if (part.dragged) return true;
            if (!bendable) return false;
            Part bp = BendablePart;
            if (bp == null) return false;
            if (bp.dragged || bp.original != null && bp.original.dragged) return true;
            ConnectionPoint cp1, cp2;
            bp.GetConnectionEndPoints(out cp1, out cp2);
            if (cp2.connection.dragged_cp == 1)
                return true;
            return false;
        } }
    };

    public class Part
    {
        public int uid = ++Program.uid;

        private static Hashtable htByName = new Hashtable();

        public LuaTable tbl;
        public string name;
        public string type;
        public Part parent;

        public bool show_label = true;
        public string label = null;
        public float value = 0.0f;
        public string value_unit = null;
        public string value_name = null;
        public string values = null;
        public string value_units = null;

        public Color org_color;
        public Color color;
        public Color frame_color;

        public string form;

        public int z_order;

        public RectangleF bounds = new RectangleF(0, 0, -1, -1);

        public List<Via> vias = new List<Via>();
        public List<Pin> pins = new List<Pin>();
        public List<Connection> own_connections = new List<Connection>();

        public List<Connection> direct_connections = new List<Connection>();

        public Pin bendable_pin = null;

        public TreeNode tnHierarchical = null;

        public bool placed = false;
        public PointF pos = new PointF(0, 0);
        public float rot = 0;

        public int save_index = -1;

        public Part original = null;
        public Part copy = null;

        public bool selected = false;
        public bool highlighted = false;
        public bool hovered = false;
        public bool printed = false;

        public bool locked = false;

        public bool dragged = false;
        public PointF drag_point; // in local coordinates

        public static Part Get(LuaTable t)
        {
            if (t == null) return null;
            return t["__csobj"] as Part;
        }

        public static Part Get(string name)
        {
            return htByName[name] as Part;
        }

        public static Part Get(object obj)
        {
            Part part = obj as Part;
            if (part != null) return part;

            Pin pin = obj as Pin;
            if (pin != null) return pin.part;

            Via via = obj as Via;
            if (via != null) return via.part;

            Connection c = obj as Connection;
            if (c != null) return c.part;

            ConnectionPoint cp = obj as ConnectionPoint;
            if (cp != null) return cp.connection.part;

            ContactsList cl = obj as ContactsList;
            if (cl != null) return Part.Get(cl.owner);

            return null;
        }

        public Part(Part p, bool bCopyChildren = true)
        {
            tbl = p.tbl;
            name = p.name;
            type = p.type;
            parent = p;
            label = p.label;
            show_label = p.show_label;
            value = p.value;
            value_unit = p.value_unit;
            value_name = p.value_name;
            values = p.values;
            value_units = p.value_units;

            org_color = p.org_color;
            color = p.color;
            frame_color = p.frame_color;
            form = p.form;
            z_order = p.z_order;
            bounds = p.bounds;
            pos = p.pos;
            rot = p.rot;
            drag_point = p.drag_point;
            locked = p.locked;
            if (bCopyChildren)
            {
                foreach (Connection c in p.own_connections) own_connections.Add(new Connection(this, c));
                foreach (Via via in p.vias) vias.Add(new Via(this, via));
                foreach (Pin pin in p.pins) pins.Add(new Pin(this, pin));
                bendable_pin = p.bendable_pin;
            }
        }

        public Part(LuaTable t)
        {
            tbl = t;
            tbl["__csobj"] = this;
            name = t["name"] as string;
            type = t["type"] as string;
            parent = Get(tbl["__base"] as LuaTable);

            htByName.Add(name, this);

            if (parent == null)
                Program.partsRoot = this;

            label = t["label"] as string;
            value = Convert.ToSingle(t["value"]);
            value_unit = ProcessLabel(t["value_unit"] as string);
            value_name = t["value_name"] as string;
            values = ProcessLabel(t["values"] as string);
            value_units = ProcessLabel(t["value_units"] as string);

            org_color = color = Program.lua.ReadColor(tbl["color"] as LuaTable);
            frame_color = Program.lua.ReadColor(tbl["frame_color"] as LuaTable);

            form = tbl["form"] as string;

            z_order = Convert.ToInt32(tbl["z_order"]);

            LuaTable tBounds = tbl["bounds"] as LuaTable;
            if (tBounds != null)
            {
                float min_x = Convert.ToSingle(tBounds["min_x"]);
                float min_y = Convert.ToSingle(tBounds["min_y"]);
                float max_x = Convert.ToSingle(tBounds["max_x"]);
                float max_y = Convert.ToSingle(tBounds["max_y"]);
                bounds = new RectangleF(min_x, min_y, max_x - min_x, max_y - min_y);
            }
            else
                bounds = new RectangleF(0, 0, 0, 0);

            drag_point = new PointF((bounds.Left + bounds.Right) / 2, (bounds.Top + bounds.Bottom) / 2);

            LuaTable tTracks = tbl["tracks"] as LuaTable;
            if (tTracks != null)
            {
                for (int i = 1; ; ++i)
                {
                    LuaTable tTrack = tTracks[i] as LuaTable;
                    if (tTrack == null)
                        break;
                    Connection track = new Connection(this, tTrack);
                    own_connections.Add(track);
                }
            }

            LuaTable tVias = tbl["vias"] as LuaTable;
            if (tVias != null)
            {
                for (int i = 1; ; ++i)
                {
                    LuaTable tVia = tVias[i] as LuaTable;
                    if (tVia == null)
                        break;
                    Via via = new Via(this, i - 1, tVia);
                    vias.Add(via);
                }
            }

            LuaTable tWires = tbl["wires"] as LuaTable;
            if (tWires != null)
            {
                for (int i = 1; ; ++i)
                {
                    LuaTable tWire = tWires[i] as LuaTable;
                    if (tWire == null)
                        break;
                    Connection wire = new Connection(this, tWire);
                    own_connections.Add(wire);
                }
            }

            LuaTable tPins = tbl["pins"] as LuaTable;
            if (tPins != null)
            {
                for (int i = 1; ; ++i)
                {
                    LuaTable tPin = tPins[i] as LuaTable;
                    if (tPin == null)
                        break;
                    Pin pin = new Pin(this, i - 1, tPin);
                    pins.Add(pin);
                }
            }
        }

        public Part Copy(bool bAlways)
        {
            copy = new Part(this, false);
            foreach (Via via in vias) copy.vias.Add(via.Copy(copy, bAlways));
            foreach (Pin pin in pins) copy.pins.Add(pin.Copy(copy, bAlways));
            if (bendable_pin != null)
            {
                if (bendable_pin.copy != null)
                    copy.bendable_pin = bendable_pin.copy;
                else
                    copy.bendable_pin = bendable_pin.Copy(bendable_pin.part, bAlways);
            }
            foreach (Connection c in own_connections)
            {
                Connection cc = c.Copy(copy, bAlways);
                if (cc == null)
                {
                    ClearCopy();
                    return null;
                }
                copy.own_connections.Add(cc);
            }
            copy.original = this;
            return copy;
        }

        public void ClearCopy()
        {
            foreach (Via via in vias) via.ClearCopy();
            foreach (Pin pin in pins) pin.ClearCopy();
            foreach (Connection c in own_connections) c.ClearCopy();
            copy = null;
        }

        public void ClearOriginal()
        {
            foreach (Via via in vias) via.ClearOriginal();
            foreach (Pin pin in pins) pin.ClearOriginal();
            foreach (Connection c in own_connections) c.ClearOriginal();
            original = null;
        }

        static public string ProcessLabel(string s)
        {
            if (s == null) return null;
            return s.Replace("{ohm}", "Ω").Replace("{micro}", "µ");
        }

        public string GetLabel()
        {
            if (!String.IsNullOrEmpty(label))
                return ProcessLabel(label);
            if (value_unit != null)
                return ProcessLabel(value.ToString(System.Globalization.CultureInfo.InvariantCulture) + " " + value_unit);
            if (type == "Component")
                return name;
            return "";
        }

        public override string ToString()
        {
            if (type == "Via" && vias.Count == 1)
                return vias[0].ToString();
            string s = name; // +"(" + uid + ")";
            if (type != "Connection") return s;
            ConnectionPoint cp1, cp2;
            GetConnectionEndPoints(out cp1, out cp2);
            return s + " from " + cp1.ToString() + " to " + cp2.ToString();
        }

        public void DetectOwnContacts()
        {
            foreach (Pin pin1 in pins)
            {
                foreach (Pin pin2 in pins)
                {
                    if (Contact.Check_Pin_Pin(pin1, pin2))
                    {
                        pin1.own_contacts.Add(pin2);
                        pin2.own_contacts.Add(pin1);
                    }
                }
                foreach (Via via2 in vias)
                {
                    if (Contact.Check_Pin_Via(pin1, via2))
                    {
                        pin1.own_contacts.Add(via2);
                        via2.own_contacts.Add(pin1);
                    }
                }
                foreach (Connection con2 in own_connections)
                {
                    if (Contact.Check(pin1, con2))
                    {
                        pin1.own_contacts.Add(con2);
                        con2.own_contacts.Add(pin1);
                    }
                }
            }
            foreach (Via via1 in vias)
            {
                foreach (Via via2 in vias)
                {
                    if (Contact.Check_Via_Via(via1, via2))
                    {
                        via1.own_contacts.Add(via2);
                        via2.own_contacts.Add(via1);
                    }
                }
                foreach (Connection con2 in own_connections)
                {
                    if (Contact.Check(via1, con2))
                    {
                        via1.own_contacts.Add(con2);
                        con2.own_contacts.Add(via1);
                    }
                }
            }
            foreach (Connection con1 in own_connections)
            {
                foreach (Connection con2 in own_connections)
                {
                    if (Contact.Check(con1, con2))
                    {
                        con1.own_contacts.Add(con2);
                        con2.own_contacts.Add(con1);
                    }
                }
            }
        }

        public void ClearDirectContacts()
        {
            foreach (Pin pin in pins) pin.direct_contacts.Clear();
            foreach (Via via in vias) via.direct_contacts.Clear();
            foreach (Connection c in own_connections) c.direct_contacts.Clear();
        }

        public void DetectDirectContacts(Contact c)
        {
            foreach (Pin pin in pins)
            {
                if (Contact.Check(pin, c))
                {
                    pin.direct_contacts.Add(c);
                    c.direct_contacts.Add(pin);
                }
            }
            foreach (Via via in vias)
            {
                if (Contact.Check(via, c))
                {
                    via.direct_contacts.Add(c);
                    c.direct_contacts.Add(via);
                }
            }
            foreach (Connection con in own_connections)
            {
                if (Contact.Check(con, c))
                {
                    con.direct_contacts.Add(c);
                    c.direct_contacts.Add(con);
                }
            }
        }

        public void DetectDirectContacts(Part part)
        {
            if (part == this) return;
            foreach (Pin pin in part.pins) DetectDirectContacts(pin);
            foreach (Via via in part.vias) DetectDirectContacts(via);
            foreach (Connection con in part.own_connections) DetectDirectContacts(con);
        }

        public void ClearTracedContacts()
        {
            foreach (Pin pin in pins) pin.trace_open = pin.trace_closed = false;
            foreach (Via via in vias) via.trace_open = via.trace_closed = false;
            foreach (Connection con in own_connections) con.trace_open = con.trace_closed = false;
        }

        public List<Connection> GetConnectionEndPoints(out ConnectionPoint cp1, out ConnectionPoint cp2)
        {
            if (type != "Connection")
            {
                cp1 = null;
                cp2 = null;
                return null;
            }

            if (own_connections.Count > 0)
            {
                cp1 = own_connections[0].cp1;
                cp2 = own_connections[own_connections.Count - 1].cp2;
                return own_connections;
            }

            cp1 = null;
            cp2 = null;
            return null;
        }

        public void UpdateConnections()
        {
            foreach (Connection c in own_connections) c.Update();
                
            foreach (Connection c in direct_connections)
            {
                if (c.part.bendable_pin != null)
                {
                    c.Update();
                    ConnectionPoint cp1, cp2;
                    c.part.GetConnectionEndPoints(out cp1, out cp2);
                    c.part.bendable_pin.ofs = c.part.bendable_pin.part.MMToLocal(cp2.pos);
                    if (c.part.bendable_pin.part != this)
                        c.part.bendable_pin.part.UpdateConnections();
                }
            }

            foreach (Connection c in direct_connections)
            {
                c.Update();
            }

            if (bendable_pin != null && bendable_pin.part != this)
                bendable_pin.part.UpdateConnections();

            AdjustOrigin();
        }

        public PointF DraggedPos(PointF ptm)
        {
            return Geom.Sub(ptm, Geom.Rotate(drag_point,rot));
        }

        public PointF LocalToMM(PointF pt)
        {
            return Geom.Add(pos, Geom.Rotate(pt, rot));
        }

        public PointF MMToLocal(PointF pt)
        {
            return Geom.Rotate(Geom.Sub(pt, pos), -rot);
        }

        public AABB CalcAABB()
        {
            AABB aabb = new AABB();
            if (type != "Connection")
            {
                aabb.Add(LocalToMM(new PointF(bounds.Left, bounds.Top)));
                aabb.Add(LocalToMM(new PointF(bounds.Right, bounds.Top)));
                aabb.Add(LocalToMM(new PointF(bounds.Right, bounds.Bottom)));
                aabb.Add(LocalToMM(new PointF(bounds.Left, bounds.Bottom)));
                return aabb;
            }
            foreach (Connection c in own_connections)
            {
                AABB aabb1 = new AABB(); aabb1.Add(c.cp1.pos); aabb1.Grow(c.width / 2);
                AABB aabb2 = new AABB(); aabb2.Add(c.cp2.pos); aabb2.Grow(c.width / 2);
                aabb.Add(aabb1);
                aabb.Add(aabb2);
            }
            return aabb;
        }

        public bool Contains(PointF ptm)
        {
            PointF ofs = MMToLocal(ptm);
            if (bounds.Contains(ofs))
                return true;
            foreach (Connection c in own_connections)
            {
                if (c.Contains(ptm))
                    return true;
            }
            foreach (Via via in vias)
            {
                if (via.Contains(ptm))
                    return true;
            }
            foreach (Pin pin in pins)
            {
                if (pin.Contains(ptm))
                    return true;
            }
            return false;
        }

        public void AdjustOrigin()
        {
            if (type != "Connection") return;
            if (own_connections.Count == 0) return;
            
            PointF new_pos = own_connections[0].cp1.pos; //!!!
            PointF ptl = MMToLocal(new_pos);

            foreach (Connection c in own_connections)
            {
                c.cp1.ofs = Geom.Sub(c.cp1.ofs, ptl);
                c.cp2.ofs = Geom.Sub(c.cp2.ofs, ptl);
            }
            drag_point = Geom.Sub(drag_point, ptl);
            pos = new_pos;
        }

        public bool IsMoveable()
        {
            if (type != "Connection") return true;
            if (original == null) return true;
            if (!selected && !original.selected) return false;
            //if (bendable_pin != null) return false;
            foreach (Connection c in own_connections)
            {
                if (!c.IsMoveable())
                    return false;
            }
            return true;
        }

        public string GetSaveProperties()
        {
            string s = "";
            if (locked)
                s += ", locked = true"; 
            if (selected)
                s += ", selected = true";
            if (!show_label)
                s += ", hide_label = true";
            if (color != org_color)
                s += ", color = { " + color.R + ", " + color.G + ", " + color.B + ", " + color.A + " }";
            return s;
        }

        public void ApplySavedProperties(LuaTable tbl)
        {
            locked = Convert.ToBoolean(tbl["locked"]);
            if (Convert.ToBoolean(tbl["selected"]))
                Program.mainForm.SelectPart(this, true, false);
            if (Convert.ToBoolean(tbl["hide_label"]))
                show_label = false;
            LuaTable tColor = tbl["color"] as LuaTable;
            if (tColor != null)
            {
                color = Program.lua.ReadColor(tColor);
                //!!! temporary
                foreach (Connection c in own_connections) c.color = color;
            }
        }

        public string GetSaveString()
        {
            string s;
            if (type != "Connection")
            {
                s = "local part" + save_index + " = cs:PlacePart { name = '" + name + "', x = " + pos.X.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture) + ", y = " + pos.Y.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture) + ", rot = " + rot.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture);
                s += GetSaveProperties();
                s += " }";
                return s;
            }
            if (own_connections.Count == 0 || own_connections[0].cp1 == null || own_connections[0].cp2 == null)
                return "";
            s = "local part" + save_index + " = cs:PlaceConnection { name = '" + name + "'";
            if (bendable_pin != null)
                s += ", bendable_pin = part" + bendable_pin.part.save_index + ".pins[" + bendable_pin.index + "]";
            s += GetSaveProperties();
            s += ", points = { { " + own_connections[0].cp1.GetSaveString() + " }, { " + own_connections[0].cp2.GetSaveString() + " }";
            for (int i = 1; i < own_connections.Count; ++i)
            {
                Connection c = own_connections[i];
                s += ", { ";
                if (c.name != name)
                    s += "name = '" + c.name + "', ";
                s += c.cp2.GetSaveString() + " }";
            }
            s += " } }";
            return s;
        }
    };

    public class UndoItem
    {
        public List<Part> PlacedParts = new List<Part>();
        public List<Part> SelectedParts = new List<Part>();
        public string operation;
        public object tag = null;
    };

    public class Doc
    {
        public string name = "(Untitled)";
        public string filename = "";

        public Panel panel = null;

        public float scale = 10.0f;
        public PointF offset = new PointF(0, 0);

        public List<Part> PlacedParts = new List<Part>();
        public List<Part> SelectedParts = new List<Part>();

        public List<UndoItem> UndoItems = new List<UndoItem>();
        public int nUndoIdx = -1;
        public int nRedoIdx = -1;
        public int nSavedUndoIdx = -1;

        public bool modified { get { return nSavedUndoIdx != nUndoIdx; } }
    }

    static class Program
    {
        public static LuaBindings lua;
        public static MainForm mainForm;

        public static Part partsRoot = null;

        public static int uid = 0;

        public static void Error(string text, string caption = "Error")
        {
            if (DialogResult.Cancel == MessageBox.Show(text, caption, MessageBoxButtons.OKCancel))
                System.Environment.Exit(0);
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);            
            
            mainForm = new MainForm();
            lua = new LuaBindings();
            lua.Init();
            mainForm.Init();

            Application.Run(mainForm);
        }
    }
}
