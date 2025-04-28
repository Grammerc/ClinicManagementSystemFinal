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
        private string userLoginId;
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
            const string CONNSTR =
                @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=B:\Downloads\Login.accdb;Persist Security Info=False;";

            string loginId = null;
            string roleName = null;

            // 1) use a local connection in a using
            using (var conn = new OleDbConnection(CONNSTR))
            {
                conn.Open();
                using var cmd = new OleDbCommand(@"
            SELECT A.LoginID,
                   A.RoleID,
                   R.RoleName
              FROM Account AS A
         LEFT JOIN Roles   AS R ON A.RoleID = R.RoleID
             WHERE A.username = ?
               AND A.password = ?", conn);

                cmd.Parameters.AddWithValue("?", tbxEmail.Text.Trim());
                cmd.Parameters.AddWithValue("?", tbxPassword.Text);

                using var reader = cmd.ExecuteReader();
                if (!reader.Read())
                {
                    MessageBox.Show("Incorrect username or password.", "Error",
                                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                loginId = reader["LoginID"].ToString();
                roleName = reader["RoleName"]?.ToString() ?? "";
            }

            // 2) hide the sign-in form
            this.Hide();

            // 3) dispatch based on the RoleName
            switch (roleName.Trim().ToLowerInvariant())
            {
                case "doctor":
                    new HomePage_Doctor(loginId, isSecretary: false)
                        .Show();
                    break;

                case "secretary":
                    new HomePage_Doctor(loginId, isSecretary: true)
                        .Show();
                    break;

                case "patient":
                    new HomePage_User(loginId)
                        .Show();
                    break;

                default:
                    MessageBox.Show($"Unknown role “{roleName}”.", "Error",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Show();
                    break;
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
