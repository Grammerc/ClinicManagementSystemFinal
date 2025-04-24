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
    public partial class Appointments_Create : UserControl
    {
        private OleDbConnection conn = new OleDbConnection();
        private string userLoginId;
        readonly string[] AllSlots =
{
    "8:00 A.M. - 9:00 A.M.",
    "9:00 A.M. - 10:00 A.M",
    "10:00 A.M. - 11:00 A.M.",
    "11:00 A.M. - 12:00 A.M.",
    "12:00 A.M. - 1:00 A.M.",
    "1:00 P.M. - 2:00  P.M.",
    "2:00  P.M. - 3:00  P.M.",
    "3:00  P.M. - 4:00  P.M.",
    "4:00 P.M. - 5:00  P.M.",
    "5:00  P.M. - 6:00  P.M."
};
        public Appointments_Create(string loginId)
        {
            InitializeComponent();
            cbxClinicName.SelectedIndexChanged += new EventHandler(cbxClinicName_SelectedIndexChanged);
            conn.ConnectionString = @"Provider = Microsoft.ACE.OLEDB.12.0; Data Source = ""B:\Downloads\Login.accdb"";
            Persist Security Info=False;";
            LoadClinicNames();
            userLoginId = loginId;
            cbxDoctor.SelectedIndexChanged += (s, e) => RefreshTimeSlots();
            cbxDate.ValueChanged += (s, e) => RefreshTimeSlots();
            RefreshTimeSlots();
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

            string connStr = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=B:\Downloads\Login.accdb;Persist Security Info=False;";
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
                using (var check = new OleDbCommand(
       "SELECT 1 FROM Appointments WHERE DoctorID=? AND AppointmentDate=? AND TimeSlot=? AND Status IN ('Pending','Approved')", conn))
                {
                    check.Parameters.AddWithValue("?", doctorID);
                    check.Parameters.AddWithValue("?", selectedDate);
                    check.Parameters.AddWithValue("?", selectedTime);
                    if (check.ExecuteScalar() != null)
                    {
                        MessageBox.Show("That time slot has just been taken. Pick another one.");
                        RefreshTimeSlots();
                        return;
                    }
                }

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

            using (OleDbConnection conn = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=B:\Downloads\Login.accdb;Persist Security Info=False;"))
            {
                conn.Open();
                OleDbCommand cmd = new OleDbCommand("SELECT UserInfoID FROM Information WHERE LoginID = @loginId", conn);
                //MessageBox.Show("LoginID Received in getuserinfoId(): " + loginId);
                cmd.Parameters.AddWithValue("@loginId", Convert.ToInt32(loginId));
                object result = cmd.ExecuteScalar();
                //MessageBox.Show("UserInfoID Fetched: " + userInfoId);
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


        public Appointments_Create(string loginId, string preselectedClinic)
         : this(loginId)          // call your original ctor
        {
            PreselectClinic(preselectedClinic);
        }

        private void PreselectClinic(string clinicName)
        {
            // combo is already filled by the first ctor
            int idx = cbxClinicName.Items.IndexOf(clinicName);
            if (idx >= 0)
                cbxClinicName.SelectedIndex = idx;
            else
                cbxClinicName.Text = clinicName;     // show text even if not found

            LoadDoctorsForSelectedClinic();          // populate doctors immediately
        }

        private void LoadDoctorsForSelectedClinic()
        {
            cbxDoctor.DataSource = null;

            if (cbxClinicName.SelectedItem == null) return;
            string selectedClinic = cbxClinicName.SelectedItem.ToString();

           

            using (OleDbConnection conn = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=B:\Downloads\Login.accdb;Persist Security Info=False;"))
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
            RefreshTimeSlots();
        }
        void RefreshTimeSlots()
        {
            cbxTimeSlot.Items.Clear();

            if (cbxDoctor.SelectedValue == null) return;     // no doctor chosen yet

            int doctorId = Convert.ToInt32(cbxDoctor.SelectedValue);
            DateTime d0 = cbxDate.Value.Date;
            DateTime d1 = d0.AddDays(1);

            using var c = new OleDbConnection(
                @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=B:\Downloads\Login.accdb;Persist Security Info=False;");
            c.Open();
            var cmd = new OleDbCommand(
                @"SELECT TimeSlot FROM Appointments
          WHERE DoctorID = ?
            AND AppointmentDate >= ? AND AppointmentDate < ?
            AND Status IN ('Pending','Approved')", c);
            cmd.Parameters.AddWithValue("?", doctorId);
            cmd.Parameters.AddWithValue("?", d0);
            cmd.Parameters.AddWithValue("?", d1);

            var taken = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            using (var r = cmd.ExecuteReader())
                while (r.Read()) taken.Add(r.GetString(0));

            foreach (string slot in AllSlots)
                if (!taken.Contains(slot))
                    cbxTimeSlot.Items.Add(slot);

            cbxTimeSlot.SelectedIndex = cbxTimeSlot.Items.Count > 0 ? 0 : -1;
        }
    }
}
