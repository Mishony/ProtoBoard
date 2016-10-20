using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;
using NLua;

namespace ProtoBoard
{
    public partial class MainForm : Form
    {
        public Panel panel = null;
        public Doc doc = null;

        public Graphics gr = null;

        public int show_labels = 2; // 0 - force all hidden, 1 - hovered only, 2 - default, 3 - force all shown

        public ToolTip tooltip = new ToolTip();
        public Point tooltip_pos = new Point(0, 0);
        public bool tooltip_shown = false;

        public Point ptDown;
        public PointF ofsDown;
        public bool bLBDown = false;
        public bool bRBDown = false;
        public bool bSpaceDown = false;
        public bool bLBDragged = false;
        public bool bRBDragged = false;
        
        public bool bDblClicked = false;
        public string connection_part_name = null;

        public List<Part> ClipoardParts = new List<Part>();
        public List<Part> DraggedParts = new List<Part>();
        public List<Connection> DraggedConnections = new List<Connection>();
        public string DragType = null;
        
        public List<Part> HoveredBoards = new List<Part>();
        public List<Part> HoveredComponents = new List<Part>();
        public List<Connection> HoveredConnections = new List<Connection>();
        public List<Via> HoveredVias = new List<Via>();
        public List<Pin> HoveredPins = new List<Pin>();

        public ContactsList TracedContacts = new ContactsList();

        public object ContextHighlight = null;

        public Pen penHover, penContact, penHighlight, penSelection;

        public float highlight_grow = 2; // in pixels
        float select_grow = 0; // in pixels

        public PointF grid_origin = new PointF(0, 0); // in milimeters
        public float grid_spacing = 2.54f; // in milimeters

        public float placement_snap_dist = 1.27f; // in milimeters
        public float highlight_snap_dist = 1.27f; // in milimeters
 
        public Color def_track_color;
        public Color def_wire_color;

        public struct NamedColor
        {
            public string name;
            public Color color;

            public NamedColor(string _name, Color _color) { name = _name; color = _color; }
        };

        public List<NamedColor> ConnectionColors = new List<NamedColor>();

        public MainForm()
        {
            InitializeComponent();
        }

        public void Init()
        {
            penHover = new Pen(Color.FromArgb(128, Color.White), 1);
            penContact = new Pen(Color.FromArgb(128, Color.GreenYellow));
            penHighlight = new Pen(Color.White, 3);
            penSelection = new Pen(Color.Yellow, 3);

            StatusText.Text = "";
            CoordsText.Text = "";
            GridSpacing.Text = grid_spacing.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture);

            splitContainer2.SplitterDistance = 400;
            splitContainer2.Panel2.AutoScroll = true;

            cbViaForm.Items.Add("circle");
            cbViaForm.Items.Add("square");

            def_track_color = Program.lua.ReadColor(Program.lua.Call("return Part.Colors.Track") as LuaTable);
            def_wire_color = Program.lua.ReadColor(Program.lua.Call("return Part.Colors.Wire") as LuaTable);

            LuaTable tblColors = Program.lua.Call("return Part.ConnectionColors") as LuaTable;
            if (tblColors != null)
            {
                foreach (LuaTable tblClr in tblColors.Values)
                {
                    string name = tblClr[1] as string;
                    Color clr = Program.lua.ReadColor(tblClr[2] as LuaTable);
                    ConnectionColors.Add(new NamedColor(name, clr));
                }
            }

            //Array colors = Enum.GetValues(typeof(KnownColor));
            //foreach (KnownColor kc in colors)
            //{
            //    Color color = Color.FromKnownColor(kc);
            //    if (color.IsSystemColor) continue;
            //    if (color.A != 255) continue;
            //    string name = color.Name;
            //    Console.WriteLine("  " + name + " = { " + color.R + ", " + color.G + ", " + color.B + " },");
            //}

            New();
        }

        public void SetStatus(string s)
        {
            StatusText.Text = s;
        }

        public void New()
        {
            doc = new Doc();
            int i = FilesTab.TabPages.Count;
            FilesTab.TabPages.Add(doc.name);
            panel = FilesTab.TabPages[i];
            panel.Tag = doc;
            doc.panel = panel;

            PropertyInfo aProp =
                          typeof(System.Windows.Forms.Control).GetProperty(
                          "DoubleBuffered",
                          BindingFlags.NonPublic | BindingFlags.Instance);
            aProp.SetValue(panel, true, null);
            panel.BackColor = Color.Black;
            panel.Paint += OnPaint;
            panel.MouseMove += OnMouseMove;
            panel.MouseDown += OnMouseDown;
            panel.MouseUp += OnMouseUp;
            panel.MouseDoubleClick += OnMouseUp;
            panel.MouseWheel += OnMouseWheel;
            panel.MouseLeave += OnMouseLeave;
            panel.Enter += OnPanelFocused;
            panel.Leave += OnPanelLostFocus;
            FilesTab.SelectedIndex = i;
            panel.Focus();
            UpdateProperties();
        }

        public void AddPart(Part part)
        {
            if (part.parent == null) return;
            TreeNode tnParent = part.parent.tnHierarchical;
            if (tnParent != null)
                part.tnHierarchical = tnParent.Nodes.Add(part.name);
            else
                part.tnHierarchical = PartsHierarchical.Nodes.Add(part.name);
            part.tnHierarchical.Tag = part;
        }

        public void PlacePart(Part part)
        {
            if (part.placed) return;
            part.placed = true;
            int idx = 0;
            foreach (Part p in doc.PlacedParts)
            {
                if (p.z_order > part.z_order)
                    break;
                ++idx;
            }
            if (idx < doc.PlacedParts.Count)
                doc.PlacedParts.Insert(idx, part);
            else
                doc.PlacedParts.Add(part);
        }

        public Part PlacePart(Part part, float x, float y, float rot, bool bConnectBendablePins = true)
        {
            if (part.IsMoveable())
            {
                part.pos.X = x;
                part.pos.Y = y;
                part.rot = rot;
                while (part.rot > 180) part.rot -= 360;
                while (part.rot <= -180) part.rot += 360;
            }

            if (!part.placed)
            {
                PlacePart(part);
                if (bConnectBendablePins)
                {
                    foreach (Pin pin in part.pins)
                    {
                        if (pin.bendable && pin.BendablePart == null)
                        {
                            PlaceBendableLeg(pin);
                        }
                    }
                }
                part.UpdateConnections();
                part.DetectOwnContacts();
            }
            else
                part.UpdateConnections();

            return part;
        }

        public void FixBP()
        {
            List<Pin> unconnected = new List<Pin>();
            foreach (Part part in doc.PlacedParts)
            {
                foreach (Pin pin in part.pins)
                {
                    if (!pin.bendable) continue;
                    Part bp = pin.BendablePart;
                    if (bp == null)
                    {
                        unconnected.Add(pin);
                        continue;
                    }
                    ConnectionPoint cp2 = bp.own_connections[bp.own_connections.Count - 1].cp2;
                    if (cp2.pin == pin)
                        cp2.Detach();
                }
            }
            foreach (Pin pin in unconnected)
            {
                PlaceBendableLeg(pin);
            }

        }

        public Part PlacePart(string name, float x, float y, float rot)
        {
            Part part = Part.Get(name);
            if (part == null)
            {
                Program.Error("Could not place unknown part '" + name + "'");
                return null;
            }
            return PlacePart(part, x, y, rot);
        }

        public Part PlaceConnection(Part part)
        {
            if (part == null) return null;
            PlacePart(part);
            part.UpdateConnections();
            return part;
        }

        public Part PlaceConnection(string part_name, object obj1, object obj2)
        {
            Part part = Connection.Create(part_name, obj1, obj2);
            return PlaceConnection(part);
        }

        public Part PlaceConnection(string part_name, object obj1, PointF pt2)
        {
            Part part = Connection.Create(part_name, obj1, pt2);
            return PlaceConnection(part);
        }

        public Part PlaceBendableLeg(Pin pin)
        {
            Part part = Connection.CreateBendableLeg(pin);
            return PlaceConnection(part);
        }

        public void RotatePart(Part part, float angle, PointF ptOrigin)
        {
            if (part == null) return;
            if (!part.IsMoveable())
                return;

            float rot = part.rot;
            if (angle == 0)
            {
                rot = 0;
                angle = -part.rot;
                ptOrigin = part.CalcAABB().Center;
            }
            else
                rot += angle;

            while (rot > 180) rot -= 360;
            while (rot <= -180) rot += 360;

            part.pos = Snap(Geom.Rotate(part.pos, angle, ptOrigin), placement_snap_dist);
            part.rot = rot;

            part.UpdateConnections();

            panel.Invalidate();
        }

        public void DelPart(Part part, bool bDeleteConnections, bool bForced)
        {
            if (part == null || !part.placed) return;

            if (part.bendable_pin != null && !bForced)
            {
                part.bendable_pin.ofs = part.bendable_pin.org_ofs;
                while (part.own_connections.Count > 1)
                {
                    part.own_connections[1].Unlink();
                    part.own_connections.RemoveAt(1);
                }
                part.own_connections[0].cp2.Unlink();
                PointF ofs = part.MMToLocal(part.bendable_pin.pos);
                part.own_connections[0].cp2 = new ConnectionPoint(part.own_connections[0], ofs.X, ofs.Y);
                //part.own_connections[0].cp2.Link();
                //part.own_connections[0].cp2.Update();
                part.bendable_pin.part.UpdateConnections();
                UpdateProperties();
                return;
            }

            doc.PlacedParts.Remove(part);
            if (part.selected) SelectPart(part, false);
            if (part.dragged) DragPart(part, false);

            foreach (Connection c in part.own_connections)
                c.Unlink();

            List<Connection> lst = new List<Connection>();
            foreach (Connection c in part.direct_connections) lst.Add(c);
            foreach (Connection c in lst)
            {
                if (bDeleteConnections || c.part.bendable_pin != null && c.part.bendable_pin.part == part)
                {
                    DelConnection(c, c.part.bendable_pin != null && c.part.bendable_pin.part == part);
                    continue;
                }
                if (c.cp1.dest_part == part)
                    c.cp1.Detach();
                if (c.cp2.dest_part == part)
                    c.cp2.Detach();
            }

            part.UpdateConnections();
            UpdateProperties();
            
            panel.Invalidate();
        }

        public void DelConnection(Connection c, bool bForced)
        {
            //c.Unlink();
            DelPart(c.part, false, bForced);
        }

        public void Del(bool bDeleteConnections)
        {
            SaveUndo("del");
            SuspendProperties();
            List<Part> parts = new List<Part>();
            foreach (Part part in doc.SelectedParts) parts.Add(part);
            ClearSelection();
            foreach (Part part in parts)
                DelPart(part, bDeleteConnections, false);
            ResumeProperties();
        }

        public void CopyParts(List<Part> lstFrom, List<Part> lstTo, bool bAlways)
        {
            foreach (Part part in lstFrom)
            {
                if (part.type == "Connection") continue;
                Part copy = part.Copy(bAlways);
                if (copy != null)
                    lstTo.Add(copy);
            }
            foreach (Part part in lstFrom)
            {
                if (part.type != "Connection") continue;
                Part copy = part.Copy(bAlways);
                if (copy == null) continue;
                int idx = 0;
                foreach (Part p in lstTo)
                {
                    if (p.z_order > part.z_order)
                        break;
                    ++idx;
                }
                if (idx < lstTo.Count)
                    lstTo.Insert(idx, copy);
                else
                    lstTo.Add(copy);
            }
        }

        public void ClearCopy(List<Part> lstFrom)
        {
            foreach (Part part in lstFrom) part.ClearCopy();
        }

        public void ClearOriginal(List<Part> lstTo)
        {
            foreach (Part part in lstTo) part.ClearOriginal();
        }

        public void Copy()
        {
            if (doc.SelectedParts.Count == 0) return;
            ClipoardParts.Clear();
            PointF ptm = GetSelectionCenter();
            foreach (Part part in doc.SelectedParts)
            {
                part.drag_point = part.MMToLocal(ptm);
            }
            CopyParts(doc.SelectedParts, ClipoardParts, false);
            ClearCopy(doc.SelectedParts);
            ClearOriginal(ClipoardParts);
        }

        public void Cut()
        {
            Copy();
            Del(false);
        }

        public void Paste()
        {
            CancelDrag();
            if (ClipoardParts.Count == 0)
                return;
            DragType = "paste";
            CopyParts(ClipoardParts, DraggedParts, false);
            foreach (Part part in DraggedParts) part.dragged = true;
            ClearCopy(ClipoardParts);
            ClearOriginal(DraggedParts);
        }

        public void ForgetNewConnectionUndo()
        {
            if (doc.UndoItems.Count == 0) return;
            UndoItem last = doc.UndoItems[doc.UndoItems.Count - 1];
            if (last.operation != "new connection") return;
            doc.UndoItems.RemoveAt(doc.UndoItems.Count - 1);
            doc.nUndoIdx--;
            UpdatePanelTitle();
        }

