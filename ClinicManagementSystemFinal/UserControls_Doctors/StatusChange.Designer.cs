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
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StatusChange));
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            tableLayoutPanel1 = new TableLayoutPanel();
            btnComplete = new Guna.UI2.WinForms.Guna2ImageButton();
            btnPending = new Guna.UI2.WinForms.Guna2ImageButton();
            btnCancel = new Guna.UI2.WinForms.Guna2ImageButton();
            btnSkip = new Guna.UI2.WinForms.Guna2ImageButton();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Controls.Add(btnCancel, 0, 1);
            tableLayoutPanel1.Controls.Add(btnPending, 1, 0);
            tableLayoutPanel1.Controls.Add(btnComplete, 0, 0);
            tableLayoutPanel1.Controls.Add(btnSkip, 1, 1);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Size = new Size(242, 208);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // btnComplete
            // 
            btnComplete.CheckedState.ImageSize = new Size(64, 64);
            btnComplete.HoverState.ImageSize = new Size(64, 64);
            btnComplete.Image = (Image)resources.GetObject("btnComplete.Image");
            btnComplete.ImageOffset = new Point(0, 0);
            btnComplete.ImageRotate = 0F;
            btnComplete.Location = new Point(3, 3);
            btnComplete.Name = "btnComplete";
            btnComplete.PressedState.ImageSize = new Size(64, 64);
            btnComplete.ShadowDecoration.CustomizableEdges = customizableEdges3;
            btnComplete.Size = new Size(115, 98);
            btnComplete.TabIndex = 0;
            // 
            // btnPending
            // 
            btnPending.CheckedState.ImageSize = new Size(64, 64);
            btnPending.HoverState.ImageSize = new Size(64, 64);
            btnPending.Image = (Image)resources.GetObject("btnPending.Image");
            btnPending.ImageOffset = new Point(0, 0);
            btnPending.ImageRotate = 0F;
            btnPending.Location = new Point(124, 3);
            btnPending.Name = "btnPending";
            btnPending.PressedState.ImageSize = new Size(64, 64);
            btnPending.ShadowDecoration.CustomizableEdges = customizableEdges2;
            btnPending.Size = new Size(115, 98);
            btnPending.TabIndex = 1;
            // 
            // btnCancel
            // 
            btnCancel.CheckedState.ImageSize = new Size(64, 64);
            btnCancel.HoverState.ImageSize = new Size(64, 64);
            btnCancel.Image = (Image)resources.GetObject("btnCancel.Image");
            btnCancel.ImageOffset = new Point(0, 0);
            btnCancel.ImageRotate = 0F;
            btnCancel.Location = new Point(3, 107);
            btnCancel.Name = "btnCancel";
            btnCancel.PressedState.ImageSize = new Size(64, 64);
            btnCancel.ShadowDecoration.CustomizableEdges = customizableEdges1;
            btnCancel.Size = new Size(115, 98);
            btnCancel.TabIndex = 2;
            // 
            // btnSkip
            // 
            btnSkip.CheckedState.ImageSize = new Size(64, 64);
            btnSkip.HoverState.ImageSize = new Size(64, 64);
            btnSkip.Image = (Image)resources.GetObject("btnSkip.Image");
            btnSkip.ImageOffset = new Point(0, 0);
            btnSkip.ImageRotate = 0F;
            btnSkip.Location = new Point(124, 107);
            btnSkip.Name = "btnSkip";
            btnSkip.PressedState.ImageSize = new Size(64, 64);
            btnSkip.ShadowDecoration.CustomizableEdges = customizableEdges4;
            btnSkip.Size = new Size(115, 98);
            btnSkip.TabIndex = 3;
            // 
            // StatusChange
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(tableLayoutPanel1);
            Name = "StatusChange";
            Size = new Size(242, 208);
            tableLayoutPanel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private Guna.UI2.WinForms.Guna2ImageButton btnCancel;
        private Guna.UI2.WinForms.Guna2ImageButton btnPending;
        private Guna.UI2.WinForms.Guna2ImageButton btnComplete;
        private Guna.UI2.WinForms.Guna2ImageButton btnSkip;
    }
}
