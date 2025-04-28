using System;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using ClinicManagementSystemFinal.UserControls_Doctors;
using ClinicManagementSystemFinal.UserInterface;
using AppointmentUC = ClinicManagementSystemFinal.UserControls_Doctors.Appointment.AppointmentView_Doctors;

namespace ClinicManagementSystemFinal.UserControls_Doctors
{
    public partial class HomePage_Doctor : Form
    {
        private readonly string doctorLoginId;
        private readonly MyClinics myClinicsControl;
        private readonly AppointmentUC apptUC;
        private readonly PatientQueue queueUC;
        private bool _isSecretary;

        private const string CONN =
            @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=B:\Downloads\Login.accdb;Persist Security Info=False;";

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


            // Profile picture (now a PictureBox)
            if (_isSecretary)
            {
                pbxProfile.Click += (s, e) => LoadControl(new UserInformation(doctorLoginId));

            }
            else
            {
                pbxProfile.Click += (s, e) => ShowDoctorDetailPopup();
            }
               

            myClinicsControl = new MyClinics();
            apptUC = new AppointmentUC(doctorLoginId);
            queueUC = new PatientQueue(doctorLoginId);

            myClinicsControl.EditRequested += clinicId =>
                LoadControl(new EditClinic(clinicId));

            queueUC.PatientSelected += (uid, name, photo, cid) =>
                LoadControl(new MedicalHistory(uid, name, photo, cid, doctorLoginId));

            myClinicsControl.LoadMyClinics(doctorLoginId);
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
        }

        private void LoadDoctorHeader()
        {
            // we’ll load from Account → Information (shared table) 
            // and Roles (to get “Doctor” or “Secretary”)
            const string sql = @"
SELECT 
  I.ProfileImagePath,
  I.ProfilePicture,
  I.Name,
  R.RoleName
FROM   (Account      AS A
        INNER JOIN Information AS I ON A.LoginID = I.LoginID)
       LEFT JOIN Roles AS R ON A.RoleID = R.RoleID
WHERE  A.LoginID = ?";

            using var conn = new OleDbConnection(CONN);
            conn.Open();
            using var cmd = new OleDbCommand(sql, conn);

            // RoleID is numeric in Access:
            cmd.Parameters.Add("?", OleDbType.Integer).Value = Convert.ToInt32(doctorLoginId);

            using var rdr = cmd.ExecuteReader();
            if (!rdr.Read()) return;

            // 1) picture (path preferred, otherwise blob)
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
            if (img != null)
                pbxProfile.Image = img;

            // 2) your name
            lblName.Text = rdr["Name"].ToString();

            // 3) role (“Doctor” or “Secretary”)
            lblRole.Text = rdr["RoleName"]?.ToString() ?? "(no role)";
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

        private void ShowDoctorDetailPopup()
        {
            // 1) instantiate the detail control
            var detail = new DoctorDetail(doctorLoginId);

            // 2) create a new top-level form
            var popup = new Form
            {
                Text = "Doctor Details",            // window title
                StartPosition = FormStartPosition.CenterScreen,
                FormBorderStyle = FormBorderStyle.Sizable,  // resizable with normal title bar
                ControlBox = true,                  // shows minimize/maximize/close
                MinimizeBox = true,
                MaximizeBox = false,
                ShowInTaskbar = true,
                AutoScaleMode = AutoScaleMode.None, // keep your control's own scaling
            };

            // 3) size the form to fit your control
            //    (you could also hard-code a Size if you prefer)
            detail.Dock = DockStyle.Fill;
            popup.ClientSize = detail.Size;

            // 4) add your control and show
            popup.Controls.Add(detail);
            popup.Show();  // non-modal; use ShowDialog() if you want it modal
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
    }
}