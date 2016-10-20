using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using NLua;

namespace ProtoBoard
{
    public partial class ConnectionProperties : Form
    {
        public Part part;
        List<String> ColorNames = new List<string>();
        List<Color> ColorValues = new List<Color>();

        public ConnectionProperties(Part _part)
        {
            part = _part;
            InitializeComponent();
        }

        private int ColorDist(Color clr1, Color clr2)
        {
            int dr = clr1.R - clr2.R;
            int dg = clr1.G - clr2.G;
            int db = clr1.B - clr2.B;
            return dr * dr + dg * dg + db * db;
        }

        private void ConnectionProperties_Load(object sender, EventArgs e)
        {
            labelName.Text = part.ToString();

            LuaTable tblColors = part.tbl["ConnectionColors"] as LuaTable;
            if (tblColors != null)
            {
                int best_idx = 0;
                int best_dist = 128 * 128;
                foreach (LuaTable tblClr in tblColors.Values)
                {
                    string name = tblClr[1] as string;
                    Color clr = Program.lua.ReadColor(tblClr[2] as LuaTable);

                    int dist = ColorDist(clr, part.color);
                    if (dist < best_dist)
                    {
                        best_idx = ColorNames.Count;
                        best_dist = dist;
                    }

                    ColorNames.Add(name);
                    ColorValues.Add(clr);
                    cbColor.Items.Add(name);
                }
                cbColor.SelectedIndex = best_idx;
            }
        }

        private void cbColor_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0) return;
            string val = ColorNames[e.Index];
            Color clr = (e.Index > 0) ? ColorValues[e.Index] : part.org_color;
            e.DrawBackground();
            Rectangle rcClr = new Rectangle(e.Bounds.Left, e.Bounds.Top + 1, e.Bounds.Height - 2, e.Bounds.Height - 2);
            Rectangle rcText = new Rectangle(e.Bounds.Left + e.Bounds.Height + 4, e.Bounds.Top + 1, e.Bounds.Width - e.Bounds.Height - 4, e.Bounds.Height - 2);
            e.Graphics.FillRectangle(new SolidBrush(clr), rcClr);
            e.Graphics.DrawString(val, cbColor.Font, Brushes.Black, rcText);
            e.DrawFocusRectangle();
        }

        private void cbColor_SelectedIndexChanged(object sender, EventArgs e)
        {
            part.color = (cbColor.SelectedIndex > 0) ? ColorValues[cbColor.SelectedIndex] : part.org_color;
            Program.mainForm.panel.Invalidate();
        }
    }
}
