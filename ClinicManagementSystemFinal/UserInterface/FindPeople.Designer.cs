namespace ClinicManagementSystemFinal.UserInterface
{
    partial class FindPeople
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            cbxSpec = new ComboBox();
            cbxRole = new ComboBox();
            tbxName = new Guna.UI2.WinForms.Guna2TextBox();
            panel1 = new Panel();
            panelMain = new FlowLayoutPanel();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // cbxSpec
            // 
            cbxSpec.FormattingEnabled = true;
            cbxSpec.Location = new Point(3, 24);
            cbxSpec.Name = "cbxSpec";
            cbxSpec.Size = new Size(164, 23);
            cbxSpec.TabIndex = 4;
            // 
            // cbxRole
            // 
            cbxRole.FormattingEnabled = true;
            cbxRole.Location = new Point(203, 24);
            cbxRole.Name = "cbxRole";
            cbxRole.Size = new Size(121, 23);
            cbxRole.TabIndex = 3;
            cbxRole.Visible = false;
            // 
            // tbxName
            // 
            tbxName.CustomizableEdges = customizableEdges1;
            tbxName.DefaultText = "";
            tbxName.DisabledState.BorderColor = Color.FromArgb(208, 208, 208);
            tbxName.DisabledState.FillColor = Color.FromArgb(226, 226, 226);
            tbxName.DisabledState.ForeColor = Color.FromArgb(138, 138, 138);
            tbxName.DisabledState.PlaceholderForeColor = Color.FromArgb(138, 138, 138);
            tbxName.FocusedState.BorderColor = Color.FromArgb(94, 148, 255);
            tbxName.Font = new Font("Segoe UI", 9F);
            tbxName.HoverState.BorderColor = Color.FromArgb(94, 148, 255);
            tbxName.Location = new Point(577, 11);
            tbxName.Name = "tbxName";
            tbxName.PlaceholderText = "";
            tbxName.SelectedText = "";
            tbxName.ShadowDecoration.CustomizableEdges = customizableEdges2;
            tbxName.Size = new Size(200, 36);
            tbxName.TabIndex = 2;
            // 
            // panel1
            // 
            panel1.Controls.Add(cbxRole);
            panel1.Controls.Add(tbxName);
            panel1.Controls.Add(cbxSpec);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(793, 67);
            panel1.TabIndex = 3;
            // 
            // panelMain
            // 
            panelMain.AutoScroll = true;
            panelMain.Dock = DockStyle.Top;
            panelMain.Location = new Point(0, 67);
            panelMain.Name = "panelMain";
            panelMain.Size = new Size(793, 595);
            panelMain.TabIndex = 4;
            // 
            // FindPeople
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(panelMain);
            Controls.Add(panel1);
            Name = "FindPeople";
            Size = new Size(793, 662);
            panel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private Guna.UI2.WinForms.Guna2TextBox tbxName;
        private ComboBox cbxRole;
        private ComboBox cbxSpec;
        private Panel panel1;
        private FlowLayoutPanel panelMain;
    }
}
