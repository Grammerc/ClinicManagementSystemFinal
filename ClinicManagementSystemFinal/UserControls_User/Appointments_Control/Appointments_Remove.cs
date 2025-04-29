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

namespace ClinicManagementSystemFinal.UserControls_User.Appointments_Control
{
    public partial class Appointments_Remove : UserControl
    {
        private string userLoginId;
        public Appointments_Remove(string loginId)
        {
            InitializeComponent();
            userLoginId = loginId;
            // this.Load += Appointments_Remove_Load;

            //MessageBox.Show("Event Attached");

        }

        private bool isProcessingClick = false;
        private void dgvRemove_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //MessageBox.Show($"Clicked: Row {e.RowIndex}, Column {e.ColumnIndex} - {dgvRemove.Columns[e.ColumnIndex].Name}");
            if (isProcessingClick) return;
            isProcessingClick = true;

            try
            {
                if (dgvRemove.Columns[e.ColumnIndex].Name == "Delete" && e.RowIndex >= 0)
                {
                    DialogResult result = MessageBox.Show(
                        "Are you sure you want to permanently delete this appointment?",
                        "Delete Confirmation",
                        MessageBoxButtons.OKCancel,
                        MessageBoxIcon.Warning);

                    if (result == DialogResult.OK)
                    {
                        int appointmentId = Convert.ToInt32(dgvRemove.Rows[e.RowIndex].Cells["AppointmentID"].Value);
                        DeleteAppointment(appointmentId);

                        // Delay the reload slightly to prevent second event from interfering
                        this.BeginInvoke(new Action(() =>
                        {
                            LoadAppointments();
                        }));
                    }
                }
            }
            finally
            {
                isProcessingClick = false;
            }
        }

        private void DeleteAppointment(int appointmentId)
        {
            string connStr = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\Raphael\Downloads\Login.accdb;";
            using (OleDbConnection conn = new OleDbConnection(connStr))
            {
                conn.Open();
                string query = "DELETE FROM Appointments WHERE AppointmentID = ?";
                OleDbCommand cmd = new OleDbCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", appointmentId);
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }

        private void LoadAppointments()
        {
            string connStr = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\Raphael\Downloads\Login.accdb;";
            using (OleDbConnection conn = new OleDbConnection(connStr))
            {
                conn.Open();
                string query = @"
        SELECT 
            A.AppointmentID,
            A.AppointmentDate AS [Date],
            C.ClinicName AS [Clinic],
            A.ReasonForVisit AS [Reason For Visit],
            A.TimeSlot AS [Time Slot],
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

                dgvRemove.DataSource = null;
                dgvRemove.Columns.Clear();
                dgvRemove.AutoGenerateColumns = false;

                DataGridViewTextBoxColumn idCol = new DataGridViewTextBoxColumn();
                idCol.Name = "AppointmentID";
                idCol.HeaderText = "ID";
                idCol.Visible = false;
                idCol.DataPropertyName = "AppointmentID";
                dgvRemove.Columns.Add(idCol);

                dgvRemove.Columns.Add(new DataGridViewTextBoxColumn { Name = "c1", HeaderText = "Date", DataPropertyName = "Date" });
                dgvRemove.Columns.Add(new DataGridViewTextBoxColumn { Name = "c2", HeaderText = "Clinic", DataPropertyName = "Clinic" });
                dgvRemove.Columns.Add(new DataGridViewTextBoxColumn { Name = "c3", HeaderText = "Reason For Visit", DataPropertyName = "Reason For Visit" });
                dgvRemove.Columns.Add(new DataGridViewTextBoxColumn { Name = "c4", HeaderText = "Time Slot", DataPropertyName = "Time Slot" });
                dgvRemove.Columns.Add(new DataGridViewTextBoxColumn { Name = "c5", HeaderText = "Doctor-In-Charge", DataPropertyName = "Doctor-In-Charge" });
                dgvRemove.Columns.Add(new DataGridViewTextBoxColumn { Name = "c6", HeaderText = "Status", DataPropertyName = "Status" });

                DataGridViewButtonColumn deleteCol = new DataGridViewButtonColumn();
                deleteCol.Name = "Delete";
                deleteCol.HeaderText = "Delete";
                deleteCol.Text = "Delete";
                deleteCol.UseColumnTextForButtonValue = true;
                dgvRemove.Columns.Add(deleteCol);

                dgvRemove.DataSource = dt;

                conn.Close();
            }
        }

        private void Appointments_Remove_Load(object sender, EventArgs e)
        {
            LoadAppointments();
        }

        private void dgvRemove_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
