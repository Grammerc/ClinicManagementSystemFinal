using System;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using ClinicManagementSystemFinal.UserControls_Doctors;
using ClinicManagementSystemFinal.UserInterface;
using AppointmentUC = ClinicManagementSystemFinal.UserControls_Doctors.Appointment.AppointmentView_Doctors;
using ClinicManagementSystemFinal.UserControls_Doctors.Appointment;
using System.Runtime.InteropServices;

namespace ClinicManagementSystemFinal.UserControls_Doctors
{
    public partial class HomePage_Doctor : Form
    {
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

        private readonly string doctorLoginId;
        private readonly MyClinics myClinicsControl;
        private readonly AppointmentUC apptUC;
        private readonly PatientQueue queueUC;
        private readonly bool _isSecretary;
        private readonly AppointmentNotification _appointmentNotification;

        private const string CONN =
            @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\Raphael\source\repos\ClinicManagementSystemFinal\ClinicManagementSystemFinal\Login.accdb;Persist Security Info=False;";

        public HomePage_Doctor(string loginID, bool isSecretary = false)
        {
            InitializeComponent();
            doctorLoginId = loginID;
            _isSecretary = isSecretary;

            btnDashboard.Visible = !_isSecretary;
            btnCalendar.Visible = !_isSecretary;
            btnPatientQueue.Visible = !_isSecretary;
            panelDashboard.Enabled = !_isSecretary;
            panelCalendar.Enabled = !_isSecretary;
            panelPatientQueue.Enabled = !_isSecretary;

            btnMyClinics.Visible = true;
            btnAppointments.Visible = true;
            btnViewPatients.Visible = true;
            if (_isSecretary)
                pbxProfile.Click += (s, e) => ShowUserInformationPopup();
            else
                pbxProfile.Click += (s, e) => ShowDoctorDetailPopup();

            myClinicsControl = new MyClinics();
            apptUC = new AppointmentUC(doctorLoginId);
            queueUC = new PatientQueue(doctorLoginId);

            myClinicsControl.EditRequested += clinicId => LoadControl(new EditClinic(clinicId));
            queueUC.PatientSelected += (uid, name, photo, cid)
                                           => LoadControl(new MedicalHistory(uid, name, photo, cid, doctorLoginId));

            myClinicsControl.LoadMyClinics(doctorLoginId);

            _appointmentNotification = new AppointmentNotification();

            guna2Panel11.MouseDown += (s, e) =>
            {
                if (e.Button == MouseButtons.Left)
                {
                    ReleaseCapture();
                    SendMessage(this.Handle, 0x112, 0xf012, 0);
                }
            };
        }

        private void HomePage_Doctor_Load(object sender, EventArgs e)
        {
            try
            {
                LoadDoctorHeader();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error in LoadDoctorHeader:\n" + ex.Message, "Debug");
            }

            if (!_isSecretary)
                LoadControl(new Dashboard_Doctors(doctorLoginId));
        }

        private void LoadDoctorHeader()
        {
            const string sql = @"
SELECT 
  I.ProfileImagePath,
  I.ProfilePicture,
  I.Name,
  R.RoleName
FROM   (Account      AS A
        INNER JOIN Information AS I ON A.LoginID = I.LOGINID)
       LEFT JOIN Roles       AS R ON A.RoleID  = R.RoleID
WHERE  A.LoginID = ?";

            using var conn = new OleDbConnection(CONN);
            conn.Open();
            using var cmd = new OleDbCommand(sql, conn);
            cmd.Parameters.Add("?", OleDbType.Integer).Value = Convert.ToInt32(doctorLoginId);

            using var rdr = cmd.ExecuteReader();
            if (!rdr.Read()) return;

            Image img = null;
            var path = rdr["ProfileImagePath"].ToString();
            if (!string.IsNullOrEmpty(path) && File.Exists(path))
                img = Image.FromFile(path);
            else if (rdr["ProfilePicture"] != DBNull.Value)
            {
                var blob = (byte[])rdr["ProfilePicture"];
                using var ms = new MemoryStream(blob);
                img = Image.FromStream(ms);
            }
            pbxProfile.Image = img;

            lblName.Text = rdr["Name"].ToString();
            lblRole.Text = rdr["RoleName"].ToString();
        }

        public void LoadControl(Control c)
        {
            panelMainDesktop.Controls.Clear();
            if (c is Form f)
            {
                f.TopLevel = false;
                f.FormBorderStyle = FormBorderStyle.None;
                f.Dock = DockStyle.Fill;
                panelMainDesktop.Controls.Add(f);
                panelMainDesktop.Tag = f;
                f.Show();
            }
            else
            {
                c.Dock = DockStyle.Fill;
                panelMainDesktop.Controls.Add(c);
                panelMainDesktop.Tag = c;
                c.BringToFront();
            }
        }

        private void btnDashboard_Click(object sender, EventArgs e)
            => LoadControl(new Dashboard_Doctors(doctorLoginId));

        private void btnCalendar_Click(object sender, EventArgs e)
        {
            var cal = new Calendar_Doctor(doctorLoginId, apptUC);
            cal.DayClicked += d =>
            {
                queueUC.JumpToDate(d);
                if (!queueUC.JumpToFirstClinicWithPatients(d))
                    MessageBox.Show("No pending patients on " + d.ToShortDateString());
                LoadControl(queueUC);
            };
            LoadControl(cal);
        }

        private void btnPatientQueue_Click(object sender, EventArgs e)
            => LoadControl(queueUC);

        private void btnMyClinics_Click(object sender, EventArgs e)
            => LoadControl(myClinicsControl);

        private void btnAppointments_Click(object sender, EventArgs e)
            => LoadControl(apptUC);

        private void btnViewPatients_Click(object sender, EventArgs e)
            => LoadControl(new FindPeople(doctorLoginId, _isSecretary));

        private void btnLogOut_Click(object sender, EventArgs e)
        {
            Hide();
            new SignIn().Show();
        }

        private void ShowDoctorDetailPopup()
        {
            LoadControl(new DoctorInformation(doctorLoginId));
        }

        private void ShowUserInformationPopup()
        {
            var detail = new UserInformation(doctorLoginId);
            var popup = new Form
            {
                Text = "Your Profile",
                StartPosition = FormStartPosition.CenterParent,
                FormBorderStyle = FormBorderStyle.Sizable,
                MinimizeBox = true,
                MaximizeBox = false,
                ControlBox = true,
                ShowInTaskbar = true,
            };
            detail.Dock = DockStyle.Fill;
            popup.ClientSize = detail.PreferredSize;
            popup.Controls.Add(detail);
            popup.ShowDialog(this);
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            _appointmentNotification.Stop();
        }
    }
}