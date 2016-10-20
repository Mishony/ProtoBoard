namespace ProtoBoard
{
    partial class ComponentProperties
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.labelLabel = new System.Windows.Forms.Label();
            this.textLabel = new System.Windows.Forms.TextBox();
            this.cbShowLabel = new System.Windows.Forms.CheckBox();
            this.cbLocked = new System.Windows.Forms.CheckBox();
            this.labelName = new System.Windows.Forms.Label();
            this.labelValue = new System.Windows.Forms.Label();
            this.cbValue = new System.Windows.Forms.ComboBox();
            this.cbUnit = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // labelLabel
            // 
            this.labelLabel.AutoSize = true;
            this.labelLabel.Location = new System.Drawing.Point(3, 30);
            this.labelLabel.Name = "labelLabel";
            this.labelLabel.Size = new System.Drawing.Size(36, 13);
            this.labelLabel.TabIndex = 0;
            this.labelLabel.Text = "Label:";
            // 
            // textLabel
            // 
            this.textLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textLabel.Location = new System.Drawing.Point(45, 27);
            this.textLabel.Name = "textLabel";
            this.textLabel.Size = new System.Drawing.Size(250, 20);
            this.textLabel.TabIndex = 1;
            this.textLabel.TextChanged += new System.EventHandler(this.textLabel_TextChanged);
            // 
            // cbShowLabel
            // 
            this.cbShowLabel.AutoSize = true;
            this.cbShowLabel.Location = new System.Drawing.Point(6, 56);
            this.cbShowLabel.Name = "cbShowLabel";
            this.cbShowLabel.Size = new System.Drawing.Size(82, 17);
            this.cbShowLabel.TabIndex = 2;
            this.cbShowLabel.Text = "Show Label";
            this.cbShowLabel.UseVisualStyleBackColor = true;
            this.cbShowLabel.CheckedChanged += new System.EventHandler(this.cbShowLabel_CheckedChanged);
            // 
            // cbLocked
            // 
            this.cbLocked.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbLocked.AutoSize = true;
            this.cbLocked.Location = new System.Drawing.Point(233, 56);
            this.cbLocked.Name = "cbLocked";
            this.cbLocked.Size = new System.Drawing.Size(62, 17);
            this.cbLocked.TabIndex = 3;
            this.cbLocked.Text = "Locked";
            this.cbLocked.UseVisualStyleBackColor = true;
            this.cbLocked.CheckedChanged += new System.EventHandler(this.cbLocked_CheckedChanged);
            // 
            // labelName
            // 
            this.labelName.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.labelName.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelName.Location = new System.Drawing.Point(0, 0);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(297, 20);
            this.labelName.TabIndex = 4;
            this.labelName.Text = "Part name";
            this.labelName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelValue
            // 
            this.labelValue.AutoSize = true;
            this.labelValue.Location = new System.Drawing.Point(6, 84);
            this.labelValue.Name = "labelValue";
            this.labelValue.Size = new System.Drawing.Size(37, 13);
            this.labelValue.TabIndex = 5;
            this.labelValue.Text = "Value:";
            // 
            // cbValue
            // 
            this.cbValue.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbValue.FormattingEnabled = true;
            this.cbValue.Location = new System.Drawing.Point(45, 80);
            this.cbValue.Name = "cbValue";
            this.cbValue.Size = new System.Drawing.Size(153, 21);
            this.cbValue.TabIndex = 6;
            this.cbValue.SelectedIndexChanged += new System.EventHandler(this.cbValue_SelectedIndexChanged);
            this.cbValue.TextUpdate += new System.EventHandler(this.cbValue_TextUpdate);
            // 
            // cbUnit
            // 
            this.cbUnit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbUnit.FormattingEnabled = true;
            this.cbUnit.Location = new System.Drawing.Point(204, 80);
            this.cbUnit.Name = "cbUnit";
            this.cbUnit.Size = new System.Drawing.Size(89, 21);
            this.cbUnit.TabIndex = 7;
            this.cbUnit.SelectedIndexChanged += new System.EventHandler(this.cbUnit_SelectedIndexChanged);
            this.cbUnit.TextUpdate += new System.EventHandler(this.cbUnit_TextUpdate);
            // 
            // ComponentProperties
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(297, 112);
            this.Controls.Add(this.cbUnit);
            this.Controls.Add(this.cbValue);
            this.Controls.Add(this.labelValue);
            this.Controls.Add(this.labelName);
            this.Controls.Add(this.cbLocked);
            this.Controls.Add(this.cbShowLabel);
            this.Controls.Add(this.textLabel);
            this.Controls.Add(this.labelLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ComponentProperties";
            this.Text = "ComponentProperties";
            this.Load += new System.EventHandler(this.ComponentProperties_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelLabel;
        private System.Windows.Forms.TextBox textLabel;
        private System.Windows.Forms.CheckBox cbShowLabel;
        private System.Windows.Forms.CheckBox cbLocked;
        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.Label labelValue;
        private System.Windows.Forms.ComboBox cbValue;
        private System.Windows.Forms.ComboBox cbUnit;
    }
}