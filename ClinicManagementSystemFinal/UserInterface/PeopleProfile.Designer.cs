namespace ClinicManagementSystemFinal.UserInterface
{
    partial class PeopleProfile
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
            panelMain = new Panel();
            lblSpecialization = new Label();
            btnView = new Guna.UI2.WinForms.Guna2Button();
            lblRole = new Label();
            lblName = new Label();
            pbxProfile = new PictureBox();
            panelMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pbxProfile).BeginInit();
            SuspendLayout();
            // 
            // panelMain
            // 
            panelMain.Controls.Add(lblSpecialization);
            panelMain.Controls.Add(btnView);
            panelMain.Controls.Add(lblRole);
            panelMain.Controls.Add(lblName);
            panelMain.Controls.Add(pbxProfile);
            panelMain.Dock = DockStyle.Fill;
            panelMain.Location = new Point(0, 0);
            panelMain.Name = "panelMain";
            panelMain.Size = new Size(245, 262);
            panelMain.TabIndex = 1;
            // 
            // lblSpecialization
            // 
            lblSpecialization.AutoSize = true;
            lblSpecialization.Location = new Point(3, 213);
            lblSpecialization.Name = "lblSpecialization";
            lblSpecialization.Size = new Size(79, 15);
            lblSpecialization.TabIndex = 4;
            lblSpecialization.Text = "Specialization";
            // 
            // btnView
            // 
            btnView.CustomizableEdges = customizableEdges3;
            btnView.DisabledState.BorderColor = Color.DarkGray;
            btnView.DisabledState.CustomBorderColor = Color.DarkGray;
            btnView.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btnView.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btnView.Font = new Font("Segoe UI", 9F);
            btnView.ForeColor = Color.White;
            btnView.Location = new Point(126, 228);
            btnView.Name = "btnView";
            btnView.ShadowDecoration.CustomizableEdges = customizableEdges4;
            btnView.Size = new Size(116, 31);
            btnView.TabIndex = 3;
            btnView.Text = "View Profile";
            // 
            // lblRole
            // 
            lblRole.AutoSize = true;
            lblRole.Location = new Point(3, 192);
            lblRole.Name = "lblRole";
            lblRole.Size = new Size(30, 15);
            lblRole.TabIndex = 2;
            lblRole.Text = "Role";
            // 
            // lblName
            // 
            lblName.AutoSize = true;
            lblName.Location = new Point(3, 170);
            lblName.Name = "lblName";
            lblName.Size = new Size(39, 15);
            lblName.TabIndex = 1;
            lblName.Text = "Name";
            // 
            // pbxProfile
            // 
            pbxProfile.Dock = DockStyle.Top;
            pbxProfile.Location = new Point(0, 0);
            pbxProfile.Name = "pbxProfile";
            pbxProfile.Size = new Size(245, 164);
            pbxProfile.TabIndex = 0;
            pbxProfile.TabStop = false;
            // 
            // PeopleProfile
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(panelMain);
            Name = "PeopleProfile";
            Size = new Size(245, 262);
            Load += PeopleProfile_Load;
            panelMain.ResumeLayout(false);
            panelMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pbxProfile).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panelMain;
        private Label lblSpecialization;
        private Guna.UI2.WinForms.Guna2Button btnView;
        private Label lblRole;
        private Label lblName;
        private PictureBox pbxProfile;
    }
}
