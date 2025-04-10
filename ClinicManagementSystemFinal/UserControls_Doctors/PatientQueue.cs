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

namespace ClinicManagementSystemFinal.UserControls_Doctors
{
    public partial class PatientQueue : UserControl
    {
        private string doctorLoginId;
        private List<string> displayedAppointmentIds = new List<string>();
        public PatientQueue(string loginId)
        {
            InitializeComponent();
            HideAllPatientPanels();
            cbxDate.Value = DateTime.Today;
            cbxDate.ValueChanged += cbxDate_ValueChanged;
            doctorLoginId = loginId;
            LoadDoctorClinics();

        }

        private void HideAllPatientPanels()
        {
            for (int i = 1; i <= 6; i++)
            {
                var panel = Controls.Find($"panelPatient{i}", true).FirstOrDefault();
                if (panel is Panel) panel.Visible = false;
            }
        }



        private void LoadDoctorClinics()
        {
            cbxPatientQueue.Items.Clear();

            string connStr = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=B:\Downloads\Login.accdb;Persist Security Info=False;";
            using (OleDbConnection conn = new OleDbConnection(connStr))
            {
                conn.Open();
                string query = @"SELECT C.ClinicID, C.ClinicName 
                         FROM Clinics C 
                         INNER JOIN Doctors D ON C.ClinicID = D.ClinicID 
                         WHERE D.LoginID = @loginId";

                OleDbCommand cmd = new OleDbCommand(query, conn);
                cmd.Parameters.AddWithValue("@loginId", doctorLoginId);

                using (OleDbDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        cbxPatientQueue.Items.Add(new ComboBoxItem
                        {
                            Text = reader["ClinicName"].ToString(),
                            Value = reader["ClinicID"].ToString()
                        });
                    }
                }

                conn.Close();
            }
        }

        public class ComboBoxItem
        {
            public string Text { get; set; }
            public string Value { get; set; }
            public override string ToString() => Text;

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void cbxPatientQueue_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxPatientQueue.SelectedItem is ComboBoxItem selectedClinic)
            {
                string clinicId = selectedClinic.Value;

                string connStr = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=B:\Downloads\Login.accdb;Persist Security Info=False;";
                using (OleDbConnection conn = new OleDbConnection(connStr))
                {
                    conn.Open();

                    OleDbCommand cmd = new OleDbCommand("SELECT Address FROM Clinics WHERE ClinicID = @clinicId", conn);
                    cmd.Parameters.AddWithValue("@clinicId", clinicId);

                    object result = cmd.ExecuteScalar();
                    lblClinicLocation.Text = result != null ? result.ToString() : "Unknown";

                    conn.Close();
                }

                LoadPatientQueue(clinicId, cbxDate.Value.Date);
            }
        }

        private void LoadPatientQueue(string clinicId, DateTime selectedDate)
        {
            string connStr = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=B:\Downloads\Login.accdb;Persist Security Info=False;";
            using (OleDbConnection conn = new OleDbConnection(connStr))
            {
                conn.Open();

                string query = @"
                SELECT TOP 6 A.AppointmentID, I.Name, I.ProfilePicture
                FROM Appointments A
                INNER JOIN Information I ON A.UserInfoID = I.UserInfoID
                WHERE A.ClinicID = @clinicID AND A.AppointmentDate = @apptDate AND A.Status = 'Pending'
                ORDER BY A.AppointmentID ASC";

                OleDbCommand cmd = new OleDbCommand(query, conn);
                cmd.Parameters.AddWithValue("@clinicID", clinicId);
                cmd.Parameters.AddWithValue("@apptDate", selectedDate);

                using (OleDbDataReader reader = cmd.ExecuteReader())
                {
                    int index = 1;
                    bool hasPatients = false;

                    for (int i = 1; i <= 6; i++)
                    {
                        var panel = Controls.Find($"panelPatient{i}", true).FirstOrDefault();
                        var nameLabel = Controls.Find($"lblName{i}", true).FirstOrDefault();
                        var pictureBox = Controls.Find($"pbxProfile{i}", true).FirstOrDefault();

                        if (panel is Panel) panel.Visible = false;
                        if (nameLabel is Label lbl) lbl.Text = "";
                        if (pictureBox is PictureBox pbx) pbx.Image = null;
                    }

                    displayedAppointmentIds = new List<string>();
                    while (reader.Read() && index <= 6)
                    {
                        hasPatients = true;
                        if (hasPatients)
                        {
                            panelInvisible.Visible = false;
                        }
                        string name = reader["Name"].ToString();
                        byte[] photo = reader["ProfilePicture"] != DBNull.Value ? (byte[])reader["ProfilePicture"] : null;

                        var nameLabel = Controls.Find($"lblName{index}", true).FirstOrDefault() as Label;
                        var pictureBox = Controls.Find($"pbxProfile{index}", true).FirstOrDefault() as PictureBox;
                        var panel = Controls.Find($"panelPatient{index}", true).FirstOrDefault() as Panel;

                        if (nameLabel != null) nameLabel.Text = name;
                        if (pictureBox != null && photo != null)
                        {
                            using (MemoryStream ms = new MemoryStream(photo))
                            {
                                pictureBox.Image = Image.FromStream(ms);
                            }
                        }
                        if (panel != null) panel.Visible = true;

                        index++;
                        displayedAppointmentIds.Add(reader["AppointmentID"].ToString());


                        panelInvisible.Visible = !hasPatients;

                    }

                    conn.Close();
                }
            }
        }

        private void cbxDate_ValueChanged(object sender, EventArgs e)
        {
            if (cbxPatientQueue.SelectedItem is ComboBoxItem selectedClinic)
            {
                LoadPatientQueue(selectedClinic.Value, cbxDate.Value.Date);
            }
        }

        private void panelPatient3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pbxStatus1_Click(object sender, EventArgs e)
        {
            if (displayedAppointmentIds.Count >= 1)
            {
                StatusChange statusForm = new StatusChange(displayedAppointmentIds[0], pbxStatus1);
                statusForm.ShowDialog();
            }
        }
    }
}
