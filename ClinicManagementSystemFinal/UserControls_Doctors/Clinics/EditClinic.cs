﻿using System;
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

        public event EventHandler ChangesSaved;

        public EditClinic(int clinicId)
        {
            InitializeComponent();
            _clinicId = clinicId;
            btnDelete.Click += btnDelete_Click;

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

                using (var cmd = new OleDbCommand(
                    "SELECT ClinicName, Address, PhoneNumber, Email, Picture " +
                    "FROM Clinics WHERE ClinicID = ?", conn))
                {
                    cmd.Parameters.AddWithValue("?", _clinicId);
                    using var r = cmd.ExecuteReader();
                    if (r.Read())
                    {
                        tbxClinicName.Text = r["ClinicName"]?.ToString() ?? string.Empty;
                        tbxAddress.Text = r["Address"]?.ToString() ?? string.Empty;
                        tbxPhoneNumber.Text = r["PhoneNumber"]?.ToString() ?? string.Empty;
                        tbxEmail.Text = r["Email"]?.ToString() ?? string.Empty;

                        object pictureData = r["Picture"];
                        if (pictureData != DBNull.Value)
                        {
                            try
                            {
                                if (pictureData is byte[] bytes)
                                {
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
                        MessageBox.Show("Clinic not found.", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                LoadInsuranceAndTimeSlots(conn);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading clinic data: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadInsuranceAndTimeSlots(OleDbConnection conn)
        {
            // Load insurance coverages
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
                    dgvInsurance.Rows.Add(
                        rdr.GetInt32(0),
                        rdr.GetString(1),
                        Convert.ToBoolean(rdr.GetValue(2))
                    );
                }
            }

            dgvTime.Rows.Clear();
            using (var cmdAll = new OleDbCommand("SELECT SlotID, SlotText FROM TimeSlots ORDER BY SlotID", conn))
            using (var rdrAll = cmdAll.ExecuteReader())
            {
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
                Filter = "Image Files|*.png;*.jpg;*.jpeg;*.bmp",
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures)
            };

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    _currentImageBytes = File.ReadAllBytes(dlg.FileName);

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

                string updateSql = "UPDATE Clinics SET ClinicName=?, Address=?, PhoneNumber=?, Email=?, Picture=? WHERE ClinicID=?";
                
                using (var cmd = new OleDbCommand(updateSql, conn))
                {
                    cmd.Parameters.AddWithValue("?", tbxClinicName.Text.Trim());
                    cmd.Parameters.AddWithValue("?", tbxAddress.Text.Trim());
                    cmd.Parameters.AddWithValue("?", tbxPhoneNumber.Text.Trim());
                    cmd.Parameters.AddWithValue("?", tbxEmail.Text.Trim());
                    
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

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to delete this clinic? This action cannot be undone.",
                "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
            {
                return;
            }

            try
            {
                using var conn = new OleDbConnection(CONN);
                conn.Open();

                using (var cmd = new OleDbCommand("DELETE FROM ClinicTimeSlots WHERE ClinicID = ?", conn))
                {
                    cmd.Parameters.AddWithValue("?", _clinicId);
                    cmd.ExecuteNonQuery();
                }

                using (var cmd = new OleDbCommand("DELETE FROM ClinicInsurance WHERE ClinicID = ?", conn))
                {
                    cmd.Parameters.AddWithValue("?", _clinicId);
                    cmd.ExecuteNonQuery();
                }

                using (var cmd = new OleDbCommand("DELETE FROM Doctors WHERE ClinicID = ?", conn))
                {
                    cmd.Parameters.AddWithValue("?", _clinicId);
                    cmd.ExecuteNonQuery();
                }

                using (var cmd = new OleDbCommand("DELETE FROM Clinics WHERE ClinicID = ?", conn))
                {
                    cmd.Parameters.AddWithValue("?", _clinicId);
                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("Clinic has been deleted successfully.", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                ChangesSaved?.Invoke(this, EventArgs.Empty);
                ParentForm?.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting clinic: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void label1_Click(object sender, EventArgs e) { }
    }
}