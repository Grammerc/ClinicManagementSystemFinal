using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Security.Principal;
using ClinicManagementSystemFinal.UserControls_Doctors;

namespace ClinicManagementSystemFinal
{
    public partial class SignIn : Form
    {

        private OleDbConnection conn = new OleDbConnection();
        public SignIn()
        {
            InitializeComponent();
            conn.ConnectionString = @"Provider = Microsoft.ACE.OLEDB.12.0; Data Source = ""C:\Users\Raphael\Downloads\Login.accdb"";
            Persist Security Info=False;";
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams handleParams = base.CreateParams;
                handleParams.ExStyle = 0x02000000;
                return handleParams;
            }
        }

        private void SignIn_Load(object sender, EventArgs e)
        {

        }

        private void btnLogIn_Click(object sender, EventArgs e)
        {
            conn.Open();
            OleDbCommand cmd = new OleDbCommand();
            cmd.Connection = conn;
            cmd.CommandText = "select * from Account where username = '" + tbxEmail.Text + "' and password='" + tbxPassword.Text;

            OleDbDataReader or = cmd.ExecuteReader();

            int count = 0;
            while (or.Read())
            {
                count = count + 1;
            }
            if (count == 1)
            {
                this.Hide();
                HomePage_User hm = new HomePage_User();
                hm.Show();
            }
            else
            {
                MessageBox.Show("Incorrect username and Password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            conn.Close();

        }

        private void linkRegister_Click(object sender, EventArgs e)
        {
            this.Hide();
            Register rm = new Register();
            rm.Show();
        }

        private void panelMainSignIn_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            conn.Open();

            OleDbCommand cmd = new OleDbCommand();
            cmd.Connection = conn;

            cmd.CommandText = "SELECT RoleID FROM Account WHERE username = @username AND password = @password";

            cmd.Parameters.AddWithValue("@username", tbxEmail.Text);
            cmd.Parameters.AddWithValue("@password", tbxPassword.Text);

            OleDbDataReader or = cmd.ExecuteReader();

            if (or.Read())
            {
                string roleID = or["RoleID"].ToString(); // Since RoleID is stored as Short Text

                this.Hide();

                if (roleID == "1")
                {
                    HomePage_Doctor doctorHome = new HomePage_Doctor();
                    doctorHome.Show();
                }
               /* else if (roleID == "2")
                {
                    HomePage_Secretary secHome = new HomePage_Secretary();
                    secHome.Show();
               } */
                else if (roleID == "3")
                {
                    HomePage_User userHome = new HomePage_User();
                    userHome.Show();
                }
                else
                {
                    MessageBox.Show("Unknown RoleID detected.");
                    this.Show(); // fallback
                }
            }
            else
            {
                MessageBox.Show("Incorrect username or password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            conn.Close();
        }
    }
}
