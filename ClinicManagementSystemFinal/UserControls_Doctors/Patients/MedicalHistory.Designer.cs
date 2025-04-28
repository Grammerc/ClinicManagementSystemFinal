namespace ClinicManagementSystemFinal.UserControls_Doctors
{
    partial class MedicalHistory
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
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges7 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges8 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges9 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            panel2 = new Panel();
            label4 = new Label();
            guna2Panel1 = new Guna.UI2.WinForms.Guna2Panel();
            tableLayoutPanel1 = new TableLayoutPanel();
            pbxProfilePatient = new Guna.UI2.WinForms.Guna2CirclePictureBox();
            panel1 = new Panel();
            lblName = new Label();
            btnSave = new Guna.UI2.WinForms.Guna2Button();
            tbpPatient = new ReaLTaiizor.Controls.ForeverTabPage();
            tabPage3 = new TabPage();
            txtNote = new Guna.UI2.WinForms.Guna2TextBox();
            doctorNotes1 = new DoctorNotes();
            guna2Button1 = new Guna.UI2.WinForms.Guna2Button();
            tabPage4 = new TabPage();
            dgvHistory = new Guna.UI2.WinForms.Guna2DataGridView();
            createMedicalHistory1 = new CreateMedicalHistory();
            panel2.SuspendLayout();
            guna2Panel1.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pbxProfilePatient).BeginInit();
            panel1.SuspendLayout();
            tbpPatient.SuspendLayout();
            tabPage3.SuspendLayout();
            tabPage4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvHistory).BeginInit();
            SuspendLayout();
            // 
            // panel2
            // 
            panel2.Controls.Add(label4);
            panel2.Dock = DockStyle.Top;
            panel2.Location = new Point(0, 0);
            panel2.Name = "panel2";
            panel2.Size = new Size(793, 56);
            panel2.TabIndex = 6;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Microsoft Sans Serif", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label4.Location = new Point(12, 18);
            label4.Name = "label4";
            label4.Size = new Size(161, 25);
            label4.TabIndex = 1;
            label4.Text = "Patient Profile";
            // 
            // guna2Panel1
            // 
            guna2Panel1.Controls.Add(tableLayoutPanel1);
            guna2Panel1.CustomizableEdges = customizableEdges4;
            guna2Panel1.Dock = DockStyle.Top;
            guna2Panel1.Location = new Point(0, 56);
            guna2Panel1.Name = "guna2Panel1";
            guna2Panel1.ShadowDecoration.CustomizableEdges = customizableEdges5;
            guna2Panel1.Size = new Size(793, 100);
            guna2Panel1.TabIndex = 7;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 3;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 14.4385033F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 42.78075F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 42.78075F));
            tableLayoutPanel1.Controls.Add(pbxProfilePatient, 0, 0);
            tableLayoutPanel1.Controls.Add(panel1, 1, 0);
            tableLayoutPanel1.Controls.Add(btnSave, 2, 0);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Size = new Size(793, 100);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // pbxProfilePatient
            // 
            pbxProfilePatient.Dock = DockStyle.Fill;
            pbxProfilePatient.ImageRotate = 0F;
            pbxProfilePatient.Location = new Point(3, 3);
            pbxProfilePatient.Name = "pbxProfilePatient";
            pbxProfilePatient.ShadowDecoration.CustomizableEdges = customizableEdges1;
            pbxProfilePatient.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            pbxProfilePatient.Size = new Size(108, 94);
            pbxProfilePatient.TabIndex = 0;
            pbxProfilePatient.TabStop = false;
            // 
            // panel1
            // 
            panel1.Controls.Add(lblName);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(117, 3);
            panel1.Name = "panel1";
            panel1.Size = new Size(333, 94);
            panel1.TabIndex = 2;
            // 
            // lblName
            // 
            lblName.Anchor = AnchorStyles.None;
            lblName.AutoSize = true;
            lblName.Location = new Point(3, 0);
            lblName.Name = "lblName";
            lblName.Size = new Size(39, 15);
            lblName.TabIndex = 1;
            lblName.Text = "Name";
            // 
            // btnSave
            // 
            btnSave.CustomizableEdges = customizableEdges2;
            btnSave.DisabledState.BorderColor = Color.DarkGray;
            btnSave.DisabledState.CustomBorderColor = Color.DarkGray;
            btnSave.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btnSave.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btnSave.Font = new Font("Segoe UI", 9F);
            btnSave.ForeColor = Color.White;
            btnSave.Location = new Point(456, 3);
            btnSave.Name = "btnSave";
            btnSave.ShadowDecoration.CustomizableEdges = customizableEdges3;
            btnSave.Size = new Size(180, 45);
            btnSave.TabIndex = 3;
            btnSave.Text = "Save";
            // 
            // tbpPatient
            // 
            tbpPatient.ActiveColor = Color.FromArgb(35, 168, 109);
            tbpPatient.ActiveFontColor = Color.White;
            tbpPatient.BaseColor = Color.FromArgb(45, 47, 49);
            tbpPatient.BGColor = Color.FromArgb(60, 70, 73);
            tbpPatient.Controls.Add(tabPage3);
            tbpPatient.Controls.Add(tabPage4);
            tbpPatient.DeactiveFontColor = Color.White;
            tbpPatient.Dock = DockStyle.Fill;
            tbpPatient.Font = new Font("Segoe UI", 10F);
            tbpPatient.ItemSize = new Size(395, 60);
            tbpPatient.Location = new Point(0, 156);
            tbpPatient.Name = "tbpPatient";
            tbpPatient.SelectedIndex = 0;
            tbpPatient.Size = new Size(793, 506);
            tbpPatient.SizeMode = TabSizeMode.Fixed;
            tbpPatient.TabIndex = 9;
            // 
            // tabPage3
            // 
            tabPage3.BackColor = Color.FromArgb(60, 70, 73);
            tabPage3.Controls.Add(txtNote);
            tabPage3.Controls.Add(doctorNotes1);
            tabPage3.Controls.Add(guna2Button1);
            tabPage3.Location = new Point(4, 64);
            tabPage3.Name = "tabPage3";
            tabPage3.Padding = new Padding(3);
            tabPage3.Size = new Size(785, 438);
            tabPage3.TabIndex = 0;
            tabPage3.Text = "Doctor's Notes";
            // 
            // txtNote
            // 
            txtNote.CustomizableEdges = customizableEdges6;
            txtNote.DefaultText = "";
            txtNote.DisabledState.BorderColor = Color.FromArgb(208, 208, 208);
            txtNote.DisabledState.FillColor = Color.FromArgb(226, 226, 226);
            txtNote.DisabledState.ForeColor = Color.FromArgb(138, 138, 138);
            txtNote.DisabledState.PlaceholderForeColor = Color.FromArgb(138, 138, 138);
            txtNote.Dock = DockStyle.Fill;
            txtNote.FocusedState.BorderColor = Color.FromArgb(94, 148, 255);
            txtNote.Font = new Font("Segoe UI", 9F);
            txtNote.HoverState.BorderColor = Color.FromArgb(94, 148, 255);
            txtNote.Location = new Point(3, 3);
            txtNote.Multiline = true;
            txtNote.Name = "txtNote";
            txtNote.PlaceholderText = "";
            txtNote.SelectedText = "";
            txtNote.ShadowDecoration.CustomizableEdges = customizableEdges7;
            txtNote.Size = new Size(779, 432);
            txtNote.TabIndex = 2;
            // 
            // doctorNotes1
            // 
            doctorNotes1.BackColor = Color.White;
            doctorNotes1.Dock = DockStyle.Fill;
            doctorNotes1.Location = new Point(3, 3);
            doctorNotes1.Name = "doctorNotes1";
            doctorNotes1.Size = new Size(779, 432);
            doctorNotes1.TabIndex = 0;
            // 
            // guna2Button1
            // 
            guna2Button1.CustomizableEdges = customizableEdges8;
            guna2Button1.DisabledState.BorderColor = Color.DarkGray;
            guna2Button1.DisabledState.CustomBorderColor = Color.DarkGray;
            guna2Button1.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            guna2Button1.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            guna2Button1.Font = new Font("Segoe UI", 9F);
            guna2Button1.ForeColor = Color.White;
            guna2Button1.Location = new Point(577, 45);
            guna2Button1.Name = "guna2Button1";
            guna2Button1.ShadowDecoration.CustomizableEdges = customizableEdges9;
            guna2Button1.Size = new Size(180, 45);
            guna2Button1.TabIndex = 3;
            guna2Button1.Text = "guna2Button1";
            // 
            // tabPage4
            // 
            tabPage4.BackColor = Color.FromArgb(60, 70, 73);
            tabPage4.Controls.Add(dgvHistory);
            tabPage4.Controls.Add(createMedicalHistory1);
            tabPage4.Location = new Point(4, 64);
            tabPage4.Name = "tabPage4";
            tabPage4.Padding = new Padding(3);
            tabPage4.Size = new Size(785, 438);
            tabPage4.TabIndex = 1;
            tabPage4.Text = "Medical History";
            // 
            // dgvHistory
            // 
            dataGridViewCellStyle1.BackColor = Color.White;
            dgvHistory.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dgvHistory.BackgroundColor = Color.DimGray;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = Color.FromArgb(100, 88, 255);
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 10F);
            dataGridViewCellStyle2.ForeColor = Color.White;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            dgvHistory.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            dgvHistory.ColumnHeadersHeight = 4;
            dgvHistory.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = Color.White;
            dataGridViewCellStyle3.Font = new Font("Segoe UI", 10F);
            dataGridViewCellStyle3.ForeColor = Color.FromArgb(71, 69, 94);
            dataGridViewCellStyle3.SelectionBackColor = Color.FromArgb(231, 229, 255);
            dataGridViewCellStyle3.SelectionForeColor = Color.FromArgb(71, 69, 94);
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.False;
            dgvHistory.DefaultCellStyle = dataGridViewCellStyle3;
            dgvHistory.Dock = DockStyle.Fill;
            dgvHistory.GridColor = Color.FromArgb(231, 229, 255);
            dgvHistory.Location = new Point(3, 3);
            dgvHistory.Name = "dgvHistory";
            dgvHistory.RowHeadersVisible = false;
            dgvHistory.Size = new Size(779, 432);
            dgvHistory.TabIndex = 2;
            dgvHistory.ThemeStyle.AlternatingRowsStyle.BackColor = Color.White;
            dgvHistory.ThemeStyle.AlternatingRowsStyle.Font = null;
            dgvHistory.ThemeStyle.AlternatingRowsStyle.ForeColor = Color.Empty;
            dgvHistory.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = Color.Empty;
            dgvHistory.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = Color.Empty;
            dgvHistory.ThemeStyle.BackColor = Color.DimGray;
            dgvHistory.ThemeStyle.GridColor = Color.FromArgb(231, 229, 255);
            dgvHistory.ThemeStyle.HeaderStyle.BackColor = Color.FromArgb(100, 88, 255);
            dgvHistory.ThemeStyle.HeaderStyle.BorderStyle = DataGridViewHeaderBorderStyle.None;
            dgvHistory.ThemeStyle.HeaderStyle.Font = new Font("Segoe UI", 10F);
            dgvHistory.ThemeStyle.HeaderStyle.ForeColor = Color.White;
            dgvHistory.ThemeStyle.HeaderStyle.HeaightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            dgvHistory.ThemeStyle.HeaderStyle.Height = 4;
            dgvHistory.ThemeStyle.ReadOnly = false;
            dgvHistory.ThemeStyle.RowsStyle.BackColor = Color.White;
            dgvHistory.ThemeStyle.RowsStyle.BorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvHistory.ThemeStyle.RowsStyle.Font = new Font("Segoe UI", 10F);
            dgvHistory.ThemeStyle.RowsStyle.ForeColor = Color.FromArgb(71, 69, 94);
            dgvHistory.ThemeStyle.RowsStyle.Height = 25;
            dgvHistory.ThemeStyle.RowsStyle.SelectionBackColor = Color.FromArgb(231, 229, 255);
            dgvHistory.ThemeStyle.RowsStyle.SelectionForeColor = Color.FromArgb(71, 69, 94);
            // 
            // createMedicalHistory1
            // 
            createMedicalHistory1.BackColor = Color.White;
            createMedicalHistory1.Dock = DockStyle.Fill;
            createMedicalHistory1.Location = new Point(3, 3);
            createMedicalHistory1.Name = "createMedicalHistory1";
            createMedicalHistory1.Size = new Size(779, 432);
            createMedicalHistory1.TabIndex = 0;
            // 
            // MedicalHistory
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(tbpPatient);
            Controls.Add(guna2Panel1);
            Controls.Add(panel2);
            Name = "MedicalHistory";
            Size = new Size(793, 662);
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            guna2Panel1.ResumeLayout(false);
            tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pbxProfilePatient).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            tbpPatient.ResumeLayout(false);
            tabPage3.ResumeLayout(false);
            tabPage4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvHistory).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel2;
        private Label label4;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel1;
        private TableLayoutPanel tableLayoutPanel1;
        private Guna.UI2.WinForms.Guna2CirclePictureBox pbxProfilePatient;
        private ReaLTaiizor.Controls.ForeverTabPage tbpPatient;
        private TabPage tabPage3;
        private TabPage tabPage4;
        private DoctorNotes doctorNotes1;
        private CreateMedicalHistory createMedicalHistory1;
        private Panel panel1;
        private Label lblName;
        private Guna.UI2.WinForms.Guna2TextBox txtNote;
        private Guna.UI2.WinForms.Guna2Button btnSave;
        private Guna.UI2.WinForms.Guna2Button guna2Button1;
        private Guna.UI2.WinForms.Guna2DataGridView dgvHistory;
    }
}
