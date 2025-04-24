
using System.Data.OleDb;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Windows.Forms;

namespace ClinicManagementSystemFinal.UserControls_Doctors.Appointment
{
    public partial class Appointment : UserControl
    {
        private readonly string doctorLoginId;
        DateTime? _filterDate;

        private const string CONN =
            @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=B:\Downloads\Login.accdb;Persist Security Info=False;";

        public Appointment(string loginId)
        {
            InitializeComponent();
            doctorLoginId = loginId;

            cbxPending.Checked = true;   // or set all three true if you want every status by default
            cbxApproved.Checked = false;
            cbxDeclined.Checked = false;

            cbxSelectDate.Checked = false;
            cbxDateTime.Enabled = false;
            cbxSelectDate.CheckedChanged += cbxSelectDate_CheckedChanged;
            cbxDateTime.ValueChanged += cbxDateTime_ValueChanged;

            Load += Appointment_Load;
            dgvAppts.CellContentClick += dgvAppts_CellContentClick;
            cbxPending.CheckedChanged += (s, e) => LoadAppointments();
            cbxApproved.CheckedChanged += (s, e) => LoadAppointments();
            cbxDeclined.CheckedChanged += (s, e) => LoadAppointments();
            cbxClinic.SelectedIndexChanged += (s, e) => LoadAppointments();
        }

        /* --------  populate clinic combo  -------- */
        private void PopulateClinicCombo()
        {
            cbxClinic.Items.Clear();                 // ② NO “All Clinics”

            using var conn = new OleDbConnection(CONN);
            conn.Open();
            var cmd = new OleDbCommand(
                "SELECT ClinicID, ClinicName FROM Clinics " +
                "WHERE ClinicID IN (SELECT ClinicID FROM Doctors WHERE LoginID = ?)", conn);
            cmd.Parameters.AddWithValue("?", doctorLoginId);

            using var rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                cbxClinic.Items.Add(new ComboItem
                {
                    Text = rdr["ClinicName"].ToString(),
                    Value = rdr["ClinicID"].ToString()
                });
            }

            if (cbxClinic.Items.Count > 0)            // auto-pick the first clinic
                cbxClinic.SelectedIndex = 0;
        }

        /* --------  Build query — clinic filter now mandatory -------- */
        void LoadAppointments()
        {
            List<string> wanted = new();
            if (cbxPending.Checked) wanted.Add("Pending");
            if (cbxApproved.Checked) wanted.Add("Approved");
            if (cbxDeclined.Checked) wanted.Add("Declined");
            if (wanted.Count == 0 || cbxClinic.SelectedItem == null)
            {
                dgvAppts.Rows.Clear();
                return;
            }

            string sql = @"
SELECT  A.AppointmentID, A.AppointmentDate, A.[TimeSlot],
        I.Name, A.ReasonForVisit, A.[Status]
FROM   ((Appointments  AS A
         INNER JOIN Doctors     AS D ON A.DoctorID  = D.DoctorID)
         INNER JOIN Information AS I ON A.UserInfoID = I.UserInfoID)
WHERE  D.LoginID  = ?
  AND  A.ClinicID = ?
  AND  A.[Status] IN (" + string.Join(",", wanted.ConvertAll(_ => "?")) + ")";

            if (_filterDate.HasValue)
                sql += " AND A.AppointmentDate >= ? AND A.AppointmentDate < ?";

            sql += " ORDER BY A.AppointmentDate, A.[TimeSlot];";

            using var conn = new OleDbConnection(CONN);
            conn.Open();
            using var cmd = new OleDbCommand(sql, conn);

            cmd.Parameters.AddWithValue("?", doctorLoginId);
            var clinicId = ((ComboItem)cbxClinic.SelectedItem).Value;
            cmd.Parameters.AddWithValue("?", clinicId);
            wanted.ForEach(s => cmd.Parameters.AddWithValue("?", s));
            if (_filterDate.HasValue)
            {
                DateTime d0 = _filterDate.Value.Date;   
                DateTime d1 = d0.AddDays(1);            
                cmd.Parameters.AddWithValue("?", d0);
                cmd.Parameters.AddWithValue("?", d1);
            }

            using var rdr = cmd.ExecuteReader();
            dgvAppts.Rows.Clear();
            while (rdr.Read())
            {
                int r = dgvAppts.Rows.Add();
                dgvAppts.Rows[r].Tag = rdr["AppointmentID"];
                dgvAppts.Rows[r].Cells["colDate"].Value =
                    ((DateTime)rdr["AppointmentDate"]).ToShortDateString();
                dgvAppts.Rows[r].Cells["colName"].Value = rdr["Name"].ToString();
                dgvAppts.Rows[r].Cells["colReason"].Value = rdr["ReasonForVisit"].ToString();
                dgvAppts.Rows[r].Cells["colStatus"].Value = rdr["Status"].ToString();
                dgvAppts.Rows[r].Cells["colTime"].Value = rdr["TimeSlot"].ToString();
            }
        }

        /* ---------- 3. Approve / Decline buttons ---------- */
        private void dgvAppts_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            string col = dgvAppts.Columns[e.ColumnIndex].Name;
            if (col != "colApprove" && col != "colDecline") return;

            string newStatus = (col == "colApprove") ? "Approved" : "Declined";
            string apptId = dgvAppts.Rows[e.RowIndex].Tag.ToString();

            UpdateStatus(apptId, newStatus);
            LoadAppointments();      // refresh view
        }

        private void UpdateStatus(string apptId, string status)
        {
            using var conn = new OleDbConnection(CONN);
            conn.Open();
            using var cmd = new OleDbCommand(
                "UPDATE Appointments SET [Status] = ? WHERE AppointmentID = ?", conn);
            cmd.Parameters.AddWithValue("?", status);
            cmd.Parameters.AddWithValue("?", apptId);
            cmd.ExecuteNonQuery();
        }

        /* ---------- 4. Helper class for ComboBox ---------- */
        private class ComboItem
        {
            public string Text { get; set; }
            public string Value { get; set; }
            public override string ToString() => Text;
        }

        public void SelectDate(DateTime date)
        {
            cbxSelectDate.Checked = true;
            cbxDateTime.Enabled = true;
            cbxDateTime.Value = date;
            _filterDate = date.Date;
            LoadAppointments();
        }

        private void Appointment_Load(object sender, EventArgs e)
        {
            PopulateClinicCombo();
            LoadAppointments();
        }

        void cbxSelectDate_CheckedChanged(object sender, EventArgs e)
        {
            if (cbxSelectDate.Checked)
            {
                cbxDateTime.Enabled = true;
                _filterDate = cbxDateTime.Value.Date;
            }
            else
            {
                cbxDateTime.Enabled = false;
                _filterDate = null;
            }
            LoadAppointments();
        }

        void cbxDateTime_ValueChanged(object sender, EventArgs e)
        {
            if (cbxSelectDate.Checked)
            {
                _filterDate = cbxDateTime.Value.Date;
                LoadAppointments();
            }
        }
    }
}