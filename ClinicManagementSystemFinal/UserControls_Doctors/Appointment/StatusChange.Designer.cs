namespace ClinicManagementSystemFinal.UserControls_Doctors
{
    partial class StatusChange : Form
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StatusChange));
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            tableLayoutPanel1 = new TableLayoutPanel();
            btnPending = new Guna.UI2.WinForms.Guna2ImageButton();
            nightControlBox1 = new ReaLTaiizor.Controls.NightControlBox();
            btnCancel = new Guna.UI2.WinForms.Guna2ImageButton();
            btnApproved = new Guna.UI2.WinForms.Guna2ImageButton();
            btnComplete = new Guna.UI2.WinForms.Guna2ImageButton();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Controls.Add(btnPending, 1, 1);
            tableLayoutPanel1.Controls.Add(nightControlBox1, 1, 0);
            tableLayoutPanel1.Controls.Add(btnCancel, 0, 2);
            tableLayoutPanel1.Controls.Add(btnApproved, 1, 2);
            tableLayoutPanel1.Controls.Add(btnComplete, 0, 1);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 3;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Size = new Size(300, 300);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // btnPending
            // 
            btnPending.CheckedState.ImageSize = new Size(64, 64);
            btnPending.HoverState.ImageSize = new Size(64, 64);
            btnPending.Image = (Image)resources.GetObject("btnPending.Image");
            btnPending.ImageOffset = new Point(0, 0);
            btnPending.ImageRotate = 0F;
            btnPending.Location = new Point(153, 23);
            btnPending.Name = "btnPending";
            btnPending.PressedState.ImageSize = new Size(64, 64);
            btnPending.ShadowDecoration.CustomizableEdges = customizableEdges1;
            btnPending.Size = new Size(115, 98);
            btnPending.TabIndex = 1;
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
            nightControlBox1.Location = new Point(158, 3);
            nightControlBox1.MaximizeHoverColor = Color.FromArgb(15, 255, 255, 255);
            nightControlBox1.MaximizeHoverForeColor = Color.White;
            nightControlBox1.MinimizeHoverColor = Color.FromArgb(15, 255, 255, 255);
            nightControlBox1.MinimizeHoverForeColor = Color.White;
            nightControlBox1.Name = "nightControlBox1";
            nightControlBox1.Size = new Size(139, 31);
            nightControlBox1.TabIndex = 4;
            // 
            // btnCancel
            // 
            btnCancel.CheckedState.ImageSize = new Size(64, 64);
            btnCancel.HoverState.ImageSize = new Size(64, 64);
            btnCancel.Image = (Image)resources.GetObject("btnCancel.Image");
            btnCancel.ImageOffset = new Point(0, 0);
            btnCancel.ImageRotate = 0F;
            btnCancel.Location = new Point(3, 163);
            btnCancel.Name = "btnCancel";
            btnCancel.PressedState.ImageSize = new Size(64, 64);
            btnCancel.ShadowDecoration.CustomizableEdges = customizableEdges2;
            btnCancel.Size = new Size(115, 98);
            btnCancel.TabIndex = 2;
            // 
            // btnApproved
            // 
            btnApproved.CheckedState.ImageSize = new Size(64, 64);
            btnApproved.HoverState.ImageSize = new Size(64, 64);
            btnApproved.Image = (Image)resources.GetObject("btnApproved.Image");
            btnApproved.ImageOffset = new Point(0, 0);
            btnApproved.ImageRotate = 0F;
            btnApproved.Location = new Point(153, 163);
            btnApproved.Name = "btnApproved";
            btnApproved.PressedState.ImageSize = new Size(64, 64);
            btnApproved.ShadowDecoration.CustomizableEdges = customizableEdges3;
            btnApproved.Size = new Size(115, 98);
            btnApproved.TabIndex = 3;
            // 
            // btnComplete
            // 
            btnComplete.CheckedState.ImageSize = new Size(64, 64);
            btnComplete.HoverState.ImageSize = new Size(64, 64);
            btnComplete.Image = (Image)resources.GetObject("btnComplete.Image");
            btnComplete.ImageOffset = new Point(0, 0);
            btnComplete.ImageRotate = 0F;
            btnComplete.Location = new Point(3, 23);
            btnComplete.Name = "btnComplete";
            btnComplete.PressedState.ImageSize = new Size(64, 64);
            btnComplete.ShadowDecoration.CustomizableEdges = customizableEdges4;
            btnComplete.Size = new Size(115, 98);
            btnComplete.TabIndex = 0;
            // 
            // StatusChange
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(300, 300);
            Controls.Add(tableLayoutPanel1);
            FormBorderStyle = FormBorderStyle.None;
            Location = new Point(800, 800);
            Name = "StatusChange";
            StartPosition = FormStartPosition.CenterScreen;
            tableLayoutPanel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private Guna.UI2.WinForms.Guna2ImageButton btnCancel;
        private Guna.UI2.WinForms.Guna2ImageButton btnPending;
        private Guna.UI2.WinForms.Guna2ImageButton btnComplete;
        private Guna.UI2.WinForms.Guna2ImageButton btnApproved;
        private ReaLTaiizor.Controls.NightControlBox nightControlBox1;
    }
}
