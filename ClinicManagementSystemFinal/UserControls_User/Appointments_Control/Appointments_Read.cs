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
    public partial class Appointments_Read : UserControl
    {
        private string userLoginId;
        public Appointments_Read(string loginId)
        {
            InitializeComponent();
            userLoginId = loginId;
        }
        private void LoadAppointments()
        {
            string connStr = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=B:\Downloads\Login.accdb;";
            using (OleDbConnection conn = new OleDbConnection(connStr))
            {
                conn.Open();
                string query = @"
            SELECT 
                A.AppointmentDate AS [Date],
                C.ClinicName AS [Clinic],
                A.ReasonForVisit AS [Reason For Visit],
                D.DoctorName AS [Doctor-In-Charge],
                A.Status
            FROM Appointments A
            INNER JOIN Clinics C ON A.ClinicID = C.ClinicID
            INNER JOIN Doctors D ON A.DoctorID = D.DoctorID
            WHERE A.UserInfoID = (SELECT UserInfoID FROM Information WHERE LoginID = @loginId)
            ORDER BY A.AppointmentDate DESC";

                OleDbCommand cmd = new OleDbCommand(query, conn);
                cmd.Parameters.AddWithValue("@loginId", userLoginId);

                OleDbDataAdapter adapter = new OleDbDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                dgvRead.DataSource = dt;

                conn.Close();
            }
        }


        private void Appointments_Read_Load(object sender, EventArgs e)
        {
            LoadAppointments();
        }
    }
}
