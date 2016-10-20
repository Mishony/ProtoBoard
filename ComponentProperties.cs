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
    public partial class ComponentProperties : Form
    {
        public Part part;

        public ComponentProperties(Part _part)
        {
            part = _part;
            InitializeComponent();            
        }

        private void ComponentProperties_Load(object sender, EventArgs e)
        {
            labelName.Text = part.ToString();
            textLabel.Text = part.label;
            cbLocked.Checked = part.locked;
            cbShowLabel.Checked = part.show_label;

            if (part.value_unit != null)
            {
                LuaTable tblValues = part.tbl["values"] as LuaTable;
                if (tblValues != null)
                {
                    foreach (object val in tblValues.Values)
                    {
                        cbValue.Items.Add(Part.ProcessLabel(Convert.ToString(val)));
                    }
                }

                LuaTable tblUnits = part.tbl["value_units"] as LuaTable;
                if (tblUnits != null)
                {
                    foreach (string val in tblUnits.Values)
                    {
                        cbUnit.Items.Add(Part.ProcessLabel(val));
                    }
                }

                this.BeginInvoke((MethodInvoker)delegate
                {
                    cbUnit.Text = Part.ProcessLabel(part.value_unit);
                    cbValue.Text = part.value.ToString();
                });
            }
            else
            {
                labelValue.Hide();
                cbValue.Hide();
                cbUnit.Hide();
            }
        }

        private void textLabel_TextChanged(object sender, EventArgs e)
        {
            part.label = textLabel.Text;
            Program.mainForm.panel.Invalidate();
        }

        private void cbShowLabel_CheckedChanged(object sender, EventArgs e)
        {
            part.show_label = cbShowLabel.Checked;
            Program.mainForm.panel.Invalidate();
        }

        private void cbLocked_CheckedChanged(object sender, EventArgs e)
        {
            part.locked = cbLocked.Checked;
            Program.mainForm.panel.Invalidate();
        }

        private void cbValue_TextUpdate(object sender, EventArgs e)
        {
            try
            {
                part.value = Single.Parse(cbValue.Text, System.Globalization.CultureInfo.InvariantCulture);
                part.value_unit = cbUnit.Text;
                Program.mainForm.panel.Invalidate();
            }
            catch (Exception) { }
        }

        private void cbValue_SelectedIndexChanged(object sender, EventArgs e)
        {
            string[] vals = cbValue.Text.Split(' ');
            int cnt = vals.Length;
            if (cnt <= 1)
            {
                cbValue_TextUpdate(sender, e);
                return;
            }
            this.BeginInvoke((MethodInvoker)delegate {                
                cbValue.Text = vals[0];
                cbUnit.Text = vals[1];
                cbValue_TextUpdate(sender, e);
            });
        }

        private void cbUnit_TextUpdate(object sender, EventArgs e)
        {
            cbValue_TextUpdate(sender, e);
        }

        private void cbUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbValue_TextUpdate(sender, e);
        }

    }
}
