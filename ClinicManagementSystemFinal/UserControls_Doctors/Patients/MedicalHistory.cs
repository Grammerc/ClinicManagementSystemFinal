using System;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace ClinicManagementSystemFinal.UserControls_Doctors
{
    public partial class MedicalHistory : UserControl
    {
        private readonly int _userInfoId;
        private readonly int _doctorLoginId;
        private readonly string _clinicId;

        private const string CONN =
            @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\Raphael\source\repos\ClinicManagementSystemFinal\ClinicManagementSystemFinal\Login.accdb;Persist Security Info=False;";

        public MedicalHistory(string uId, string pName, string photo, string cId, string docLogin)
        {
            InitializeComponent();

            _userInfoId = int.Parse(uId);
            _doctorLoginId = int.Parse(docLogin);
            _clinicId = cId;

            lblName.Text = pName;
            if (File.Exists(photo))
                pbxProfilePatient.Load(photo);

            LoadHistoryGrid();
            btnSave.Click += btnSave_Click;
        }

        private void LoadHistoryGrid()
        {
            using var conn = new OleDbConnection(CONN);
            conn.Open();

            var da = new OleDbDataAdapter(
                "SELECT NoteDate, Notes " +
                "FROM DoctorNotes " +
                "WHERE UserInfoID = ? AND DoctorLoginID = ? " +
                "ORDER BY NoteDate DESC",
                conn);

            da.SelectCommand.Parameters.Add("?", OleDbType.Integer).Value = _userInfoId;
            da.SelectCommand.Parameters.Add("?", OleDbType.Integer).Value = _doctorLoginId;

            var dt = new DataTable();
            da.Fill(dt);
            dgvHistory.DataSource = dt;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            using var conn = new OleDbConnection(CONN);
            conn.Open();

            using var cmd = new OleDbCommand(
                "INSERT INTO DoctorNotes (UserInfoID, DoctorLoginID, NoteDate, Notes) " +
                "VALUES (?, ?, ?, ?)",
                conn);

            cmd.Parameters.Add("?", OleDbType.Integer).Value = _userInfoId;
            cmd.Parameters.Add("?", OleDbType.Integer).Value = _doctorLoginId;

            cmd.Parameters.Add("?", OleDbType.Date).Value = DateTime.Now;

            cmd.Parameters.Add("?", OleDbType.VarChar).Value = txtNote.Text.Trim();

            cmd.ExecuteNonQuery();

            txtNote.Clear();
            LoadHistoryGrid();
        }

        private void LoadVitalsDates()
        {
            try
            {
                using (var conn = new OleDbConnection(CONN))
                {
                    conn.Open();
                    string sql = @"
                        SELECT DISTINCT DateValue(VitalsDate) as VitalsDate
                        FROM PatientVitals
                        WHERE PatientID = ?
                        ORDER BY VitalsDate DESC";

                    using (var cmd = new OleDbCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("?", _userInfoId);
                        using (var rdr = cmd.ExecuteReader())
                        {
                            cboVitalsDate.Items.Clear();
                            while (rdr.Read())
                            {
                                DateTime vitalsDate = rdr.GetDateTime(0);
                                cboVitalsDate.Items.Add(vitalsDate.ToString("MM/dd/yyyy"));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading vitals dates: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAddVitals_Click(object sender, EventArgs e)
        {
            try
            {
                using (var conn = new OleDbConnection(CONN))
                {
                    conn.Open();
                    string sql = @"
                        INSERT INTO PatientVitals (PatientID, VitalsDate, BloodPressure, 
                                                 HeartRate, Temperature, Weight, Height, 
                                                 OxygenSaturation, Notes)
                        VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?)";

                    using (var cmd = new OleDbCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("?", _userInfoId);
                        cmd.Parameters.AddWithValue("?", DateTime.Now);
                        cmd.Parameters.AddWithValue("?", txtBloodPressure.Text);
                        cmd.Parameters.AddWithValue("?", txtHeartRate.Text);
                        cmd.Parameters.AddWithValue("?", txtTemperature.Text);
                        cmd.Parameters.AddWithValue("?", txtWeight.Text);
                        cmd.Parameters.AddWithValue("?", txtHeight.Text);
                        cmd.Parameters.AddWithValue("?", txtOxygenSaturation.Text);
                        cmd.Parameters.AddWithValue("?", txtVitalsNotes.Text);

                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Vitals recorded successfully!", "Success",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        
                        // Refresh the dates combobox
                        LoadVitalsDates();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error recording vitals: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cboVitalsDate_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboVitalsDate.SelectedItem == null) return;

            try
            {
                using (var conn = new OleDbConnection(CONN))
                {
                    conn.Open();
                    string sql = @"
                        SELECT BloodPressure, HeartRate, Temperature, Weight, Height,
                               OxygenSaturation, Notes
                        FROM PatientVitals
                        WHERE PatientID = ? AND DateValue(VitalsDate) = DateValue(?)
                        ORDER BY VitalsDate DESC";

                    using (var cmd = new OleDbCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("?", _userInfoId);
                        cmd.Parameters.AddWithValue("?", DateTime.Parse(cboVitalsDate.SelectedItem.ToString()));
                        
                        using (var rdr = cmd.ExecuteReader())
                        {
                            if (rdr.Read())
                            {
                                txtBloodPressure.Text = rdr["BloodPressure"].ToString();
                                txtHeartRate.Text = rdr["HeartRate"].ToString();
                                txtTemperature.Text = rdr["Temperature"].ToString();
                                txtWeight.Text = rdr["Weight"].ToString();
                                txtHeight.Text = rdr["Height"].ToString();
                                txtOxygenSaturation.Text = rdr["OxygenSaturation"].ToString();
                                txtVitalsNotes.Text = rdr["Notes"].ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading vitals: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}