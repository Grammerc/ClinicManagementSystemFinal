using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClinicManagementSystemFinal.UserControls_User
{
    public partial class Appointments_Update : UserControl
    {
        private string userLoginId;
        public Appointments_Update(string loginId)
        {
            InitializeComponent();
            userLoginId = loginId;
            this.Load += Appointments_Update_Load;
            if (scheduleTime.Items.Count > 0)
            {
                scheduleTime.SelectedIndex = 0;
            }
        }
        private void Appointments_Update_Load(object sender, EventArgs e)
        {
            LoadAppointments();
        }

        private void LoadAppointments()
        {
            string connStr = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=B:\Downloads\Login.accdb;";
            using (OleDbConnection conn = new OleDbConnection(connStr))
            {
                conn.Open();
                string query = @"
SELECT 
    A.AppointmentID,
    A.AppointmentDate AS [Date],
    C.ClinicName AS [Clinic],
    A.ReasonForVisit AS [Reason For Visit],
    D.DoctorName AS [Doctor-In-Charge],
    A.Status
FROM ((Appointments A
INNER JOIN Clinics C ON A.ClinicID = C.ClinicID)
INNER JOIN Doctors D ON A.DoctorID = D.DoctorID)
INNER JOIN Information I ON A.UserInfoID = I.UserInfoID
WHERE I.LoginID = @loginId
ORDER BY A.AppointmentDate DESC";

                OleDbCommand cmd = new OleDbCommand(query, conn);
                cmd.Parameters.AddWithValue("@loginId", userLoginId);

                OleDbDataAdapter adapter = new OleDbDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                dgvAppointments.AutoGenerateColumns = false;

                dgvAppointments.Columns["c1"].DataPropertyName = "Date";
                dgvAppointments.Columns["c2"].DataPropertyName = "Clinic";
                dgvAppointments.Columns["c3"].DataPropertyName = "Reason For Visit";
                dgvAppointments.Columns["c4"].DataPropertyName = "Doctor-In-Charge";
                dgvAppointments.Columns["c5"].DataPropertyName = "Status";

                dgvAppointments.DataSource = dt;

                conn.Close();
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dgvAppointments.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a row to update.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (scheduleTime.SelectedItem == null)
            {
                MessageBox.Show("Please select a time slot.", "Time Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataGridViewRow row = dgvAppointments.SelectedRows[0];
            string appointmentId = row.Cells["AppointmentID"].Value.ToString();
            DateTime newDate = scheduleDate.Value.Date;
            string newTime = scheduleTime.SelectedItem.ToString();

            DialogResult result = MessageBox.Show(
                "Updating this appointment will reset its status to 'Pending'. Do you want to continue?",
                "Confirm Update",
                MessageBoxButtons.OKCancel,
                MessageBoxIcon.Warning);

            if (result == DialogResult.OK)
            {
                string connStr = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=B:\Downloads\Login.accdb;";
                using (OleDbConnection conn = new OleDbConnection(connStr))
                {
                    conn.Open();

                    string updateQuery = @"
                        UPDATE Appointments
                        SET AppointmentDate = @date, TimeSlot = @time, Status = 'Pending'
                        WHERE AppointmentID = @id";

                    OleDbCommand cmd = new OleDbCommand(updateQuery, conn);
                    cmd.Parameters.AddWithValue("@date", newDate);
                    cmd.Parameters.AddWithValue("@time", newTime);
                    cmd.Parameters.AddWithValue("@id", appointmentId);

                    int rows = cmd.ExecuteNonQuery();

                    if (rows > 0)
                    {
                        MessageBox.Show("Appointment updated successfully. Status is now 'Pending'.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadAppointments(); // Refresh the table
                    }
                    else
                    {
                        MessageBox.Show("Update failed. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    conn.Close();
                }
            }

            
    }
    }
}
