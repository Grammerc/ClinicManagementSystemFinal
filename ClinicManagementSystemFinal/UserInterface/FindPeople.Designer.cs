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
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            panelMain = new FlowLayoutPanel();
            tbxName = new Guna.UI2.WinForms.Guna2TextBox();
            SuspendLayout();
            // 
            // panelMain
            // 
            panelMain.Dock = DockStyle.Bottom;
            panelMain.Location = new Point(0, 60);
            panelMain.Name = "panelMain";
            panelMain.Size = new Size(793, 602);
            panelMain.TabIndex = 1;
            // 
            // tbxName
            // 
            tbxName.CustomizableEdges = customizableEdges3;
            tbxName.DefaultText = "";
            tbxName.DisabledState.BorderColor = Color.FromArgb(208, 208, 208);
            tbxName.DisabledState.FillColor = Color.FromArgb(226, 226, 226);
            tbxName.DisabledState.ForeColor = Color.FromArgb(138, 138, 138);
            tbxName.DisabledState.PlaceholderForeColor = Color.FromArgb(138, 138, 138);
            tbxName.FocusedState.BorderColor = Color.FromArgb(94, 148, 255);
            tbxName.Font = new Font("Segoe UI", 9F);
            tbxName.HoverState.BorderColor = Color.FromArgb(94, 148, 255);
            tbxName.Location = new Point(573, 12);
            tbxName.Name = "tbxName";
            tbxName.PlaceholderText = "";
            tbxName.SelectedText = "";
            tbxName.ShadowDecoration.CustomizableEdges = customizableEdges4;
            tbxName.Size = new Size(200, 36);
            tbxName.TabIndex = 2;
            // 
            // FindPeople
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(tbxName);
            Controls.Add(panelMain);
            Name = "FindPeople";
            Size = new Size(793, 662);
            ResumeLayout(false);
        }

        #endregion

        private FlowLayoutPanel panelMain;
        private Guna.UI2.WinForms.Guna2TextBox tbxName;
    }
}
