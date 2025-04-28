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
using ClinicManagementSystemFinal.UserControls_Doctors;

namespace ClinicManagementSystemFinal
{
    public partial class UserInformation : UserControl
    {
        private readonly string _loginId;
        private string _selectedImagePath;
        private const string CONN =
            @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=B:\Downloads\Login.accdb;Persist Security Info=False;";
        public UserInformation(string loginId)
        {
            InitializeComponent();
            _loginId = loginId;
            this.Load += UserInformation_Load;
            btnSave.Click += BtnSave_Click;
            btnDelete.Click += BtnDelete_Click;
            btnProfile.Click += BtnProfile_Click;
        }

        private void tbxAllergies_TextChanged(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void tabPage6_Click(object sender, EventArgs e)
        {

        }

        private void UserInformation_Load(object sender, EventArgs e)
        {
            using var conn = new OleDbConnection(CONN);
            conn.Open();

            var cmd = new OleDbCommand(@"
        SELECT 
            A.username,
            A.[Name]        AS FullName,
            I.Email,
            I.City,
            I.PhoneNumber,
            I.EmergencyContact,
            I.EmergencyPhone,
            I.ProfileImagePath
        FROM Account AS A
        INNER JOIN Information AS I
          ON A.LoginID = I.LoginID
        WHERE A.LoginID = ?
    ", conn);
            cmd.Parameters.AddWithValue("?", _loginId);

            using var rdr = cmd.ExecuteReader();
            if (rdr.Read())
            {
                tbxUsername.Text = rdr["username"]?.ToString() ?? "";
                tbxName.Text = rdr["FullName"]?.ToString() ?? "";
                tbxEmail.Text = rdr["Email"]?.ToString() ?? "";
                tbxCity.Text = rdr["City"]?.ToString() ?? "";
                tbxPhone.Text = rdr["PhoneNumber"]?.ToString() ?? "";
                tbxEmergency.Text = rdr["EmergencyContact"]?.ToString() ?? "";
                tbxEmergencyPhone.Text = rdr["EmergencyPhone"]?.ToString() ?? "";

                var path = rdr["ProfileImagePath"]?.ToString();
                if (!string.IsNullOrEmpty(path) && File.Exists(path))
                {
                    _selectedImagePath = path;
                    pbxProfile.Image = Image.FromFile(path);
                    pbxProfile.SizeMode = PictureBoxSizeMode.Zoom;
                }
            }
        }

        private void BtnProfile_Click(object sender, EventArgs e)
        {
            using var dlg = new OpenFileDialog { Filter = "Image Files|*.png;*.jpg;*.jpeg;*.bmp" };
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                _selectedImagePath = dlg.FileName;
                pbxProfile.Image?.Dispose();
                pbxProfile.Image = Image.FromFile(dlg.FileName);
                pbxProfile.SizeMode = PictureBoxSizeMode.Zoom;
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            using var conn = new OleDbConnection(CONN);
            conn.Open();
            using var tran = conn.BeginTransaction();

            var cmdAcc = new OleDbCommand(
                "UPDATE Account SET username = ?, [Name] = ? WHERE LoginID = ?", conn, tran);
            cmdAcc.Parameters.AddWithValue("?", tbxUsername.Text.Trim());
            cmdAcc.Parameters.AddWithValue("?", tbxName.Text.Trim());
            cmdAcc.Parameters.AddWithValue("?", _loginId);
            cmdAcc.ExecuteNonQuery();

            var cmdInfo = new OleDbCommand(
                "UPDATE Information SET Email = ?, City = ?, PhoneNumber = ?, " +
                "EmergencyContact = ?, EmergencyPhone = ?, ProfileImagePath = ? " +
                "WHERE LoginID = ?", conn, tran);
            cmdInfo.Parameters.AddWithValue("?", tbxEmail.Text.Trim());
            cmdInfo.Parameters.AddWithValue("?", tbxCity.Text.Trim());
            cmdInfo.Parameters.AddWithValue("?", tbxPhone.Text.Trim());
            cmdInfo.Parameters.AddWithValue("?", tbxEmergency.Text.Trim());
            cmdInfo.Parameters.AddWithValue("?", tbxEmergencyPhone.Text.Trim());
            cmdInfo.Parameters.AddWithValue("?", _selectedImagePath ?? "");
            cmdInfo.Parameters.AddWithValue("?", _loginId);
            cmdInfo.ExecuteNonQuery();

            tran.Commit();
            MessageBox.Show("Your changes have been saved.", "Success",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(
                "Are you sure you want to delete your account? This cannot be undone.",
                "Confirm Deletion",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning) != DialogResult.Yes)
                return;

            using var conn = new OleDbConnection(CONN);
            conn.Open();
            using var tran = conn.BeginTransaction();

            var cmdInfo = new OleDbCommand(
                "DELETE FROM Information WHERE LoginID = ?", conn, tran);
            cmdInfo.Parameters.AddWithValue("?", _loginId);
            cmdInfo.ExecuteNonQuery();

            var cmdAcc = new OleDbCommand(
                "DELETE FROM Account WHERE LoginID = ?", conn, tran);
            cmdAcc.Parameters.AddWithValue("?", _loginId);
            cmdAcc.ExecuteNonQuery();

            tran.Commit();

            MessageBox.Show("Your account has been deleted.", "Goodbye",
                MessageBoxButtons.OK, MessageBoxIcon.Information);

            var host = this.FindForm() as HomePage_Doctor;
            host?.LoadControl(new FrontPage());
        }
    }
}
