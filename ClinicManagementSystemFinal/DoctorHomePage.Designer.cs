namespace ClinicManagementSystemFinal
{
    partial class DoctorHomePage
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
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges13 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges14 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges11 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges12 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DoctorHomePage));
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges17 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges18 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges15 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges16 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges19 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges20 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            panelMenu = new Guna.UI2.WinForms.Guna2Panel();
            btnSignOut = new FontAwesome.Sharp.IconButton();
            btnDoctor = new FontAwesome.Sharp.IconButton();
            btnAppointment = new FontAwesome.Sharp.IconButton();
            btnHome = new FontAwesome.Sharp.IconButton();
            guna2Panel2 = new Guna.UI2.WinForms.Guna2Panel();
            pictureBox1 = new PictureBox();
            btnMenu = new FontAwesome.Sharp.IconButton();
            panelTitleBar = new Guna.UI2.WinForms.Guna2Panel();
            btnSwitch = new Guna.UI2.WinForms.Guna2ToggleSwitch();
            nightControlBox1 = new ReaLTaiizor.Controls.NightControlBox();
            label2 = new Label();
            panelDesktop = new Guna.UI2.WinForms.Guna2Panel();
            panelMenu.SuspendLayout();
            guna2Panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            panelTitleBar.SuspendLayout();
            SuspendLayout();
            // 
            // panelMenu
            // 
            panelMenu.BackColor = Color.FromArgb(114, 137, 218);
            panelMenu.Controls.Add(btnSignOut);
            panelMenu.Controls.Add(btnDoctor);
            panelMenu.Controls.Add(btnAppointment);
            panelMenu.Controls.Add(btnHome);
            panelMenu.Controls.Add(guna2Panel2);
            panelMenu.CustomizableEdges = customizableEdges13;
            panelMenu.Dock = DockStyle.Left;
            panelMenu.Location = new Point(0, 0);
            panelMenu.Name = "panelMenu";
            panelMenu.ShadowDecoration.CustomizableEdges = customizableEdges14;
            panelMenu.Size = new Size(230, 565);
            panelMenu.TabIndex = 0;
            // 
            // btnSignOut
            // 
            btnSignOut.Dock = DockStyle.Bottom;
            btnSignOut.FlatAppearance.BorderSize = 0;
            btnSignOut.FlatStyle = FlatStyle.Flat;
            btnSignOut.IconChar = FontAwesome.Sharp.IconChar.SignOut;
            btnSignOut.IconColor = Color.Black;
            btnSignOut.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnSignOut.IconSize = 30;
            btnSignOut.ImageAlign = ContentAlignment.MiddleLeft;
            btnSignOut.Location = new Point(0, 514);
            btnSignOut.Name = "btnSignOut";
            btnSignOut.Padding = new Padding(10, 0, 0, 10);
            btnSignOut.Size = new Size(230, 51);
            btnSignOut.TabIndex = 8;
            btnSignOut.Tag = "Sign Out";
            btnSignOut.Text = "Sign Out";
            btnSignOut.UseVisualStyleBackColor = true;
            btnSignOut.Click += btnSignOut_Click;
            // 
            // btnDoctor
            // 
            btnDoctor.Dock = DockStyle.Top;
            btnDoctor.FlatAppearance.BorderSize = 0;
            btnDoctor.FlatStyle = FlatStyle.Flat;
            btnDoctor.IconChar = FontAwesome.Sharp.IconChar.HospitalUser;
            btnDoctor.IconColor = Color.Black;
            btnDoctor.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnDoctor.IconSize = 30;
            btnDoctor.ImageAlign = ContentAlignment.MiddleLeft;
            btnDoctor.Location = new Point(0, 222);
            btnDoctor.Name = "btnDoctor";
            btnDoctor.Padding = new Padding(10, 15, 0, 5);
            btnDoctor.Size = new Size(230, 49);
            btnDoctor.TabIndex = 3;
            btnDoctor.Tag = "    Edit Clinic";
            btnDoctor.Text = "    Edit Clinic";
            btnDoctor.UseVisualStyleBackColor = true;
            // 
            // btnAppointment
            // 
            btnAppointment.Dock = DockStyle.Top;
            btnAppointment.FlatAppearance.BorderSize = 0;
            btnAppointment.FlatStyle = FlatStyle.Flat;
            btnAppointment.IconChar = FontAwesome.Sharp.IconChar.Calendar;
            btnAppointment.IconColor = Color.Black;
            btnAppointment.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnAppointment.IconSize = 30;
            btnAppointment.ImageAlign = ContentAlignment.MiddleLeft;
            btnAppointment.Location = new Point(0, 173);
            btnAppointment.Name = "btnAppointment";
            btnAppointment.Padding = new Padding(10, 15, 0, 5);
            btnAppointment.Size = new Size(230, 49);
            btnAppointment.TabIndex = 2;
            btnAppointment.Tag = "          Appointments";
            btnAppointment.Text = "           Appointments";
            btnAppointment.UseVisualStyleBackColor = true;
            // 
            // btnHome
            // 
            btnHome.Dock = DockStyle.Top;
            btnHome.FlatAppearance.BorderSize = 0;
            btnHome.FlatStyle = FlatStyle.Flat;
            btnHome.IconChar = FontAwesome.Sharp.IconChar.HomeLg;
            btnHome.IconColor = Color.Black;
            btnHome.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnHome.IconSize = 30;
            btnHome.ImageAlign = ContentAlignment.MiddleLeft;
            btnHome.Location = new Point(0, 124);
            btnHome.Name = "btnHome";
            btnHome.Padding = new Padding(10, 15, 0, 5);
            btnHome.Size = new Size(230, 49);
            btnHome.TabIndex = 1;
            btnHome.Tag = "Home";
            btnHome.Text = "Home";
            btnHome.UseVisualStyleBackColor = true;
            btnHome.Click += btnHome_Click;
            // 
            // guna2Panel2
            // 
            guna2Panel2.Controls.Add(pictureBox1);
            guna2Panel2.Controls.Add(btnMenu);
            guna2Panel2.CustomizableEdges = customizableEdges11;
            guna2Panel2.Dock = DockStyle.Top;
            guna2Panel2.Location = new Point(0, 0);
            guna2Panel2.Name = "guna2Panel2";
            guna2Panel2.ShadowDecoration.CustomizableEdges = customizableEdges12;
            guna2Panel2.Size = new Size(230, 124);
            guna2Panel2.TabIndex = 0;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(3, 0);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(123, 91);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 2;
            pictureBox1.TabStop = false;
            // 
            // btnMenu
            // 
            btnMenu.FlatAppearance.BorderSize = 0;
            btnMenu.FlatStyle = FlatStyle.Flat;
            btnMenu.IconChar = FontAwesome.Sharp.IconChar.Bars;
            btnMenu.IconColor = Color.Black;
            btnMenu.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnMenu.IconSize = 30;
            btnMenu.Location = new Point(151, 12);
            btnMenu.Name = "btnMenu";
            btnMenu.Size = new Size(46, 44);
            btnMenu.TabIndex = 1;
            btnMenu.UseVisualStyleBackColor = true;
            btnMenu.Click += btnMenu_Click;
            // 
            // panelTitleBar
            // 
            panelTitleBar.BackColor = Color.White;
            panelTitleBar.Controls.Add(btnSwitch);
            panelTitleBar.Controls.Add(nightControlBox1);
            panelTitleBar.Controls.Add(label2);
            panelTitleBar.CustomizableEdges = customizableEdges17;
            panelTitleBar.Dock = DockStyle.Top;
            panelTitleBar.Location = new Point(230, 0);
            panelTitleBar.Name = "panelTitleBar";
            panelTitleBar.ShadowDecoration.CustomizableEdges = customizableEdges18;
            panelTitleBar.Size = new Size(695, 56);
            panelTitleBar.TabIndex = 1;
            panelTitleBar.Paint += panelTitleBar_Paint;
            panelTitleBar.MouseDown += panelTitleBar_MouseDown;
            // 
            // btnSwitch
            // 
            btnSwitch.CheckedState.BorderColor = Color.FromArgb(94, 148, 255);
            btnSwitch.CheckedState.FillColor = Color.FromArgb(94, 148, 255);
            btnSwitch.CheckedState.InnerBorderColor = Color.White;
            btnSwitch.CheckedState.InnerColor = Color.White;
            btnSwitch.CustomizableEdges = customizableEdges15;
            btnSwitch.Location = new Point(515, 22);
            btnSwitch.Name = "btnSwitch";
            btnSwitch.ShadowDecoration.CustomizableEdges = customizableEdges16;
            btnSwitch.Size = new Size(35, 20);
            btnSwitch.TabIndex = 6;
            btnSwitch.UncheckedState.BorderColor = Color.FromArgb(125, 137, 149);
            btnSwitch.UncheckedState.FillColor = Color.FromArgb(125, 137, 149);
            btnSwitch.UncheckedState.InnerBorderColor = Color.White;
            btnSwitch.UncheckedState.InnerColor = Color.White;
            // 
            // nightControlBox1
            // 
            nightControlBox1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            nightControlBox1.BackColor = Color.Transparent;
            nightControlBox1.CloseHoverColor = Color.FromArgb(199, 80, 80);
            nightControlBox1.CloseHoverForeColor = Color.White;
            nightControlBox1.DefaultLocation = true;
            nightControlBox1.DisableMaximizeColor = Color.FromArgb(105, 105, 105);
            nightControlBox1.DisableMinimizeColor = Color.FromArgb(105, 105, 105);
            nightControlBox1.EnableCloseColor = Color.FromArgb(160, 160, 160);
            nightControlBox1.EnableMaximizeButton = true;
            nightControlBox1.EnableMaximizeColor = Color.FromArgb(160, 160, 160);
            nightControlBox1.EnableMinimizeButton = true;
            nightControlBox1.EnableMinimizeColor = Color.FromArgb(160, 160, 160);
            nightControlBox1.Location = new Point(556, 0);
            nightControlBox1.MaximizeHoverColor = Color.FromArgb(15, 255, 255, 255);
            nightControlBox1.MaximizeHoverForeColor = Color.White;
            nightControlBox1.MinimizeHoverColor = Color.FromArgb(15, 255, 255, 255);
            nightControlBox1.MinimizeHoverForeColor = Color.White;
            nightControlBox1.Name = "nightControlBox1";
            nightControlBox1.Size = new Size(139, 31);
            nightControlBox1.TabIndex = 2;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Microsoft Sans Serif", 21.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.ForeColor = Color.Navy;
            label2.Location = new Point(6, 9);
            label2.Name = "label2";
            label2.Size = new Size(290, 33);
            label2.TabIndex = 5;
            label2.Text = "Doctor's Dashboard";
            // 
            // panelDesktop
            // 
            panelDesktop.BackColor = Color.FromArgb(66, 69, 73);
            panelDesktop.CustomizableEdges = customizableEdges19;
            panelDesktop.Dock = DockStyle.Fill;
            panelDesktop.Location = new Point(230, 56);
            panelDesktop.Name = "panelDesktop";
            panelDesktop.ShadowDecoration.CustomizableEdges = customizableEdges20;
            panelDesktop.Size = new Size(695, 509);
            panelDesktop.TabIndex = 2;
            panelDesktop.Paint += panelDesktop_Paint;
            // 
            // DoctorHomePage
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(925, 565);
            Controls.Add(panelDesktop);
            Controls.Add(panelTitleBar);
            Controls.Add(panelMenu);
            FormBorderStyle = FormBorderStyle.None;
            Name = "DoctorHomePage";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Form3";
            panelMenu.ResumeLayout(false);
            guna2Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            panelTitleBar.ResumeLayout(false);
            panelTitleBar.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private Guna.UI2.WinForms.Guna2Panel panelDesktop;
        private Guna.UI2.WinForms.Guna2Panel panelTitleBar;
        private Guna.UI2.WinForms.Guna2Panel panelMenu;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel2;
        private FontAwesome.Sharp.IconButton btnMenu;
        private FontAwesome.Sharp.IconButton btnSignOut;
        private FontAwesome.Sharp.IconButton btnDoctor;
        private FontAwesome.Sharp.IconButton btnAppointment;
        private FontAwesome.Sharp.IconButton btnHome;
        private Label label2;
        private ReaLTaiizor.Controls.NightControlBox nightControlBox1;
        private PictureBox pictureBox1;
        private Guna.UI2.WinForms.Guna2ToggleSwitch btnSwitch;
    }
}