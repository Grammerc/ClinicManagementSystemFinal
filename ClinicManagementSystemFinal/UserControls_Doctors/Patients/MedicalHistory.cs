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
            
            try
            {
                if (!string.IsNullOrEmpty(photo) && File.Exists(photo))
                {
                    using (var stream = new MemoryStream(File.ReadAllBytes(photo)))
                    {
                        pbxProfilePatient.Image?.Dispose();
                        pbxProfilePatient.Image = Image.FromStream(stream);
                        pbxProfilePatient.SizeMode = PictureBoxSizeMode.Zoom;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error loading profile picture: {ex.Message}");
            }

            LoadPatientProfile();
            LoadHistoryGrid();
            LoadVitalsDates();

            if (btnSaves != null)
                btnSaves.Click += btnSaves_Click;
            if (btnAddVital != null)
                btnAddVital.Click += btnAddVitals_Click;
            if (cbxDateVitals != null)
                cbxDateVitals.SelectedIndexChanged += cboVitalsDate_SelectedIndexChanged;
        }

        private void LoadPatientProfile()
        {
            try
            {
                using (var conn = new OleDbConnection(CONN))
                {
                    conn.Open();
                    string sql = @"
                        SELECT 
                            I.BloodType, 
                            I.Gender,
                            (SELECT MAX(AppointmentDate) 
                             FROM Appointments 
                             WHERE UserInfoID = ? AND Status = 'Completed') as LastAppointment
                        FROM Information I
                        WHERE I.UserInfoID = ?";

                    using (var cmd = new OleDbCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("?", _userInfoId);
                        cmd.Parameters.AddWithValue("?", _userInfoId);
                        
                        using (var rdr = cmd.ExecuteReader())
                        {
                            if (rdr.Read())
                            {
                                lblBloodType.Text = rdr["BloodType"]?.ToString() ?? "-";
                                lblGender.Text = rdr["Gender"]?.ToString() ?? "-";
                                
                                if (rdr["LastAppointment"] != DBNull.Value)
                                {
                                    DateTime lastAppointment = Convert.ToDateTime(rdr["LastAppointment"]);
                                    lblLastAppointment.Text = lastAppointment.ToString("MM/dd/yyyy");
                                }
                                else
                                {
                                    lblLastAppointment.Text = "-";
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading patient profile: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadHistoryGrid()
        {
            try
            {
                if (dgvHistory == null)
                {
                    System.Diagnostics.Debug.WriteLine("DataGridView not initialized.");
                    return;
                }

                using var conn = new OleDbConnection(CONN);
                conn.Open();

                var da = new OleDbDataAdapter(
                    @"SELECT ID, Format(NoteDate,'mm/dd/yyyy hh:nn AMPM') as NoteDate, Notes 
                    FROM DoctorNotes 
                    WHERE UserInfoID = ? AND DoctorLoginID = ? 
                    ORDER BY NoteDate DESC",
                    conn);

                da.SelectCommand.Parameters.Add("?", OleDbType.Integer).Value = _userInfoId;
                da.SelectCommand.Parameters.Add("?", OleDbType.Integer).Value = _doctorLoginId;

                var dt = new DataTable();
                da.Fill(dt);

                dgvHistory.DataSource = null;
                dgvHistory.Columns.Clear();
                dgvHistory.DataSource = dt;

                dgvHistory.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
                dgvHistory.AllowUserToAddRows = false;
                dgvHistory.AllowUserToDeleteRows = false;
                dgvHistory.ReadOnly = true;

                try
                {
                    if (dgvHistory.Columns.Contains("ID"))
                    {
                        var idColumn = dgvHistory.Columns["ID"];
                        if (idColumn != null)
                        {
                            idColumn.Visible = false;
                        }
                    }

                    if (dgvHistory.Columns.Contains("NoteDate"))
                    {
                        var dateColumn = dgvHistory.Columns["NoteDate"];
                        if (dateColumn != null)
                        {
                            dateColumn.HeaderText = "Date & Time";
                            dateColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                        }
                    }

                    if (dgvHistory.Columns.Contains("Notes"))
                    {
                        var notesColumn = dgvHistory.Columns["Notes"];
                        if (notesColumn != null)
                        {
                            notesColumn.HeaderText = "Doctor's Notes";
                            notesColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                        }
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Error formatting columns: {ex.Message}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading history: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                System.Diagnostics.Debug.WriteLine($"Detailed error: {ex}");
            }
        }

        private void LoadVitalsDates()
        {
            try
            {
                using (var conn = new OleDbConnection(CONN))
                {
                    conn.Open();
                    string sql = @"
                        SELECT DISTINCT DateValue(DateRecorded) as RecordDate
                        FROM PatientVitals
                        WHERE UserInfoID = ?
                        ORDER BY DateValue(DateRecorded) DESC";

                    using (var cmd = new OleDbCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("?", _userInfoId);
                        using (var rdr = cmd.ExecuteReader())
                        {
                            cbxDateVitals.Items.Clear();
                            while (rdr.Read())
                            {
                                if (rdr["RecordDate"] != DBNull.Value)
                                {
                                    DateTime vitalsDate = Convert.ToDateTime(rdr["RecordDate"]);
                                    cbxDateVitals.Items.Add(vitalsDate.ToString("MM/dd/yyyy"));
                                }
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

        private void btnSaves_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtNote == null)
                {
                    MessageBox.Show("Note control not initialized.", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtNote.Text))
                {
                    MessageBox.Show("Please enter a note before saving.", "Warning",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                using (var conn = new OleDbConnection(CONN))
                {
                    conn.Open();
                    using (var cmd = new OleDbCommand(
                        "INSERT INTO DoctorNotes (UserInfoID, DoctorLoginID, NoteDate, Notes) " +
                        "VALUES (?, ?, ?, ?)",
                        conn))
                    {
                        cmd.Parameters.Add("?", OleDbType.Integer).Value = _userInfoId;
                        cmd.Parameters.Add("?", OleDbType.Integer).Value = _doctorLoginId;
                        cmd.Parameters.Add("?", OleDbType.Date).Value = DateTime.Now;
                        cmd.Parameters.Add("?", OleDbType.LongVarChar).Value = txtNote.Text.Trim();

                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Note saved successfully!", "Success",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        
                        txtNote.Clear();
                        LoadHistoryGrid();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving note: {ex.Message}\n\nDetails: {ex.StackTrace}", "Error",
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
                        INSERT INTO PatientVitals 
                            (ClinicID, DateRecorded, BloodPressure, HeartRate, 
                             Temperature, Weight, Height, Notes)
                        VALUES (?, ?, ?, ?, ?, ?, ?, ?)";

                    using (var cmd = new OleDbCommand(sql, conn))
                    {
                        cmd.Parameters.Add("?", OleDbType.Integer).Value = int.Parse(_clinicId);
                        cmd.Parameters.Add("?", OleDbType.Date).Value = DateTime.Now;
                        cmd.Parameters.Add("?", OleDbType.VarChar).Value = tbxBloodPressure.Text.Trim();
                        cmd.Parameters.Add("?", OleDbType.VarChar).Value = tbxHeartRate.Text.Trim();
                        cmd.Parameters.Add("?", OleDbType.VarChar).Value = tbxTemperature.Text.Trim();
                        cmd.Parameters.Add("?", OleDbType.VarChar).Value = tbxWeight.Text.Trim();
                        cmd.Parameters.Add("?", OleDbType.VarChar).Value = tbxHeight.Text.Trim();
                        cmd.Parameters.Add("?", OleDbType.LongVarChar).Value = txtVitalNotes.Text.Trim();

                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Vitals recorded successfully!", "Success",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        
                        tbxBloodPressure.Clear();
                        tbxHeartRate.Clear();
                        tbxTemperature.Clear();
                        tbxWeight.Clear();
                        tbxHeight.Clear();
                        txtVitalNotes.Clear();
                        
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
            if (cbxDateVitals.SelectedItem == null) return;

            try
            {
                using (var conn = new OleDbConnection(CONN))
                {
                    conn.Open();
                    string sql = @"
                        SELECT BloodPressure, HeartRate, Temperature, Weight, Height,
                               Notes
                        FROM PatientVitals
                        WHERE UserInfoID = ? AND DateValue(DateRecorded) = DateValue(?)";

                    using (var cmd = new OleDbCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("?", _userInfoId);
                        cmd.Parameters.AddWithValue("?", DateTime.Parse(cbxDateVitals.SelectedItem.ToString()));
                        
                        using (var rdr = cmd.ExecuteReader())
                        {
                            if (rdr.Read())
                            {
                                tbxBloodPressure.Text = rdr["BloodPressure"]?.ToString() ?? "";
                                tbxHeartRate.Text = rdr["HeartRate"]?.ToString() ?? "";
                                tbxTemperature.Text = rdr["Temperature"]?.ToString() ?? "";
                                tbxWeight.Text = rdr["Weight"]?.ToString() ?? "";
                                tbxHeight.Text = rdr["Height"]?.ToString() ?? "";
                                txtVitalNotes.Text = rdr["Notes"]?.ToString() ?? "";
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