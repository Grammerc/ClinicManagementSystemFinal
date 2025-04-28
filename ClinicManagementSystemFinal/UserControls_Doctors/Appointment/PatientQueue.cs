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
        private readonly Panel _patientTemplate;
        private string doctorLoginId;
        private List<string> displayedAppointmentIds = new List<string>();
        public event Action<string, string, string, string> PatientSelected;
        private string clinicPicFolder =
    @"C:\Users\Raphael Perocho\source\repos\ClinicManagementSystemFinal\ProjectClinic\ClinicManagementSystemFinal\Pictures\ClinicPictures\";

        private const string CONN =
  @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=B:\Downloads\Login.accdb;Persist Security Info=False;";
        private string[] picExt = { ".png", ".jpg", ".jpeg" };

        public PatientQueue(string loginId)
        {
            InitializeComponent();
            doctorLoginId = loginId;

            _patientTemplate = panelTemplate;
            _patientTemplate.Visible = false;

            cbxDate.Value = DateTime.Today;
            cbxDate.ValueChanged += cbxDate_ValueChanged;

            LoadDoctorClinics();
            cbxPatientQueue.SelectedIndexChanged += cbxPatientQueue_SelectedIndexChanged;

            if (cbxPatientQueue.Items.Count > 0)
            {
                cbxPatientQueue.SelectedIndex = 0;
                var item = (ComboBoxItem)cbxPatientQueue.SelectedItem;
                LoadPatientQueue(item.Value, cbxDate.Value.Date);
            }
            else
            {
                panelInvisible.Visible = true;
                cbxDate.Enabled = false;
            }

            // Hook up single status button instead of six
            pbxStatus.Click += StatusIcon_Click;
        }


        void StatusIcon_Click(object sender, EventArgs e)
        {
            const int idx = 1; // we only have one status icon
            if (displayedAppointmentIds.Count >= idx)
            {
                var btn = sender as Guna.UI2.WinForms.Guna2ImageButton;
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
                "Cancelled" => "cancelled.png",   
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
            flpPatients.Controls.Clear();
            displayedAppointmentIds.Clear();

            using var conn = new OleDbConnection(CONN);
            conn.Open();
            const string sql = @"
SELECT
    A.AppointmentID,
    A.Status,
    A.UserInfoID,
    I.Name,
    I.ProfileImagePath,
    I.ProfilePicture,
    A.ReasonForVisit,
    A.TimeSlot
  FROM (Appointments A
        INNER JOIN Information I ON A.UserInfoID = I.UserInfoID)
       INNER JOIN Doctors D ON A.DoctorID = D.DoctorID
 WHERE D.LoginID       = ?
   AND A.ClinicID      = ?
   AND A.AppointmentDate >= ? 
   AND A.AppointmentDate < ?
 ORDER BY A.TimeSlot ASC, A.AppointmentDate";

            using var cmd = new OleDbCommand(sql, conn);
            cmd.Parameters.AddWithValue("?", doctorLoginId);
            cmd.Parameters.AddWithValue("?", clinicId);
            cmd.Parameters.AddWithValue("?", selectedDate.Date);
            cmd.Parameters.AddWithValue("?", selectedDate.Date.AddDays(1));

            using var reader = cmd.ExecuteReader();

            int idx = 1;
            while (reader.Read())
            {
                // pull fields
                var apptId = reader["AppointmentID"].ToString();
                var status = reader["Status"].ToString();
                var userId = reader["UserInfoID"].ToString();
                var name = reader["Name"].ToString();
                var imgPath = reader["ProfileImagePath"].ToString();
                var blob = reader["ProfilePicture"] as byte[];
                var reason = reader["ReasonForVisit"]?.ToString() ?? "";
                var timeslot = reader["TimeSlot"]?.ToString() ?? "";

                // build profile thumbnail once
                Image pic = null;
                if (!string.IsNullOrEmpty(imgPath) && File.Exists(imgPath))
                {
                    pic = Image.FromFile(imgPath);
                }
                else if (blob != null && blob.Length > 0)
                {
                    using var ms = new MemoryStream(blob);
                    pic = Image.FromStream(ms);
                }

                // clone the template panel
                var card = new Panel
                {
                    Size = _patientTemplate.Size,
                    Margin = _patientTemplate.Margin,
                    Padding = _patientTemplate.Padding,
                    BackColor = _patientTemplate.BackColor
                };

                // deep-clone each control inside it
                foreach (Control src in _patientTemplate.Controls)
                {
                    if (src is Panel srcPanel)
                    {
                        // copy the panel itself
                        var panelCopy = new Panel
                        {
                            Name = srcPanel.Name,
                            Size = srcPanel.Size,
                            Location = srcPanel.Location,
                            BackColor = srcPanel.BackColor,
                            Padding = srcPanel.Padding,
                            Margin = srcPanel.Margin
                        };

                        // now clone its children (Label, PictureBox, Guna2ImageButton)
                        foreach (Control child in srcPanel.Controls)
                        {
                            Control childCopy = null;
                            switch (child)
                            {
                                case Label l:
                                    childCopy = new Label
                                    {
                                        Name = l.Name,
                                        Size = l.Size,
                                        Location = l.Location,
                                        Font = l.Font,
                                        ForeColor = l.ForeColor,
                                        TextAlign = l.TextAlign,
                                        BackColor = Color.Transparent,
                                        AutoSize = l.AutoSize
                                    };
                                    break;
                                case PictureBox pb:
                                    childCopy = new PictureBox
                                    {
                                        Name = pb.Name,
                                        Size = pb.Size,
                                        Location = pb.Location,
                                        SizeMode = pb.SizeMode,
                                        BackColor = Color.Transparent
                                    };
                                    break;
                                case Guna2ImageButton gb:
                                    childCopy = new Guna2ImageButton
                                    {
                                        Name = gb.Name,
                                        Size = gb.Size,
                                        Location = gb.Location,
                                        ImageSize = gb.ImageSize
                                    };

                                    break;
                            }
                            if (childCopy != null)
                                panelCopy.Controls.Add(childCopy);
                        }

                        // add the cloned panel (with its children) to the card
                        card.Controls.Add(panelCopy);
                    }
                }

                // now bind data:

                var lblNum = card.Controls.Find("number", true)
                      .OfType<Label>()
                      .First();
                lblNum.Text = "#" + idx;

                // #2) patient name
                var lblName = card.Controls.Find("lblName", true)
                                       .OfType<Label>()
                                       .First();
                // ← set the text and click handler here
                lblName.Text = name;
                lblName.Cursor = Cursors.Hand;
                lblName.Click += (s, e) => PatientSelected?.Invoke(userId, name, imgPath, clinicId);

                // Reason
                // Reason
                var lblReason = card.Controls
                                     .Find("lblReason", true)
                                     .OfType<Label>()
                                     .FirstOrDefault();
                if (lblReason != null) lblReason.Text = reason;

                // Time slot
                var lblTime = card.Controls
                                   .Find("lblTime", true)
                                   .OfType<Label>()
                                   .FirstOrDefault();
                if (lblTime != null) lblTime.Text = timeslot;

                // #3) profile picture
                var pbxProfile = card.Controls.Find("pbxProfile", true)
                                          .OfType<PictureBox>()      // or Guna2ImageButton if you switched back
                                          .First();
                pbxProfile.Image = pic;
                pbxProfile.SizeMode = PictureBoxSizeMode.Zoom;
                pbxProfile.Cursor = Cursors.Hand;
                pbxProfile.Click += (s, e) => PatientSelected?.Invoke(userId, name, imgPath, clinicId);

                // #4) status icon
                var pbxStatus = card.Controls.Find("pbxStatus", true)
                                         .OfType<Guna2ImageButton>()
                                         .First();
                pbxStatus.Image = GetStatusIcon(status);
                pbxStatus.Click += StatusIcon_Click;

                // finally…
                flpPatients.Controls.Add(card);
                displayedAppointmentIds.Add(apptId);
                idx++;
            }

            panelInvisible.Visible = (displayedAppointmentIds.Count == 0);
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
