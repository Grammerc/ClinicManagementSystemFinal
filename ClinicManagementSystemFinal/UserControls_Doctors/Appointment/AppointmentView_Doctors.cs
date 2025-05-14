using System.Data.OleDb;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Windows.Forms;
using System.Text;

namespace ClinicManagementSystemFinal.UserControls_Doctors.Appointment
{
    public partial class AppointmentView_Doctors : UserControl
    {
        private readonly string doctorLoginId;
        DateTime? _filterDate;

        private const string CONN =
            @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\Raphael\source\repos\ClinicManagementSystemFinal\ClinicManagementSystemFinal\Login.accdb;Persist Security Info=False;";

        public AppointmentView_Doctors(string loginId)
        {
            InitializeComponent();
            doctorLoginId = loginId;

            cbxPending.Checked = true; 
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
            cbxClinic.Items.Clear();                 // ② NO "All Clinics"

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
        private void LoadAppointments()
        {
            // 1) Build the list of statuses to include
            var wanted = new List<string>();
            if (cbxPending.Checked) wanted.Add("Pending");
            if (cbxApproved.Checked) wanted.Add("Approved");
            if (cbxDeclined.Checked) wanted.Add("Declined");

            // if nothing selected or no clinic, clear and bail
            if (wanted.Count == 0 || cbxClinic.SelectedItem == null)
            {
                dgvAppts.Rows.Clear();
                return;
            }

            // 2) Build the SQL, now including D.DoctorName AS Dr
            var sql = new StringBuilder(@"
SELECT
    A.AppointmentID,
    A.AppointmentDate,
    A.TimeSlot,
    I.Name,
    A.ReasonForVisit,
    A.Status,
    D.DoctorName AS Dr
FROM
    ((Appointments AS A
      INNER JOIN Doctors     AS D ON A.DoctorID    = D.DoctorID)
     INNER JOIN Information AS I ON A.UserInfoID = I.UserInfoID)
WHERE
    D.LoginID  = ?
  AND A.ClinicID = ?
  AND A.Status   IN (");
            sql.Append(string.Join(",", wanted.ConvertAll(_ => "?")));
            sql.Append(")");

            // optional date‐filter
            if (_filterDate.HasValue)
                sql.Append(" AND A.AppointmentDate >= ? AND A.AppointmentDate < ?");
            sql.Append(" ORDER BY A.AppointmentDate, A.TimeSlot;");

            // 3) Run it
            using var conn = new OleDbConnection(CONN);
            conn.Open();
            using var cmd = new OleDbCommand(sql.ToString(), conn);

            // bind parameters
            cmd.Parameters.AddWithValue("?", doctorLoginId);
            cmd.Parameters.AddWithValue("?", ((ComboItem)cbxClinic.SelectedItem).Value);
            foreach (var s in wanted)
                cmd.Parameters.AddWithValue("?", s);
            if (_filterDate.HasValue)
            {
                var d0 = _filterDate.Value.Date;
                cmd.Parameters.AddWithValue("?", d0);
                cmd.Parameters.AddWithValue("?", d0.AddDays(1));
            }

            using var rdr = cmd.ExecuteReader();

            // 4) Populate grid
            dgvAppts.Rows.Clear();
            while (rdr.Read())
            {
                int row = dgvAppts.Rows.Add();
                dgvAppts.Rows[row].Tag = rdr["AppointmentID"];
                dgvAppts.Rows[row].Cells["colDate"].Value = ((DateTime)rdr["AppointmentDate"]).ToShortDateString();
                dgvAppts.Rows[row].Cells["colTime"].Value = rdr["TimeSlot"].ToString();
                dgvAppts.Rows[row].Cells["colName"].Value = rdr["Name"].ToString();
                dgvAppts.Rows[row].Cells["colStatus"].Value = rdr["Status"].ToString();
                dgvAppts.Rows[row].Cells["colDr"].Value = rdr["Dr"].ToString();
            }

            // 5) Hide the columns we no longer want, then reorder to Date, Time, Name, Status, Dr
            dgvAppts.SuspendLayout();
            if (dgvAppts.Columns.Contains("colReason")) dgvAppts.Columns["colReason"].Visible = false;

            dgvAppts.Columns["colDate"].DisplayIndex = 0;
            dgvAppts.Columns["colTime"].DisplayIndex = 1;
            dgvAppts.Columns["colName"].DisplayIndex = 2;
            dgvAppts.Columns["colStatus"].DisplayIndex = 3;
            dgvAppts.Columns["colDr"].DisplayIndex = 4;
            dgvAppts.ResumeLayout();
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
            
            // First get the appointment details for email
            string email = "N/A";
            string userName = "N/A";
            string clinicName = "N/A";
            DateTime appointmentDate = DateTime.Now;
            string timeSlot = "N/A";
            
            if (status == "Approved")
            {
                try
                {
                    using var getDetailsCmd = new OleDbCommand(
                        @"SELECT a.AppointmentID, a.AppointmentDate, a.TimeSlot, a.Status,
                                 i.Email,
                                 i.Name as UserName,
                                 c.ClinicName
                          FROM ((Appointments a
                          INNER JOIN Information i ON a.UserInfoID = i.UserInfoID)
                          INNER JOIN Clinics c ON a.ClinicID = c.ClinicID)
                          WHERE a.AppointmentID = ?", conn);
                    getDetailsCmd.Parameters.AddWithValue("?", apptId);
                    
                    using var rdr = getDetailsCmd.ExecuteReader();
                    if (rdr.Read())
                    {
                        // Use null-coalescing operator to provide default values
                        email = rdr["Email"]?.ToString() ?? "N/A";
                        userName = rdr["UserName"]?.ToString() ?? "N/A";
                        clinicName = rdr["ClinicName"]?.ToString() ?? "N/A";
                        timeSlot = rdr["TimeSlot"]?.ToString() ?? "N/A";
                        
                        // Handle DateTime separately to avoid conversion errors
                        if (rdr["AppointmentDate"] != DBNull.Value)
                        {
                            appointmentDate = Convert.ToDateTime(rdr["AppointmentDate"]);
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Log the error but continue with default values
                    System.Diagnostics.Debug.WriteLine($"Error retrieving appointment details: {ex.Message}");
                }
            }
            
            // Update the status
            try
            {
                using var cmd = new OleDbCommand(
                    "UPDATE Appointments SET Status = ? WHERE AppointmentID = ?", conn);
                cmd.Parameters.AddWithValue("?", status);
                cmd.Parameters.AddWithValue("?", apptId);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating appointment status: {ex.Message}", 
                    "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (status == "Approved")
            {
                try
                {
                    using var smtpClient = new System.Net.Mail.SmtpClient("smtp.gmail.com", 587)
                    {
                        EnableSsl = true,
                        UseDefaultCredentials = false,
                        Credentials = new System.Net.NetworkCredential("ClinicManagementSystemC@gmail.com", "hyop ejoi vhlm miss"),
                        DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network,
                        Timeout = 30000
                    };

                    var mailMessage = new System.Net.Mail.MailMessage
                    {
                        From = new System.Net.Mail.MailAddress("ClinicManagementSystemC@gmail.com", "Clinic Management System"),
                        Subject = "Appointment Approved",
                        Body = $@"Dear {userName},

Your appointment at {clinicName} on {appointmentDate:MMMM dd, yyyy} at {timeSlot} has been approved.

Please arrive 15 minutes before your scheduled appointment time.

Best regards,
Clinic Management System",
                        IsBodyHtml = false,
                        Priority = System.Net.Mail.MailPriority.High
                    };

                    if (email != "N/A" && email.Contains("@"))
                    {
                        mailMessage.To.Add(new System.Net.Mail.MailAddress(email));
                        smtpClient.Send(mailMessage);
                    }
                    else
                    {
                        MessageBox.Show("Appointment was approved but no valid email address was found to send the confirmation.", 
                            "Email Notice", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Appointment was approved but email could not be sent: {ex.Message}", 
                        "Email Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

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