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
            string connString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=""B:\Downloads\Login.accdb"";Persist Security Info=False;";

            using (OleDbConnection conn = new OleDbConnection(connString))
            {
                try
                {
                    conn.Open();

                    if (string.IsNullOrWhiteSpace(tbxUsername.Text) || string.IsNullOrWhiteSpace(tbxPassword.Text) || string.IsNullOrWhiteSpace(tbxName.Text) || string.IsNullOrWhiteSpace(tbxEmail.Text))
                    {
                        MessageBox.Show("Please fill in all required fields.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    string insertAccount = "INSERT INTO Account ([username], [password], [RoleID], [ClinicID]) VALUES (?, ?, ?, ?)";
                    using (OleDbCommand cmd = new OleDbCommand(insertAccount, conn))
                    {
                        cmd.Parameters.AddWithValue("?", tbxUsername.Text);
                        cmd.Parameters.AddWithValue("?", tbxPassword.Text);
                        cmd.Parameters.AddWithValue("?", 3); // RoleID = 3 = User
                        cmd.Parameters.AddWithValue("?", 1); // Default ClinicID
                        cmd.ExecuteNonQuery();
                    }

                    OleDbCommand getId = new OleDbCommand("SELECT @@IDENTITY", conn);
                    int loginID = Convert.ToInt32(getId.ExecuteScalar());

                    string insertInfo = "INSERT INTO Information (LoginID, Name, Email) VALUES (?, ?, ?)";
                    OleDbCommand cmdInsertInfo = new OleDbCommand(insertInfo, conn);
                    cmdInsertInfo.Parameters.AddWithValue("?", loginID);
                    cmdInsertInfo.Parameters.AddWithValue("?", tbxName.Text);
                    cmdInsertInfo.Parameters.AddWithValue("?", tbxEmail.Text);
                    cmdInsertInfo.ExecuteNonQuery();

                    if (pbxProfilePicture.Image != null)
                    {
                        using (MemoryStream ms = new MemoryStream())
                        {
                            pbxProfilePicture.Image.Save(ms, pbxProfilePicture.Image.RawFormat);
                            byte[] imageBytes = ms.ToArray();

                            string updatePic = "UPDATE Information SET ProfilePicture = ? WHERE LoginID = ?";
                            OleDbCommand updateCmd = new OleDbCommand(updatePic, conn);
                            updateCmd.Parameters.AddWithValue("?", imageBytes);
                            updateCmd.Parameters.AddWithValue("?", loginID);
                            updateCmd.ExecuteNonQuery();
                        }
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
            SignIn signIn = new SignIn();
            signIn.Show();
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
