using System;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;

namespace ClinicManagementSystemFinal.UserControls_Doctors
{
    public partial class LastPatient : UserControl
    {
        private readonly string _doctorLoginId;
        const string CONN = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=B:\Downloads\Login.accdb;Persist Security Info=False;";

        public LastPatient(string doctorLoginId)
        {
            InitializeComponent();
            _doctorLoginId = doctorLoginId;
            LoadHistory();
        }

        private void LoadHistory()
        {
            var dt = new DataTable();
            using (var conn = new OleDbConnection(CONN))
            {
                conn.Open();
                var sql = @"
SELECT 
    I.Name            AS [Patient Name],
    A.ReasonForVisit  AS [Reason for Visit],
    A.AppointmentDate AS [Date],
    A.Status          AS Status
FROM 
    (Appointments A 
       INNER JOIN Doctors D ON A.DoctorID = D.DoctorID)
       INNER JOIN Information I ON A.UserInfoID = I.UserInfoID
WHERE 
    D.LoginID = ?
ORDER BY 
    A.AppointmentDate DESC";
                using (var cmd = new OleDbCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("?", _doctorLoginId);
                    using (var da = new OleDbDataAdapter(cmd))
                        da.Fill(dt);
                }
            }

            // bind to your Guna2DataGridView (named e.g. dgvHistory)
            dgvHistory.DataSource = dt;
            dgvHistory.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }
    }
}