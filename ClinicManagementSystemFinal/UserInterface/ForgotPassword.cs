using System;
using System.Data.OleDb;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RestSharp;

namespace ClinicManagementSystemFinal
{
    public partial class ForgotPassword : Form
    {
        public ForgotPassword()
        {
            InitializeComponent();
            btnSubmit.Click += new EventHandler(btnSubmit_Click);
            btnCancel.Click += (s, e) => this.Close();
        }

        private async void btnSubmit_Click(object sender, EventArgs e)
        {
            string email = tbxEmail.Text.Trim();

            if (string.IsNullOrEmpty(email))
            {
                MessageBox.Show("Please enter your email.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                await ResetPassword(email);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unhandled error: " + ex.Message);
            }
        }

        private async Task ResetPassword(string email)
        {
            string newPassword = GenerateRandomPassword();
            bool passwordUpdated = UpdatePasswordInDatabase(email, newPassword);

            if (!passwordUpdated)
            {
                MessageBox.Show("Email not found or case does not match exactly.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            bool emailSent = await SendEmailWithMailgun(email, newPassword);

            if (emailSent)
            {
                MessageBox.Show("A new password has been sent to your email.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show("There was an issue sending the email. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string GenerateRandomPassword()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            StringBuilder password = new StringBuilder();
            Random random = new Random();

            for (int i = 0; i < 8; i++)
            {
                password.Append(chars[random.Next(chars.Length)]);
            }

            return password.ToString();
        }

        private bool UpdatePasswordInDatabase(string email, string newPassword)
        {
            string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=B:\Users\Raphael Perocho\source\repos\ClinicManagementSystemFinal\ProjectClinic\ClinicManagementSystemFinal\Login.accdb;Persist Security Info=False;";

            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                connection.Open();

                try
                {
                    string checkEmailQuery = "SELECT Email FROM Account WHERE Email = ?";
                    using (OleDbCommand checkCmd = new OleDbCommand(checkEmailQuery, connection))
                    {
                        checkCmd.Parameters.AddWithValue("?", email);
                        object result = checkCmd.ExecuteScalar();

                        if (result == null || !string.Equals(result.ToString(), email, StringComparison.Ordinal))
                        {
                            return false;
                        }
                    }
                    string updateQuery = "UPDATE Account SET password = ? WHERE [Email] = ?";
                    using (OleDbCommand updateCmd = new OleDbCommand(updateQuery, connection))
                    {
                        updateCmd.Parameters.AddWithValue("?", newPassword);
                        updateCmd.Parameters.AddWithValue("?", email);
                        int rowsAffected = updateCmd.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Database error: " + ex.Message);
                    return false;
                }
            }
        }

        private async Task<bool> SendEmailWithMailgun(string toEmail, string newPassword)
        {
            var client = new RestClient("https://api.mailgun.net/v3/sandbox8c7fec71d27047c288fceae08279ef39.mailgun.org/messages");
            var request = new RestRequest("", Method.Post);

            string apiKey = "key-f39a104345439bd46d80cee3c8fc868a-24bda9c7-894ef6b9";
            request.AddHeader("Authorization", "Basic " + Convert.ToBase64String(Encoding.ASCII.GetBytes("api:" + apiKey)));
            request.AddParameter("from", "no-reply@yourclinic.com");
            request.AddParameter("to", toEmail);
            request.AddParameter("subject", "Your Password Reset");
            request.AddParameter("text", $"Your new password is: {newPassword}\nPlease log in and change it immediately.");
            request.AlwaysMultipartFormData = true;

            try
            {
                var response = await client.ExecuteAsync(request);
                return response.IsSuccessful;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Email error: " + ex.Message);
                return false;
            }
        }
    }
}