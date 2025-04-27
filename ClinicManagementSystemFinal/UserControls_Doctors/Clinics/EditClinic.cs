using System;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace ClinicManagementSystemFinal.UserControls_Doctors
{
    public partial class EditClinic : UserControl
    {
        readonly int _clinicId;
        const string CONN = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=B:\Downloads\Login.accdb;Persist Security Info=False;";

        public EditClinic(int clinicId)
        {
            InitializeComponent();
            _clinicId = clinicId;
            this.Load += EditClinic_Load;
            btnSave.Click += BtnSave_Click;
            btnAddPerson.Click += BtnAddPerson_Click;
        }

        private void EditClinic_Load(object sender, EventArgs e)
        {
            using var conn = new OleDbConnection(CONN);
            conn.Open();
            var cmd = new OleDbCommand(@"
                SELECT ClinicName, Address, PhoneNumber, Email, [Insurance Coverage], TimeSlot
                  FROM Clinics
                 WHERE ClinicID = ?", conn);
            cmd.Parameters.AddWithValue("?", _clinicId);
            using var rdr = cmd.ExecuteReader();
            if (rdr.Read())
            {
                tbxName.Text = rdr["ClinicName"]?.ToString() ?? "";
                tbxAddress.Text = rdr["Address"]?.ToString() ?? "";
                tbxPhoneNumber.Text = rdr["PhoneNumber"]?.ToString() ?? "";
                tbxEmail.Text = rdr["Email"]?.ToString() ?? "";

                dgvInsurance.Rows.Clear();
                var insRaw = rdr["Insurance Coverage"]?.ToString() ?? "";
                foreach (var ins in insRaw.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                    dgvInsurance.Rows.Add(ins.Trim());

                dgvTime.Rows.Clear();
                var timeRaw = rdr["TimeSlot"]?.ToString() ?? "";
                foreach (var ts in timeRaw.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                    dgvTime.Rows.Add(ts.Trim());
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            var address = tbxAddress.Text.Trim();
            var phone = tbxPhoneNumber.Text.Trim();
            var email = tbxEmail.Text.Trim();
            var insList = string.Join(", ",
                dgvInsurance.Rows
                    .OfType<DataGridViewRow>()
                    .Where(r => r.Cells[0].Value != null)
                    .Select(r => r.Cells[0].Value.ToString()));
            var timeList = string.Join(", ",
                dgvTime.Rows
                    .OfType<DataGridViewRow>()
                    .Where(r => r.Cells[0].Value != null)
                    .Select(r => r.Cells[0].Value.ToString()));

            using var conn = new OleDbConnection(CONN);
            conn.Open();
            var cmd = new OleDbCommand(@"
                UPDATE Clinics
                   SET Address = ?, PhoneNumber = ?, Email = ?, 
                       [Insurance Coverage] = ?, TimeSlot = ?
                 WHERE ClinicID = ?", conn);
            cmd.Parameters.AddWithValue("?", address);
            cmd.Parameters.AddWithValue("?", phone);
            cmd.Parameters.AddWithValue("?", email);
            cmd.Parameters.AddWithValue("?", insList);
            cmd.Parameters.AddWithValue("?", timeList);
            cmd.Parameters.AddWithValue("?", _clinicId);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Clinic information saved.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtnAddPerson_Click(object sender, EventArgs e)
        {
            var username = tbxName.Text.Trim();
            if (string.IsNullOrEmpty(username))
            {
                MessageBox.Show("Please enter the user’s username.", "Missing Username", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using var conn = new OleDbConnection(CONN);
            conn.Open();
            var cmdFind = new OleDbCommand("SELECT LoginID FROM Account WHERE [username] = ?", conn);
            cmdFind.Parameters.AddWithValue("?", username);
            var o = cmdFind.ExecuteScalar();
            if (o == null)
            {
                MessageBox.Show("No such account found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            int userLoginId = Convert.ToInt32(o);

            if (MessageBox.Show($"Add '{username}' to clinic '{tbxName.Text}'?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            var cmdIns = new OleDbCommand("INSERT INTO Doctors (LoginID, ClinicID) VALUES (?, ?)", conn);
            cmdIns.Parameters.AddWithValue("?", userLoginId);
            cmdIns.Parameters.AddWithValue("?", _clinicId);
            cmdIns.ExecuteNonQuery();
            MessageBox.Show("User added to clinic.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}