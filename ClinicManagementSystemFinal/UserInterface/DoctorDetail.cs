using System;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace ClinicManagementSystemFinal.UserInterface
{
    public partial class DoctorDetail : Form
    {
        private readonly string loginId;
        private const string CONN =
            @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\Raphael\source\repos\ClinicManagementSystemFinal\ClinicManagementSystemFinal\Login.accdb;Persist Security Info=False;";

        public DoctorDetail(string docLoginId)
        {
            InitializeComponent();
            loginId = docLoginId;
            this.Load += DoctorDetail_Load;
        }

        private void DoctorDetail_Load(object sender, EventArgs e)
        {
            Fill();
        }

        private void Fill()
        {
            using (var conn = new OleDbConnection(CONN))
            {
                conn.Open();

                using (var cmd = new OleDbCommand(@"
SELECT 
    I.Name,
    I.ProfilePicture,
    D.Specialization,
    I.Email,
    I.PhoneNumber,
    I.Address,
    I.Gender,
    I.BloodType
FROM Information I
INNER JOIN Doctors D ON I.LoginID = D.LoginID
WHERE I.LoginID = ?", conn))
                {
                    cmd.Parameters.AddWithValue("?", loginId);
                    using (var rdr = cmd.ExecuteReader())
                    {
                        if (rdr.Read())
                        {
                            string name = rdr["Name"]?.ToString() ?? "N/A";
                            string specialization = rdr["Specialization"]?.ToString() ?? "N/A";

                            lblName.Text = name;
                            lblBigName.Text = name;
                            lblJobTitle.Text = specialization;

                            var pictureData = rdr["ProfilePicture"];
                            if (pictureData != DBNull.Value && pictureData is byte[] imageBytes)
                            {
                                try
                                {
                                    using (var ms = new MemoryStream(imageBytes))
                                    {
                                        pbxProfile.Image?.Dispose();
                                        pbxProfile.Image = Image.FromStream(ms);
                                        pbxProfile.SizeMode = PictureBoxSizeMode.Zoom;
                                        pbxProfile.BackColor = Color.Transparent;
                                    }
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show($"Error loading profile picture: {ex.Message}", "Warning",
                                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    pbxProfile.Image = null;
                                }
                            }
                            else
                            {
                                pbxProfile.Image = null;
                            }
                        }
                    }
                }

                using (var cmd = new OleDbCommand(@"
SELECT 
    C.ClinicName,
    IIF(C.Address IS NULL, '', ' - ' & C.Address) & 
    IIF(C.PhoneNumber IS NULL, '', ' (' & C.PhoneNumber & ')') as AdditionalInfo
FROM Clinics C
INNER JOIN Doctors D ON C.ClinicID = D.ClinicID
WHERE D.LoginID = ?
ORDER BY C.ClinicName", conn))
                {
                    cmd.Parameters.AddWithValue("?", loginId);
                    
                    var dt = new DataTable();
                    using (var da = new OleDbDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }

                    var formattedList = new DataTable();
                    formattedList.Columns.Add("DisplayText", typeof(string));
                    formattedList.Columns.Add("ClinicName", typeof(string));

                    foreach (DataRow row in dt.Rows)
                    {
                        string clinicName = row["ClinicName"]?.ToString() ?? "";
                        string additionalInfo = row["AdditionalInfo"]?.ToString() ?? "";
                        formattedList.Rows.Add(clinicName + additionalInfo, clinicName);
                    }

                    listClinics.DataSource = formattedList;
                    listClinics.DisplayMember = "DisplayText";
                    listClinics.ValueMember = "ClinicName";
                }
            }
        }
    }
}