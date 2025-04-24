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
using System.IO;
using Guna.UI2.WinForms;

namespace ClinicManagementSystemFinal.UserControls_Doctors
{
    
    public partial class PatientQueue : UserControl
    {
        private string doctorLoginId;
        private List<string> displayedAppointmentIds = new List<string>();
        public event Action<string, string, string, string> PatientSelected;
        private string clinicPicFolder =
    @"C:\Users\Raphael Perocho\source\repos\ClinicManagementSystemFinal\ProjectClinic\ClinicManagementSystemFinal\Pictures\ClinicPictures\";

        private string[] picExt = { ".png", ".jpg", ".jpeg" };

        public PatientQueue(string loginId)
        {
            InitializeComponent();

            doctorLoginId = loginId;

            HideAllPatientPanels();
            cbxDate.Value = DateTime.Today;  
            cbxDate.ValueChanged += cbxDate_ValueChanged;

            LoadDoctorClinics();
            cbxPatientQueue.SelectedIndexChanged += cbxPatientQueue_SelectedIndexChanged;
            if (cbxPatientQueue.Items.Count > 0)
                cbxPatientQueue.SelectedIndex = 0;
            LoadPatientQueue(((ComboBoxItem)cbxPatientQueue.SelectedItem).Value, cbxDate.Value.Date);

            foreach (var btn in new[] { pbxStatus1, pbxStatus2, pbxStatus3, pbxStatus4, pbxStatus5, pbxStatus6 })
                btn.Click += StatusIcon_Click;
        }

      
        private void HideAllPatientPanels()
        {
            for (int i = 1; i <= 6; i++)
            {
                var panel = Controls.Find($"panelPatient{i}", true).FirstOrDefault();
                if (panel is Panel) panel.Visible = false;
            }
        }

        void StatusIcon_Click(object sender, EventArgs e)
        {
            var btn = sender as Guna.UI2.WinForms.Guna2ImageButton;
            int idx = int.Parse(btn.Name.Substring(btn.Name.Length - 1));    // 1-6
            if (displayedAppointmentIds.Count >= idx)
            {
                var sc = new StatusChange(displayedAppointmentIds[idx - 1], btn, this);
                sc.Show(this);   
            }
        }

        void ShowClinicPhoto(string clinicName)
        {
            string path = picExt
                .Select(ext => System.IO.Path.Combine(clinicPicFolder, clinicName + ext))
                .FirstOrDefault(System.IO.File.Exists);

            if (path != null)
            {
                if (pbxClinic.Image != null) { var old = pbxClinic.Image; pbxClinic.Image = null; old.Dispose(); }
                pbxClinic.Image = Image.FromFile(path);
                pbxClinic.SizeMode = PictureBoxSizeMode.Zoom;
            }
            else
                pbxClinic.Image = null;      // or a placeholder if you prefer
        }

        static readonly string IconDir =
    @"C:\Users\Raphael Perocho\source\repos\ClinicManagementSystemFinal\ProjectClinic\ClinicManagementSystemFinal\Pictures\StatusIcons";

        Image GetStatusIcon(string status)
        {
            string filename = status switch
            {
                "Approved" => "approved.png",
                "Completed" => "completed.png",
                "Cancelled" => "declined.png",   
                _ => "pending.png"
            };

            string path = System.IO.Path.Combine(IconDir, filename);
            return System.IO.File.Exists(path) ? Image.FromFile(path) : null;
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
                    if (cbxPatientQueue.Items.Count > 0)
                        ShowClinicPhoto(((ComboBoxItem)cbxPatientQueue.Items[0]).Text);

                    conn.Close();
                }

