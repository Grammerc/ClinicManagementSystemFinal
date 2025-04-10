namespace ClinicManagementSystemFinal.UserControls_User.Appointments_Control
{
    partial class Appointments_Read
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
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            panel2 = new Panel();
            label4 = new Label();
            dgvRead = new Guna.UI2.WinForms.Guna2DataGridView();
            guna2Panel1 = new Guna.UI2.WinForms.Guna2Panel();
            c1 = new DataGridViewTextBoxColumn();
            c2 = new DataGridViewTextBoxColumn();
            c3 = new DataGridViewTextBoxColumn();
            c4 = new DataGridViewTextBoxColumn();
            c5 = new DataGridViewTextBoxColumn();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvRead).BeginInit();
            guna2Panel1.SuspendLayout();
            SuspendLayout();
            // 
            // panel2
            // 
            panel2.Controls.Add(label4);
            panel2.Dock = DockStyle.Top;
            panel2.Location = new Point(0, 0);
            panel2.Name = "panel2";
            panel2.Size = new Size(793, 56);
            panel2.TabIndex = 7;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Microsoft Sans Serif", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label4.Location = new Point(12, 18);
            label4.Name = "label4";
            label4.Size = new Size(240, 25);
            label4.TabIndex = 1;
            label4.Text = "Current Appointments";
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
            dgvRead.Columns.AddRange(new DataGridViewColumn[] { c1, c2, c3, c4, c5 });
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
            dgvRead.Size = new Size(793, 606);
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
            // guna2Panel1
            // 
            guna2Panel1.Controls.Add(dgvRead);
            guna2Panel1.CustomizableEdges = customizableEdges1;
            guna2Panel1.Dock = DockStyle.Fill;
            guna2Panel1.Location = new Point(0, 56);
            guna2Panel1.Name = "guna2Panel1";
            guna2Panel1.ShadowDecoration.CustomizableEdges = customizableEdges2;
            guna2Panel1.Size = new Size(793, 606);
            guna2Panel1.TabIndex = 9;
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
            c4.HeaderText = "Doctor-In-Charge";
            c4.Name = "c4";
            // 
            // c5
            // 
            c5.HeaderText = "Status";
            c5.Name = "c5";
            // 
            // Appointments_Read
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(guna2Panel1);
            Controls.Add(panel2);
            Name = "Appointments_Read";
            Size = new Size(793, 662);
            Load += Appointments_Read_Load;
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvRead).EndInit();
            guna2Panel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel panel2;
        private Label label4;
        private Guna.UI2.WinForms.Guna2DataGridView dgvRead;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel1;
        private DataGridViewTextBoxColumn c1;
        private DataGridViewTextBoxColumn c2;
        private DataGridViewTextBoxColumn c3;
        private DataGridViewTextBoxColumn c4;
        private DataGridViewTextBoxColumn c5;
    }
}
