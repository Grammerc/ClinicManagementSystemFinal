using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using ClinicManagementSystemFinal.UserControls_User;

namespace ClinicManagementSystemFinal.UserControls_Doctors
{
    public partial class MedicalHistory : UserControl
    {
        readonly string userInfoId;
        readonly string doctorLoginId;
        readonly string clinicId;

        const string CONN =
            @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=B:\Downloads\Login.accdb;Persist Security Info=False;";
        public MedicalHistory(string uId, string pName, string photo, string cId, string docLogin)
        {
            InitializeComponent();
            userInfoId = uId;
            doctorLoginId = docLogin;
            clinicId = cId;

            lblName.Text = pName;
            if (System.IO.File.Exists(photo)) pbxProfilePatient.Load(photo);

            LoadHistoryGrid();

            btnSave.Click += btnSave_Click;
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        

        void LoadHistoryGrid()
        {
            using var c = new OleDbConnection(CONN);
            c.Open();
            var da = new OleDbDataAdapter(
                "SELECT NoteDate, Notes FROM DoctorNotes WHERE UserInfoID = ? AND DoctorLoginID = ? ORDER BY NoteDate DESC", c);
            da.SelectCommand.Parameters.AddWithValue("?", userInfoId);
            da.SelectCommand.Parameters.AddWithValue("?", doctorLoginId);
            var dt = new DataTable();
            da.Fill(dt);
            dgvHistory.DataSource = dt;
        }

        void btnSave_Click(object sender, EventArgs e)
        {
            using var c = new OleDbConnection(CONN);
            c.Open();
            var cmd = new OleDbCommand(
                "INSERT INTO DoctorNotes(UserInfoID,DoctorLoginID,NoteDate,Notes) VALUES(?,?,?,?)", c);
            cmd.Parameters.AddWithValue("?", userInfoId);
            cmd.Parameters.AddWithValue("?", doctorLoginId);
            cmd.Parameters.AddWithValue("?", DateTime.Now);
            cmd.Parameters.AddWithValue("?", txtNote.Text.Trim());
            cmd.ExecuteNonQuery();
            txtNote.Clear();
            LoadHistoryGrid();
        }
    }
}
