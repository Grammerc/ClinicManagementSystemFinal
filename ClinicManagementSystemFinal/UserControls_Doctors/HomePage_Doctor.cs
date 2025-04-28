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

        private const string CONN =
            @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=B:\Downloads\Login.accdb;Persist Security Info=False;";

        public HomePage_Doctor(string loginId)
        {
            InitializeComponent();
            doctorLoginId = loginId;

            // Profile picture (now a PictureBox)
            pbxProfile.Click += (s, e) => LoadControl(new DoctorInformation(doctorLoginId));

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
            const string sql = @"
SELECT 
    I.ProfileImagePath,
    I.ProfilePicture,
    D.DoctorName,
    R.RoleName,
    R.Permission
FROM ((Account      AS A
       INNER JOIN Doctors     AS D ON A.LoginID = D.LoginID)
      INNER JOIN Information AS I ON A.LoginID = I.LoginID)
     LEFT JOIN Roles       AS R ON CInt(A.RoleID) = R.RoleID
WHERE A.LoginID = ?";

            OleDbCommand cmd = null;
            try
            {
                using var conn = new OleDbConnection(CONN);
                conn.Open();

                cmd = new OleDbCommand(sql, conn);
                cmd.Parameters.Add("?", OleDbType.Integer).Value = Convert.ToInt32(doctorLoginId);

                using var rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    // 1) Doctor name
                    lblName.Text = rdr["DoctorName"].ToString();

                    // 2) Role name
                    lblRole.Text = rdr["RoleName"]?.ToString() ?? "(no role)";

                    // 3) Profile picture (path preferred, else blob)
                    Image img = null;
                    var path = rdr["ProfileImagePath"].ToString();
                    if (!string.IsNullOrEmpty(path) && File.Exists(path))
                    {
                        img = Image.FromFile(path);
                    }
                    else if (rdr["ProfilePicture"] != DBNull.Value)
                    {
                        var blob = (byte[])rdr["ProfilePicture"];
                        using var ms = new MemoryStream(blob);
                        img = Image.FromStream(ms);
                    }

                    if (img != null)
                        pbxProfile.Image = img;
                }
            }
            catch (OleDbException ex)
            {
                var sb = new StringBuilder();
                sb.AppendLine("ExecuteReader failed in LoadDoctorHeader.");
                sb.AppendLine("SQL:");
                sb.AppendLine(sql);
                sb.AppendLine("Parameters:");
                if (cmd != null)
                {
                    foreach (OleDbParameter p in cmd.Parameters)
                        sb.AppendLine($"  {p.ParameterName} = {p.Value} ({p.OleDbType})");
                }
                sb.AppendLine();
                sb.AppendLine("Exception:");
                sb.AppendLine(ex.Message);

                MessageBox.Show(sb.ToString(), "Debug", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
            => LoadControl(new FindPeople(doctorLoginId));

        private void btnLogOut_Click(object sender, EventArgs e)
        {
            Hide();
            new SignIn().Show();
        }
    }
}