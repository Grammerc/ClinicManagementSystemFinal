using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Runtime.Intrinsics.Arm;
using System.Windows.Forms;

namespace ClinicManagementSystemFinal.UserControls_Doctors
{
    public partial class EditClinic : UserControl
    {
        private readonly int _clinicId;
        private string _selectedImagePath;
        private const string CONN =
            @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\Raphael\Downloads\Login.accdb;Persist Security Info=False;";

        public EditClinic(int clinicId)
        {
            InitializeComponent();
            _clinicId = clinicId;

            Load += EditClinic_Load;
            btnChange.Click += BtnChange_Click;
            btnSave.Click += BtnSave_Click;
            btnAddPerson.Click += BtnAddPerson_Click;
        }

        private void EditClinic_Load(object sender, EventArgs e)
        {
            using var conn = new OleDbConnection(CONN);
            conn.Open();

            // 1) Load basic clinic info:
            using (var cmd = new OleDbCommand(
                "SELECT ClinicName, Address, PhoneNumber, Email, IIF(Picture IS NULL,'',Picture) AS PicPath " +
                "  FROM Clinics WHERE ClinicID = ?", conn))
            {
                cmd.Parameters.AddWithValue("?", _clinicId);
                using var r = cmd.ExecuteReader();
                if (r.Read())
                {
                    tbxName.Text = r["ClinicName"].ToString();
                    tbxAddress.Text = r["Address"].ToString();
                    tbxPhoneNumber.Text = r["PhoneNumber"].ToString();
                    tbxEmail.Text = r["Email"].ToString();
                    _selectedImagePath = r["PicPath"].ToString();

                    if (File.Exists(_selectedImagePath))
                    {
                        pbxClinic.Image?.Dispose();
                        pbxClinic.Image = Image.FromFile(_selectedImagePath);
                        pbxClinic.SizeMode = PictureBoxSizeMode.Zoom;
                    }
                }
            }

            // 2) Load insurance coverages, marking each as assigned or not:
            dgvInsurance.Rows.Clear();
            using (var cmd = new OleDbCommand(
                "SELECT IC.CoverageID, IC.CoverageName, " +
                "       IIF(EXISTS(" +
                "         SELECT 1 FROM ClinicInsurance CI " +
                "          WHERE CI.ClinicID = ? AND CI.CoverageID = IC.CoverageID), True, False) AS Assigned " +
                "  FROM InsuranceCoverages IC", conn))
            {
                cmd.Parameters.AddWithValue("?", _clinicId);
                using var rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    int covId = rdr.GetInt32(0);
                    string covName = rdr.GetString(1);
                    object raw = rdr.GetValue(2);

                    bool assigned;
                    if (raw is bool b)
                        assigned = b;
                    else
                        assigned = Convert.ToInt32(raw) != 0;

                    dgvInsurance.Rows.Add(covId, covName, assigned);
                }
            }

            // 3) Load time slots, marking each as assigned or not:
            dgvTime.Rows.Clear();
            using (var cmdAll = new OleDbCommand("SELECT SlotID, SlotText FROM TimeSlots ORDER BY SlotID", conn))
            using (var rdrAll = cmdAll.ExecuteReader())
            {
                // cache the assigned ones
                var assigned = new HashSet<int>();
                using (var cmdAssigned = new OleDbCommand(
                    "SELECT SlotID FROM ClinicTimeSlots WHERE ClinicID = ?", conn))
                {
                    cmdAssigned.Parameters.AddWithValue("?", _clinicId);
                    using var r2 = cmdAssigned.ExecuteReader();
                    while (r2.Read()) assigned.Add(r2.GetInt32(0));
                }

                while (rdrAll.Read())
                {
                    var slotId = rdrAll.GetInt32(0);
                    dgvTime.Rows.Add(
                        slotId,
                        rdrAll.GetString(1),
                        assigned.Contains(slotId)
                    );
                }
            }
        }

