namespace ClinicManagementSystemFinal.UserControls_Doctors.Appointment
{
    partial class Appointment
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
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            panel1 = new Panel();
            label4 = new Label();
            panel2 = new Panel();
            cbxSelectDate = new Guna.UI2.WinForms.Guna2CheckBox();
            cbxDateTime = new Guna.UI2.WinForms.Guna2DateTimePicker();
            cbxDeclined = new Guna.UI2.WinForms.Guna2CheckBox();
            cbxApproved = new Guna.UI2.WinForms.Guna2CheckBox();
            cbxPending = new Guna.UI2.WinForms.Guna2CheckBox();
            cbxClinic = new Guna.UI2.WinForms.Guna2ComboBox();
            dgvAppts = new Guna.UI2.WinForms.Guna2DataGridView();
            colDate = new DataGridViewTextBoxColumn();
            colName = new DataGridViewTextBoxColumn();
            colStatus = new DataGridViewTextBoxColumn();
            colReason = new DataGridViewTextBoxColumn();
            colTime = new DataGridViewTextBoxColumn();
            colApprove = new DataGridViewButtonColumn();
            colDecline = new DataGridViewButtonColumn();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvAppts).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(label4);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(793, 56);
            panel1.TabIndex = 0;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Microsoft Sans Serif", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label4.Location = new Point(14, 14);
            label4.Name = "label4";
            label4.Size = new Size(240, 25);
            label4.TabIndex = 2;
            label4.Text = "Current Appointments";
            // 
            // panel2
            // 
            panel2.Controls.Add(cbxSelectDate);
            panel2.Controls.Add(cbxDateTime);
            panel2.Controls.Add(cbxDeclined);
            panel2.Controls.Add(cbxApproved);
            panel2.Controls.Add(cbxPending);
            panel2.Controls.Add(cbxClinic);
            panel2.Dock = DockStyle.Top;
            panel2.Location = new Point(0, 56);
            panel2.Name = "panel2";
            panel2.Size = new Size(793, 54);
            panel2.TabIndex = 1;
            // 
            // cbxSelectDate
            // 
            cbxSelectDate.AutoSize = true;
            cbxSelectDate.CheckedState.BorderColor = Color.FromArgb(94, 148, 255);
            cbxSelectDate.CheckedState.BorderRadius = 0;
            cbxSelectDate.CheckedState.BorderThickness = 0;
            cbxSelectDate.CheckedState.FillColor = Color.FromArgb(94, 148, 255);
            cbxSelectDate.Location = new Point(263, 18);
            cbxSelectDate.Name = "cbxSelectDate";
            cbxSelectDate.Size = new Size(84, 19);
            cbxSelectDate.TabIndex = 5;
            cbxSelectDate.Text = "Select Date";
            cbxSelectDate.UncheckedState.BorderColor = Color.FromArgb(125, 137, 149);
            cbxSelectDate.UncheckedState.BorderRadius = 0;
            cbxSelectDate.UncheckedState.BorderThickness = 0;
            cbxSelectDate.UncheckedState.FillColor = Color.FromArgb(125, 137, 149);
            cbxSelectDate.CheckedChanged += cbxSelectDate_CheckedChanged;
            // 
            // cbxDateTime
            // 
            cbxDateTime.Checked = true;
            cbxDateTime.CustomizableEdges = customizableEdges1;
            cbxDateTime.Font = new Font("Segoe UI", 9F);
            cbxDateTime.Format = DateTimePickerFormat.Long;
            cbxDateTime.Location = new Point(353, 12);
            cbxDateTime.MaxDate = new DateTime(9998, 12, 31, 0, 0, 0, 0);
            cbxDateTime.MinDate = new DateTime(1753, 1, 1, 0, 0, 0, 0);
            cbxDateTime.Name = "cbxDateTime";
            cbxDateTime.ShadowDecoration.CustomizableEdges = customizableEdges2;
            cbxDateTime.Size = new Size(200, 36);
            cbxDateTime.TabIndex = 4;
            cbxDateTime.Value = new DateTime(2025, 4, 24, 18, 37, 24, 667);
            cbxDateTime.ValueChanged += cbxDateTime_ValueChanged;
            // 
            // cbxDeclined
            // 
            cbxDeclined.AutoSize = true;
            cbxDeclined.CheckedState.BorderColor = Color.FromArgb(94, 148, 255);
            cbxDeclined.CheckedState.BorderRadius = 0;
            cbxDeclined.CheckedState.BorderThickness = 0;
            cbxDeclined.CheckedState.FillColor = Color.FromArgb(94, 148, 255);
            cbxDeclined.Location = new Point(168, 18);
            cbxDeclined.Name = "cbxDeclined";
            cbxDeclined.Size = new Size(72, 19);
            cbxDeclined.TabIndex = 3;
            cbxDeclined.Text = "Declined";
            cbxDeclined.UncheckedState.BorderColor = Color.FromArgb(125, 137, 149);
            cbxDeclined.UncheckedState.BorderRadius = 0;
            cbxDeclined.UncheckedState.BorderThickness = 0;
            cbxDeclined.UncheckedState.FillColor = Color.FromArgb(125, 137, 149);
            // 
            // cbxApproved
            // 
            cbxApproved.AutoSize = true;
            cbxApproved.CheckedState.BorderColor = Color.FromArgb(94, 148, 255);
            cbxApproved.CheckedState.BorderRadius = 0;
            cbxApproved.CheckedState.BorderThickness = 0;
            cbxApproved.CheckedState.FillColor = Color.FromArgb(94, 148, 255);
            cbxApproved.Location = new Point(84, 18);
            cbxApproved.Name = "cbxApproved";
            cbxApproved.Size = new Size(78, 19);
            cbxApproved.TabIndex = 2;
            cbxApproved.Text = "Approved";
            cbxApproved.UncheckedState.BorderColor = Color.FromArgb(125, 137, 149);
            cbxApproved.UncheckedState.BorderRadius = 0;
            cbxApproved.UncheckedState.BorderThickness = 0;
            cbxApproved.UncheckedState.FillColor = Color.FromArgb(125, 137, 149);
            // 
            // cbxPending
            // 
            cbxPending.AutoSize = true;
            cbxPending.CheckedState.BorderColor = Color.FromArgb(94, 148, 255);
            cbxPending.CheckedState.BorderRadius = 0;
            cbxPending.CheckedState.BorderThickness = 0;
            cbxPending.CheckedState.FillColor = Color.FromArgb(94, 148, 255);
            cbxPending.Location = new Point(8, 18);
            cbxPending.Name = "cbxPending";
            cbxPending.Size = new Size(70, 19);
            cbxPending.TabIndex = 1;
            cbxPending.Text = "Pending";
            cbxPending.UncheckedState.BorderColor = Color.FromArgb(125, 137, 149);
            cbxPending.UncheckedState.BorderRadius = 0;
            cbxPending.UncheckedState.BorderThickness = 0;
            cbxPending.UncheckedState.FillColor = Color.FromArgb(125, 137, 149);
            // 
            // cbxClinic
            // 
            cbxClinic.BackColor = Color.Transparent;
            cbxClinic.CustomizableEdges = customizableEdges3;
            cbxClinic.DrawMode = DrawMode.OwnerDrawFixed;
            cbxClinic.DropDownStyle = ComboBoxStyle.DropDownList;
            cbxClinic.FocusedColor = Color.FromArgb(94, 148, 255);
            cbxClinic.FocusedState.BorderColor = Color.FromArgb(94, 148, 255);
            cbxClinic.Font = new Font("Segoe UI", 10F);
            cbxClinic.ForeColor = Color.FromArgb(68, 88, 112);
            cbxClinic.ItemHeight = 30;
            cbxClinic.Location = new Point(559, 12);
            cbxClinic.Name = "cbxClinic";
            cbxClinic.ShadowDecoration.CustomizableEdges = customizableEdges4;
            cbxClinic.Size = new Size(222, 36);
            cbxClinic.TabIndex = 0;
            // 
            // dgvAppts
            // 
            dataGridViewCellStyle1.BackColor = Color.White;
            dgvAppts.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = Color.FromArgb(100, 88, 255);
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle2.ForeColor = Color.White;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            dgvAppts.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            dgvAppts.ColumnHeadersHeight = 17;
            dgvAppts.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            dgvAppts.Columns.AddRange(new DataGridViewColumn[] { colDate, colName, colStatus, colReason, colTime, colApprove, colDecline });
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = Color.White;
            dataGridViewCellStyle3.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle3.ForeColor = Color.FromArgb(71, 69, 94);
            dataGridViewCellStyle3.SelectionBackColor = Color.FromArgb(231, 229, 255);
            dataGridViewCellStyle3.SelectionForeColor = Color.FromArgb(71, 69, 94);
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.False;
            dgvAppts.DefaultCellStyle = dataGridViewCellStyle3;
            dgvAppts.Dock = DockStyle.Fill;
            dgvAppts.GridColor = Color.FromArgb(231, 229, 255);
            dgvAppts.Location = new Point(0, 110);
            dgvAppts.Name = "dgvAppts";
            dgvAppts.RowHeadersVisible = false;
            dgvAppts.Size = new Size(793, 552);
            dgvAppts.TabIndex = 2;
            dgvAppts.ThemeStyle.AlternatingRowsStyle.BackColor = Color.White;
            dgvAppts.ThemeStyle.AlternatingRowsStyle.Font = null;
            dgvAppts.ThemeStyle.AlternatingRowsStyle.ForeColor = Color.Empty;
            dgvAppts.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = Color.Empty;
            dgvAppts.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = Color.Empty;
            dgvAppts.ThemeStyle.BackColor = Color.White;
            dgvAppts.ThemeStyle.GridColor = Color.FromArgb(231, 229, 255);
            dgvAppts.ThemeStyle.HeaderStyle.BackColor = Color.FromArgb(100, 88, 255);
            dgvAppts.ThemeStyle.HeaderStyle.BorderStyle = DataGridViewHeaderBorderStyle.None;
            dgvAppts.ThemeStyle.HeaderStyle.Font = new Font("Segoe UI", 9F);
            dgvAppts.ThemeStyle.HeaderStyle.ForeColor = Color.White;
            dgvAppts.ThemeStyle.HeaderStyle.HeaightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            dgvAppts.ThemeStyle.HeaderStyle.Height = 17;
            dgvAppts.ThemeStyle.ReadOnly = false;
            dgvAppts.ThemeStyle.RowsStyle.BackColor = Color.White;
            dgvAppts.ThemeStyle.RowsStyle.BorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvAppts.ThemeStyle.RowsStyle.Font = new Font("Segoe UI", 9F);
            dgvAppts.ThemeStyle.RowsStyle.ForeColor = Color.FromArgb(71, 69, 94);
            dgvAppts.ThemeStyle.RowsStyle.Height = 25;
            dgvAppts.ThemeStyle.RowsStyle.SelectionBackColor = Color.FromArgb(231, 229, 255);
            dgvAppts.ThemeStyle.RowsStyle.SelectionForeColor = Color.FromArgb(71, 69, 94);
            // 
            // colDate
            // 
            colDate.HeaderText = "Date";
            colDate.Name = "colDate";
            // 
            // colName
            // 
            colName.HeaderText = "Name";
            colName.Name = "colName";
            // 
            // colStatus
            // 
            colStatus.HeaderText = "Status";
            colStatus.Name = "colStatus";
            // 
            // colReason
            // 
            colReason.HeaderText = "Reason For Visit";
            colReason.Name = "colReason";
            // 
            // colTime
            // 
            colTime.HeaderText = "Time Slot";
            colTime.Name = "colTime";
            // 
            // colApprove
            // 
            colApprove.HeaderText = "Approve";
            colApprove.Name = "colApprove";
            // 
            // colDecline
            // 
            colDecline.HeaderText = "Decline";
            colDecline.Name = "colDecline";
            // 
            // Appointment
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(dgvAppts);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Name = "Appointment";
            Size = new Size(793, 662);
            Load += Appointment_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvAppts).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Panel panel2;
        private Guna.UI2.WinForms.Guna2DataGridView dgvAppts;
        private Guna.UI2.WinForms.Guna2CheckBox guna2CheckBox3;
        private Guna.UI2.WinForms.Guna2CheckBox guna2CheckBox2;
        private Guna.UI2.WinForms.Guna2CheckBox guna2CheckBox1;
        private Guna.UI2.WinForms.Guna2ComboBox cbxClinic;
        private Label label4;
        private DataGridViewTextBoxColumn colDate;
        private DataGridViewTextBoxColumn colName;
        private DataGridViewTextBoxColumn colStatus;
        private DataGridViewTextBoxColumn colReason;
        private DataGridViewTextBoxColumn colTime;
        private DataGridViewButtonColumn colApprove;
        private DataGridViewButtonColumn colDecline;
        private Guna.UI2.WinForms.Guna2CheckBox cbxDeclined;
        private Guna.UI2.WinForms.Guna2CheckBox cbxApproved;
        private Guna.UI2.WinForms.Guna2CheckBox cbxPending;
        private Guna.UI2.WinForms.Guna2DateTimePicker cbxDateTime;
        private Guna.UI2.WinForms.Guna2CheckBox cbxSelectDate;
    }
}
