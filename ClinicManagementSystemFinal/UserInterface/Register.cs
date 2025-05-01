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
            string connString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=""C:\Users\Raphael\source\repos\ClinicManagementSystemFinal\ClinicManagementSystemFinal\Login.accdb"";Persist Security Info=False;";

            using (OleDbConnection conn = new OleDbConnection(connString))
            {
                try
                {
                    conn.Open();

  
                    if (string.IsNullOrWhiteSpace(tbxUsername.Text)
                     || string.IsNullOrWhiteSpace(tbxPassword.Text)
                     || string.IsNullOrWhiteSpace(tbxName.Text)
                     || string.IsNullOrWhiteSpace(tbxEmail.Text))
                    {
                        MessageBox.Show("Please fill in all required fields.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    using (var cmd = new OleDbCommand("SELECT COUNT(*) FROM Account WHERE [username] = ?", conn))
                    {
                        cmd.Parameters.AddWithValue("?", tbxUsername.Text.Trim());
                        int userCount = Convert.ToInt32(cmd.ExecuteScalar());
                        if (userCount > 0)
                        {
                            MessageBox.Show("That username is already taken.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }

                    using (var cmd = new OleDbCommand("SELECT COUNT(*) FROM Information WHERE Email = ?", conn))
                    {
                        cmd.Parameters.AddWithValue("?", tbxEmail.Text.Trim());
                        int emailCount = Convert.ToInt32(cmd.ExecuteScalar());
                        if (emailCount > 0)
                        {
                            MessageBox.Show("That email is already registered.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }

 
                    int roleId;
                    if (rdbDoctor.Checked) roleId = 1;
                    else if (rdbSecretary.Checked) roleId = 2;
                    else       roleId = 3;

                    string insertAccount = @"
                INSERT INTO Account ([username], [password], [RoleID], [ClinicID])
                VALUES (?, ?, ?, ?)";
                    using (var cmd = new OleDbCommand(insertAccount, conn))
                    {
                        cmd.Parameters.AddWithValue("?", tbxUsername.Text.Trim());
                        cmd.Parameters.AddWithValue("?", tbxPassword.Text);
                        cmd.Parameters.AddWithValue("?", roleId);
                        cmd.Parameters.AddWithValue("?", 1); 
                        cmd.ExecuteNonQuery();
                    }

                    int loginID = Convert.ToInt32(new OleDbCommand("SELECT @@IDENTITY", conn).ExecuteScalar());

                    string insertInfo = @"
                INSERT INTO Information (LoginID, Name, Email)
                VALUES (?, ?, ?)";
                    using (var cmd = new OleDbCommand(insertInfo, conn))
                    {
                        cmd.Parameters.AddWithValue("?", loginID);
                        cmd.Parameters.AddWithValue("?", tbxName.Text.Trim());
                        cmd.Parameters.AddWithValue("?", tbxEmail.Text.Trim());
                        cmd.ExecuteNonQuery();
                    }

                    if (pbxProfilePicture.Image != null)
                    {
                        using var ms = new MemoryStream();
                        pbxProfilePicture.Image.Save(ms, pbxProfilePicture.Image.RawFormat);
                        byte[] imageBytes = ms.ToArray();
                        string updatePic = "UPDATE Information SET ProfilePicture = ? WHERE LoginID = ?";
                        using var cmd = new OleDbCommand(updatePic, conn);
                        cmd.Parameters.AddWithValue("?", imageBytes);
                        cmd.Parameters.AddWithValue("?", loginID);
                        cmd.ExecuteNonQuery();
                    }

                    MessageBox.Show("Registration successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            new SignIn().Show();
        }

        private void pbxProfilePicture_Click(object sender, EventArgs e)
        {

        }

        private void btnChangeImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";

            if (open.ShowDialog() == DialogResult.OK)
            {
                pbxProfilePicture.Image = Image.FromFile(open.FileName);
                pbxProfilePicture.Tag = open.FileName;
            }
        }
    }
}