        public void UpdatePanelTitle()
        {
            var tp = FilesTab.TabPages[FilesTab.SelectedIndex];
            tp.Text = (doc.modified ? "* " : "") + doc.name;
            tp.ToolTipText = doc.filename;
        }

        public void SaveUndo(string operation, object tag = null)
        {
            if (doc.UndoItems.Count > 0)
            {
                UndoItem last = doc.UndoItems[doc.UndoItems.Count - 1];
                if (operation == "property" && last.operation == operation && last.tag == tag)
                    return;
                if (last.operation == "new connection")
                {
                    if (operation == "accept drag")
                    {
                        last.operation = operation;
                        return;
                    }
                    else
                        last.operation = "error";
                }
            }

            UndoItem undo = new UndoItem();
            undo.operation = operation;
            undo.tag = tag;
            CopyParts(doc.PlacedParts, undo.PlacedParts, true);
            foreach (Part part in doc.SelectedParts)
            {
                if (part.copy != null)
                    undo.SelectedParts.Add(part.copy);
            }
            ClearCopy(doc.PlacedParts);
            ClearOriginal(undo.PlacedParts);
            if (doc.nSavedUndoIdx > doc.nUndoIdx)
                doc.nSavedUndoIdx = -2;
            while (doc.UndoItems.Count > doc.nUndoIdx + 1) doc.UndoItems.RemoveAt(doc.UndoItems.Count - 1);
            doc.UndoItems.Add(undo);
            ++doc.nUndoIdx;
            doc.nRedoIdx = -1;
            UpdatePanelTitle();
            panel.Invalidate();
        }

        public void RestoreUndo(UndoItem undo)
        {
            SuspendProperties();
            foreach (Part part in doc.PlacedParts) part.placed = false;
            doc.PlacedParts.Clear();
            doc.SelectedParts.Clear();
            CopyParts(undo.PlacedParts, doc.PlacedParts, true);
            foreach (Part part in doc.PlacedParts)
            {
                part.placed = true;
                part.DetectOwnContacts();
            }
            foreach (Part part in undo.SelectedParts)
            {
                if (part.copy != null)
                    SelectPart(part.copy, true, false);
            }
            ClearCopy(undo.PlacedParts);
            ClearOriginal(doc.PlacedParts);
            DetectDirectContacts();
            ResumeProperties();
            panel.Invalidate();
        }

        public void Undo()
        {
            if (doc.nUndoIdx < 0) return;
            if (doc.nUndoIdx + 1 == doc.UndoItems.Count)
            {
                SaveUndo("undo");
                --doc.nUndoIdx;
            }
            RestoreUndo(doc.UndoItems[doc.nUndoIdx]);
            doc.nRedoIdx = doc.nUndoIdx + 1;
            --doc.nUndoIdx;
            UpdatePanelTitle();
        }

        public void Redo()
        {
            if (doc.nRedoIdx < 0 || doc.nRedoIdx >= doc.UndoItems.Count) return;
            RestoreUndo(doc.UndoItems[doc.nRedoIdx]);
            doc.nUndoIdx = doc.nRedoIdx - 1;
            ++doc.nRedoIdx;
            UpdatePanelTitle();
        }

        public PointF ClientToMM(PointF ptc)
        {
            PointF ptm = new PointF((ptc.X - doc.offset.X) / doc.scale, (ptc.Y - doc.offset.Y) / doc.scale);
            return ptm;
        }

        public PointF MMToClient(PointF ptm)
        {
            PointF ptc = new PointF(ptm.X * doc.scale + doc.offset.X, ptm.Y * doc.scale + doc.offset.Y);
            return ptc;
        }

        public PointF GetMouseInMM()
        {
            Point ptc = panel.PointToClient(Control.MousePosition);
            PointF ptm = ClientToMM(ptc);
            return ptm;
        }

        public PointF Snap(PointF ptm, float dist, out Pin pin, out Via via, out ConnectionPoint cp)
        {
            bool alt = (Control.ModifierKeys & Keys.Alt) != 0;
            bool ctrl = (Control.ModifierKeys & Keys.Control) != 0;
            if (alt && ctrl)
            {
                pin = null;
                via = null;
                cp = null;
                return ptm;
            }
            if (alt || ctrl)
                dist = 0;
            float pin_dist = dist;
            pin = FindNearestPin(ptm, ref pin_dist);
            float via_dist = dist;
            via = FindNearestVia(ptm, ref via_dist);
            float cp_dist = dist;
            cp = FindNearestConnectionPoint(ptm, ref cp_dist);
            if (pin != null && pin_dist <= via_dist && pin_dist <= cp_dist)
            {
                via = null;
                cp = null;
                return pin.pos;
            }
            if (via != null && via_dist <= cp_dist)
            {
                pin = null;
                cp = null;
                return via.pos;
            }
            if (cp != null)
            {
                pin = null;
                via = null;
                return cp.pos;
            }            

            if (!alt)
                return Geom.Snap(ptm, grid_origin, grid_spacing);
            
            return ptm;

            //float spacing = 2.54f;
            //float x = Convert.ToSingle(Math.Round(ptm.X / spacing) * spacing);
            //float y = Convert.ToSingle(Math.Round(ptm.Y / spacing) * spacing);
            //return new PointF(x, y);
        }

        public PointF Snap(PointF ptm, float dist)
        {
            Pin pin;
            Via via;
            ConnectionPoint cp;
            return Snap(ptm, dist, out pin, out via, out cp);
        }

        public Pin FindNearestPin(PointF ptm, ref float dist)
        {
            Pin res = null;
            foreach (Part part in doc.PlacedParts)
            {
                if (part.dragged || part.original != null && part.original.dragged)
                    continue;
                foreach (Pin pin in part.pins)
                {
                    if (pin.dragged) continue;
                    float d = Geom.Dist(ptm, pin.pos);// -pin.radius;
                    //if (d < 0) d = 0;
                    if (d <= dist)
                    {
                        res = pin;
                        dist = d;
                    }
                }
            }
            return res;
        }

        public Via FindNearestVia(PointF ptm, ref float dist)
        {
            Via res = null;
            foreach (Part part in doc.PlacedParts)
            {
                if (part.dragged || part.original != null && part.original.dragged)
                    continue;
                foreach (Via via in part.vias)
                {
                    float d = Geom.Dist(ptm, via.pos);// -via.outer_radius;
                    //if (d < 0) d = 0;
                    if (d <= dist)
                    {
                        res = via;
                        dist = d;
                    }
                }
            }
            return res;
        }

        public ConnectionPoint FindNearestConnectionPoint(PointF ptm, ref float dist)
        {
            ConnectionPoint res = null;
            foreach (Part part in doc.PlacedParts)
            {
                if (part.dragged || part.original != null && part.original.dragged)
                    continue;
                foreach (Connection c in part.own_connections)
                {
                    if (c.dragged_cp != 0)
                    {
                        float d1 = Geom.Dist(ptm, c.cp1.pos);
                        if (d1 <= dist)
                        {
                            res = c.cp1;
                            dist = d1;
                        }
                    }
                    if (c.dragged_cp != 1)
                    {
                        float d2 = Geom.Dist(ptm, c.cp2.pos);
                        if (d2 <= dist)
                        {
                            res = c.cp2;
                            dist = d2;
                        }
                    }
                }
            }
            return res;
        }

        public ConnectionPoint FindConnectionPoint(Connection c, PointF ptm)
        {
            if (c.cp1.dragable && Geom.Eq(ptm, c.cp1.pos))
                return c.cp1;
            if (c.cp2.dragable && Geom.Eq(ptm, c.cp2.pos))
                return c.cp2;
            return null;
        }

        public ConnectionPoint FindConnectionPoint(Part part, PointF ptm)
        {
            int cnt = part.own_connections.Count;
            if (cnt == 0)
                return null;
            foreach (Connection c in part.own_connections)
            {
                ConnectionPoint cp = FindConnectionPoint(c, ptm);
                if (cp != null)
                    return cp;
            }
            return null;
        }

        public void Zoom(float factor, PointF ptc)
        {
            PointF ptm = ClientToMM(ptc);
            doc.scale *= factor;
            PointF ptc2 = MMToClient(ptm);
            float dx = ptc.X - ptc2.X;
            float dy = ptc.Y - ptc2.Y;
            doc.offset.X += dx;
            doc.offset.Y += dy;
            ofsDown.X += dx;
            ofsDown.Y += dy;
        }

        public AABB CalcAABB(List<Part> parts)
        {
            AABB res = new AABB();
            foreach (Part part in parts)
            {
                AABB aabb = part.CalcAABB(); 
                if (aabb.Width > 0 || aabb.Height > 0)
                    res.Add(aabb);
            }
            return res;
        }

        public PointF GetSelectionCenter()
        {
            return CalcAABB(doc.SelectedParts).Center;
        }

        public void CancelDrag()
        {
            if (panel == null) return;
            DragType = null;
            if (DraggedConnections.Count > 0)
            {
                ForgetNewConnectionUndo();
                foreach (Connection c in DraggedConnections)
                {
                    c.dragged_cp = -1;
                    ConnectionPoint cp1, cp2;
                    List<Connection> lst = c.part.GetConnectionEndPoints(out cp1, out cp2);
                    if (lst != null && lst.Count == 1 && cp1.Matches(cp2))
                        DelPart(c.part, false, false);
                }
                DraggedConnections.Clear();
            }

            if (DraggedParts.Count > 0)
            {
                foreach (Part part in DraggedParts)
                {
                    part.dragged = false;
                    if (!part.placed)
                    {
                        foreach (Connection c in part.own_connections)
                            c.Unlink();
                    }
                }
                foreach (Part part in DraggedParts)
                {
                    if (part.original != null)
                    {
                        part.original.dragged = false;
                        part.original.UpdateConnections();
                    }
                }
                DraggedParts.Clear();
            }
            
            panel.Invalidate();
        }

        public void AcceptDrag(PointF ptm)
        {
            if (DraggedParts.Count == 0 && DraggedConnections.Count == 0) return;
            SaveUndo("accept drag");
            SuspendProperties();

            foreach (Part part in DraggedParts)
            {
                if (part.type == "Connection") continue;
                if (!part.IsMoveable()) continue;
                PointF pt = part.DraggedPos(ptm);
                PointF pts = Snap(pt, placement_snap_dist);
                Part p = (part.original != null) ? part.original : part;
                p = PlacePart(p, pts.X, pts.Y, part.rot);
                //SelectPart(p, true);
            }
            foreach (Part part in DraggedParts)
            {
                if (part.type != "Connection") continue;
                if (!part.IsMoveable()) continue;
                PointF pt = part.DraggedPos(ptm);
                PointF pts = Snap(pt, placement_snap_dist);
                Part p = (part.original != null) ? part.original : part;
                p = PlacePart(p, pts.X, pts.Y, part.rot);
                //SelectPart(p, true);
            }
            foreach (Connection c in DraggedConnections)
            {
                UpdateHover(ptm);
                ConnectionPoint cp;
                if (HoveredPins.Count > 0)
                    cp = new ConnectionPoint(c, HoveredPins[HoveredPins.Count - 1]);
                else if (HoveredVias.Count > 0)
                    cp = new ConnectionPoint(c, HoveredVias[HoveredVias.Count - 1]);
                else
                {
                    PointF pts = Snap(ptm, placement_snap_dist);
                    cp = new ConnectionPoint(c);
                    cp.ofs = c.part.MMToLocal(pts);
                }
                if (c.dragged_cp == 0)
                {
                    c.cp1.Unlink();
                    c.cp1 = cp;
                }
                else if (c.dragged_cp == 1)
                {
                    c.cp2.Unlink();
                    c.cp2 = cp;
                }
                cp.Link();
                c.part.UpdateConnections();

                if (c.dragged_cp == 1 && c.part.bendable_pin != null)
                {
                    c.part.bendable_pin.part.UpdateConnections();
                    //List<Connection> cl = c.part.GetConnections();
                    //if (cl.Count > 0 && c == cl[cl.Count - 1])
                    //{
                    //    c.part.bendable_pin.ofs = c.part.bendable_pin.part.MMToLocal(cp.pos);
                    //    c.part.bendable_pin.part.UpdateConnections();
                    //}
                }
            }

            string dt = DragType;
            Part dp = null;
            bool shift = (Control.ModifierKeys & Keys.Shift) != 0;

            foreach (Part part in DraggedParts)
            {
                if (shift && dp == null)
                    dp = part;
                Part p = (part.original != null) ? part.original : part;
                p.dragged = false;
            }
            CancelDrag();

            DetectDirectContacts();
            ResumeProperties();

            if (shift)
            {
                if (dt == "paste")
                    Paste();
                else if (dt == "new part" && dp != null)
                {
                    DragType = dt;
                    DragPart(new Part(dp), true);
                }
            }
        }

        public void DragPart(Part part, bool bDrag)
        {
            if (part == null || part.dragged == bDrag || part.bounds.Width <= 0 || part.bounds.Height <= 0) return;
            part.dragged = bDrag;
            if (bDrag)
            {
                DraggedParts.Add(part);
            }
            else
                DraggedParts.Remove(part);
            panel.Invalidate();
        }

