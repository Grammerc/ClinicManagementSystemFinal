﻿using System;
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
        private string userLoginId;
        public SignIn()
        {
            InitializeComponent();
            conn.ConnectionString = @"Provider = Microsoft.ACE.OLEDB.12.0; Data Source = ""C:\Users\Raphael\source\repos\ClinicManagementSystemFinal\ClinicManagementSystemFinal\Login.accdb"";
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
            string loginId;
            string rawRoleId;
            bool loginOk;

            using (var conn = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\Raphael\source\repos\ClinicManagementSystemFinal\ClinicManagementSystemFinal\Login.accdb;Persist Security Info=False;"))
            {
                conn.Open();
                using (var cmd = new OleDbCommand("SELECT LoginID, RoleID FROM Account WHERE username = @username AND password = @password", conn))
                {
                    cmd.Parameters.AddWithValue("@username", tbxEmail.Text);
                    cmd.Parameters.AddWithValue("@password", tbxPassword.Text);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (!reader.Read())
                        {
                            MessageBox.Show("Incorrect username or password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }

                        loginId = reader["LoginID"].ToString();
                        rawRoleId = reader["RoleID"].ToString();
                        loginOk = true;
                    }
                }
            }

            bool isDoctor = false;
            bool isSecretary = false;
            bool isStandardUser = false;

            if (int.TryParse(rawRoleId, out var numericRole))
            {
                isDoctor = numericRole == 1;
                isSecretary = numericRole == 2;
                isStandardUser = numericRole == 3;
            }
            else
            {
                isDoctor = rawRoleId.Equals("Doctor", StringComparison.OrdinalIgnoreCase);
                isSecretary = rawRoleId.Equals("Secretary", StringComparison.OrdinalIgnoreCase);
                isStandardUser = rawRoleId.Equals("User", StringComparison.OrdinalIgnoreCase);
            }

            this.Hide();

            if (isDoctor)
            {
                var doctorHome = new HomePage_Doctor(loginId, isSecretary: false);
                doctorHome.Show();
            }
            else if (isSecretary)
            {
                var secretaryHome = new HomePage_Doctor(loginId, isSecretary: true);
                secretaryHome.Show();
            }
            else if (isStandardUser)
            {
                var userHome = new HomePage_User(loginId);
                userHome.Show();
            }
            else
            {
                MessageBox.Show("Unknown RoleID detected.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Show();
            }
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
