using System;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace ClinicManagementSystemFinal.UserControls_Doctors
{
    public partial class DoctorInformation : UserControl
    {
        private readonly string _loginId;
        private string _selectedImagePath;
        private const string CONN =
            @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=B:\Downloads\Login.accdb;Persist Security Info=False;";

        public DoctorInformation(string loginId)
        {
            InitializeComponent();
            _loginId = loginId;

            this.Load += DoctorInformation_Load;
            btnProfile.Click += BtnProfile_Click;
            btnSave.Click += BtnSave_Click;
        }

        private void DoctorInformation_Load(object sender, EventArgs e)
        {
            using var conn = new OleDbConnection(CONN);
            conn.Open();

            var cmd = new OleDbCommand(@"
        SELECT 
            A.username,
            D.DoctorName,
            I.Gender,
            D.Specialization,
            I.Email,
            I.ProfileImagePath
        FROM (Account AS A
              INNER JOIN Doctors     AS D ON A.LoginID = D.LoginID)
             INNER JOIN Information AS I ON A.LoginID = I.LoginID
        WHERE A.LoginID = ?", conn);

            cmd.Parameters.AddWithValue("?", _loginId);

            using var rdr = cmd.ExecuteReader();
            if (rdr.Read())
            {
                // Username and name
                tbxUsername.Text = rdr["username"]?.ToString() ?? "";
                tbxName.Text = rdr["DoctorName"]?.ToString() ?? "";

                // Gender radio buttons
                var gender = rdr["Gender"]?.ToString() ?? "";
                rbnMale.Checked = gender.Equals("Male", StringComparison.OrdinalIgnoreCase);
                rbnFemale.Checked = gender.Equals("Female", StringComparison.OrdinalIgnoreCase);

                cbxSpec.Items.Clear();
                using (var listCmd = new OleDbCommand(
                       "SELECT SpecializationName FROM Specializations ORDER BY SpecializationName", conn))
                using (var listRdr = listCmd.ExecuteReader())
                {
                    while (listRdr.Read())
                        cbxSpec.Items.Add(listRdr.GetString(0));
                }
                // Specialization and email
                string currentSpec = rdr["Specialization"]?.ToString() ?? "";
                if (!string.IsNullOrEmpty(currentSpec))
                    cbxSpec.SelectedItem = currentSpec;
                tbxEmail.Text = rdr["Email"]?.ToString() ?? "";

                // Profile picture
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
            using var dlg = new OpenFileDialog
            {
                Filter = "Image Files|*.png;*.jpg;*.jpeg;*.bmp"
            };
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                _selectedImagePath = dlg.FileName;
                pbxProfile.Image?.Dispose();
                pbxProfile.Image = Image.FromFile(_selectedImagePath);
                pbxProfile.SizeMode = PictureBoxSizeMode.Zoom;
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            using var conn = new OleDbConnection(CONN);
            conn.Open();
            using var tran = conn.BeginTransaction();

            // 1) Account → username
            var cmdAcc = new OleDbCommand(
                "UPDATE Account SET username = ? WHERE LoginID = ?", conn, tran);
            cmdAcc.Parameters.AddWithValue("?", tbxUsername.Text.Trim());
            cmdAcc.Parameters.AddWithValue("?", _loginId);
            cmdAcc.ExecuteNonQuery();

            // 2) Doctors → DoctorName, Specialization
            var cmdDoc = new OleDbCommand(
                "UPDATE Doctors SET DoctorName = ?, Specialization = ? WHERE LoginID = ?", conn, tran);
            cmdDoc.Parameters.AddWithValue("?", tbxName.Text.Trim());
            cmdDoc.Parameters.AddWithValue("?",
    cbxSpec.SelectedItem?.ToString() ?? "");
            cmdDoc.Parameters.AddWithValue("?", _loginId);
            cmdDoc.ExecuteNonQuery();

            // 3) Information → Gender, Email, ProfileImagePath
            var gender = rbnMale.Checked ? "Male" : "Female";
            var cmdInfo = new OleDbCommand(
                "UPDATE Information SET Gender = ?, Email = ?, ProfileImagePath = ? WHERE LoginID = ?", conn, tran);
            cmdInfo.Parameters.AddWithValue("?", gender);
            cmdInfo.Parameters.AddWithValue("?", tbxEmail.Text.Trim());
            cmdInfo.Parameters.AddWithValue("?", _selectedImagePath ?? "");
            cmdInfo.Parameters.AddWithValue("?", _loginId);
            cmdInfo.ExecuteNonQuery();

            tran.Commit();
            MessageBox.Show("Your changes have been saved.", "Success",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}