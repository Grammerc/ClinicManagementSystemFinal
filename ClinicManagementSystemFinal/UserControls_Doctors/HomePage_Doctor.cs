using System;
using System.Windows.Forms;
using ClinicManagementSystemFinal.UserControls_Doctors;
using ClinicManagementSystemFinal.UserInterface;
using AppointmentUC = ClinicManagementSystemFinal.UserControls_Doctors.Appointment.Appointment;

namespace ClinicManagementSystemFinal.UserControls_Doctors
{
    public partial class HomePage_Doctor : Form
    {
        private readonly string doctorLoginId;
        private readonly MyClinics myClinicsControl;
        private readonly AppointmentUC apptUC;
        private readonly PatientQueue queueUC;

        public HomePage_Doctor(string loginId)
        {
            InitializeComponent();
            doctorLoginId = loginId;
            myClinicsControl = new MyClinics();
            apptUC = new AppointmentUC(doctorLoginId);
            queueUC = new PatientQueue(doctorLoginId);

            myClinicsControl.EditRequested += clinicId =>
            {
                var edit = new EditClinic(clinicId);
                LoadControl(edit);
            };

            queueUC.PatientSelected += (uid, name, photo, cid) =>
            {
                var mh = new MedicalHistory(uid, name, photo, cid, doctorLoginId);
                LoadControl(mh);
            };

            myClinicsControl.LoadMyClinics(doctorLoginId);
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

        private void HomePage_Doctor_Load(object sender, EventArgs e)
        {
            LoadControl(new Dashboard_Doctors());
        }

        private void btnDashboard_Click(object sender, EventArgs e) => LoadControl(new Dashboard_Doctors());
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
        private void btnPatientQueue_Click(object sender, EventArgs e) => LoadControl(queueUC);
        private void btnMyClinics_Click(object sender, EventArgs e) => LoadControl(myClinicsControl);
        private void btnAppointments_Click(object sender, EventArgs e) => LoadControl(apptUC);
        private void btnViewPatients_Click(object sender, EventArgs e) => LoadControl(new FindPeople(doctorLoginId));
        private void btnLogOut_Click(object sender, EventArgs e)
        {
            Hide();
            new SignIn().Show();
        }
    }
}