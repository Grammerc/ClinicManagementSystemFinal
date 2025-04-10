namespace ClinicManagementSystemFinal.UserControls_User.Appointments_Control
{
    partial class Appointments_Remove
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
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle5 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle6 = new DataGridViewCellStyle();
            panel2 = new Panel();
            label4 = new Label();
            dgvRead = new Guna.UI2.WinForms.Guna2DataGridView();
            Clinic = new DataGridViewTextBoxColumn();
            Column1 = new DataGridViewTextBoxColumn();
            Column2 = new DataGridViewTextBoxColumn();
            Column3 = new DataGridViewTextBoxColumn();
            Column4 = new DataGridViewTextBoxColumn();
            Column5 = new DataGridViewButtonColumn();
            guna2Panel1 = new Guna.UI2.WinForms.Guna2Panel();
            guna2Panel2 = new Guna.UI2.WinForms.Guna2Panel();
            dgvRemove = new Guna.UI2.WinForms.Guna2DataGridView();
            panel1 = new Panel();
            label1 = new Label();
            c1 = new DataGridViewTextBoxColumn();
            c2 = new DataGridViewTextBoxColumn();
            c3 = new DataGridViewTextBoxColumn();
            c4 = new DataGridViewTextBoxColumn();
            c5 = new DataGridViewTextBoxColumn();
            c6 = new DataGridViewTextBoxColumn();
            Delete = new DataGridViewButtonColumn();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvRead).BeginInit();
            guna2Panel1.SuspendLayout();
            guna2Panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvRemove).BeginInit();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // panel2
            // 
            panel2.Controls.Add(label4);
            panel2.Dock = DockStyle.Top;
            panel2.Location = new Point(0, 0);
            panel2.Name = "panel2";
            panel2.Size = new Size(793, 56);
            panel2.TabIndex = 10;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Microsoft Sans Serif", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label4.Location = new Point(12, 18);
            label4.Name = "label4";
            label4.Size = new Size(247, 25);
            label4.TabIndex = 1;
            label4.Text = "Remove Appointments";
            // 
            // dgvRead
            // 
            dataGridViewCellStyle1.BackColor = Color.White;
            dgvRead.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = Color.FromArgb(100, 88, 255);
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle2.ForeColor = Color.White;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            dgvRead.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            dgvRead.ColumnHeadersHeight = 17;
            dgvRead.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            dgvRead.Columns.AddRange(new DataGridViewColumn[] { Clinic, Column1, Column2, Column3, Column4, Column5 });
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = Color.White;
            dataGridViewCellStyle3.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle3.ForeColor = Color.FromArgb(71, 69, 94);
            dataGridViewCellStyle3.SelectionBackColor = Color.FromArgb(231, 229, 255);
            dataGridViewCellStyle3.SelectionForeColor = Color.FromArgb(71, 69, 94);
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.False;
            dgvRead.DefaultCellStyle = dataGridViewCellStyle3;
            dgvRead.Dock = DockStyle.Fill;
            dgvRead.GridColor = Color.FromArgb(231, 229, 255);
            dgvRead.Location = new Point(0, 0);
            dgvRead.Name = "dgvRead";
            dgvRead.RowHeadersVisible = false;
            dgvRead.Size = new Size(793, 662);
            dgvRead.TabIndex = 8;
            dgvRead.ThemeStyle.AlternatingRowsStyle.BackColor = Color.White;
            dgvRead.ThemeStyle.AlternatingRowsStyle.Font = null;
            dgvRead.ThemeStyle.AlternatingRowsStyle.ForeColor = Color.Empty;
            dgvRead.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = Color.Empty;
            dgvRead.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = Color.Empty;
            dgvRead.ThemeStyle.BackColor = Color.White;
            dgvRead.ThemeStyle.GridColor = Color.FromArgb(231, 229, 255);
            dgvRead.ThemeStyle.HeaderStyle.BackColor = Color.FromArgb(100, 88, 255);
            dgvRead.ThemeStyle.HeaderStyle.BorderStyle = DataGridViewHeaderBorderStyle.None;
            dgvRead.ThemeStyle.HeaderStyle.Font = new Font("Segoe UI", 9F);
            dgvRead.ThemeStyle.HeaderStyle.ForeColor = Color.White;
            dgvRead.ThemeStyle.HeaderStyle.HeaightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            dgvRead.ThemeStyle.HeaderStyle.Height = 17;
            dgvRead.ThemeStyle.ReadOnly = false;
            dgvRead.ThemeStyle.RowsStyle.BackColor = Color.White;
            dgvRead.ThemeStyle.RowsStyle.BorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvRead.ThemeStyle.RowsStyle.Font = new Font("Segoe UI", 9F);
            dgvRead.ThemeStyle.RowsStyle.ForeColor = Color.FromArgb(71, 69, 94);
            dgvRead.ThemeStyle.RowsStyle.Height = 25;
            dgvRead.ThemeStyle.RowsStyle.SelectionBackColor = Color.FromArgb(231, 229, 255);
            dgvRead.ThemeStyle.RowsStyle.SelectionForeColor = Color.FromArgb(71, 69, 94);
            // 
            // Clinic
            // 
            Clinic.HeaderText = "Date";
            Clinic.Name = "Clinic";
            // 
            // Column1
            // 
            Column1.HeaderText = "Clinic ";
            Column1.Name = "Column1";
            // 
            // Column2
            // 
            Column2.HeaderText = "Reason For Visit";
            Column2.Name = "Column2";
            // 
            // Column3
            // 
            Column3.HeaderText = "Doctor-In-Charge";
            Column3.Name = "Column3";
            // 
            // Column4
            // 
            Column4.HeaderText = "Status";
            Column4.Name = "Column4";
            // 
            // Column5
            // 
            Column5.HeaderText = "Edit";
            Column5.Name = "Column5";
            // 
            // guna2Panel1
            // 
            guna2Panel1.Controls.Add(guna2Panel2);
            guna2Panel1.Controls.Add(panel1);
            guna2Panel1.Controls.Add(dgvRead);
            guna2Panel1.CustomizableEdges = customizableEdges3;
            guna2Panel1.Dock = DockStyle.Fill;
            guna2Panel1.Location = new Point(0, 0);
            guna2Panel1.Name = "guna2Panel1";
            guna2Panel1.ShadowDecoration.CustomizableEdges = customizableEdges4;
            guna2Panel1.Size = new Size(793, 662);
            guna2Panel1.TabIndex = 11;
            // 
            // guna2Panel2
            // 
            guna2Panel2.Controls.Add(dgvRemove);
            guna2Panel2.CustomizableEdges = customizableEdges1;
            guna2Panel2.Dock = DockStyle.Fill;
            guna2Panel2.Location = new Point(0, 56);
            guna2Panel2.Name = "guna2Panel2";
            guna2Panel2.ShadowDecoration.CustomizableEdges = customizableEdges2;
            guna2Panel2.Size = new Size(793, 606);
            guna2Panel2.TabIndex = 11;
            // 
            // dgvRemove
            // 
            dataGridViewCellStyle4.BackColor = Color.White;
            dgvRemove.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle4;
            dataGridViewCellStyle5.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = Color.FromArgb(100, 88, 255);
            dataGridViewCellStyle5.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle5.ForeColor = Color.White;
            dataGridViewCellStyle5.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = DataGridViewTriState.True;
            dgvRemove.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            dgvRemove.ColumnHeadersHeight = 17;
            dgvRemove.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            dgvRemove.Columns.AddRange(new DataGridViewColumn[] { c1, c2, c3, c4, c5, c6, Delete });
            dataGridViewCellStyle6.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = Color.White;
            dataGridViewCellStyle6.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle6.ForeColor = Color.FromArgb(71, 69, 94);
            dataGridViewCellStyle6.SelectionBackColor = Color.FromArgb(231, 229, 255);
            dataGridViewCellStyle6.SelectionForeColor = Color.FromArgb(71, 69, 94);
            dataGridViewCellStyle6.WrapMode = DataGridViewTriState.False;
            dgvRemove.DefaultCellStyle = dataGridViewCellStyle6;
            dgvRemove.Dock = DockStyle.Fill;
            dgvRemove.GridColor = Color.FromArgb(231, 229, 255);
            dgvRemove.Location = new Point(0, 0);
            dgvRemove.Name = "dgvRemove";
            dgvRemove.RowHeadersVisible = false;
            dgvRemove.Size = new Size(793, 606);
            dgvRemove.TabIndex = 8;
            dgvRemove.ThemeStyle.AlternatingRowsStyle.BackColor = Color.White;
            dgvRemove.ThemeStyle.AlternatingRowsStyle.Font = null;
            dgvRemove.ThemeStyle.AlternatingRowsStyle.ForeColor = Color.Empty;
            dgvRemove.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = Color.Empty;
            dgvRemove.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = Color.Empty;
            dgvRemove.ThemeStyle.BackColor = Color.White;
            dgvRemove.ThemeStyle.GridColor = Color.FromArgb(231, 229, 255);
            dgvRemove.ThemeStyle.HeaderStyle.BackColor = Color.FromArgb(100, 88, 255);
            dgvRemove.ThemeStyle.HeaderStyle.BorderStyle = DataGridViewHeaderBorderStyle.None;
            dgvRemove.ThemeStyle.HeaderStyle.Font = new Font("Segoe UI", 9F);
            dgvRemove.ThemeStyle.HeaderStyle.ForeColor = Color.White;
            dgvRemove.ThemeStyle.HeaderStyle.HeaightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            dgvRemove.ThemeStyle.HeaderStyle.Height = 17;
            dgvRemove.ThemeStyle.ReadOnly = false;
            dgvRemove.ThemeStyle.RowsStyle.BackColor = Color.White;
            dgvRemove.ThemeStyle.RowsStyle.BorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvRemove.ThemeStyle.RowsStyle.Font = new Font("Segoe UI", 9F);
            dgvRemove.ThemeStyle.RowsStyle.ForeColor = Color.FromArgb(71, 69, 94);
            dgvRemove.ThemeStyle.RowsStyle.Height = 25;
            dgvRemove.ThemeStyle.RowsStyle.SelectionBackColor = Color.FromArgb(231, 229, 255);
            dgvRemove.ThemeStyle.RowsStyle.SelectionForeColor = Color.FromArgb(71, 69, 94);
            dgvRemove.CellClick += dgvRemove_CellClick;
            dgvRemove.CellContentClick += dgvRemove_CellContentClick;
            // 
            // panel1
            // 
            panel1.Controls.Add(label1);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(793, 56);
            panel1.TabIndex = 10;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Microsoft Sans Serif", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(12, 18);
            label1.Name = "label1";
            label1.Size = new Size(240, 25);
            label1.TabIndex = 1;
            label1.Text = "Current Appointments";
            // 
            // c1
            // 
            c1.HeaderText = "Date";
            c1.Name = "c1";
            // 
            // c2
            // 
            c2.HeaderText = "Clinic ";
            c2.Name = "c2";
            // 
            // c3
            // 
            c3.HeaderText = "Reason For Visit";
            c3.Name = "c3";
            // 
            // c4
            // 
            c4.HeaderText = "Time Slot";
            c4.Name = "c4";
            // 
            // c5
            // 
            c5.HeaderText = "Doctor-In-Charge";
            c5.Name = "c5";
            // 
            // c6
            // 
            c6.HeaderText = "Status";
            c6.Name = "c6";
            // 
            // Delete
            // 
            Delete.HeaderText = "Delete";
            Delete.Name = "Delete";
            // 
            // Appointments_Remove
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(panel2);
            Controls.Add(guna2Panel1);
            Name = "Appointments_Remove";
            Size = new Size(793, 662);
            Load += Appointments_Remove_Load;
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvRead).EndInit();
            guna2Panel1.ResumeLayout(false);
            guna2Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvRemove).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel2;
        private Label label4;
        private Guna.UI2.WinForms.Guna2DataGridView dgvRead;
        private DataGridViewTextBoxColumn Clinic;
        private DataGridViewTextBoxColumn Column1;
        private DataGridViewTextBoxColumn Column2;
        private DataGridViewTextBoxColumn Column3;
        private DataGridViewTextBoxColumn Column4;
        private DataGridViewButtonColumn Column5;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel1;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel2;
        private Guna.UI2.WinForms.Guna2DataGridView dgvRemove;
        private Panel panel1;
        private Label label1;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private DataGridViewTextBoxColumn c1;
        private DataGridViewTextBoxColumn c2;
        private DataGridViewTextBoxColumn c3;
        private DataGridViewTextBoxColumn c4;
        private DataGridViewTextBoxColumn c5;
        private DataGridViewTextBoxColumn c6;
        private DataGridViewButtonColumn Delete;
    }
}