        public void DragSelected(PointF ptm)
        {
            CancelDrag();
            if (doc.SelectedParts.Count == 0) return;

            DragType = "selected";
            List<Part> lst = new List<Part>();
            foreach (Part part in doc.SelectedParts)
            {
                part.drag_point = part.MMToLocal(ptm);
                part.dragged = true;
                lst.Add(part);
                foreach (Connection c in part.direct_connections)
                {
                    if (c.part.dragged || c.part.selected) continue;
                    c.part.drag_point = c.part.MMToLocal(ptm);
                    c.part.dragged = true;
                    lst.Add(c.part);
                }
            }
            
            CopyParts(lst, DraggedParts, true);
            ClearCopy(lst);
            foreach (Part part in DraggedParts) part.dragged = true;
        }

        public void DragConnectionPoint(Connection c, int idx)
        {
            if (c == null || c.dragged_cp == idx) return;
            if (idx >= 0 && c.dragged_cp < 0)
                DraggedConnections.Add(c);
            else if (idx < 0 && c.dragged_cp >= 0)
                DraggedConnections.Remove(c);
            c.dragged_cp = idx;
            panel.Invalidate();
        }

        public void ClearSelection()
        {
            foreach (Part part in doc.SelectedParts)
                part.selected = false;
            doc.SelectedParts.Clear();
            UpdateProperties();
            panel.Invalidate();
        }

        public void SelectPart(Part part, bool bSelect, bool bLinked = true)
        {
            if (part == null) return;
            if (part.selected == bSelect) return;
            part.selected = bSelect;
            if (bSelect)
                doc.SelectedParts.Add(part);
            else
                doc.SelectedParts.Remove(part);

            if (bLinked)
            {
                if (part.bendable_pin != null)
                    SelectPart(part.bendable_pin.part, bSelect, true);
                else
                {
                    foreach (Connection c in part.direct_connections)
                    {
                        if (c.part.bendable_pin != null && c.part.bendable_pin.part == part)
                            SelectPart(c.part, bSelect, false);
                    }
                }
            }

            UpdateProperties();
            panel.Invalidate();
        }

        private void DrawHighlight(float x1, float y1, float w, float h, bool selection, bool highlight, bool hover, bool contact)
        {
            if (selection)
                DrawHighlight(x1, y1, w, h, penSelection, select_grow);
            if (highlight)
                DrawHighlight(x1, y1, w, h, penHighlight, highlight_grow);
            else if (hover)
                DrawHighlight(x1, y1, w, h, penHover);
            else if (contact)
                DrawHighlight(x1, y1, w, h, penContact);
        }

        private void DrawHighlight(float x1, float y1, float w, float h, Pen pen, float grow = 0)
        {
            if (w <= 0 || h <= 0) return;
            gr.DrawRectangle(pen, x1 - grow, y1 - grow, w + 2 * grow, h + 2 * grow);
        }

        private void DrawVia(Via via, float cx, float cy)
        {
            Color clr_ring = via.part.dragged ? Color.FromArgb(via.color_ring.A / 2, via.color_ring) : via.color_ring;
            Color clr_hole = via.part.dragged ? Color.FromArgb(via.color_hole.A / 2, via.color_hole) : via.color_hole;
            float x = (cx - via.ring_radius) * doc.scale;
            float y = (cy - via.ring_radius) * doc.scale;
            float r = 2 * via.ring_radius * doc.scale;
            if (via.form == "square")
                gr.FillRectangle(new SolidBrush(clr_ring), x, y, r, r);
            else if (via.form == "circle")
                gr.FillEllipse(new SolidBrush(clr_ring), x, y, r, r);
            gr.FillEllipse(new SolidBrush(clr_hole), (cx - via.hole_radius) * doc.scale, (cy - via.hole_radius) * doc.scale, 2 * via.hole_radius * doc.scale, 2 * via.hole_radius * doc.scale);

            DrawHighlight(x, y, r, r, via.selected, via.highlighted, via.hovered, via.trace_open);
        }

        private GraphicsPath GenerateRectangle(float x1, float y1, float w, float h)
        {
            GraphicsPath path = new GraphicsPath();
            RectangleF rc = new RectangleF(x1, y1, w, h);
            path.AddRectangle(rc);
            return path;
        }

        private GraphicsPath GenerateSquare(float x1, float y1, float w, float h)
        {
            if (w >= h)
            {
                y1 -= (w - h) / 2;
                h = w;
            }
            else
            {
                x1 -= (h - w) / 2;
                w = h;
            }
            return GenerateRectangle(x1, y1, w, h);
        }


        private GraphicsPath GenerateCircle(float x1, float y1, float w, float h)
        {
            if (w >= h)
            {
                y1 -= (w - h) / 2;
                h = w;
            }
            else
            {
                x1 -= (h - w) / 2;
                w = h;
            }
            return GenerateEllipse(x1, y1, w, h);
        }

        private GraphicsPath GenerateEllipse(float x1, float y1, float w, float h)
        {
            GraphicsPath path = new GraphicsPath();
            RectangleF rc = new RectangleF(x1, y1, w, h);
            path.AddEllipse(rc);
            return path;
        }

        private GraphicsPath GenerateCapsule(float x1, float y1, float w, float h)
        {
            GraphicsPath path = new GraphicsPath();
            if (w >= h)
            {
                RectangleF rc = new RectangleF(x1, y1, h, h);
                path.AddArc(rc, 90, 180);
                rc.X = x1 + w - h;
                path.AddArc(rc, 270, 180);
            }
            else
            {
                RectangleF rc = new RectangleF(x1, y1, w, w);
                path.AddArc(rc, 0, 180);
                rc.Y = y1 + h - w;
                path.AddArc(rc, 180, 180);
            }
            path.CloseFigure();
            return path;
        }

        private GraphicsPath GenerateDup(float x1, float y1, float w, float h)
        {
            if (w / 2 > h) return null;
            GraphicsPath path = new GraphicsPath();
            RectangleF rc = new RectangleF(x1, y1, w, w);
            path.AddArc(rc, -180, 180);
            //path.AddLine(x1 + w, y1 + h, x1, y1 + h);
            path.CloseFigure();
            return path;
        }

        private GraphicsPath GenerateShape(string form, float x1, float y1, float w, float h)
        {
            if (form == "rectangle")
                return GenerateRectangle(x1, y1, w, h);
            if (form == "square")
                return GenerateSquare(x1, y1, w, h);
            if (form == "circle")
                return GenerateCircle(x1, y1, w, h);
            if (form == "ellipse")
                return GenerateEllipse(x1, y1, w, h);
            if (form == "capsule")
                return GenerateCapsule(x1, y1, w, h);
            if (form == "D-up")
                return GenerateDup(x1, y1, w, h);

            return GenerateRectangle(x1, y1, w, h);
        }

        private void DrawLine(float x1, float y1, float x2, float y2, float width, Color clr)
        {
            x1 *= doc.scale;
            y1 *= doc.scale;
            x2 *= doc.scale;
            y2 *= doc.scale;
            gr.DrawLine(new Pen(clr, width * doc.scale), x1, y1, x2, y2);
            var brush = new SolidBrush(clr);
            float r = width / 2 * doc.scale;
            gr.FillEllipse(brush, x1 - r, y1 - r, width * doc.scale, width * doc.scale);
            gr.FillEllipse(brush, x2 - r, y2 - r, width * doc.scale, width * doc.scale);
        }

        private void DrawConnection(Connection c)
        {
            Color clr = /*c.dynamic ? c.part.color :*/ c.color;
            if (c.part.dragged || c.dragged_cp >= 0)
                clr = Color.FromArgb(clr.A / 2, clr);
            PointF pt1 = c.cp1.ofs;
            PointF pt2 = c.cp2.ofs;
            DrawLine(pt1.X, pt1.Y, pt2.X, pt2.Y, c.width, clr);
            if (c.dragged_cp >= 0)
            {
                PointF ofs = c.part.MMToLocal(GetMouseInMM());
                if (c.dragged_cp == 0)
                    pt1 = ofs;
                else if (c.dragged_cp == 1)
                    pt2 = ofs;
                DrawLine(pt1.X, pt1.Y, pt2.X, pt2.Y, c.width, clr);
            }
        }

        private void DrawConnectionHighlight(Connection c)
        {
            bool selected = c.selected || c.dynamic && c.part.selected;
            bool highlighted = c.highlighted || c.dynamic && (c.part.highlighted || ContextHighlight == c || ContextHighlight == c.part);
            bool hovered = c.hovered || c.part.hovered && c.dynamic;
            bool contact = c.trace_open;
            if (!highlighted && !hovered && !selected && !contact)
                return;
            var save = gr.Save();
            float rot = Geom.Angle(c.cp1.ofs, c.cp2.ofs);
            gr.TranslateTransform(c.cp1.ofs.X * doc.scale, c.cp1.ofs.Y * doc.scale);
            gr.RotateTransform(rot);
            float r = c.width * doc.scale / 2;
            float w = Geom.Dist(c.cp1.ofs, c.cp2.ofs) * doc.scale;
            DrawHighlight(-r, -r, w + 2 * r, 2 * r, selected, highlighted, hovered, contact);
            gr.Restore(save);
        }

        private void DrawPin(Pin pin, float cx, float cy)
        {
            Color clr = pin.part.dragged ? Color.FromArgb(pin.color.A / 2, pin.color) : pin.color;
            float x = (cx - pin.radius) * doc.scale;
            float y = (cy - pin.radius) * doc.scale;
            float r = 2 * pin.radius * doc.scale;
            if (pin.form == "square")
                gr.FillRectangle(new SolidBrush(clr), x, y, r, r);
            else if (pin.form == "circle") 
                gr.FillEllipse(new SolidBrush(clr), x, y, r, r);
            DrawHighlight(x, y, r, r, pin.selected, pin.highlighted, pin.hovered, pin.trace_open);
        }

        private void DrawTracks(Part part)
        {
            var save = gr.Save();
            gr.TranslateTransform(part.pos.X * doc.scale, part.pos.Y * doc.scale);
            gr.RotateTransform(part.rot);

            foreach (Connection c in part.own_connections)
            {
                if (c.type == "Track")
                    DrawConnection(c);
            }

            gr.Restore(save);
        }

        private void DrawWires(Part part)
        {
            var save = gr.Save();
            gr.TranslateTransform(part.pos.X * doc.scale, part.pos.Y * doc.scale);
            gr.RotateTransform(part.rot);

            foreach (Connection c in part.own_connections)
            {
                if (c.type == "Wire")
                    DrawConnection(c);
            }
            if (!part.placed && part.direct_connections.Count == 0)
            {
                Part leg = Part.Get("Leg");
                foreach (Pin pin in part.pins)
                {
                    if (pin.bendable && leg != null)
                    {
                        DrawLine(pin.ofs.X, pin.ofs.Y, pin.org_ofs.X, pin.org_ofs.Y, Convert.ToSingle(leg.tbl["width"]), leg.color);
                    }
                }
            }

            gr.Restore(save);
        }

        private void DrawVias(Part part)
        {
            var save = gr.Save();
            gr.TranslateTransform(part.pos.X * doc.scale, part.pos.Y * doc.scale);
            gr.RotateTransform(part.rot);

            foreach (Via via in part.vias)
            {
                DrawVia(via, via.ofs.X, via.ofs.Y);
            }

            gr.Restore(save);
        }

        private void DrawPins(Part part)
        {
            var save = gr.Save();
            gr.TranslateTransform(part.pos.X * doc.scale, part.pos.Y * doc.scale);
            gr.RotateTransform(part.rot);

            foreach (Pin pin in part.pins)
            {
                PointF ofs = pin.ofs;
                //if (pin.bendable && dragged) continue;
                DrawPin(pin, ofs.X, ofs.Y);
            }

            gr.Restore(save);
        }

        private void DrawPartLabel(Part part)
        {
            if (show_labels == 0) return;

            if (!part.hovered)
            {
                if (show_labels == 1) return;
                if (show_labels == 2 && !part.show_label) return;
            }

            String label = part.GetLabel();
            if (label == "") return;

            var save = gr.Save();
            gr.TranslateTransform(part.pos.X * doc.scale, part.pos.Y * doc.scale);
            gr.RotateTransform(part.rot);

            float left = part.bounds.Left * doc.scale;
            float top = part.bounds.Top * doc.scale;
            float w = part.bounds.Width * doc.scale;
            float h = part.bounds.Height * doc.scale;

            float mx = w * 0.05f;
            float my = w * 0.05f;
            left += mx;
            top += my;
            w -= 2 * mx;
            h -= 2 * my;

            Font font = new Font("Times New Roman", h, GraphicsUnit.Pixel);
            float size_pt = font.SizeInPoints;
            SizeF sz = gr.MeasureString(label, font);
            if (sz.Width > w)
            {
                float scale = w / sz.Width;
                font = new Font(font.FontFamily, font.SizeInPoints * scale);
                size_pt *= scale;
                sz.Width = w;
                sz.Height *= scale;
            }

            PointF so = Geom.Rotate(new PointF(2, 2), -part.rot);
            gr.DrawString(label, font, Brushes.Black, left + (w - sz.Width) / 2 + so.X, top + (h - sz.Height) / 2 + so.Y);
            gr.DrawString(label, font, Brushes.Khaki, left + (w - sz.Width) / 2, top + (h - sz.Height) / 2);

            //GraphicsPath path = new GraphicsPath();
            //path.AddString(label, font.FontFamily, (int)font.Style, font.SizeInPoints, new PointF(left + (w - sz.Width) / 2, top + (h - sz.Height) / 2), new StringFormat());
            //gr.FillPath(Brushes.White, path);
            //gr.DrawPath(Pens.DarkGray, path);

            gr.Restore(save);
        }

