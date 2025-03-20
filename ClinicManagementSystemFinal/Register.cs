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
    public partial class Register : Form
    {
        public Register()
        {
            InitializeComponent();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
 
        }

        private void linkRR_Click(object sender, EventArgs e)
        {
            this.Hide();
            SignIn rm = new SignIn();
            rm.Show();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            this.Hide();
            SignIn rs = new SignIn();
            rs.Show();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            this.Hide();
            SignIn rp = new SignIn();
            rp.Show();
        }
    }
}
