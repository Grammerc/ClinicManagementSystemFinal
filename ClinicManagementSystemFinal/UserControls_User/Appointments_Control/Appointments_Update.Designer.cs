namespace ClinicManagementSystemFinal.UserControls_User
{
    partial class Appointments_Update
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
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            panel2 = new Panel();
            label4 = new Label();
            dgvAppointments = new Guna.UI2.WinForms.Guna2DataGridView();
            c1 = new DataGridViewTextBoxColumn();
            c2 = new DataGridViewTextBoxColumn();
            c3 = new DataGridViewTextBoxColumn();
            c5 = new DataGridViewTextBoxColumn();
            c4 = new DataGridViewTextBoxColumn();
            c6 = new DataGridViewTextBoxColumn();
            Select = new DataGridViewButtonColumn();
            scheduleDate = new Guna.UI2.WinForms.Guna2DateTimePicker();
            scheduleTime = new Guna.UI2.WinForms.Guna2ComboBox();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvAppointments).BeginInit();
            SuspendLayout();
            // 
            // panel2
            // 
            panel2.Controls.Add(label4);
            panel2.Dock = DockStyle.Top;
            panel2.Location = new Point(0, 0);
            panel2.Name = "panel2";
            panel2.Size = new Size(793, 56);
            panel2.TabIndex = 11;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Microsoft Sans Serif", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label4.Location = new Point(12, 18);
            label4.Name = "label4";
            label4.Size = new Size(237, 25);
            label4.TabIndex = 1;
            label4.Text = "Update Appointments";
            // 
            // dgvAppointments
            // 
            dataGridViewCellStyle1.BackColor = Color.White;
            dgvAppointments.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = Color.FromArgb(100, 88, 255);
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle2.ForeColor = Color.White;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            dgvAppointments.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            dgvAppointments.ColumnHeadersHeight = 17;
            dgvAppointments.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            dgvAppointments.Columns.AddRange(new DataGridViewColumn[] { c1, c2, c3, c5, c4, c6, Select });
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = Color.White;
            dataGridViewCellStyle3.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle3.ForeColor = Color.FromArgb(71, 69, 94);
            dataGridViewCellStyle3.SelectionBackColor = Color.FromArgb(231, 229, 255);
            dataGridViewCellStyle3.SelectionForeColor = Color.FromArgb(71, 69, 94);
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.False;
            dgvAppointments.DefaultCellStyle = dataGridViewCellStyle3;
            dgvAppointments.Dock = DockStyle.Top;
            dgvAppointments.GridColor = Color.FromArgb(231, 229, 255);
            dgvAppointments.Location = new Point(0, 56);
            dgvAppointments.Name = "dgvAppointments";
            dgvAppointments.RowHeadersVisible = false;
            dgvAppointments.Size = new Size(793, 345);
            dgvAppointments.TabIndex = 12;
            dgvAppointments.ThemeStyle.AlternatingRowsStyle.BackColor = Color.White;
            dgvAppointments.ThemeStyle.AlternatingRowsStyle.Font = null;
            dgvAppointments.ThemeStyle.AlternatingRowsStyle.ForeColor = Color.Empty;
            dgvAppointments.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = Color.Empty;
            dgvAppointments.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = Color.Empty;
            dgvAppointments.ThemeStyle.BackColor = Color.White;
            dgvAppointments.ThemeStyle.GridColor = Color.FromArgb(231, 229, 255);
            dgvAppointments.ThemeStyle.HeaderStyle.BackColor = Color.FromArgb(100, 88, 255);
            dgvAppointments.ThemeStyle.HeaderStyle.BorderStyle = DataGridViewHeaderBorderStyle.None;
            dgvAppointments.ThemeStyle.HeaderStyle.Font = new Font("Segoe UI", 9F);
            dgvAppointments.ThemeStyle.HeaderStyle.ForeColor = Color.White;
            dgvAppointments.ThemeStyle.HeaderStyle.HeaightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            dgvAppointments.ThemeStyle.HeaderStyle.Height = 17;
            dgvAppointments.ThemeStyle.ReadOnly = false;
            dgvAppointments.ThemeStyle.RowsStyle.BackColor = Color.White;
            dgvAppointments.ThemeStyle.RowsStyle.BorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvAppointments.ThemeStyle.RowsStyle.Font = new Font("Segoe UI", 9F);
            dgvAppointments.ThemeStyle.RowsStyle.ForeColor = Color.FromArgb(71, 69, 94);
            dgvAppointments.ThemeStyle.RowsStyle.Height = 25;
            dgvAppointments.ThemeStyle.RowsStyle.SelectionBackColor = Color.FromArgb(231, 229, 255);
            dgvAppointments.ThemeStyle.RowsStyle.SelectionForeColor = Color.FromArgb(71, 69, 94);
            dgvAppointments.CellClick += dgvAppointments_CellClick;
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
            // c5
            // 
            c5.HeaderText = "Doctor-In-Charge";
            c5.Name = "c5";
            // 
            // c4
            // 
            c4.HeaderText = "Time Slot";
            c4.Name = "c4";
            // 
            // c6
            // 
            c6.HeaderText = "Status";
            c6.Name = "c6";
            // 
            // Select
            // 
            Select.HeaderText = "Select";
            Select.Name = "Select";
            Select.Resizable = DataGridViewTriState.True;
            Select.SortMode = DataGridViewColumnSortMode.Automatic;
            // 
            // scheduleDate
            // 
            scheduleDate.Checked = true;
            scheduleDate.CustomizableEdges = customizableEdges1;
            scheduleDate.Font = new Font("Segoe UI", 9F);
            scheduleDate.Format = DateTimePickerFormat.Long;
            scheduleDate.Location = new Point(46, 438);
            scheduleDate.MaxDate = new DateTime(9998, 12, 31, 0, 0, 0, 0);
            scheduleDate.MinDate = new DateTime(1753, 1, 1, 0, 0, 0, 0);
            scheduleDate.Name = "scheduleDate";
            scheduleDate.ShadowDecoration.CustomizableEdges = customizableEdges2;
            scheduleDate.Size = new Size(200, 36);
            scheduleDate.TabIndex = 14;
            scheduleDate.Value = new DateTime(2025, 4, 11, 0, 21, 9, 996);
            // 
            // scheduleTime
            // 
            scheduleTime.BackColor = Color.Transparent;
            scheduleTime.CustomizableEdges = customizableEdges3;
            scheduleTime.DrawMode = DrawMode.OwnerDrawFixed;
            scheduleTime.DropDownStyle = ComboBoxStyle.DropDownList;
            scheduleTime.FocusedColor = Color.FromArgb(94, 148, 255);
            scheduleTime.FocusedState.BorderColor = Color.FromArgb(94, 148, 255);
            scheduleTime.Font = new Font("Segoe UI", 10F);
            scheduleTime.ForeColor = Color.FromArgb(68, 88, 112);
            scheduleTime.ItemHeight = 30;
            scheduleTime.Items.AddRange(new object[] { "8:00 A.M. - 9:00 A.M.", "9:00 A.M. - 10:00 A.M", "10:00 A.M. - 11:00 A.M.", "11:00 A.M. - 12:00 A.M. ", "12:00 A.M. - 1:00 A.M.", "1:00 P.M. - 2:00  P.M.", "2:00  P.M. - 3:00  P.M.", "3:00  P.M. - 4:00  P.M.", "4:00 P.M. - 5:00  P.M.", "5:00  P.M. - 6:00  P.M." });
            scheduleTime.Location = new Point(277, 438);
            scheduleTime.Name = "scheduleTime";
            scheduleTime.ShadowDecoration.CustomizableEdges = customizableEdges4;
            scheduleTime.Size = new Size(221, 36);
            scheduleTime.TabIndex = 15;
            // 
            // Appointments_Update
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(scheduleTime);
            Controls.Add(scheduleDate);
            Controls.Add(dgvAppointments);
            Controls.Add(panel2);
            Name = "Appointments_Update";
            Size = new Size(793, 662);
            Load += Appointments_Update_Load;
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvAppointments).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel2;
        private Label label4;
        private Guna.UI2.WinForms.Guna2DataGridView dgvAppointments;
        private Guna.UI2.WinForms.Guna2DateTimePicker scheduleDate;
        private Guna.UI2.WinForms.Guna2ComboBox scheduleTime;
        private DataGridViewTextBoxColumn c1;
        private DataGridViewTextBoxColumn c2;
        private DataGridViewTextBoxColumn c3;
        private DataGridViewTextBoxColumn c5;
        private DataGridViewTextBoxColumn c4;
        private DataGridViewTextBoxColumn c6;
        private DataGridViewButtonColumn Select;
    }
}
