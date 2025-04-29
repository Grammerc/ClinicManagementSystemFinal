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

namespace ClinicManagementSystemFinal.UserControls_Doctors.Clinics
{
    public partial class AddClinic : UserControl
    {

        private readonly int _loginId;
        public event EventHandler ClinicAdded;
        // adjust your path as needed
        private const string CONN =
            @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\Raphael\Downloads\Login.accdb;Persist Security Info=False;";

        public AddClinic(int loginId)
        {
            _loginId = loginId;
            InitializeComponent();
            btnAdd.Click += BtnAdd_Click;
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            var name = tbxSearch.Text.Trim();
            var address = tbxAddress.Text.Trim();
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(address))
            {
                MessageBox.Show("Please enter both a clinic name and address.",
                                "Missing Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using var conn = new OleDbConnection(CONN);
                conn.Open();

                // 1) Insert clinic
                using var cmd = new OleDbCommand(
                    "INSERT INTO Clinics (ClinicName, Address) VALUES (?, ?)", conn);
                cmd.Parameters.AddWithValue("?", name);
                cmd.Parameters.AddWithValue("?", address);
                cmd.ExecuteNonQuery();

                // 2) Get its new ID
                using var idCmd = new OleDbCommand("SELECT @@IDENTITY", conn);
                int newClinicId = Convert.ToInt32(idCmd.ExecuteScalar());

                // 3) Assign this user as a doctor/secretary there
                using var link = new OleDbCommand(
                    "INSERT INTO Doctors (LoginID, ClinicID) VALUES (?, ?)", conn);
                link.Parameters.AddWithValue("?", _loginId);
                link.Parameters.AddWithValue("?", newClinicId);
                link.ExecuteNonQuery();

                MessageBox.Show("Clinic added and linked to your account.",
                                "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                ClinicAdded?.Invoke(this, EventArgs.Empty);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving clinic:\n" + ex.Message,
                                "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
