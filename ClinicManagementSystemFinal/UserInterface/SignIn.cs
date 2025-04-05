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
            conn.ConnectionString = @"Provider = Microsoft.ACE.OLEDB.12.0; Data Source = ""B:\Downloads\Login.accdb"";
            Persist Security Info=False;";
            tbxPassword.UseSystemPasswordChar = true;
            tbxEmail.ForeColor = Color.DarkGray;
     
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

        private void btnLogin_Click(object sender, EventArgs e)
        {
            conn.Open();

            OleDbCommand cmd = new OleDbCommand();
            cmd.Connection = conn;

            cmd.CommandText = "SELECT LoginID, RoleID FROM Account WHERE username = @username AND password = @password";

            cmd.Parameters.AddWithValue("@username", tbxEmail.Text);
            cmd.Parameters.AddWithValue("@password", tbxPassword.Text);

            OleDbDataReader or = cmd.ExecuteReader();

            if (or.Read())
            {
                string loginId = or["LoginID"].ToString();
                string roleID = or["RoleID"].ToString();

                this.Hide();

                if (roleID == "1")
                {
                    HomePage_Doctor doctorHome = new HomePage_Doctor(loginId);
                    doctorHome.Show();
                }
                else if (roleID == "3")
                {
                    HomePage_User userHome = new HomePage_User();
                    userHome.Show();
                }
                else
                {
                    MessageBox.Show("Unknown RoleID detected.");
                    this.Show();
                }
            }
            else
            {
                MessageBox.Show("Incorrect username or password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void btnShow_MouseDown(object sender, MouseEventArgs e)
        {
            tbxPassword.UseSystemPasswordChar = false;
        }

        private void btnShow_MouseUp(object sender, MouseEventArgs e)
        {
            tbxPassword.UseSystemPasswordChar = true;
        }

        private void tbxPassword_Click(object sender, EventArgs e)
        {
            if (tbxPassword.Text == "Enter Password")
            {
                tbxPassword.Clear();
                tbxPassword.UseSystemPasswordChar = true;
                tbxPassword.ForeColor = Color.Black;
            }

            if (tbxEmail.Text.Length < 1)
            {
                tbxEmail.ForeColor = Color.DarkGray;
                tbxEmail.Text = "Enter Username";
                tbxEmail.ForeColor = Color.DarkGray;


            }
        }

        private void tbxEmail_Click(object sender, EventArgs e)
        {
            if (tbxEmail.Text == "Enter Username")
            {
          
                tbxEmail.Clear();
                tbxEmail.ForeColor = Color.Black;
            }

            if(tbxPassword.Text.Length < 1)
            {
                tbxPassword.ForeColor = Color.DarkGray;
                tbxPassword.UseSystemPasswordChar = false;
                tbxPassword.Text = "Enter Password";
                tbxPassword.ForeColor = Color.DarkGray;
            }
       
        }
    }
}
