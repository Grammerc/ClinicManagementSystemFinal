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

namespace ClinicManagementSystemFinal
{
    public partial class SignIn : Form
    {

        private OleDbConnection conn = new OleDbConnection();
        public SignIn()
        {
            InitializeComponent();
            conn.ConnectionString = @"Provider = Microsoft.ACE.OLEDB.12.0; Data Source = C:\Users\Raphael Perocho\source\repos\ClinicManagementSystemFinal\ProjectClinic\ClinicManagementSystemFinal\Login.accdb;
            Persist Security Info=False;" ;
        }

        private void SignIn_Load(object sender, EventArgs e)
        {

        }

        private void btnLogIn_Click(object sender, EventArgs e)
        {
            conn.Open();
            OleDbCommand cmd = new OleDbCommand();
            cmd.Connection = conn;
            cmd.CommandText = "select * from Account where username = '" + tbxEmail.Text+ "' and password='" + tbxPassword.Text;

            OleDbDataReader or = cmd.ExecuteReader();

            int count = 0;
            while (or.Read())
            {
                count = count + 1;
            }
            if(count == 1)
            {
                this.Hide();
                HomePage hm = new HomePage();
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

            cmd.CommandText = "SELECT * FROM Account WHERE username = @username AND password = @password";

            cmd.Parameters.AddWithValue("@username", tbxEmail.Text);
            cmd.Parameters.AddWithValue("@password", tbxPassword.Text);

            OleDbDataReader or = cmd.ExecuteReader();

            if (or.HasRows)
            {
                this.Hide(); 
                HomePage hm = new HomePage();
                hm.Show();
            }
            else
            {
                MessageBox.Show("Incorrect username or password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            conn.Close();
        }
    }
}