                LoadPatientQueue(clinicId, cbxDate.Value.Date);
            }
        }

        public void JumpToDate(DateTime d)
        {
            cbxDate.Value = d.Date;
        }

        public bool JumpToFirstClinicWithPatients(DateTime d)
        {
            for (int i = 0; i < cbxPatientQueue.Items.Count; i++)
            {
                var item = (ComboBoxItem)cbxPatientQueue.Items[i];
                if (CountPatients(item.Value, d) > 0)
                {
                    cbxPatientQueue.SelectedIndex = i;
                    return true;
                }
            }
            return false;
        }

        int CountPatients(string clinicId, DateTime d)
        {
            using var conn = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=B:\Downloads\Login.accdb;Persist Security Info=False;");
            conn.Open();
            var cmd = new OleDbCommand(
    @"SELECT COUNT(*) FROM Appointments A
      INNER JOIN Doctors D ON A.DoctorID = D.DoctorID
      WHERE D.LoginID = ?
        AND A.ClinicID = ?
        AND A.AppointmentDate >= ? AND A.AppointmentDate < ?", conn);
            cmd.Parameters.AddWithValue("?", doctorLoginId);
            cmd.Parameters.AddWithValue("?", clinicId);
            DateTime d0 = d.Date;
            DateTime d1 = d0.AddDays(1);
            cmd.Parameters.AddWithValue("?", d0);
            cmd.Parameters.AddWithValue("?", d1);
            return Convert.ToInt32(cmd.ExecuteScalar());
        }

        private void LoadPatientQueue(string clinicId, DateTime selectedDate)
        {
            string connStr = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=B:\Downloads\Login.accdb;Persist Security Info=False;";
            using (OleDbConnection conn = new OleDbConnection(connStr))
            {
                conn.Open();

                string query = @"
SELECT TOP 6
       A.AppointmentID,
       A.Status,
       A.ClinicID,
       A.UserInfoID,
       I.Name,
       I.ProfileImagePath,
       I.ProfilePicture
FROM   (Appointments  A
        INNER JOIN Information I ON A.UserInfoID = I.UserInfoID)
        INNER JOIN Doctors     D ON A.DoctorID    = D.DoctorID
WHERE  D.LoginID              = ?
  AND  A.ClinicID             = ?
  AND  A.AppointmentDate >= ? AND A.AppointmentDate < ?
ORDER  BY A.AppointmentDate, A.AppointmentID";

                OleDbCommand cmd = new OleDbCommand(query, conn);
                cmd.Parameters.AddWithValue("?", doctorLoginId);
                cmd.Parameters.AddWithValue("?", clinicId);
                DateTime d0 = selectedDate.Date;
                DateTime d1 = d0.AddDays(1);
                cmd.Parameters.AddWithValue("?", d0);
                cmd.Parameters.AddWithValue("?", d1);

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
                        panelInvisible.Visible = false;

                        string userId = reader["UserInfoID"].ToString();
                        string clinicIdRow = reader["ClinicID"].ToString();
                        string name = reader["Name"].ToString();
                        string imgPath = reader["ProfileImagePath"].ToString();
                        byte[] blob = reader["ProfilePicture"] as byte[];
                        string status = reader["Status"].ToString();

                        var nameLabel = Controls.Find($"lblName{index}", true)
                                        .FirstOrDefault() as Label;

                        var pictureBtn = Controls.Find($"pbxProfile{index}", true)
                                        .FirstOrDefault() as Guna2ImageButton;   // 🠘 cast corrected

                        var statusBox = Controls.Find($"pbxStatus{index}", true)
                                        .FirstOrDefault() as Guna2ImageButton;

                        var panel = Controls.Find($"panelPatient{index}", true)
                                        .FirstOrDefault() as Panel;

                        /* ---------- name label ---------- */
                        if (nameLabel != null)
                        {
                            nameLabel.Text = name;
                            nameLabel.Cursor = Cursors.Hand;
                            nameLabel.Click += (s, e) =>
                                PatientSelected?.Invoke(userId, name, imgPath, clinicIdRow);
                        }

                        /* ---------- profile thumbnail ---------- */
                        if (pictureBtn != null)
                        {
                            Image pic = null;

                            if (!string.IsNullOrWhiteSpace(imgPath) && File.Exists(imgPath))
                                pic = Image.FromFile(imgPath);
                            else if (blob != null && blob.Length > 0)
                                using (var ms = new MemoryStream(blob))
                                    pic = Image.FromStream(ms);

                            pictureBtn.Image = pic; // fallback
                            pictureBtn.ImageRotate = 0;
                            pictureBtn.Cursor = Cursors.Hand;
                            pictureBtn.Click += (s, e) =>
                                PatientSelected?.Invoke(userId, name, imgPath, clinicIdRow);
                        }

                        /* ---------- status icon ---------- */
                        if (statusBox != null)
                        {
                            statusBox.Image = GetStatusIcon(status);
                            statusBox.Tag = index - 1;
                        }

                        if (panel != null) panel.Visible = true;

                        displayedAppointmentIds.Add(reader["AppointmentID"].ToString());
                        index++;
                    }

                    panelInvisible.Visible = !hasPatients;




                    conn.Close();
                }
            }
        }

  

        public void RefreshCurrentQueue()
        {
            if (cbxPatientQueue.SelectedItem is ComboBoxItem sel)
                LoadPatientQueue(sel.Value, cbxDate.Value.Date);
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
                
            }
        }
    }
}
