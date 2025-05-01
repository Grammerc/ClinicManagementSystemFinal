using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Mail;
using System.Net;

namespace ClinicManagementSystemFinal.UserControls_Doctors.Clinics
{
    public partial class AddClinic : UserControl
    {
        private readonly int _loginId;
        private string _generatedCode;
        private bool _isVerified = false;
        public event EventHandler ClinicAdded;
        // adjust your path as needed
        private const string CONN =
            @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\Raphael\source\repos\ClinicManagementSystemFinal\ClinicManagementSystemFinal\Login.accdb;Persist Security Info=False;";

        public AddClinic(int loginId)
        {
            _loginId = loginId;
            InitializeComponent();
            btnAdd.Click += BtnAdd_Click;
            btnSendCode.Click += BtnSendCode_Click;
            btnVerifyCode.Click += BtnVerifyCode_Click;
        }

        private void BtnSendCode_Click(object sender, EventArgs e)
        {
            var email = tbxEmail.Text.Trim();
            if (string.IsNullOrEmpty(email))
            {
                MessageBox.Show("Please enter your email address.", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Generate a 6-digit verification code
            Random random = new Random();
            _generatedCode = random.Next(100000, 999999).ToString();

            try
            {
                // Configure the SMTP client
                SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                smtp.Credentials = new NetworkCredential("ClinicManagementSystemC@gmail.com", "hyop ejoi vhlm miss");
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;

                // Create the email message
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress("ClinicManagementSystemC@gmail.com");
                mail.To.Add(email);
                mail.Subject = "Clinic Creation Verification Code";
                mail.Body = $"Your verification code for creating a new clinic is: {_generatedCode}";

                smtp.Send(mail);

                MessageBox.Show("Verification code sent! Check your email.", "Success", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error sending email: " + ex.Message, "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnVerifyCode_Click(object sender, EventArgs e)
        {
            if (tbxVerificationCode.Text.Trim() == _generatedCode)
            {
                _isVerified = true;
                MessageBox.Show("Verification successful! You can now create your clinic.", "Verified", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Incorrect verification code!", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            if (!_isVerified)
            {
                MessageBox.Show("Please verify your email first by entering the correct verification code!", 
                    "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var name = tbxSearch.Text.Trim();
            var address = tbxAddress.Text.Trim();
            var email = tbxEmail.Text.Trim();

            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(address))
            {
                MessageBox.Show("Please enter both a clinic name and address.",
                    "Missing Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using var conn = new OleDbConnection(CONN);
                conn.Open();

                // 1) Insert clinic
                using var cmd = new OleDbCommand(
                    "INSERT INTO Clinics (ClinicName, Address, Email) VALUES (?, ?, ?)", conn);
                cmd.Parameters.AddWithValue("?", name);
                cmd.Parameters.AddWithValue("?", address);
                cmd.Parameters.AddWithValue("?", email);
                cmd.ExecuteNonQuery();

                // 2) Get its new ID
                using var idCmd = new OleDbCommand("SELECT @@IDENTITY", conn);
                int newClinicId = Convert.ToInt32(idCmd.ExecuteScalar());

                // 3) Assign this user as a doctor/secretary there
                using var link = new OleDbCommand(
                    "INSERT INTO Doctors (LoginID, ClinicID) VALUES (?, ?)", conn);
                link.Parameters.AddWithValue("?", _loginId);
                link.Parameters.AddWithValue("?", newClinicId);
                link.ExecuteNonQuery();

                MessageBox.Show("Clinic added and linked to your account.",
                    "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                ClinicAdded?.Invoke(this, EventArgs.Empty);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving clinic:\n" + ex.Message,
                    "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
