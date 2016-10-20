using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

//using KeraLua;
using NLua;

namespace ProtoBoard
{
    public class LuaBindings
    {
        public NLua.Lua L;

        public LuaBindings()
        {
            L = new NLua.Lua();
        }

        public void Init()
        {
            L.LoadCLRPackage();
            L["cs"] = this;
            L["frm"] = Program.mainForm;
            
            //L.HookException += new EventHandler<NLua.Event.HookExceptionEventArgs>(OnLuaError);
            
            //Call(@"import('ProtoBoard', 'ProtoBoard')");            
            //Call(@"print = LuaBindings.print");

            L.DoString("function OnLuaError(err) cs:OnLuaError(tostring(err) .. '\\n' .. debug.traceback()) end");

		    Call("dofile('Scripts/core.lua')");
            
            Call("InitParts()");

            Call("SelectedDocumentChanged()");
        }

        //public static void OnLuaError(object sender, EventArgs e)
        //{
        //}

        public Object Call(string expr)
        {
            try
            {
                Object[] res = L.DoString("return xpcall(function()\n  " + expr + "\nend, OnLuaError)");
                if (res == null) return null;
                if (res.Length < 2) return null;
                return res[1];
            }
            catch (NLua.Exceptions.LuaScriptException e)
            {
                print("Lua Exception in '" + e.Source + "': " + e.Message);
            }
            catch (Exception e)
            {
                print("Lua Exception: " + e.Message);
            }
            return null;
        }

        public void OnLuaError(string err)
        {
            print("Lua Error: " + err.Replace("\n", "\r\n"));
            //Program.Error(err, "Lua Error");
        }

		public void print(string s)
        {
            Console.WriteLine(s);
            Program.mainForm.Log(s + "\r\n");
        }

        public Color ReadColor(LuaTable tbl)
        {
            if (tbl == null) return Color.Transparent;
            int r = Convert.ToByte(tbl[1]);
            int g = Convert.ToByte(tbl[2]);
            int b = Convert.ToByte(tbl[3]);
            int a = (tbl[4] != null) ? Convert.ToByte(tbl[4]) : 255;
            return Color.FromArgb(a, r, g, b);
        }

        public void AddPart(LuaTable tbl)
        {
            Part part = new Part(tbl);
            Program.mainForm.AddPart(part);
        }

        public void View(float x, float y, float scale)
        {
            Program.mainForm.doc.offset = new PointF(x, y);
            Program.mainForm.doc.scale = scale;
        }

        public Part PlacePart(LuaTable tbl)
        {
            string name = tbl["name"] as string;
            Part part = Part.Get(name);
            if (part == null) return null;
            if (part.type == "Connection") return null;
            part = new Part(part);
            float x = Convert.ToSingle(tbl["x"]);
            float y = Convert.ToSingle(tbl["y"]);
            float rot = Convert.ToSingle(tbl["rot"]);
            bool locked = Convert.ToBoolean(tbl["locked"]);
            part = Program.mainForm.PlacePart(part, x, y, rot, false);
            if (part != null)
                part.ApplySavedProperties(tbl);
            return part;
        }

        public Part PlaceConnection(LuaTable tbl)
        {
            Part part = Connection.Create(tbl);
            part = Program.mainForm.PlaceConnection(part);
            if (part != null)
                part.ApplySavedProperties(tbl);
            return part;
        }
    };

}