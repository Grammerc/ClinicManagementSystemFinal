using System;
using System.Data.OleDb;
using System.Drawing;
using System.Windows.Forms;

namespace ClinicManagementSystemFinal.UserControls_Doctors
{
    public partial class StatusChange : Form
    {
        readonly string appointmentId;
        readonly Guna.UI2.WinForms.Guna2ImageButton targetButton;
        readonly PatientQueue host; 

        public StatusChange(string apptId,
                            Guna.UI2.WinForms.Guna2ImageButton statusButton,
                            PatientQueue owner)
        {
            InitializeComponent();
            appointmentId = apptId;
            targetButton = statusButton;
            host = owner;

            btnApproved.Click += btnApproved_Click;
            btnCancel.Click += btnCancel_Click;
            btnPending.Click += btnPending_Click;
            btnComplete.Click += btnComplete_Click;
        }

        void UpdateStatus(string newStatus)
        {
            const string CONN =
                @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\Raphael\source\repos\ClinicManagementSystemFinal\ClinicManagementSystemFinal\Login.accdb;Persist Security Info=False;";
            using (var conn = new OleDbConnection(CONN))
            {
                conn.Open();
                var cmd = new OleDbCommand(
                    "UPDATE Appointments SET Status = ? WHERE AppointmentID = ?", conn);
                cmd.Parameters.AddWithValue("?", newStatus);
                cmd.Parameters.AddWithValue("?", appointmentId);
                cmd.ExecuteNonQuery();
            }

            host?.RefreshCurrentQueue();
            Close();
        }

        void btnApproved_Click(object s, EventArgs e) => UpdateStatus("Approved");
        void btnCancel_Click(object s, EventArgs e) => UpdateStatus("Cancelled");
        void btnPending_Click(object s, EventArgs e) => UpdateStatus("Pending");
        void btnComplete_Click(object s, EventArgs e) => UpdateStatus("Completed");
    }
}