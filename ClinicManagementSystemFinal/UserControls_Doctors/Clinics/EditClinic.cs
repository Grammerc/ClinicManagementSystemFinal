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
        private byte[] _currentImageBytes;
        private const string CONN =
            @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\Raphael\source\repos\ClinicManagementSystemFinal\ClinicManagementSystemFinal\Login.accdb;Persist Security Info=False;";

        // Add an event to notify when changes are saved
        public event EventHandler ChangesSaved;

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
            try
        {
            using var conn = new OleDbConnection(CONN);
            conn.Open();

                // Debug: Show the SQL query
                string sql = "SELECT ClinicName, Address, PhoneNumber, Email, Picture " +
                            "FROM Clinics WHERE ClinicID = ?";
                MessageBox.Show($"Executing query: {sql}\nWith ClinicID: {_clinicId}", "Debug Info");

            // 1) Load basic clinic info:
                using (var cmd = new OleDbCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("?", _clinicId);
                using var r = cmd.ExecuteReader();
                if (r.Read())
                {
                        // Debug: Show what we got from the database
                        string debugInfo = $"ClinicName: {r["ClinicName"]}\n" +
                                         $"Address: {r["Address"]}\n" +
                                         $"PhoneNumber: {r["PhoneNumber"]}\n" +
                                         $"Email: {r["Email"]}";
                        MessageBox.Show(debugInfo, "Loaded Data");

                        // Make sure to set the clinic name
                        tbxClinicName.Text = r["ClinicName"]?.ToString() ?? string.Empty;
                        tbxAddress.Text = r["Address"]?.ToString() ?? string.Empty;
                        tbxPhoneNumber.Text = r["PhoneNumber"]?.ToString() ?? string.Empty;
                        tbxEmail.Text = r["Email"]?.ToString() ?? string.Empty;

                        // Handle the Picture field
                        object pictureData = r["Picture"];
                        if (pictureData != DBNull.Value)
                        {
                            try
                            {
                                if (pictureData is byte[] bytes)
                                {
                                    // Handle OLE Object data
                                    _currentImageBytes = bytes;
                                    using (var ms = new MemoryStream(_currentImageBytes))
                                    {
                                        pbxClinic.Image?.Dispose();
                                        pbxClinic.Image = Image.FromStream(ms);
                                        pbxClinic.SizeMode = PictureBoxSizeMode.Zoom;
                                    }
                                }
                                else if (pictureData is string path && File.Exists(path))
                                {
                                    // Handle legacy path data
                                    _currentImageBytes = File.ReadAllBytes(path);
                                    using (var ms = new MemoryStream(_currentImageBytes))
                                    {
                                        pbxClinic.Image?.Dispose();
                                        pbxClinic.Image = Image.FromStream(ms);
                                        pbxClinic.SizeMode = PictureBoxSizeMode.Zoom;
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show($"Error loading image: {ex.Message}", "Error",
                                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show($"No clinic found with ID: {_clinicId}", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading clinic data: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnChange_Click(object sender, EventArgs e)
        {
            using var dlg = new OpenFileDialog
            {
                Title = "Select Clinic Picture",
                Filter = "Image Files|*.png;*.jpg;*.jpeg;*.bmp",
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures)
            };

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    // Read the image file into a byte array
                    _currentImageBytes = File.ReadAllBytes(dlg.FileName);

                    // Update the preview
                    using (var ms = new MemoryStream(_currentImageBytes))
                    {
            pbxClinic.Image?.Dispose();
                        pbxClinic.Image = Image.FromStream(ms);
            pbxClinic.SizeMode = PictureBoxSizeMode.Zoom;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error loading image: {ex.Message}", "Error", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                using var conn = new OleDbConnection(CONN);
                conn.Open();

                // First verify the clinic exists
                using (var checkCmd = new OleDbCommand("SELECT COUNT(*) FROM Clinics WHERE ClinicID = ?", conn))
                {
                    checkCmd.Parameters.AddWithValue("?", _clinicId);
                    int count = (int)checkCmd.ExecuteScalar();
                    if (count == 0)
                    {
                        MessageBox.Show("Clinic not found in database.", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                // Update the Clinics table
                string updateSql = "UPDATE Clinics SET ClinicName=?, Address=?, PhoneNumber=?, Email=?, Picture=? WHERE ClinicID=?";
                
                using (var cmd = new OleDbCommand(updateSql, conn))
                {
                    // Add parameters in the exact order they appear in the SQL
                    cmd.Parameters.AddWithValue("?", tbxClinicName.Text.Trim());
                    cmd.Parameters.AddWithValue("?", tbxAddress.Text.Trim());
                    cmd.Parameters.AddWithValue("?", tbxPhoneNumber.Text.Trim());
                    cmd.Parameters.AddWithValue("?", tbxEmail.Text.Trim());
                    
                    // Handle the image parameter
                    if (_currentImageBytes != null)
                    {
                        cmd.Parameters.AddWithValue("?", _currentImageBytes);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("?", DBNull.Value);
                    }
                    
                    cmd.Parameters.AddWithValue("?", _clinicId);
                    
                    int rowsAffected = cmd.ExecuteNonQuery();
                    
                    if (rowsAffected > 0)
                    {
                        // Save time slots
                        using (var deleteCmd = new OleDbCommand("DELETE FROM ClinicTimeSlots WHERE ClinicID = ?", conn))
                        {
                            deleteCmd.Parameters.AddWithValue("?", _clinicId);
                            deleteCmd.ExecuteNonQuery();
                        }

                        foreach (DataGridViewRow row in dgvTime.Rows)
                        {
                            if (row.Cells[2].Value is bool isSelected && isSelected)
                            {
                                int slotId = Convert.ToInt32(row.Cells[0].Value);
                                using (var insertCmd = new OleDbCommand(
                                    "INSERT INTO ClinicTimeSlots (ClinicID, SlotID) VALUES (?, ?)", conn))
                                {
                                    insertCmd.Parameters.AddWithValue("?", _clinicId);
                                    insertCmd.Parameters.AddWithValue("?", slotId);
                                    insertCmd.ExecuteNonQuery();
                                }
                            }
                        }

                        // Save insurance coverages
                        using (var deleteCmd = new OleDbCommand("DELETE FROM ClinicInsurance WHERE ClinicID = ?", conn))
                        {
                            deleteCmd.Parameters.AddWithValue("?", _clinicId);
                            deleteCmd.ExecuteNonQuery();
                        }

                        foreach (DataGridViewRow row in dgvInsurance.Rows)
                        {
                            if (row.Cells[2].Value is bool isSelected && isSelected)
                            {
                                int coverageId = Convert.ToInt32(row.Cells[0].Value);
                                using (var insertCmd = new OleDbCommand(
                                    "INSERT INTO ClinicInsurance (ClinicID, CoverageID) VALUES (?, ?)", conn))
                                {
                                    insertCmd.Parameters.AddWithValue("?", _clinicId);
                                    insertCmd.Parameters.AddWithValue("?", coverageId);
                                    insertCmd.ExecuteNonQuery();
                                }
                            }
                        }

                        MessageBox.Show("Clinic information saved successfully!", "Success", 
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ChangesSaved?.Invoke(this, EventArgs.Empty);
                    }
                    else
                    {
                        MessageBox.Show("No changes were saved. Please check if the clinic exists.", "Warning",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving changes: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnAddPerson_Click(object sender, EventArgs e)
        {
            var username = tbxUsername.Text.Trim();
            if (string.IsNullOrEmpty(username))
            {
                MessageBox.Show("Please enter the doctor's username.", "Missing Username",
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