        private void BtnChange_Click(object sender, EventArgs e)
        {
            using var dlg = new OpenFileDialog
            {
                Title = "Select Clinic Picture",
                Filter = "Image Files|*.png;*.jpg;*.jpeg;*.bmp"
            };
            if (dlg.ShowDialog() != DialogResult.OK) return;

            _selectedImagePath = dlg.FileName;
            pbxClinic.Image?.Dispose();
            pbxClinic.Image = Image.FromFile(_selectedImagePath);
            pbxClinic.SizeMode = PictureBoxSizeMode.Zoom;
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            using var conn = new OleDbConnection(CONN);
            conn.Open();

            // 1) Update the Clinics table as before…
            using (var cmd = new OleDbCommand(
                "UPDATE Clinics SET ClinicName=?, Address=?, PhoneNumber=?, Email=?, Picture=? WHERE ClinicID=?", conn))
            {
                cmd.Parameters.AddWithValue("?", tbxName.Text.Trim());
                cmd.Parameters.AddWithValue("?", tbxAddress.Text.Trim());
                cmd.Parameters.AddWithValue("?", tbxPhoneNumber.Text.Trim());
                cmd.Parameters.AddWithValue("?", tbxEmail.Text.Trim());
                cmd.Parameters.AddWithValue("?", _selectedImagePath ?? "");
                cmd.Parameters.AddWithValue("?", _clinicId);
                cmd.ExecuteNonQuery();
            }

            // 2) Sync Insurance coverages
            //   a) load all currently-assigned CoverageIDs into a HashSet
            var existingIns = new HashSet<int>();
            using (var load = new OleDbCommand(
                "SELECT CoverageID FROM ClinicInsurance WHERE ClinicID=?", conn))
            {
                load.Parameters.AddWithValue("?", _clinicId);
                using var r = load.ExecuteReader();
                while (r.Read()) existingIns.Add(r.GetInt32(0));
            }

            //   b) for each row in the grid, insert/delete as needed
            foreach (DataGridViewRow row in dgvInsurance.Rows)
            {
                if (row.IsNewRow) continue;
                int covId = Convert.ToInt32(row.Cells["CoverageID"].Value);
                bool assigned = Convert.ToBoolean(row.Cells["Assigned"].Value);

                if (assigned && !existingIns.Contains(covId))
                {
                    using var ins = new OleDbCommand(
                        "INSERT INTO ClinicInsurance (ClinicID, CoverageID) VALUES (?, ?)",
                        conn);
                    ins.Parameters.AddWithValue("?", _clinicId);
                    ins.Parameters.AddWithValue("?", covId);
                    ins.ExecuteNonQuery();
                }
                else if (!assigned && existingIns.Contains(covId))
                {
                    using var del = new OleDbCommand(
                        "DELETE FROM ClinicInsurance WHERE ClinicID=? AND CoverageID=?",
                        conn);
                    del.Parameters.AddWithValue("?", _clinicId);
                    del.Parameters.AddWithValue("?", covId);
                    del.ExecuteNonQuery();
                }
            }

            // 3) Sync TimeSlots exactly the same way
            var existingSlots = new HashSet<int>();
            using (var load = new OleDbCommand(
                "SELECT SlotID FROM ClinicTimeSlots WHERE ClinicID=?", conn))
            {
                load.Parameters.AddWithValue("?", _clinicId);
                using var r = load.ExecuteReader();
                while (r.Read()) existingSlots.Add(r.GetInt32(0));
            }

            foreach (DataGridViewRow row in dgvTime.Rows)
            {
                if (row.IsNewRow) continue;
                int slotId = Convert.ToInt32(row.Cells["SlotID"].Value);
                bool wanted = Convert.ToBoolean(row.Cells["AssignedTime"].Value);

                if (wanted && !existingSlots.Contains(slotId))
                {
                    using var ins = new OleDbCommand(
                        "INSERT INTO ClinicTimeSlots (ClinicID, SlotID) VALUES (?, ?)",
                        conn);
                    ins.Parameters.AddWithValue("?", _clinicId);
                    ins.Parameters.AddWithValue("?", slotId);
                    ins.ExecuteNonQuery();
                }
                else if (!wanted && existingSlots.Contains(slotId))
                {
                    using var del = new OleDbCommand(
                        "DELETE FROM ClinicTimeSlots WHERE ClinicID=? AND SlotID=?",
                        conn);
                    del.Parameters.AddWithValue("?", _clinicId);
                    del.Parameters.AddWithValue("?", slotId);
                    del.ExecuteNonQuery();
                }
            }

            MessageBox.Show("Clinic information saved.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtnAddPerson_Click(object sender, EventArgs e)
        {
            var username = tbxUsername.Text.Trim();
            if (string.IsNullOrEmpty(username))
            {
                MessageBox.Show("Please enter the doctor’s username.", "Missing Username",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using var conn = new OleDbConnection(CONN);
            conn.Open();

            using var cmd = new OleDbCommand(
                "SELECT LoginID FROM Account WHERE username = ?", conn);
            cmd.Parameters.AddWithValue("?", username);
            var obj = cmd.ExecuteScalar();
            if (obj == null)
            {
                MessageBox.Show("No such account found.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int userLoginId = Convert.ToInt32(obj);
            using var add = new OleDbCommand(
                "INSERT INTO Doctors(LoginID,ClinicID) VALUES(?, ?)", conn);
            add.Parameters.AddWithValue("?", userLoginId);
            add.Parameters.AddWithValue("?", _clinicId);
            add.ExecuteNonQuery();

            MessageBox.Show("Doctor added to clinic.", "Success",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // no-op
        private void label1_Click(object sender, EventArgs e) { }
    }
}