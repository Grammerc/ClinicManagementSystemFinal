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

namespace ClinicManagementSystemFinal.UserControls_User
{
    public partial class Appointments_User : UserControl
    {

        private OleDbConnection conn = new OleDbConnection();
        private string userLoginId;
        public Appointments_User(string loginId)
        {
            InitializeComponent();
            cbxClinicName.SelectedIndexChanged += new EventHandler(cbxClinicName_SelectedIndexChanged);
            conn.ConnectionString = @"Provider = Microsoft.ACE.OLEDB.12.0; Data Source = ""C:\Downloads\Login.accdb"";
            Persist Security Info=False;";
            LoadClinicNames();
            userLoginId = loginId;
        }


        private void LoadClinicNames()
        {
            cbxClinicName.Items.Clear();

            conn.Open();
            OleDbCommand cmd = new OleDbCommand("SELECT ClinicName FROM Clinics ORDER BY ClinicName ASC", conn);
            OleDbDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                cbxClinicName.Items.Add(reader["ClinicName"].ToString());
            }

            reader.Close();
            conn.Close();
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (cbxClinicName.SelectedIndex == -1 || cbxTimeSlot.SelectedIndex == -1 || cbxDoctor.SelectedIndex == -1 || cbxReasonForVisit.SelectedIndex == -1)
            {
                MessageBox.Show("Please complete all required fields.", "Missing Info", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cbxDate.Value.Date < DateTime.Today)
            {
                MessageBox.Show("You cannot select a past date.", "Invalid Date", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string reason = cbxReasonForVisit.SelectedItem.ToString();
            if (reason == "Other Concerns")
            {
                if (string.IsNullOrWhiteSpace(tbxOtherConcerns.Text))
                {
                    MessageBox.Show("Please specify your concern.", "Missing Info", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                reason = tbxOtherConcerns.Text;
            }

            string selectedClinic = cbxClinicName.SelectedItem.ToString();
            string selectedDoctor = cbxDoctor.SelectedItem.ToString();
            DateTime selectedDate = cbxDate.Value.Date;
            string selectedTime = cbxTimeSlot.SelectedItem.ToString();

            string connStr = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Downloads\Login.accdb;Persist Security Info=False;";
            using (OleDbConnection conn = new OleDbConnection(connStr))
            {
                conn.Open();

                OleDbCommand clinicCmd = new OleDbCommand("SELECT ClinicID FROM Clinics WHERE ClinicName = @clinicName", conn);
                clinicCmd.Parameters.AddWithValue("@clinicName", selectedClinic);
                object clinicResult = clinicCmd.ExecuteScalar();
                if (clinicResult == null)
                {
                    MessageBox.Show("Selected clinic not found.");
                    return;
                }
                int clinicID = Convert.ToInt32(clinicResult);

                int doctorID = Convert.ToInt32(cbxDoctor.SelectedValue);

                int userInfoID = GetUserInfoID(userLoginId);

                OleDbCommand insertCmd = new OleDbCommand(@"
    INSERT INTO Appointments 
    (UserInfoID, DoctorID, AppointmentDate, TimeSlot, ReasonForVisit, Status, ClinicID)
    VALUES (?, ?, ?, ?, ?, ?, ?)", conn);

                insertCmd.Parameters.AddWithValue("?", userInfoID);
                insertCmd.Parameters.AddWithValue("?", doctorID);
                insertCmd.Parameters.AddWithValue("?", selectedDate);  
                insertCmd.Parameters.AddWithValue("?", selectedTime);
                insertCmd.Parameters.AddWithValue("?", reason);       
                insertCmd.Parameters.AddWithValue("?", "Pending");      
                insertCmd.Parameters.AddWithValue("?", clinicID);     

                insertCmd.ExecuteNonQuery();

                MessageBox.Show("Appointment successfully booked!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearFields();
            }
        }

        private void ClearFields()
        {
            cbxClinicName.SelectedIndex = -1;
            cbxDate.Value = DateTime.Today;
            cbxTimeSlot.SelectedIndex = -1;
            cbxDoctor.SelectedIndex = -1;
            cbxReasonForVisit.SelectedIndex = -1;
            tbxOtherConcerns.Text = "";
            tbxOtherConcerns.Visible = false;
        }



        private int GetUserInfoID(string loginId)
        {
            int userInfoId = -1;

            using (OleDbConnection conn = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Downloads\Login.accdb;Persist Security Info=False;"))
            {
                conn.Open();
                OleDbCommand cmd = new OleDbCommand("SELECT UserInfoID FROM Information WHERE LoginID = @loginId", conn);
                MessageBox.Show("LoginID Received in getuserinfoId(): " + loginId);
                cmd.Parameters.AddWithValue("@loginId", Convert.ToInt32(loginId));
                object result = cmd.ExecuteScalar();
                MessageBox.Show("UserInfoID Fetched: " + userInfoId);
                if (result != null)
                {
                    userInfoId = Convert.ToInt32(result);
                }
            }

            return userInfoId;
        }

    

        private void cbxClinicName_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadDoctorsForSelectedClinic();
        }

        private void LoadDoctorsForSelectedClinic()
        {
            cbxDoctor.DataSource = null;

            if (cbxClinicName.SelectedItem == null) return;
            string selectedClinic = cbxClinicName.SelectedItem.ToString();

           

            using (OleDbConnection conn = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Downloads\Login.accdb;Persist Security Info=False;"))
            {
                conn.Open();

                OleDbCommand clinicCmd = new OleDbCommand("SELECT ClinicID FROM Clinics WHERE ClinicName = @clinicName", conn);
                clinicCmd.Parameters.AddWithValue("@clinicName", selectedClinic);
                object clinicResult = clinicCmd.ExecuteScalar();
                if (clinicResult == null) return;
                int clinicID = Convert.ToInt32(clinicResult);

                OleDbCommand cmd = new OleDbCommand(@"
            SELECT D.DoctorID, I.Name 
            FROM Doctors D 
            INNER JOIN Information I ON D.LoginID = I.LoginID 
            WHERE D.ClinicID = @clinicID", conn);
                cmd.Parameters.AddWithValue("@clinicID", clinicID);

                OleDbDataAdapter adapter = new OleDbDataAdapter(cmd);
                DataTable table = new DataTable();
                adapter.Fill(table);

                cbxDoctor.DisplayMember = "Name";
                cbxDoctor.ValueMember = "DoctorID";
                cbxDoctor.DataSource = table;

                conn.Close();
            }
        }
    }
}
