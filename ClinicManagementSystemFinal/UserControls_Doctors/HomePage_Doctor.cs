using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClinicManagementSystemFinal.UserControls_Doctors
{
    public partial class HomePage_Doctor : Form
    {
        public HomePage_Doctor()
        {
            InitializeComponent();
        }

        public void LoadControl(Control c)
        {
            if (panelMainDesktop.Controls.Count > 0)
                panelMainDesktop.Controls.RemoveAt(0);


            if (c is Form childForm)
            {
                childForm.TopLevel = false;
                childForm.FormBorderStyle = FormBorderStyle.None;
                childForm.Dock = DockStyle.Fill;
                panelMainDesktop.Controls.Add(childForm);
                panelMainDesktop.Tag = childForm;
                childForm.Show();
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

        }

        private void btnDashboard_Click(object sender, EventArgs e)
        {
            LoadControl(new Dashboard_Doctors());
        }

        private void btnCalendar_Click(object sender, EventArgs e)
        {
            LoadControl(new Calendar_Doctor());
        }

        private void btnPatientQueue_Click(object sender, EventArgs e)
        {
            LoadControl(new PatientQueue());
        }

        private void btnMyClinics_Click(object sender, EventArgs e)
        {
            LoadControl(new PatientQueue());
        }
    }
}