        private void DrawBody(Part part)
        {
            var save = gr.Save();
            gr.TranslateTransform(part.pos.X * doc.scale, part.pos.Y * doc.scale);
            gr.RotateTransform(part.rot);

            //if (part.type == "Connection")
            //    gr.DrawRectangle(new Pen(Color.Blue), -10, -10, 20, 20);

            float left = part.bounds.Left * doc.scale;
            float top = part.bounds.Top * doc.scale;
            float w = part.bounds.Width * doc.scale;
            float h = part.bounds.Height * doc.scale;

            Color clr = part.dragged ? Color.FromArgb(part.color.A / 2, part.color) : part.color;

            GraphicsPath path = GenerateShape(part.form, left, top, w, h);
            gr.FillPath(new SolidBrush(clr), path);
            gr.DrawPath(new Pen(part.frame_color, 2), path);

            gr.Restore(save);
        }

        private void DrawHighlight(Part part)
        {
            var save = gr.Save();
            gr.TranslateTransform(part.pos.X * doc.scale, part.pos.Y * doc.scale);
            gr.RotateTransform(part.rot);

            foreach (Connection c in part.own_connections)
                DrawConnectionHighlight(c);

            //if (part.type == "Connection")
            //    gr.DrawRectangle(new Pen(Color.Blue), -10, -10, 20, 20);

            bool selected = part.selected;
            bool highlighted = part.highlighted|| ContextHighlight == part;
            if (selected || highlighted || part.hovered)
            {
                float left = part.bounds.Left * doc.scale;
                float top = part.bounds.Top * doc.scale;
                float w = part.bounds.Width * doc.scale;
                float h = part.bounds.Height * doc.scale;
                DrawHighlight(left, top, w, h, selected, highlighted, part.hovered, false);
            }

            gr.Restore(save);
        }

        private void DrawGrid()
        {
            if (grid_spacing == 0) return;
            PointF ptmMin = ClientToMM(new PointF(0, 0));
            PointF ptmMax = ClientToMM(new PointF(panel.Width, panel.Height));
            Pen pen = new Pen(Color.FromArgb(64, 64, 64, 128));
            for (float y = grid_origin.Y; y > ptmMin.Y; y -= grid_spacing)
                gr.DrawLine(pen, ptmMin.X * doc.scale, y * doc.scale, ptmMax.X * doc.scale, y * doc.scale);
            for (float y = grid_origin.Y + grid_spacing; y < ptmMax.Y; y += grid_spacing)
                gr.DrawLine(pen, ptmMin.X * doc.scale, y * doc.scale, ptmMax.X * doc.scale, y * doc.scale);
            for (float x = grid_origin.X; x > ptmMin.X; x -= grid_spacing)
                gr.DrawLine(pen, x * doc.scale, ptmMin.Y * doc.scale, x * doc.scale, ptmMax.Y * doc.scale);
            for (float x = grid_origin.X + grid_spacing; x < ptmMax.X; x += grid_spacing)
                gr.DrawLine(pen, x * doc.scale, ptmMin.Y * doc.scale, x * doc.scale, ptmMax.Y * doc.scale);
        }

        private string ConnectionHighlighText(Connection c)
        {
            if (c.printed) return null;
            c.printed = true;
            c.part.printed = true;
            if (!c.dynamic)
                return c.name + " (" + c.part.name + ")";
            
            string s = "";
            if (!c.cp1.printed)
                s += " from " + c.cp1.ToString();
            if (!c.cp2.printed)
                s += " to " + c.cp2.ToString();

            s = c.name + s;
            return s;
        }

        private string PinHighlighText(Pin pin)
        {
            if (pin.printed) return null;
            pin.printed = true;
            pin.part.printed = true;
            return pin.ToString(); //pin.part.name + " " + pin.name;
        }

        private string ViaHighlighText(Via via)
        {
            if (via.printed) return null;
            via.printed = true;
            via.part.printed = true;
            return via.ToString(); //via.name + " (" + via.part.name + ")";
        }

        private string PartHighlighText(Part part)
        {
            if (part.printed) return null;
            part.printed = true;
            if (part.type == "Via" && part.vias.Count == 1)
                return ViaHighlighText(part.vias[0]);
            if (part.type != "Connection")
                return part.name;
            string s = part.name;
            ConnectionPoint cp1, cp2;
            part.GetConnectionEndPoints(out cp1, out cp2);
            if (cp1 != null && !cp1.printed)
              s += " from " + cp1.ToString();
            if (cp2 != null && !cp2.printed)
            {
                if (part.bendable_pin != null && Geom.Eq(part.bendable_pin.ofs, part.bendable_pin.org_ofs))
                    return null;
                s += " to " + cp2.ToString();
            }
            return s;
        }

        private void AddHighlightText(ref string s, string text)
        {
            if (String.IsNullOrWhiteSpace(text)) return;
            if (s != "") s += "; ";
            s += text;
        }

        private void UpdateHover(Part part, PointF ptm, PointF pts, bool bIgnoreLock = false)
        {
            part.highlighted = false;
            part.hovered = false;
            part.printed = false;
            if (part.dragged) return;
            bool added = false;
            if ((bIgnoreLock || !part.locked) && part.Contains(ptm))
            {
                part.hovered = true;
                added = true;
                if (part.type == "Board") 
                    HoveredBoards.Add(part);
                else
                    HoveredComponents.Add(part);
            }
            foreach (Connection c in part.own_connections)
            {
                c.highlighted = false;
                c.hovered = false;
                c.printed = false;
                if (c.Contains(ptm) || Geom.Eq(c.cp1.pos, pts) || Geom.Eq(c.cp2.pos, pts))
                {
                    c.hovered = true;
                    HoveredConnections.Add(c);
                    if (!added && (bIgnoreLock || !part.locked))
                    {
                        added = true;
                        if (part.type == "Board")
                            HoveredBoards.Add(part);
                        else
                            HoveredComponents.Add(part);
                    }
                }
            }
            foreach (Via via in part.vias)
            {
                via.highlighted = false;
                via.hovered = false;
                via.printed = false;
                if (Geom.Eq(via.pos, pts))
                {
                    via.hovered = true;
                    HoveredVias.Add(via);
                    if (!added && (bIgnoreLock || !part.locked))
                    {
                        added = true;
                        if (part.type == "Board")
                            HoveredBoards.Add(part);
                        else
                            HoveredComponents.Add(part);
                    }
                }
            }
            foreach (Pin pin in part.pins)
            {
                pin.highlighted = false;
                pin.hovered = false;
                pin.printed = false;
                if (pin.dragged) continue;
                if (Geom.Eq(pin.pos, pts))
                {
                    pin.hovered = true;
                    HoveredPins.Add(pin);
                    if (!added && (bIgnoreLock || !part.locked))
                    {
                        added = true;
                        if (part.type == "Board")
                            HoveredBoards.Add(part);
                        else
                            HoveredComponents.Add(part);
                    }
                }
            }
        }

        private Part UpdateHover(PointF ptm, bool bIgnoreLock = false)
        {
            PointF pts = Snap(ptm, highlight_snap_dist);

            HoveredBoards.Clear();
            HoveredComponents.Clear();
            HoveredConnections.Clear();
            HoveredVias.Clear();
            HoveredPins.Clear();

            bool in_selection = false;

            foreach (Part part in doc.PlacedParts)
            {
                UpdateHover(part, ptm, pts, bIgnoreLock);
                if (part.selected && (part.Contains(ptm) || part.Contains(pts)))
                    in_selection = true;
            }

            if (DraggedParts.Count != 0 || ContextHighlight != null)
                return null;

            Part hpart = null;
            Pin hpin = null;
            Via hvia = null;
            Connection hcon = null;
            Contact hc = null;

            if (DraggedConnections.Count == 0)
            {
                if (HoveredComponents.Count != 0)
                {
                    hpart = HoveredComponents[HoveredComponents.Count - 1];
                    hpart.highlighted = true;
                }
                if (hpart == null && HoveredBoards.Count != 0)
                {
                    hpart = HoveredBoards[HoveredBoards.Count - 1];
                    hpart.highlighted = true;
                }
            }

            if (DraggedParts.Count == 0 && !in_selection)
            {
                if (HoveredPins.Count > 0)
                {
                    hpin = HoveredPins[HoveredPins.Count - 1];
                    hpin.highlighted = true;
                    hc = hpin;
                }
                else if (HoveredVias.Count > 0)
                {
                    hvia = HoveredVias[HoveredVias.Count - 1];
                    hvia.highlighted = true;
                    hc = hvia;
                }
                else if (HoveredConnections.Count > 0)
                {
                    hcon = HoveredConnections[HoveredConnections.Count - 1];
                    //hcon.highlighted = true;
                    hc = hcon;
                }
            }

            foreach (Part part in doc.SelectedParts)
            {
                if (part.type != "Connection") continue;
                ConnectionPoint cp = FindConnectionPoint(part, pts);
                if (cp == null) continue;
                if (cp.pin != null)
                    cp.pin.highlighted = true;
                if (cp.via != null)
                    cp.via.highlighted = true;
            }

            TraceContacts(hc);

            string s = "";

            foreach (Pin pin in HoveredPins)
            {
                AddHighlightText(ref s, PinHighlighText(pin));
            }

            if (hpart != null)
            {
                AddHighlightText(ref s, PartHighlighText(hpart));
            }

            //foreach (Via via in HoveredVias)
            //{
            //    if (via.highlighted)
            //        AddHighlightText(ref s, ViaHighlighText(via));
            //}

            //foreach (Connection c in HoveredConnections)
            //{
            //    //if (c.highlighted)
            //        AddHighlightText(ref s, ConnectionHighlighText(c));
            //}            

            foreach (Part part in HoveredComponents)
            {
                if (part.highlighted || part.type == "Connection")
                    AddHighlightText(ref s, PartHighlighText(part));
            }

            foreach (Part part in HoveredBoards)
            {
                if (part.highlighted)
                    AddHighlightText(ref s, PartHighlighText(part));
            }

            StatusText.Text = s;

            return hpart;
        }

        private Part UpdateHover()
        {
            return UpdateHover(GetMouseInMM());
        }

        public void DetectDirectContacts()
        {
            foreach (Part part in doc.PlacedParts) part.ClearDirectContacts();
            
            int cnt = doc.PlacedParts.Count;
            for (int i = 0; i < cnt; ++i)
            {
                Part p1 = doc.PlacedParts[i];
                for (int j = i + 1; j < cnt; ++j)
                {
                    Part p2 = doc.PlacedParts[j];
                    p1.DetectDirectContacts(p2);
                }
            }
        }

        public void TraceContacts(Contact c)
        {
            TracedContacts.Clear();
            foreach (Part part in doc.PlacedParts) part.ClearTracedContacts();
            if (c == null) return;
            c.TraceContacts(TracedContacts);
            int i = 0;
            while (i < TracedContacts.all.Count)
            {
                Contact c2 = TracedContacts.all[i];
                c2.TraceContacts(TracedContacts);
                ++i;
            }
        }

        private void OnPaint(object sender, PaintEventArgs e)
        {
            Point ptc = panel.PointToClient(Control.MousePosition);
            PointF ptm = ClientToMM(ptc);

            gr = e.Graphics;

            gr.TranslateTransform(doc.offset.X, doc.offset.Y);

            UpdateHover(ptm);

            foreach (Part part in DraggedParts)
            {
                if (part.type != "Connection" && part.IsMoveable())
                    part.pos = part.DraggedPos(ptm);
            }
            foreach (Part part in DraggedParts)
            {
                if (part.type == "Connection" && part.IsMoveable())
                    part.pos = part.DraggedPos(ptm);
                part.UpdateConnections();
            }

            foreach (Part part in doc.PlacedParts)
            {
                if (part.type == "Board")
                    DrawBody(part);
            }
            foreach (Part part in DraggedParts)
            {
                if (part.type == "Board")
                    DrawBody(part);
            }

            foreach (Part part in doc.PlacedParts) DrawTracks(part);
            foreach (Part part in DraggedParts) DrawTracks(part);
            
            foreach (Part part in doc.PlacedParts) DrawVias(part);
            foreach (Part part in DraggedParts) DrawVias(part);
            
            foreach (Part part in doc.PlacedParts) DrawWires(part);
            foreach (Part part in DraggedParts) DrawWires(part);

            foreach (Part part in doc.PlacedParts)
            {
                if (part.type != "Board")
                    DrawBody(part);
            }
            foreach (Part part in DraggedParts)
            {
                if (part.type != "Board")
                    DrawBody(part);
            }

            foreach (Part part in doc.PlacedParts) DrawPins(part);
            foreach (Part part in DraggedParts) DrawPins(part);

            foreach (Part part in doc.PlacedParts) DrawPartLabel(part);
            foreach (Part part in DraggedParts) DrawPartLabel(part);

            foreach (Part part in doc.PlacedParts) DrawHighlight(part);

            //if (doc.SelectedParts.Count > 0)
            //{
            //    RectangleF rc = CalcAABB(doc.SelectedParts).Rect;
            //    gr.DrawRectangle(Pens.Red, rc.Left * doc.scale, rc.Top * doc.scale, rc.Width * doc.scale, rc.Height * doc.scale);
            //}

            

            DrawGrid();

            gr = null;
        }

