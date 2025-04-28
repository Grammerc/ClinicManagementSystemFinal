using System;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
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
            Load += EditClinic_Load;
            btnSave.Click += BtnSave_Click;
            btnAddPerson.Click += BtnAddPerson_Click;
        }

        private void EditClinic_Load(object sender, EventArgs e)
        {
            using var conn = new OleDbConnection(CONN);
            conn.Open();

            var cmdClinic = new OleDbCommand(
                "SELECT ClinicName, Address, PhoneNumber, Email FROM Clinics WHERE ClinicID = ?", conn);
            cmdClinic.Parameters.AddWithValue("?", _clinicId);
            using var rdrClinic = cmdClinic.ExecuteReader();
            if (rdrClinic.Read())
            {
                tbxName.Text = rdrClinic["ClinicName"].ToString();
                tbxAddress.Text = rdrClinic["Address"].ToString();
                tbxPhoneNumber.Text = rdrClinic["PhoneNumber"].ToString();
                tbxEmail.Text = rdrClinic["Email"].ToString();
            }

            dgvInsurance.Rows.Clear();
            var cmdIns = new OleDbCommand(
                @"SELECT IC.CoverageID,
             IC.CoverageName,
             IIF(CI.CoverageID IS NULL, False, True) AS Assigned
        FROM InsuranceCoverages AS IC
        LEFT JOIN ClinicInsurance   AS CI
          ON IC.CoverageID = CI.CoverageID
       WHERE CI.ClinicID = ? 
          OR CI.CoverageID IS NULL", conn);
            cmdIns.Parameters.AddWithValue("?", _clinicId);

            using var rdrIns = cmdIns.ExecuteReader();
            while (rdrIns.Read())
            {
                int covId = rdrIns.GetInt32(0);
                string covName = rdrIns.GetString(1);
                object raw = rdrIns.GetValue(2);

                bool assigned;
                if (raw is bool b)
                    assigned = b;
                else
                    assigned = Convert.ToInt32(raw) != 0;

                dgvInsurance.Rows.Add(covId, covName, assigned);
            }

            var assignedSlots = new HashSet<int>();
            using (var cmdLoadSlots = new OleDbCommand(
                   "SELECT SlotID FROM ClinicTimeSlots WHERE ClinicID = ?", conn))
            {
                cmdLoadSlots.Parameters.AddWithValue("?", _clinicId);
                using var rdrLoad = cmdLoadSlots.ExecuteReader();
                while (rdrLoad.Read())
                    assignedSlots.Add(rdrLoad.GetInt32(0));
            }

            // now pull every slot
            dgvTime.Rows.Clear();
            using (var cmdAllSlots = new OleDbCommand(
                   "SELECT SlotID, SlotText FROM TimeSlots ORDER BY SlotText", conn))
            using (var rdrAll = cmdAllSlots.ExecuteReader())
            {
                while (rdrAll.Read())
                {
                    int slotId = rdrAll.GetInt32(0);
                    string text = rdrAll.GetString(1);
                    bool isAssigned = assignedSlots.Contains(slotId);
                    dgvTime.Rows.Add(slotId, text, isAssigned);
                }
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            using var conn = new OleDbConnection(CONN);
            conn.Open();

            var cmdUpdate = new OleDbCommand(
                "UPDATE Clinics SET Address = ?, PhoneNumber = ?, Email = ? WHERE ClinicID = ?", conn);
            cmdUpdate.Parameters.AddWithValue("?", tbxAddress.Text.Trim());
            cmdUpdate.Parameters.AddWithValue("?", tbxPhoneNumber.Text.Trim());
            cmdUpdate.Parameters.AddWithValue("?", tbxEmail.Text.Trim());
            cmdUpdate.Parameters.AddWithValue("?", _clinicId);
            cmdUpdate.ExecuteNonQuery();

            var existingIns = new HashSet<int>();
            var cmdLoadIns = new OleDbCommand(
                "SELECT CoverageID FROM ClinicInsurance WHERE ClinicID = ?", conn);
            cmdLoadIns.Parameters.AddWithValue("?", _clinicId);
            using (var rdr = cmdLoadIns.ExecuteReader())
                while (rdr.Read()) existingIns.Add(rdr.GetInt32(0));

            foreach (DataGridViewRow row in dgvInsurance.Rows)
            {
                if (row.IsNewRow) continue;
                int covId = Convert.ToInt32(row.Cells[0].Value);
                bool assigned = Convert.ToBoolean(row.Cells["Assigned"].Value);

                if (assigned && !existingIns.Contains(covId))
                {
                    var cmd = new OleDbCommand(
                        "INSERT INTO ClinicInsurance(ClinicID, CoverageID) VALUES(?, ?)", conn);
                    cmd.Parameters.AddWithValue("?", _clinicId);
                    cmd.Parameters.AddWithValue("?", covId);
                    cmd.ExecuteNonQuery();
                }
                else if (!assigned && existingIns.Contains(covId))
                {
                    var cmd = new OleDbCommand(
                        "DELETE FROM ClinicInsurance WHERE ClinicID = ? AND CoverageID = ?", conn);
                    cmd.Parameters.AddWithValue("?", _clinicId);
                    cmd.Parameters.AddWithValue("?", covId);
                    cmd.ExecuteNonQuery();
                }
            }

            var existingSlots = new HashSet<int>();
            using (var cmdDb = new OleDbCommand(
                   "SELECT SlotID FROM ClinicTimeSlots WHERE ClinicID = ?", conn))
            {
                cmdDb.Parameters.AddWithValue("?", _clinicId);
                using var rdrDb = cmdDb.ExecuteReader();
                while (rdrDb.Read())
                    existingSlots.Add(rdrDb.GetInt32(0));
            }

            // walk the grid and insert/delete as needed
            foreach (DataGridViewRow row in dgvTime.Rows)
            {
                if (row.IsNewRow) continue;
                int slotId = Convert.ToInt32(row.Cells["SlotID"].Value);
                bool wanted = Convert.ToBoolean(row.Cells["AssignedTime"].Value);

                if (wanted && !existingSlots.Contains(slotId))
                {
                    using var cmdIns = new OleDbCommand(
                        "INSERT INTO ClinicTimeSlots(ClinicID, SlotID) VALUES(?,?)", conn);
                    cmdIns.Parameters.AddWithValue("?", _clinicId);
                    cmdIns.Parameters.AddWithValue("?", slotId);
                    cmdIns.ExecuteNonQuery();
                }
                else if (!wanted && existingSlots.Contains(slotId))
                {
                    using var cmdDel = new OleDbCommand(
                        "DELETE FROM ClinicTimeSlots WHERE ClinicID=? AND SlotID=?", conn);
                    cmdDel.Parameters.AddWithValue("?", _clinicId);
                    cmdDel.Parameters.AddWithValue("?", slotId);
                    cmdDel.ExecuteNonQuery();
                }
            }

            MessageBox.Show("Clinic information saved.", "Success",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtnAddPerson_Click(object sender, EventArgs e)
        {
            var username = tbxUsername.Text.Trim();
            if (string.IsNullOrEmpty(username))
            {
                MessageBox.Show("Please enter the doctor’s username.", "Missing Username", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using var conn = new OleDbConnection(CONN);
            conn.Open();

            var cmdFind = new OleDbCommand(
                "SELECT LoginID FROM Account WHERE username = ?", conn);
            cmdFind.Parameters.AddWithValue("?", username);
            var result = cmdFind.ExecuteScalar();

            if (result == null)
            {
                MessageBox.Show("No such account found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int userLoginId = Convert.ToInt32(result);

            if (MessageBox.Show(
                $"Add '{username}' to clinic '{tbxName.Text}'?",
                "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                != DialogResult.Yes)
            {
                return;
            }

            var cmdInsert = new OleDbCommand(
                "INSERT INTO Doctors(LoginID, ClinicID) VALUES(?, ?)", conn);
            cmdInsert.Parameters.AddWithValue("?", userLoginId);
            cmdInsert.Parameters.AddWithValue("?", _clinicId);
            cmdInsert.ExecuteNonQuery();

            MessageBox.Show("Doctor added to clinic.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}