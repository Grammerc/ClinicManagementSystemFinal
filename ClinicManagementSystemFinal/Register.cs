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
using System.Xml.Linq;

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
            string connString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\Raphael Perocho\source\repos\ClinicManagementSystemFinal\ProjectClinic\ClinicManagementSystemFinal\Login.accdb; Persist Security Info=False;";

            using (OleDbConnection conn = new OleDbConnection(connString))
            {
                try
                {
                    conn.Open();

                    if (string.IsNullOrWhiteSpace(tbxUsername.Text) || string.IsNullOrWhiteSpace(tbxPassword.Text) || string.IsNullOrWhiteSpace(tbxName.Text))
                    {
                        MessageBox.Show("Please fill in all the required fields.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    string query = "INSERT INTO Account ([username], [password], [RoleID], [ClinicID], [Name]) VALUES (?, ?, ?, ?, ?)";

                    using (OleDbCommand cmd = new OleDbCommand(query, conn))
                    {
                        // Add parameters to the command
                        cmd.Parameters.AddWithValue("?", tbxUsername.Text); 
                        cmd.Parameters.AddWithValue("?", tbxPassword.Text);
                        cmd.Parameters.AddWithValue("?", 1); 
                        cmd.Parameters.AddWithValue("?", 1); 
                        cmd.Parameters.AddWithValue("?", tbxName.Text);

                        // Execute the command
                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Registration successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Registration failed, please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    conn.Close();
                }
            }

            this.Hide();
            SignIn rp = new SignIn();
            rp.Show();
        }



    }
}
