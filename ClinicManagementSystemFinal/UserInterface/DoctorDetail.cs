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
            @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\Raphael\Downloads\Login.accdb;Persist Security Info=False;";

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

                // 1) Basic doctor info
                using (var cmd = new OleDbCommand(@"
SELECT
    I.Name,
    D.Specialization,
    I.ProfileImagePath,
    I.ProfilePicture
  FROM Information AS I
  INNER JOIN Doctors AS D
    ON I.LoginID = D.LoginID
 WHERE I.LoginID = ?", conn))
                {
                    cmd.Parameters.AddWithValue("?", loginId);
                    using (var rdr = cmd.ExecuteReader())
                    {
                        if (rdr.Read())
                        {
                            lblName.Text = rdr["Name"].ToString();
                            lblSpec.Text = rdr["Specialization"].ToString();

                            // load picture: prefer file path, else blob
                            var path = rdr["ProfileImagePath"].ToString();
                            var blob = rdr["ProfilePicture"] as byte[];
                            if (!string.IsNullOrWhiteSpace(path) && File.Exists(path))
                            {
                                pbxProfile.Image = Image.FromFile(path);
                            }
                            else if (blob != null && blob.Length > 0)
                            {
                                using var ms = new MemoryStream(blob);
                                pbxProfile.Image = Image.FromStream(ms);
                            }
                            pbxProfile.SizeMode = PictureBoxSizeMode.Zoom;
                        }
                    }
                }

                // 2) Clinics list
                var dt = new DataTable();
                using (var da = new OleDbDataAdapter(@"
SELECT
    C.ClinicName
  FROM Clinics AS C
  INNER JOIN Doctors AS D
    ON C.ClinicID = D.ClinicID
 WHERE D.LoginID = ?", conn))
                {
                    da.SelectCommand.Parameters.AddWithValue("?", loginId);
                    da.Fill(dt);
                }

                listClinics.DataSource = dt;
                listClinics.DisplayMember = "ClinicName";
                listClinics.ValueMember = "ClinicName";
            }
        }
    }
}