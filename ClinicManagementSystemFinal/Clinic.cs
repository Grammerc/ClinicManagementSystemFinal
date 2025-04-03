using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClinicManagementSystemFinal
{
    public partial class Clinic : Form
    {
        public Clinic()
        {
            InitializeComponent();
        }

        private void materialTextBoxEdit1_Click(object sender, EventArgs e)
        {

        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
        }

        private void pbx1_Click(object sender, EventArgs e)
        {
            var openFormsList = Application.OpenForms.Cast<Form>().ToList();

            foreach (Form openForm in openFormsList)
            {
                if (openForm is HomePage_User homePage)
                {
                    homePage.loadform(new AppointmentForm());
                    return;
                }
            }
        }

        private void pbx2_Click(object sender, EventArgs e)
        {
            var openFormsList = Application.OpenForms.Cast<Form>().ToList();

            foreach (Form openForm in openFormsList)
            {
                if (openForm is HomePage_User homePage)
                {
                    homePage.loadform(new AppointmentForm());
                    return;
                }
            }
        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