        private void DragPart(Part part)
        {
            if (part == null) return;
            panel.Focus();
            CancelDrag();
            
            if (part.name == "Connection")
            {
                connection_part_name = null;
                ConnectionTypeText.Text = "Connection: Auto";
            }
            else if (part.type == "Connection")
            {
                connection_part_name = part.name;
                ConnectionTypeText.Text = "Connection: " + part.name;
            }
            else
            {
                DragType = "new part";
                DragPart(new Part(part), true);
            }
        }

        private void PartsHierarchical_DoubleClick(object sender, EventArgs e)
        {
            if (panel == null) return;
            TreeView tv = sender as TreeView;
            if (tv.SelectedNode == null) return;
            DragPart(tv.SelectedNode.Tag as Part);
        }

        private void PartsHierarchical_ItemDrag(object sender, ItemDragEventArgs e)
        {
            if (panel == null) return;
            TreeView tv = sender as TreeView;
            TreeNode node = e.Item as TreeNode;
            DragPart(node.Tag as Part);
        }

        private void SelectPart(PointF ptm, bool bAll)
        {
            SuspendProperties();
            if ((Control.ModifierKeys & (Keys.Shift | Keys.Control)) == 0)
                ClearSelection();
            Part sel = UpdateHover(ptm);
            if (bAll)
            {
                foreach (Part part in HoveredComponents)
                {
                    if (!part.locked)
                        SelectPart(part, true);
                }
                if (sel != null)
                {
                    foreach (Connection c in sel.direct_connections)
                    {
                        if (!c.part.locked)
                            SelectPart(c.part, true);
                    }
                    if (sel.type == "Connection")
                    {
                        ConnectionPoint cp1, cp2;
                        sel.GetConnectionEndPoints(out cp1, out cp2);
                        Part p1 = cp1.dest_part;
                        if (p1 != null && p1.type != "Board" && !p1.locked)
                            SelectPart(p1, true);
                        Part p2 = cp2.dest_part;
                        if (p2 != null && p2.type != "Board" && !p2.locked)
                            SelectPart(p2, true);
                    }
                }
            }
            else if (sel != null)
                SelectPart(sel, !sel.selected, (Control.ModifierKeys & Keys.Alt) == 0);
            ResumeProperties();
        }

        private void OnMouseUp(object sender, MouseEventArgs e)
        {
            if (panel == null) return; // can happen if switching panels while dragging

            if (bDblClicked)
            {
                bDblClicked = false;
                return;
            }
            else
                bDblClicked = e.Clicks == 2;

            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                bRBDown = false;
                if (!bRBDragged)
                {
                    if (DraggedParts.Count > 0 || DraggedConnections.Count > 0)
                    {
                        CancelDrag();
                        return;
                    }               
                    ShowContextMenu();
                }
                return;
            }

            if (e.Button != System.Windows.Forms.MouseButtons.Left) return;
            if (!bLBDown && DraggedParts.Count == 0 && DraggedConnections.Count == 0) return;
            bLBDown = false;
            if (DraggedParts.Count > 0 || DraggedConnections.Count > 0)
            {
                PointF ptm = ClientToMM(e.Location);
                //ClearSelection();
                AcceptDrag(ptm);
                panel.Invalidate();
                return;
            }

            if (!bLBDragged)
            {
                PointF ptm = ClientToMM(e.Location);
                SelectPart(ptm, bDblClicked);
            }

