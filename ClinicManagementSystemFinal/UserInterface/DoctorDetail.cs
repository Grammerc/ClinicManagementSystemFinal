using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;
using Org.BouncyCastle.Asn1.Pkcs;

namespace ClinicManagementSystemFinal.UserInterface
{
    public partial class DoctorDetail : UserControl
    {
        readonly string loginId;
        const string CONN =
            @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=B:\Downloads\Login.accdb;Persist Security Info=False;";

        public DoctorDetail(string docLoginId)
        {
            InitializeComponent();
            loginId = docLoginId;
            Load += (s, e) => Fill();
        }

        void Fill()
        {
            using var c = new OleDbConnection(CONN);
            c.Open();
            var cmd = new OleDbCommand(
                @"SELECT I.Name, D.Specialization, I.ProfileImagePath, I.ProfilePicture
                  FROM Information I INNER JOIN Doctors D ON I.LoginID = D.LoginID
                  WHERE I.LoginID = ?", c);
            cmd.Parameters.AddWithValue("?", loginId);
            using var r = cmd.ExecuteReader();
            if (r.Read())
            {
                lblName.Text = r["Name"].ToString();
                lblSpec.Text = r["Specialization"].ToString();
                string img = r["ProfileImagePath"].ToString();
                byte[] blob = r["ProfilePicture"] as byte[];
                if (!string.IsNullOrWhiteSpace(img) && File.Exists(img))
                    pbx.Image = Image.FromFile(img);
                else if (blob != null && blob.Length > 0)
                    using (var ms = new MemoryStream(blob))
                        pbx.Image = Image.FromStream(ms);
            }

            var da = new OleDbDataAdapter(
                @"SELECT ClinicName FROM Clinics
                  WHERE ClinicID IN (SELECT ClinicID FROM Doctors WHERE LoginID = ?)", c);
            da.SelectCommand.Parameters.AddWithValue("?", loginId);
            var dt = new DataTable();
            da.Fill(dt);
            listClinics.DataSource = dt;      // listbox or grid showing clinics
        }
    }
}