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
            @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\Raphael\source\repos\ClinicManagementSystemFinal\ClinicManagementSystemFinal\Login.accdb;Persist Security Info=False;";
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
            try
            {
                using var conn = new OleDbConnection(CONN);
                conn.Open();

                var cmd = new OleDbCommand(@"
                    SELECT 
                        A.username,
                        I.Name,
                        I.Email,
                        I.Address,
                        I.PhoneNumber,
                        I.EmergencyContact,
                        I.ProfileImagePath
                    FROM Account AS A
                    LEFT JOIN Information AS I ON A.LoginID = I.LoginID
                    WHERE A.LoginID = ?", conn);

                cmd.Parameters.AddWithValue("?", _loginId);

                using var rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    tbxUsername.Text = rdr["username"]?.ToString() ?? "N/A";
                    tbxName.Text = rdr["Name"]?.ToString() ?? "N/A";
                    tbxEmail.Text = rdr["Email"]?.ToString() ?? "N/A";
                    tbxCity.Text = rdr["Address"]?.ToString() ?? "N/A";
                    tbxPhone.Text = rdr["PhoneNumber"]?.ToString() ?? "N/A";
                    tbxEmergency.Text = rdr["EmergencyContact"]?.ToString() ?? "N/A";
                    tbxEmergencyPhone.Text = rdr["PhoneNumber"]?.ToString() ?? "N/A";

                    var path = rdr["ProfileImagePath"]?.ToString();
                    if (!string.IsNullOrEmpty(path) && File.Exists(path))
                    {
                        _selectedImagePath = path;
                        if (pbxProfile.Image != null)
                        {
                            pbxProfile.Image.Dispose();
                            pbxProfile.Image = null;
                        }
                        pbxProfile.Image = Image.FromFile(path);
                        pbxProfile.SizeMode = PictureBoxSizeMode.Zoom;
                    }
                }
                else
                {
                    tbxUsername.Text = "N/A";
                    tbxName.Text = "N/A";
                    tbxEmail.Text = "N/A";
                    tbxCity.Text = "N/A";
                    tbxPhone.Text = "N/A";
                    tbxEmergency.Text = "N/A";
                    tbxEmergencyPhone.Text = "N/A";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading user information: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            try
            {
                using var conn = new OleDbConnection(CONN);
                conn.Open();
                using var tran = conn.BeginTransaction();

                // 1) Update Account table - only username
                var cmdAcc = new OleDbCommand(
                    "UPDATE Account SET username = ? WHERE LoginID = ?", conn, tran);
                cmdAcc.Parameters.AddWithValue("?", tbxUsername.Text.Trim());
                cmdAcc.Parameters.AddWithValue("?", _loginId);
                cmdAcc.ExecuteNonQuery();

                // 2) Check if Information record exists
                var checkCmd = new OleDbCommand(
                    "SELECT COUNT(*) FROM Information WHERE LoginID = ?", conn, tran);
                checkCmd.Parameters.AddWithValue("?", _loginId);
                int infoCount = (int)checkCmd.ExecuteScalar();

                // 3) Update or Insert Information
                if (infoCount > 0)
                {
                    // Update existing record
                    var cmdInfo = new OleDbCommand(
                        "UPDATE Information SET Name = ?, Email = ?, Address = ?, PhoneNumber = ?, EmergencyContact = ?, ProfileImagePath = ? WHERE LoginID = ?", 
                        conn, tran);
                    cmdInfo.Parameters.AddWithValue("?", tbxName.Text.Trim());
                    cmdInfo.Parameters.AddWithValue("?", tbxEmail.Text.Trim());
                    cmdInfo.Parameters.AddWithValue("?", tbxCity.Text.Trim());
                    cmdInfo.Parameters.AddWithValue("?", tbxPhone.Text.Trim());
                    cmdInfo.Parameters.AddWithValue("?", tbxEmergency.Text.Trim());
                    cmdInfo.Parameters.AddWithValue("?", _selectedImagePath ?? "");
                    cmdInfo.Parameters.AddWithValue("?", _loginId);
                    cmdInfo.ExecuteNonQuery();
                }
                else
                {
                    // Insert new record
                    var cmdInfo = new OleDbCommand(
                        "INSERT INTO Information (LoginID, Name, Email, Address, PhoneNumber, EmergencyContact, ProfileImagePath) VALUES (?, ?, ?, ?, ?, ?, ?)", 
                        conn, tran);
                    cmdInfo.Parameters.AddWithValue("?", _loginId);
                    cmdInfo.Parameters.AddWithValue("?", tbxName.Text.Trim());
                    cmdInfo.Parameters.AddWithValue("?", tbxEmail.Text.Trim());
                    cmdInfo.Parameters.AddWithValue("?", tbxCity.Text.Trim());
                    cmdInfo.Parameters.AddWithValue("?", tbxPhone.Text.Trim());
                    cmdInfo.Parameters.AddWithValue("?", tbxEmergency.Text.Trim());
                    cmdInfo.Parameters.AddWithValue("?", _selectedImagePath ?? "");
                    cmdInfo.ExecuteNonQuery();
                }

                tran.Commit();
                MessageBox.Show("Your information has been saved successfully.", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving information: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