            panel.Invalidate();
        }

        private void OnMouseDown(object sender, MouseEventArgs e)
        {
            HideTooltip();
            panel.Focus();
            panel.Invalidate();

            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                bLBDown = true;
                if (!bRBDown)
                {
                    ptDown = e.Location;
                    ofsDown = doc.offset;
                    bLBDragged = false;
                }
                UpdateHover();
                return;
            }

            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                bRBDown = true;
                ptDown = e.Location;
                ofsDown = doc.offset;
                bRBDragged = false;
            }

        }

        private void OnMouseMove(object sender, MouseEventArgs e)
        {
            if (panel == null) return; // can happen if switching panels while dragging

            if (tooltip_pos != e.Location)
                ShowTooltip(e.Location);

            PointF pt = new PointF((e.Location.X - doc.offset.X) / doc.scale, (e.Location.Y - doc.offset.Y) / doc.scale);
            CoordsText.Text = pt.X.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture) +
                ", " + pt.Y.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture) + " mm";

            panel.Invalidate();

            if (bRBDown && (bRBDragged || e.Location != ptDown) || bLBDown && bSpaceDown && (bLBDragged || e.Location != ptDown))
            {
                doc.offset.X = ofsDown.X + e.Location.X - ptDown.X;
                doc.offset.Y = ofsDown.Y + e.Location.Y - ptDown.Y;
                if (bLBDown)
                    bLBDragged = true;
                if (bRBDown)
                    bRBDragged = true;
                return;
            }

            if (!bLBDown || bRBDown) return;

            if (!bLBDragged && e.Location == ptDown)
                return;

            if (!bLBDragged && DraggedParts.Count == 0 && DraggedConnections.Count == 0)
            {
                bLBDragged = true;
                PointF ptm = ClientToMM(ptDown);
                Pin pin;
                Via via;
                ConnectionPoint cp;
                PointF pts = Snap(ptm, highlight_snap_dist, out pin, out via, out cp);
                bool in_selection = false;
                //bool boards_only = true;
                List<ConnectionPoint> drag_cp = new List<ConnectionPoint>();
                foreach (Part part in doc.SelectedParts)
                {
                    if (part.Contains(ptm) || part.Contains(pts))
                    {
                        in_selection = true;
                        //if (part.type != "Board")
                        //    boards_only = false;
                        if (part.type == "Connection")
                        {
                            foreach (Connection c in part.own_connections)
                            {
                                ConnectionPoint cps = FindConnectionPoint(c, pts);
                                if (cps != null)
                                    drag_cp.Add(cps);
                            }
                        }
                    }
                }
                //if (in_selection && boards_only)
                //{
                //    if (HighlightedComponents.Count != 0)
                //        in_selection = false;
                //}
                if (in_selection)
                {
                    if (drag_cp.Count > 0)
                    {
                        DragType = "connection points";
                        foreach (ConnectionPoint dcp in drag_cp)
                            DragConnectionPoint(dcp.connection, dcp.index);
                        return;
                    }
                    DragSelected(ptm);
                    return;
                }
                //SelectPart(ptm);
                if (pin != null || via != null)
                {
                    SaveUndo("new connection");
                    Part cpart;
                    if (pin != null)
                        cpart = PlaceConnection((connection_part_name != null) ? connection_part_name : "Track", pin, pin);
                    else if (via != null)
                        cpart = PlaceConnection((connection_part_name != null) ? connection_part_name : "Wire", via, via);
                    else
                        return;
                    DragType = "new connection";
                    Connection c = cpart.own_connections[0];
                    DragConnectionPoint(c, 1);
                    return;
                }

                // TODO: drag selection
                return;
            }
            bLBDragged = true;
        }

        void OnMouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta > 0)
            {
                if (DraggedParts.Count != 0 && (Control.ModifierKeys & Keys.Control) == 0)
                {
                    foreach (Part part in DraggedParts)
                    {
                        if (part.type != "Connection") continue;
                        RotatePart(part, 15, part.LocalToMM(part.drag_point));
                    }
                    foreach (Part part in DraggedParts)
                    {
                        if (part.type == "Connection") continue;
                        RotatePart(part, 15, part.LocalToMM(part.drag_point));
                    }
                }
                else
                    Zoom(e.Delta / 100.0f, e.Location);
            }
            else if (e.Delta < 0)
            {
                if (DraggedParts.Count != 0 && (Control.ModifierKeys & Keys.Control) == 0)
                {
                    foreach (Part part in DraggedParts)
                    {
                        if (part.type != "Connection") continue;
                        RotatePart(part, -15, part.LocalToMM(part.drag_point));
                    }
                    foreach (Part part in DraggedParts)
                    {
                        if (part.type == "Connection") continue;
                        RotatePart(part, -15, part.LocalToMM(part.drag_point));
                    }
                }
                else
                    Zoom(-100.0f / e.Delta, e.Location);
            }
            panel.Invalidate();
        }

        private void OnMouseLeave(object sender, EventArgs e)
        {
            HideTooltip();
        }

        private void ShowTooltip(Point pt)
        {
            //HideTooltip();
            //if (bLBDown || bRBDown)
            //    return;

            //UpdateHover();
            //string s = StatusText.Text;
            //if (s == "")
            //    return;
            //s = s.Replace(";", "\n");
            //tooltip_pos = pt;
            //tooltip_shown = true;
            ////tooltip.AutoPopDelay = 500;
            //tooltip.Show(s, panel);
        }

        private void HideTooltip()
        {
            //if (!tooltip_shown) return;
            //tooltip_shown = false;
            //tooltip.Hide(panel);
        }

        private void ShowContextMenu()
        {
            Point ptc = panel.PointToClient(Control.MousePosition);
            PointF ptm = ClientToMM(ptc);
            PointF pts = Snap(ptm, highlight_snap_dist);

            ContextMenuStrip menu = new ContextMenuStrip();
            menu.Closed += delegate(object sender, ToolStripDropDownClosedEventArgs e)
            {
                ContextHighlight = null;
                panel.Invalidate();
            };


            ToolStripMenuItem miMoveGrid = new ToolStripMenuItem("Set grid origin");
            miMoveGrid.Click += delegate(object sender, EventArgs e)
            {
                grid_origin = pts;
                panel.Invalidate();
            };
            menu.Items.Add(miMoveGrid);

            UpdateHover(ptm, true);
            List<Part> parts = new List<Part>();
            foreach (Part part in HoveredBoards) parts.Insert(0, part);
            foreach (Part part in HoveredComponents) parts.Insert(0, part);
            if (parts.Count > 0)
            {
                menu.Items.Add("-");

                foreach (Part part in parts)
                {
                    ToolStripMenuItem mi = new ToolStripMenuItem(part.ToString());
                    mi.MouseEnter += delegate(object sender, EventArgs e)
                    {
                        ContextHighlight = part;
                        panel.Invalidate();
                    };
                    mi.MouseLeave += delegate(object sender, EventArgs e)
                    {
                        ContextHighlight = part;
                        panel.Invalidate();
                    };

                    ToolStripMenuItem miSelect = new ToolStripMenuItem("Select");
                    miSelect.Click += delegate(object sender, EventArgs e)
                    {
                        if ((Control.ModifierKeys & (Keys.Shift | Keys.Control)) == 0)
                            ClearSelection();
                        SelectPart(part, !part.selected, false);
                    };
                    mi.DropDownItems.Add(miSelect);

                    ToolStripMenuItem miDelete = new ToolStripMenuItem("Delete");
                    miDelete.Click += delegate(object sender, EventArgs e)
                    {
                        SaveUndo("del");
                        DelPart(part, false, false);
                    };
                    mi.DropDownItems.Add(miDelete);

                    //ToolStripMenuItem miLock = new ToolStripMenuItem(part.locked ? "Unlock" : "Lock");
                    //miLock.Click += delegate(object sender, EventArgs e)
                    //{
                    //    part.locked = !part.locked;
                    //    if (part.locked)
                    //        SelectPart(part, false);
                    //    else
                    //    {
                    //        if ((Control.ModifierKeys & (Keys.Shift | Keys.Control)) == 0)
                    //            ClearSelection();
                    //        SelectPart(part, true);
                    //    }
                    //};
                    //mi.DropDownItems.Add(miLock);

                    if (part.pins.Count == 2)
                    {
                        Pin pin1 = part.pins[0];
                        Pin pin2 = part.pins[part.pins.Count - 1];
                        if (pin1.bendable && pin2.bendable)
                        {
                            PointF pt1 = pin1.pos;
                            PointF pt2 = pin2.pos;
                            PointF pt1o = pin1.org_pos;
                            PointF pt2o = pin2.org_pos;
                            PointF pt1s = Geom.SnapPtToLineSeg(pt1o, pt1, pt2);
                            PointF pt2s = Geom.SnapPtToLineSeg(pt2o, pt1, pt2);
                            if ((Control.ModifierKeys & Keys.Alt) == 0)
                            {
                                float d1 = Geom.Dist(pt1s, pt2s);
                                float d2 = Geom.Dist(pt1, pt2);
                                if (d1 < d2)
                                {
                                    float d = (d2 - d1) / 2;
                                    PointF v = Geom.Sub(pt2, pt1);
                                    pt1s = Geom.Add(pt1, Geom.Mul(v, d / d2));
                                    pt2s = Geom.Add(pt1, Geom.Mul(v, (d + d1) / d2));
                                }
                            }
                            if (!Geom.Eq(pt1s, pt1o) || !Geom.Eq(pt2s, pt2o))
                            {
                                ToolStripMenuItem miStraighten = new ToolStripMenuItem("Straighten");
                                miStraighten.Click += delegate(object sender, EventArgs e)
                                {
                                    SaveUndo("straighten");
                                    float rot = Geom.Angle(pt1, pt2);
                                    PlacePart(part, pt1s.X, pt1s.Y, rot);
                                    pin1.BendablePart.UpdateConnections();
                                    pin2.BendablePart.UpdateConnections();
                                };
                                mi.DropDownItems.Add(miStraighten);
                            }
                        }
                    }

                    //if (part.type == "Component")
                    //{
                    //    ToolStripMenuItem miShowLabel = new ToolStripMenuItem(part.show_label ? "Hide label" : "Show label");
                    //    miShowLabel.Click += delegate(object sender, EventArgs e)
                    //    {
                    //        part.show_label = !part.show_label;
                    //    };
                    //    mi.DropDownItems.Add(miShowLabel);
                    //}

                    ToolStripMenuItem miFront = new ToolStripMenuItem("Bring to front");
                    miFront.Click += delegate(object sender, EventArgs e)
                    {
                        doc.PlacedParts.Remove(part);
                        int idx = 0;
                        foreach (Part p in doc.PlacedParts)
                        {
                            if (p.z_order > part.z_order)
                                break;
                            ++idx;
                        }
                        if (idx < doc.PlacedParts.Count)
                            doc.PlacedParts.Insert(idx, part);
                        else
                            doc.PlacedParts.Add(part);
                    };
                    mi.DropDownItems.Add(miFront);

                    ToolStripMenuItem miBack = new ToolStripMenuItem("Send to back");
                    miBack.Click += delegate(object sender, EventArgs e)
                    {
                        doc.PlacedParts.Remove(part);
                        int idx = 0;
                        foreach (Part p in doc.PlacedParts)
                        {
                            if (p.z_order >= part.z_order)
                                break;
                            ++idx;
                        }
                        if (idx < doc.PlacedParts.Count)
                            doc.PlacedParts.Insert(idx, part);
                        else
                            doc.PlacedParts.Add(part);
                    };
                    mi.DropDownItems.Add(miBack);

                    menu.Items.Add(mi);
                }
            }
            menu.Show(panel, ptc);
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (panel != null)
                panel.Invalidate();

            if (e.KeyCode == Keys.Escape)
            {
                if (panel == null) return;
                if (DraggedParts.Count > 0 || DraggedConnections.Count > 0)
                    CancelDrag();
                else if (doc.SelectedParts.Count > 0)
                    ClearSelection();
                panel.Focus();
                return;
            }

            if (e.KeyCode == Keys.Space)
            {
                bSpaceDown = true;
                if (panel == null) return;
                if (bLBDown && !bRBDown)
                {
                    ptDown = panel.PointToClient(Control.MousePosition);
                    ofsDown = doc.offset;
                }
                return;
            }
        }

        private void MainForm_KeyUp(object sender, KeyEventArgs e)
        {
            if (panel != null)
                panel.Invalidate();

            if (e.KeyCode == Keys.Space)
            {
                bSpaceDown = false;
            }
        }

        private void menuDelete_Click(object sender, EventArgs e)
        {
            if (panel == null) return;
            if (DraggedParts.Count > 0 || DraggedConnections.Count > 0) return;
            Del(false);
        }

        private void menuDeleteWithConnections_Click(object sender, EventArgs e)
        {
            if (panel == null) return;
            if (DraggedParts.Count > 0 || DraggedConnections.Count > 0) return;
            Del(true);
        }

        private void menuSelectAll_Click(object sender, EventArgs e)
        {
            if (panel == null) return;
            if (DraggedParts.Count > 0 || DraggedConnections.Count > 0) return;
            SuspendProperties();
            foreach (Part part in doc.PlacedParts)
            {
                if (!part.locked)
                    SelectPart(part, true, false);
            }
            ResumeProperties();
        }

        private void menuRotateReset_Click(object sender, EventArgs e)
        {
            if (panel == null) return;
            if (doc.SelectedParts.Count == 0) return;
            if (DraggedParts.Count > 0 || DraggedConnections.Count > 0) return;
            SaveUndo("rotate reset");
            PointF ptCenter = GetSelectionCenter();
            foreach (Part part in doc.SelectedParts)
            {
                if (part.type == "Connection") continue;
                RotatePart(part, 0, ptCenter);
            }
            foreach (Part part in doc.SelectedParts)
            {
                if (part.type != "Connection") continue;
                RotatePart(part, 0, ptCenter);
            }
            DetectDirectContacts();
        }

        private void menuRotateLeft_Click(object sender, EventArgs e)
        {
            if (panel == null) return;
            if (doc.SelectedParts.Count == 0) return;
            if (DraggedParts.Count > 0 || DraggedConnections.Count > 0) return;
            SaveUndo("rotate left");
            PointF ptCenter = GetSelectionCenter();
            foreach (Part part in doc.SelectedParts)
            {
                if (part.type == "Connection") continue;
                RotatePart(part, -90, ptCenter);
            }
            foreach (Part part in doc.SelectedParts)
            {
                if (part.type != "Connection") continue;
                RotatePart(part, -90, ptCenter);
            }
            DetectDirectContacts();
        }

        private void menuRotateRight_Click(object sender, EventArgs e)
        {
            if (panel == null) return;
            if (doc.SelectedParts.Count == 0) return;
            if (DraggedParts.Count > 0 || DraggedConnections.Count > 0) return;
            SaveUndo("rotate right");
            PointF ptCenter = GetSelectionCenter();
            foreach (Part part in doc.SelectedParts)
            {
                if (part.type == "Connection") continue;
                RotatePart(part, 90, ptCenter);
            }
            foreach (Part part in doc.SelectedParts)
            {
                if (part.type != "Connection") continue;
                RotatePart(part, 90, ptCenter);
            }
            DetectDirectContacts();
        }

        private void menuRotate180_Click(object sender, EventArgs e)
        {
            if (panel == null) return;
            if (doc.SelectedParts.Count == 0) return;
            if (DraggedParts.Count > 0 || DraggedConnections.Count > 0) return;
            SaveUndo("rotate 180");
            PointF ptCenter = GetSelectionCenter();
            foreach (Part part in doc.SelectedParts)
            {
                if (part.type == "Connection") continue;
                RotatePart(part, 180, ptCenter);
            }
            foreach (Part part in doc.SelectedParts)
            {
                if (part.type != "Connection") continue;
                RotatePart(part, 180, ptCenter);
            }
            DetectDirectContacts();
        }

        private void menuCut_Click(object sender, EventArgs e)
        {
            if (panel == null) return;
            if (DraggedParts.Count > 0 || DraggedConnections.Count > 0) return;
            Cut();
        }

        private void menuCopy_Click(object sender, EventArgs e)
        {
            if (panel == null) return;
            //if (DraggedParts.Count > 0 || DraggedConnections.Count > 0) return;
            Copy();
        }

        private void menuPaste_Click(object sender, EventArgs e)
        {
            if (panel == null) return;
            if (DraggedParts.Count > 0 || DraggedConnections.Count > 0) return;
            Paste();
        }


        private void menuZoomReset_Click(object sender, EventArgs e)
        {
            doc.offset = new PointF(0, 0);
            doc.scale = 10;
            panel.Invalidate();
        }

        private void menuZoomFit_Click(object sender, EventArgs e)
        {
            if (panel == null) return;
            AABB aabb;
            if (doc.SelectedParts.Count > 0)
                aabb = CalcAABB(doc.SelectedParts);
            else if (doc.PlacedParts.Count > 0)
                aabb = CalcAABB(doc.PlacedParts);
            else
                aabb = new AABB();
            if (aabb.Width <= 0 || aabb.Height <= 0)
            {
                doc.offset = new PointF(0, 0);
                doc.scale = 10;
            }
            else
            {
                float g = 10;
                doc.scale = Math.Min((panel.Width - g) / aabb.Width, (panel.Height - g) / aabb.Height);
                doc.offset = new PointF(panel.Width / 2 - (aabb.Min.X + aabb.Width / 2) * doc.scale, panel.Height / 2 - (aabb.Min.Y + aabb.Height / 2) * doc.scale);
            }
            panel.Invalidate();
        }

        private void menuZoomIn_Click(object sender, EventArgs e)
        {
            if (panel == null) return;
            Zoom(1.2f, new PointF(panel.Width / 2, panel.Height / 2));
           panel.Invalidate();
        }

        private void menuZoomOut_Click(object sender, EventArgs e)
        {
            if (panel == null) return;
            Zoom(1.0f / 1.2f, new PointF(panel.Width / 2, panel.Height / 2));
            panel.Invalidate();
        }        

        private void menuPartLabelsOff_Click(object sender, EventArgs e)
        {
            show_labels = 0;
            menuPartLabelsOff.Checked = true;
            menuPartLabelsHovered.Checked = false;
            menuPartLabelsDefault.Checked = false;
            menuPartLabelsOn.Checked = false;
            if (panel != null)
                panel.Invalidate();
        }

        private void menuPartLabelsHovered_Click(object sender, EventArgs e)
        {
            show_labels = 1;
            menuPartLabelsOff.Checked = false;
            menuPartLabelsHovered.Checked = true;
            menuPartLabelsDefault.Checked = false;
            menuPartLabelsOn.Checked = false;
            if (panel != null)
                panel.Invalidate();
        }

        private void menuPartLabelsDefault_Click(object sender, EventArgs e)
        {
            show_labels = 2;
            menuPartLabelsOff.Checked = false;
            menuPartLabelsHovered.Checked = false;
            menuPartLabelsDefault.Checked = true;
            menuPartLabelsOn.Checked = false;
            if (panel != null)
                panel.Invalidate();
        }

        private void menuPartLabelsOn_Click(object sender, EventArgs e)
        {
            show_labels = 3;
            menuPartLabelsOff.Checked = false;
            menuPartLabelsHovered.Checked = false;
            menuPartLabelsDefault.Checked = false;
            menuPartLabelsOn.Checked = true;
            if (panel != null)
                panel.Invalidate();
        }

        private void EnableMenu(ToolStripMenuItem menu, bool enable)
        {
            menu.Enabled = enable;
            foreach (var item in menu.DropDown.Items)
            {
                ToolStripMenuItem mi = item as ToolStripMenuItem;
                if (mi != null)
                    EnableMenu(mi, enable);
            }
        }

        private void OnPanelFocused(object sender, EventArgs e)
        {
            EnableMenu(menuEdit, true);
            EnableMenu(menuView, true);
        }

        private void OnPanelLostFocus(object sender, EventArgs e)
        {
            EnableMenu(menuEdit, false);
            EnableMenu(menuView, false);
        }

        private void FilesTab_SelectedIndexChanged(object sender, EventArgs e)
        {
            CancelDrag();
            int idx = FilesTab.SelectedIndex;
            panel = FilesTab.TabPages[idx];
            if (panel.Tag == null) // the console window
            {
                panel = null;
                input.Focus();
                UpdateProperties();
                return;
            }
            doc = (Doc)panel.Tag;
            panel.Focus();
            UpdateProperties();
            panel.Invalidate();

            Program.lua.Call("SelectedDocumentChanged()");
        }

        private void FilesTab_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button != System.Windows.Forms.MouseButtons.Middle && e.Button != System.Windows.Forms.MouseButtons.Right)
                return;
            int idx = -1;
            for (int i = 0; i < FilesTab.TabCount; i++)
            {
                Rectangle r = FilesTab.GetTabRect(i);
                if (r.Contains(e.Location))
                {
                    idx = i;
                    break;
                }
            }
            if (idx < 0)
                return;
            FilesTab.SelectedIndex = idx;
            if (panel != null)
                menuClose_Click(sender, e);
        }


        private void menuNew_Click(object sender, EventArgs e)
        {
            New();
        }

        private void menuOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.InitialDirectory = System.IO.Directory.GetCurrentDirectory();
            dlg.DefaultExt = ".ptb";
            dlg.Filter = "Protoboard files (*.ptb)|*.ptb|All files (*.*)|*.*";
            var res = dlg.ShowDialog();
            if (res != System.Windows.Forms.DialogResult.OK) return;
            SuspendProperties();
            New();
            doc.filename = dlg.FileName;
            doc.name = System.IO.Path.GetFileNameWithoutExtension(doc.filename);
            Program.lua.Call("dofile('" + doc.filename.Replace('\\', '/') + "')");
            UpdatePanelTitle();
            DetectDirectContacts();
            ResumeProperties();
        }

        private void menuSave_Click(object sender, EventArgs e)
        {
            if (panel == null) return;

            if (doc.filename == "")
            {
                SaveFileDialog dlg = new SaveFileDialog();
                dlg.InitialDirectory = System.IO.Directory.GetCurrentDirectory();
                dlg.DefaultExt = ".ptb";
                dlg.Filter = "Protoboard files (*.ptb)|*.ptb|All files (*.*)|*.*";
                var res = dlg.ShowDialog();
                if (res != System.Windows.Forms.DialogResult.OK) return;
                doc.filename = dlg.FileName;
                doc.name = System.IO.Path.GetFileNameWithoutExtension(doc.filename);
            }

            for (int i = 0; i < doc.PlacedParts.Count; ++i) doc.PlacedParts[i].save_index = i + 1;
            string s = "cs:View(" + doc.offset.X.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture) + ", " + doc.offset.Y.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture) + ", " + doc.scale.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture) + ")\r\n";
            foreach (Part part in doc.PlacedParts)
            {
                if (part.type == "Connection") continue;
                s += part.GetSaveString() + "\r\n";
            }
            foreach (Part part in doc.PlacedParts)
            {
                if (part.type != "Connection") continue;
                s += part.GetSaveString() + "\r\n";
            }
            try
            {
                System.IO.File.WriteAllText(doc.filename, s);
                doc.nSavedUndoIdx = doc.nUndoIdx;
            }
            catch (Exception)
            {
                MessageBox.Show("Save failed", "Error");
            }
            UpdatePanelTitle();
        }

        private void menuSaveAs_Click(object sender, EventArgs e)
        {
            if (panel == null) return;

            SaveFileDialog dlg = new SaveFileDialog();
            dlg.InitialDirectory = System.IO.Directory.GetCurrentDirectory();
            dlg.DefaultExt = ".ptb";
            dlg.Filter = "Protoboard files (*.ptb)|*.ptb|All files (*.*)|*.*";
            var res = dlg.ShowDialog();
            if (res != System.Windows.Forms.DialogResult.OK) return;
            doc.filename = dlg.FileName;
            doc.name = System.IO.Path.GetFileNameWithoutExtension(doc.filename);
            menuSave_Click(sender, e);
        }

        private void menuSaveAll_Click(object sender, EventArgs e)
        {
            int cur_idx = FilesTab.SelectedIndex;
            for (int idx = 0; idx < FilesTab.TabPages.Count; ++idx)
            {
                Panel p = FilesTab.TabPages[idx];
                Doc d = (Doc)p.Tag;
                if (d == null) continue;
                if (!d.modified) continue;
                FilesTab.SelectedIndex = idx;
                menuSave_Click(sender, e);
                if (d.modified) return;
            }
            FilesTab.SelectedIndex = cur_idx;
        }

        private void menuRevert_Click(object sender, EventArgs e)
        {

        }

        private void menuClose_Click(object sender, EventArgs e)
        {
            if (panel == null) return;
            if (doc.modified)
            {
                var res = MessageBox.Show(doc.name + " is modified. Save before closing?", "Close", MessageBoxButtons.YesNoCancel);
                if (res == System.Windows.Forms.DialogResult.Cancel) return;
                if (res == System.Windows.Forms.DialogResult.Yes)
                {
                    menuSave_Click(sender, e);
                    if (doc.modified)
                        return;
                }
            }

            int idx = FilesTab.SelectedIndex;
            if (idx > 1)
                FilesTab.SelectedIndex = idx - 1;
            else if (idx + 1 < FilesTab.TabPages.Count)
                FilesTab.SelectedIndex = idx + 1;
            FilesTab.TabPages.RemoveAt(idx);
        }

        private void menuPreferences_Click(object sender, EventArgs e)
        {

        }

        private void menuExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void menuUndo_Click(object sender, EventArgs e)
        {
            if (panel == null) return;
            Undo();
        }

        private void menuRedo_Click(object sender, EventArgs e)
        {
            if (panel == null) return;
            Redo();
        }  

        public void Log(string s)
        {
            output.AppendText(s);
        }

        bool bAllowInputHistory = true;
        bool bIgnoreInputTextChaned = false;
        bool bIgnoreInputKeyUp = false;
        List<string> InputHistory = new List<string>();
        int InputHistoryIndex = 0;

        private void input_KeyUp(object sender, KeyEventArgs e)
        {
            if (bIgnoreInputKeyUp)
            {
                e.Handled = true;
                bIgnoreInputKeyUp = false;
                if (String.IsNullOrWhiteSpace(input.Text))
                {
                    bIgnoreInputTextChaned = true;
                    input.Text = "";
                    bIgnoreInputTextChaned = false;
                }
                return;
            }
        }

         private void input_KeyDown(object sender, KeyEventArgs e)
         {
            bool blank = String.IsNullOrWhiteSpace(input.Text);
            if (e.KeyCode == Keys.Enter && ModifierKeys == 0)
            {
                e.Handled = true;
                bIgnoreInputKeyUp = true;
                if (blank)
                {
                    bIgnoreInputTextChaned = true;
                    input.Text = "";
                    bIgnoreInputTextChaned = false;
                    return;
                }
                string cmd = input.Text;
                bIgnoreInputTextChaned = true;
                input.Text = "";
                bIgnoreInputTextChaned = false;
                InputHistory.Remove(cmd);
                InputHistory.Add(cmd);
                InputHistoryIndex = InputHistory.Count;
                Log(">> " + cmd + "\r\n");
                Program.lua.Call(cmd);
                return;
            }
            if (e.KeyCode == Keys.Up && (ModifierKeys == Keys.Alt || blank || bAllowInputHistory))
            {
                e.Handled = true;
                bIgnoreInputKeyUp = true;
                if (InputHistoryIndex <= 0) return;
                InputHistoryIndex--;
                bIgnoreInputTextChaned = true;
                input.Text = InputHistory[InputHistoryIndex];
                bIgnoreInputTextChaned = false;
                input.SelectAll();
                bAllowInputHistory = true;
                return;
            }
            if (e.KeyCode == Keys.Down && (ModifierKeys == Keys.Alt || blank || bAllowInputHistory))
            {
                e.Handled = true;
                bIgnoreInputKeyUp = true;
                if (InputHistoryIndex + 1 >= InputHistory.Count) return;
                InputHistoryIndex++;
                bIgnoreInputTextChaned = true;
                input.Text = InputHistory[InputHistoryIndex];
                bIgnoreInputTextChaned = false;
                input.SelectAll();
                bAllowInputHistory = true;
                return;
            }
        }

        private void input_TextChanged(object sender, EventArgs e)
        {
            if (bIgnoreInputTextChaned) return;
            bAllowInputHistory = false;
            InputHistoryIndex = InputHistory.Count;
        }

        private void splitContainer1_Panel2_Layout(object sender, LayoutEventArgs e)
        {
            Panel p = splitContainer1.Panel2;
            searchText.Width = p.Width;
            searchText.Left = p.Left;
        }

        private void filterPartsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            searchText.Focus();
        }

        private void GridSpacing_TextChanged(object sender, EventArgs e)
        {
            string s = GridSpacing.Text;
            try
            {
                float f = Single.Parse(s, System.Globalization.CultureInfo.InvariantCulture);
                if (f >= 0)
                {
                    grid_spacing = f;
                    highlight_snap_dist = f / 2;
                    placement_snap_dist = f / 2;
                }
            }
            catch (Exception) { }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            for (int idx = 0; idx < FilesTab.TabPages.Count; ++idx)
            {
                Panel p = FilesTab.TabPages[idx];
                Doc d = (Doc)p.Tag;
                if (d == null) continue;
                if (!d.modified) continue;
                FilesTab.SelectedIndex = idx;
                var res = MessageBox.Show(d.name + " is modified. Save before quit?", "Quit", MessageBoxButtons.YesNoCancel);
                if (res == System.Windows.Forms.DialogResult.Cancel)
                {
                    e.Cancel = true;
                    return;
                }
                if (res == System.Windows.Forms.DialogResult.No)
                {
                    if ((Control.ModifierKeys & Keys.Shift) != 0)
                        return;
                    continue;
                }
                menuSave_Click(sender, e);
                if (d.modified)
                {
                    e.Cancel = true;
                    return;
                }
            }
        }

        private void DrawColorItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0) return;
            ComboBox cb = sender as ComboBox;
            NamedColor clr = (NamedColor) cb.Items[e.Index];
            e.DrawBackground();
            Rectangle rcClr = new Rectangle(e.Bounds.Left, e.Bounds.Top + 1, e.Bounds.Height - 2, e.Bounds.Height - 2);
            Rectangle rcText = new Rectangle(e.Bounds.Left + e.Bounds.Height + 4, e.Bounds.Top + 1, e.Bounds.Width - e.Bounds.Height - 4, e.Bounds.Height - 2);
            e.Graphics.FillRectangle(new SolidBrush(clr.color), rcClr);
            e.Graphics.DrawString(clr.name, cb.Font, Brushes.Black, rcText);
            e.DrawFocusRectangle();
        }

        public bool ignore_prop_change = false;

        public class PropVal
        {
            public string val = "";
            public int cnt = 0;
            public bool all_equal = true;

            public void Add(string v)
            {
                if (v == null) v = "";
                ++cnt;
                if (cnt == 1)
                {
                    val = v;
                    return;
                }
                if (v != val)
                {
                    all_equal = false;
                    val = "";
                    return;
                }
            }

            public string Clr2String(Color clr)
            {
                return clr.R + "," + clr.G + "," + clr.B;
            }

            public Color String2Clr(string s)
            {
                string[] values = s.Split(',');
                int r = Convert.ToInt32(values[0]);
                int g = Convert.ToInt32(values[1]);
                int b = Convert.ToInt32(values[2]);
                return Color.FromArgb(r, g, b);
            }

            private int ColorDist(Color clr1, Color clr2)
            {
                int dr = clr1.R - clr2.R;
                int dg = clr1.G - clr2.G;
                int db = clr1.B - clr2.B;
                return dr * dr + dg * dg + db * db;
            }

            public void Add(bool v) { Add(v ? "true" : "false"); }
            public void Add(float v) { Add(v.ToString(System.Globalization.CultureInfo.InvariantCulture)); }

            public void Add(Color clr) { Add(Clr2String(clr)); }

            public void Apply(Label label, TextBox text)
            {
                Program.mainForm.ignore_prop_change = true;
                if (!all_equal)
                    label.ForeColor = Color.Red;
                else
                    label.ForeColor = Color.Black;
                text.Text = val;
                Program.mainForm.ignore_prop_change = false;
            }

            public void Apply(CheckBox cb)
            {
                Program.mainForm.ignore_prop_change = true;
                if (!all_equal)
                {
                    cb.ForeColor = Color.Red;
                    cb.Checked = true;
                }
                else
                {
                    cb.ForeColor = Color.Black;
                    cb.Checked = (val == "true");
                }
                Program.mainForm.ignore_prop_change = false;
            }

            public void Apply(Label label, ComboBox text)
            {
                Program.mainForm.ignore_prop_change = true;
                if (!all_equal)
                    label.ForeColor = Color.Red;
                else
                    label.ForeColor = Color.Black;
                text.Text = val;
                Program.mainForm.ignore_prop_change = false;
            }

            public void ApplyColor(Label label, ComboBox cb)
            {
                Program.mainForm.ignore_prop_change = true;
                if (!all_equal)
                    label.ForeColor = Color.Red;
                else
                    label.ForeColor = Color.Black;
                if (!String.IsNullOrEmpty(val))
                {
                    Color clr = String2Clr(val);
                    int best = 0;
                    int best_dist = 64 * 64;
                    for (int i = 1; i < cb.Items.Count; ++i)
                    {
                        NamedColor nc = (NamedColor)cb.Items[i];
                        int dist = ColorDist(clr, nc.color);
                        if (dist >= best_dist) continue;
                        best = i;
                        best_dist = dist;
                    }
                    cb.SelectedIndex = best;
                }                   
                Program.mainForm.ignore_prop_change = false;
            }
        };

        public int properties_suspended = 0;

        public void SuspendProperties() { ++properties_suspended; }
        public void ResumeProperties() { if (--properties_suspended <= 0) UpdateProperties(); }

        public void UpdateProperties()
        {
            if (properties_suspended > 0) return;

            var props = splitContainer2.Panel2.Controls;

            props.Remove(panelPartProperties);
            props.Remove(panelPartValue);
            props.Remove(panelTrackProperties);
            props.Remove(panelWireProperties);
            props.Remove(panelViaProperties);
            props.Remove(panelPinProperties);

            if (panel == null) return;

            List<Part> all = new List<Part>();

            List<Part> parts = new List<Part>();
            List<Part> components = new List<Part>();
            List<Part> boards = new List<Part>();
            
            List<Connection> tracks = new List<Connection>();
            
            List<Connection> all_wires = new List<Connection>();
            List<Connection> wires = new List<Connection>();
            List<Connection> bendable_legs = new List<Connection>();
            
            List<Via> vias = new List<Via>();
            List<Pin> pins = new List<Pin>();

            PropVal vPartLabel = new PropVal();
            PropVal vPartShowLabel = new PropVal();
            PropVal vPartLocked = new PropVal();
            PropVal vPartValue = new PropVal();
            PropVal vPartValueUnit = new PropVal();
            PropVal vPartValueName = new PropVal();
            PropVal vPartValues = new PropVal();
            PropVal vPartValueUnits = new PropVal();
            PropVal vTrackColor = new PropVal();
            PropVal vTrackWidth = new PropVal();
            PropVal vWireColor = new PropVal();
            PropVal vViaRingColor = new PropVal();
            PropVal vViaConnectionColor = new PropVal();
            PropVal vViaForm = new PropVal();
            PropVal vViaHoleRadius = new PropVal();
            PropVal vViaRingRadius = new PropVal();

            List<Part> lst = (doc.SelectedParts.Count > 0) ? doc.SelectedParts : doc.PlacedParts;
            foreach (Part part in lst)
            {
                all.Add(part);

                if (part.type == "Component")
                {
                    parts.Add(part);
                    components.Add(part);
                }
                else if (part.type == "Board")
                {
                    parts.Add(part);
                    boards.Add(part);
                }
            }

            if (boards.Count > 0 && boards.Count < parts.Count && doc.SelectedParts.Count == 0)
            {
                parts = components;
                foreach (Part part in boards) all.Remove(part);
            }

            foreach (Part part in all)
            {
                foreach (Connection c in part.own_connections)
                {
                    if (c.type == "Track")
                        tracks.Add(c);
                    else if (c.type == "Wire")
                    {
                        all_wires.Add(c);
                        if (part.bendable_pin != null) bendable_legs.Add(c);
                        else wires.Add(c);
                    }
                }
                foreach (Via via in part.vias)
                {
                    vias.Add(via);
                }

                foreach (Pin pin in part.pins)
                    pins.Add(pin);
            }

            foreach (Part part in parts)
            {
                vPartLabel.Add(part.label);
                vPartShowLabel.Add(part.show_label);
                vPartLocked.Add(part.locked);
                vPartValue.Add(part.value);
                vPartValueUnit.Add(part.value_unit);
                vPartValueName.Add(part.value_name);
                vPartValues.Add(part.values);
                vPartValueUnits.Add(part.value_units);
            }
            
            foreach (Connection c in tracks)
            {
                vTrackColor.Add(c.color);
                vTrackWidth.Add(c.width);
            }
            
            if (bendable_legs.Count > 0 && wires.Count == 0 && parts.Count == 0)
                wires = bendable_legs;
            foreach (Connection c in wires)
            {
                vWireColor.Add(c.color);
            }

            foreach (Via via in vias)
            {
                vViaRingColor.Add(via.color_ring);
                // todo: connection color
                vViaForm.Add(via.form);
                vViaHoleRadius.Add(via.hole_radius);
                vViaRingRadius.Add(via.ring_radius);
            }

            List<Panel> panels = new List<Panel>();

            if (parts.Count > 0)
            {
                panelPartProperties.Tag = parts;
                panels.Add(panelPartProperties);
                if (parts.Count == 1)
                    labelPartName.Text = parts[0].ToString();
                else
                    labelPartName.Text = parts.Count + " parts";
                vPartLabel.Apply(labelPartLabel, textPartLabel);
                vPartShowLabel.Apply(cbShowPartLabel);
                vPartLocked.Apply(cbPartLocked);
                if (vPartValueName.cnt > 0 && vPartValueName.all_equal && !String.IsNullOrEmpty(vPartValueName.val))
                {
                    panelPartValue.Tag = parts;
                    panels.Add(panelPartValue);
                    
                    ignore_prop_change = true;

                    labelPartValue.Text = vPartValueName.val + ":";
                    
                    if (vPartValue.all_equal)
                        cbPartValue.Text = vPartValue.val;
                    else
                        cbPartValue.Text = "";
                    
                    if (vPartValueUnit.all_equal)
                        cbPartValueUnit.Text = vPartValueUnit.val;
                    else
                        cbPartValueUnit.Text = "";

                    if (vPartValue.all_equal && vPartValueUnit.all_equal)
                        labelPartValue.ForeColor = Color.Black;
                    else
                        labelPartValue.ForeColor = Color.Red;

                    string[] values = vPartValues.val.Split('|');
                    cbPartValue.Items.Clear();
                    foreach (string val in values) cbPartValue.Items.Add(val);

                    string[] units = vPartValueUnits.val.Split('|');
                    cbPartValueUnit.Items.Clear();
                    foreach (string val in units) cbPartValueUnit.Items.Add(val);

                    ignore_prop_change = false;
                }
            }

            if (tracks.Count > 0)
            {
                panelTrackProperties.Tag = tracks;
                panels.Add(panelTrackProperties);
                if (tracks.Count == 1)
                    labelTrackName.Text = tracks[0].part.ToString();
                else
                    labelTrackName.Text = tracks.Count + " tracks";

                cbTrackColor.Items.Clear();
                cbTrackColor.Items.Add(new NamedColor("Auto", def_track_color));
                foreach (NamedColor clr in ConnectionColors) cbTrackColor.Items.Add(clr);
                vTrackColor.ApplyColor(labelTrackColor, cbTrackColor);

                vTrackWidth.Apply(labelTrackWidth, textTrackWidth);
            }

            if (wires.Count > 0)
            {
                panelWireProperties.Tag = wires;
                panels.Add(panelWireProperties);
                if (wires.Count == 1)
                    labelWireName.Text = wires[0].part.ToString();
                else
                    labelWireName.Text = wires.Count + " wires";

                cbWireColor.Items.Clear();
                cbWireColor.Items.Add(new NamedColor("Auto", def_wire_color));
                foreach (NamedColor clr in ConnectionColors) cbWireColor.Items.Add(clr);
                vWireColor.ApplyColor(labelWireColor, cbWireColor);
            }

            if (vias.Count > 0)
            {
                panelViaProperties.Tag = vias;
                panels.Add(panelViaProperties);
                if (vias.Count == 1)
                    labelViaName.Text = vias[0].part.ToString();
                else
                    labelViaName.Text = vias.Count + " vias";

                cbViaConnectionColor.Items.Clear();
                cbViaConnectionColor.Items.Add(new NamedColor("Auto", def_track_color));
                foreach (NamedColor clr in ConnectionColors) cbViaConnectionColor.Items.Add(clr);

                vViaForm.Apply(labelViaForm, cbViaForm);
                vViaHoleRadius.Apply(labelViaHoleRadius, textViaHoleRadius);
                vViaRingRadius.Apply(labelViaRingRadius, textViaRingRadius);
            }

            if (parts.Count == 1 && pins.Count > 0)
            {
                panelPinProperties.Tag = parts[0];
                panels.Add(panelPinProperties);
                lbPins.Items.Clear();
                for (int i = 0; i < parts[0].pins.Count; ++i)
                {
                    Pin pin = parts[0].pins[i];
                    string s = (i + 1) + ": " + pin.name;
                    lbPins.Items.Add(s);
                }
            }

            for (int i = panels.Count - 1; i >= 0; --i)
                props.Add(panels[i]);
        }

        private void textPartLabel_TextChanged(object sender, EventArgs e)
        {
            if (ignore_prop_change) return;
            SaveUndo("property", sender);

            List<Part> lst = (List<Part>)panelPartProperties.Tag;
            foreach (Part part in lst)
            {
                if (part.type == "Component" || part.type == "Board")
                {
                    part.label = textPartLabel.Text;
                }
            }
            labelPartLabel.ForeColor = Color.Black;
            panel.Invalidate();
        }

        private void cbShowLabel_CheckedChanged(object sender, EventArgs e)
        {
            if (ignore_prop_change) return;
            SaveUndo("property", sender);

            bool val = cbShowPartLabel.Checked;
            List<Part> lst = (List<Part>)panelPartProperties.Tag;
            foreach (Part part in lst)
            {
                if (part.type == "Component" || part.type == "Board")
                {
                    part.show_label = val;
                }
            }
            cbShowPartLabel.ForeColor = Color.Black;
            panel.Invalidate();
        }

        private void cbLocked_CheckedChanged(object sender, EventArgs e)
        {
            if (ignore_prop_change) return;
            SaveUndo("property", sender);

            bool val = cbPartLocked.Checked;
            List<Part> lst = (List<Part>)panelPartProperties.Tag;
            foreach (Part part in lst)
            {
                if (part.type == "Component" || part.type == "Board")
                {
                    part.locked = val;
                }
            }
            cbPartLocked.ForeColor = Color.Black;
            panel.Invalidate();
        }

        private void cbPartValue_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ignore_prop_change) return;
            SaveUndo("property", sender);
            
            string[] vals = cbPartValue.Text.Split(' ');
            int cnt = vals.Length;
            if (cnt <= 1)
            {
                cbPartValue_TextUpdate(sender, e);
                return;
            }
            this.BeginInvoke((MethodInvoker)delegate
            {
                cbPartValue.Text = vals[0];
                cbPartValueUnit.Text = vals[1];
                cbPartValue_TextUpdate(sender, e);
            });
        }

        private void cbPartValue_TextUpdate(object sender, EventArgs e)
        {
            if (ignore_prop_change) return;
            SaveUndo("property", sender);

            if (cbPartValue.Text == "" || cbPartValueUnit.Text == "")
                return;
            float val;
            string unit;
            try
            {
                val = Single.Parse(cbPartValue.Text, System.Globalization.CultureInfo.InvariantCulture);
                unit = cbPartValueUnit.Text;
            }
            catch (Exception)
            {
                return;
            }
            List<Part> lst = (List<Part>)panelPartValue.Tag;
            foreach (Part part in lst)
            {
                if (part.type == "Component" || part.type == "Board")
                {
                    part.value = val;
                    part.value_unit = unit;
                }
            }
            labelPartValue.ForeColor = Color.Black;
            panel.Invalidate();
        }

        private void cbPartValueUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbPartValue_TextUpdate(sender, e);
        }

        private void cbPartValueUnit_TextUpdate(object sender, EventArgs e)
        {
            cbPartValue_TextUpdate(sender, e);
        }

        private void cbTrackColor_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ignore_prop_change) return;
            SaveUndo("property", sender);
            NamedColor val = (NamedColor)cbTrackColor.Items[cbTrackColor.SelectedIndex];
            List<Connection> lst = (List<Connection>)panelTrackProperties.Tag;
            foreach (Connection c in lst)
                c.color = val.color;
            labelTrackColor.ForeColor = Color.Black;
            panel.Invalidate();
        }

        private void textTrackWidth_TextChanged(object sender, EventArgs e)
        {
            if (ignore_prop_change) return;
            SaveUndo("property", sender);

            float val;
            try
            {
                val = Single.Parse(textTrackWidth.Text, System.Globalization.CultureInfo.InvariantCulture);
            }
            catch (Exception) 
            {
                return;
            }
            List<Connection> lst = (List<Connection>)panelTrackProperties.Tag;
            foreach (Connection c in lst)
                c.width = val;
            labelTrackWidth.ForeColor = Color.Black;
            panel.Invalidate();
        }

        private void cbWireColor_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ignore_prop_change) return;
            SaveUndo("property", sender);
            NamedColor val = (NamedColor)cbWireColor.Items[cbWireColor.SelectedIndex];
            List<Connection> lst = (List<Connection>)panelWireProperties.Tag;
            foreach (Connection c in lst)
                c.color = val.color;
            labelWireColor.ForeColor = Color.Black;
            panel.Invalidate();
        }

        private void cbViaRingColor_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ignore_prop_change) return;
            SaveUndo("property", sender);

        }

        private void cbViaConnectionColor_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ignore_prop_change) return;
            SaveUndo("property", sender);

        }

        private void cbViaForm_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ignore_prop_change) return;
            SaveUndo("property", sender);

            string val = cbViaForm.Text;
            List<Via> lst = (List<Via>)panelViaProperties.Tag;
            foreach (Via via in lst)
                via.form = val;
            labelViaForm.ForeColor = Color.Black;
            panel.Invalidate();
        }

        private void textViaHoleRadius_TextChanged(object sender, EventArgs e)
        {
            if (ignore_prop_change) return;
            SaveUndo("property", sender);

            float val;
            try
            {
                val = Single.Parse(textViaHoleRadius.Text, System.Globalization.CultureInfo.InvariantCulture);
            }
            catch (Exception)
            {
                return;
            }
            List<Via> lst = (List<Via>)panelViaProperties.Tag;
            foreach (Via via in lst)
                via.hole_radius = val;
            labelViaHoleRadius.ForeColor = Color.Black;
            panel.Invalidate();
        }

        private void textViaRingRadius_TextChanged(object sender, EventArgs e)
        {
            if (ignore_prop_change) return;
            SaveUndo("property", sender);

            float val;
            try
            {
                val = Single.Parse(textViaRingRadius.Text, System.Globalization.CultureInfo.InvariantCulture);
            }
            catch (Exception)
            {
                return;
            }
            List<Via> lst = (List<Via>)panelViaProperties.Tag;
            foreach (Via via in lst)
                via.ring_radius = val;
            labelViaRingRadius.ForeColor = Color.Black;
            panel.Invalidate();
        }

    }
}
