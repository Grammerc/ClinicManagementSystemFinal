using System;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace ClinicManagementSystemFinal.UserControls_Doctors
{
    public partial class MedicalHistory : UserControl
    {
        private readonly int _userInfoId;
        private readonly int _doctorLoginId;
        private readonly string _clinicId;

        private const string CONN =
            @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\Raphael\Downloads\Login.accdb;Persist Security Info=False;";

        public MedicalHistory(string uId, string pName, string photo, string cId, string docLogin)
        {
            InitializeComponent();

            _userInfoId = int.Parse(uId);
            _doctorLoginId = int.Parse(docLogin);
            _clinicId = cId;

            lblName.Text = pName;
            if (File.Exists(photo))
                pbxProfilePatient.Load(photo);

            LoadHistoryGrid();
            btnSave.Click += btnSave_Click;
        }

        private void LoadHistoryGrid()
        {
            using var conn = new OleDbConnection(CONN);
            conn.Open();

            var da = new OleDbDataAdapter(
                "SELECT NoteDate, Notes " +
                "FROM DoctorNotes " +
                "WHERE UserInfoID = ? AND DoctorLoginID = ? " +
                "ORDER BY NoteDate DESC",
                conn);

            da.SelectCommand.Parameters.Add("?", OleDbType.Integer).Value = _userInfoId;
            da.SelectCommand.Parameters.Add("?", OleDbType.Integer).Value = _doctorLoginId;

            var dt = new DataTable();
            da.Fill(dt);
            dgvHistory.DataSource = dt;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            using var conn = new OleDbConnection(CONN);
            conn.Open();

            using var cmd = new OleDbCommand(
                "INSERT INTO DoctorNotes (UserInfoID, DoctorLoginID, NoteDate, Notes) " +
                "VALUES (?, ?, ?, ?)",
                conn);

            cmd.Parameters.Add("?", OleDbType.Integer).Value = _userInfoId;
            cmd.Parameters.Add("?", OleDbType.Integer).Value = _doctorLoginId;

            cmd.Parameters.Add("?", OleDbType.Date).Value = DateTime.Now;

            cmd.Parameters.Add("?", OleDbType.VarChar).Value = txtNote.Text.Trim();

            cmd.ExecuteNonQuery();

            txtNote.Clear();
            LoadHistoryGrid();
        }
    }
}