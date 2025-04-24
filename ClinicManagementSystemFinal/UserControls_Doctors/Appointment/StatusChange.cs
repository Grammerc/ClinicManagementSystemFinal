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
        readonly PatientQueue host;   // ① keep a reference to the queue

        // ② NEW overload – three arguments
        public StatusChange(string apptId,
                            Guna.UI2.WinForms.Guna2ImageButton statusButton,
                            PatientQueue owner)
        {
            InitializeComponent();
            appointmentId = apptId;
            targetButton = statusButton;
            host = owner;
        }

        // ③ (optional) keep the old two-argument ctor so existing code still compiles
        public StatusChange(string apptId,
                            Guna.UI2.WinForms.Guna2ImageButton statusButton)
            : this(apptId, statusButton, null) { }

        void UpdateStatus(string newStatus, Image statusImage)
        {
            const string CONN =
                @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=B:\Downloads\Login.accdb;Persist Security Info=False;";
            using (var conn = new OleDbConnection(CONN))
            {
                conn.Open();
                var cmd = new OleDbCommand(
                    "UPDATE Appointments SET Status = ? WHERE AppointmentID = ?", conn);
                cmd.Parameters.AddWithValue("?", newStatus);
                cmd.Parameters.AddWithValue("?", appointmentId);
                cmd.ExecuteNonQuery();
            }

            if (targetButton != null) targetButton.Image = statusImage;

            host?.RefreshCurrentQueue();   // ④ refresh list if we have a host

            Close();
        }

        void btnApproved_Click(object sender, EventArgs e) => UpdateStatus("Approved", btnApproved.Image);
        void btnComplete_Click(object sender, EventArgs e) => UpdateStatus("Completed", btnComplete.Image);
        void btnCancel_Click(object sender, EventArgs e) => UpdateStatus("Cancelled", btnCancel.Image);
        void btnPending_Click(object sender, EventArgs e) => UpdateStatus("Pending", btnPending.Image);
    }
}