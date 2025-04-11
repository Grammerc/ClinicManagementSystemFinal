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

namespace ClinicManagementSystemFinal.UserControls_Doctors
{
    public partial class StatusChange : Form
    {
        private string appointmentId;
        private Guna.UI2.WinForms.Guna2ImageButton targetButton;

        public StatusChange(string apptId, Guna.UI2.WinForms.Guna2ImageButton statusButton)
        {
            InitializeComponent();
            appointmentId = apptId;
            targetButton = statusButton;
        }

        private void UpdateStatus(string newStatus, Image statusImage)
        {
            string connStr = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\Raphael\Downloads\Login.accdb;Persist Security Info=False;";
            using (OleDbConnection conn = new OleDbConnection(connStr))
            {
                conn.Open();
                OleDbCommand cmd = new OleDbCommand("UPDATE Appointments SET Status = ? WHERE AppointmentID = ?", conn);
                cmd.Parameters.AddWithValue("?", newStatus);
                cmd.Parameters.AddWithValue("?", appointmentId);
                cmd.ExecuteNonQuery();
                conn.Close();
            }

            if (targetButton != null)
            {
                targetButton.Image = statusImage;
            }

            if (this.FindForm() != null)
            {
                this.FindForm().Close();
            }
        }

        private void btnComplete_Click(object sender, EventArgs e)
        {
            UpdateStatus("Completed", btnComplete.Image);
        }

        private void btnSkip_Click(object sender, EventArgs e)
        {
            UpdateStatus("Skipped", btnSkip.Image);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            UpdateStatus("Cancelled", btnCancel.Image);
        }

        private void btnPending_Click(object sender, EventArgs e)
        {
            UpdateStatus("Pending", btnPending.Image);
        }

        private void StatusChange_Load(object sender, EventArgs e)
        {

        }
        
       
        
    }
}
