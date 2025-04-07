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
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges7 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges8 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            tableLayoutPanel1 = new TableLayoutPanel();
            btnCancel = new Guna.UI2.WinForms.Guna2ImageButton();
            btnPending = new Guna.UI2.WinForms.Guna2ImageButton();
            btnComplete = new Guna.UI2.WinForms.Guna2ImageButton();
            btnSkip = new Guna.UI2.WinForms.Guna2ImageButton();
            nightControlBox1 = new ReaLTaiizor.Controls.NightControlBox();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Controls.Add(btnCancel, 0, 2);
            tableLayoutPanel1.Controls.Add(btnPending, 1, 1);
            tableLayoutPanel1.Controls.Add(btnComplete, 0, 1);
            tableLayoutPanel1.Controls.Add(btnSkip, 1, 2);
            tableLayoutPanel1.Controls.Add(nightControlBox1, 1, 0);
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
            btnCancel.ShadowDecoration.CustomizableEdges = customizableEdges5;
            btnCancel.Size = new Size(115, 98);
            btnCancel.TabIndex = 2;
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
            btnPending.ShadowDecoration.CustomizableEdges = customizableEdges6;
            btnPending.Size = new Size(115, 98);
            btnPending.TabIndex = 1;
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
            btnComplete.ShadowDecoration.CustomizableEdges = customizableEdges7;
            btnComplete.Size = new Size(115, 98);
            btnComplete.TabIndex = 0;
            // 
            // btnSkip
            // 
            btnSkip.CheckedState.ImageSize = new Size(64, 64);
            btnSkip.HoverState.ImageSize = new Size(64, 64);
            btnSkip.Image = (Image)resources.GetObject("btnSkip.Image");
            btnSkip.ImageOffset = new Point(0, 0);
            btnSkip.ImageRotate = 0F;
            btnSkip.Location = new Point(153, 163);
            btnSkip.Name = "btnSkip";
            btnSkip.PressedState.ImageSize = new Size(64, 64);
            btnSkip.ShadowDecoration.CustomizableEdges = customizableEdges8;
            btnSkip.Size = new Size(115, 98);
            btnSkip.TabIndex = 3;
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
            // StatusChange
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(300, 300);
            Controls.Add(tableLayoutPanel1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "StatusChange";
            Load += StatusChange_Load;
            tableLayoutPanel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private Guna.UI2.WinForms.Guna2ImageButton btnCancel;
        private Guna.UI2.WinForms.Guna2ImageButton btnPending;
        private Guna.UI2.WinForms.Guna2ImageButton btnComplete;
        private Guna.UI2.WinForms.Guna2ImageButton btnSkip;
        private ReaLTaiizor.Controls.NightControlBox nightControlBox1;
    }
}
