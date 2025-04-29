using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using ClinicManagementSystemFinal.UserInterface;
using ClinicManagementSystemFinal.UserControls_Doctors;

namespace ClinicManagementSystemFinal.UserInterface
{
    public partial class FindPeople : UserControl
    {
        private readonly string myLoginId;
        private readonly string myRole;
        private bool _isSecretary;
        private const string CONN = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\Raphael\Downloads\Login.accdb;Persist Security Info=False;";
        public FindPeople(string loginId, bool isSecretary)
        {
            InitializeComponent();
            myLoginId = loginId;
            _isSecretary = isSecretary;
            myRole = GetRole(loginId);
            tbxName.TextChanged += (s, e) => RefreshList();
            cbxRole.SelectedIndexChanged += (s, e) => RefreshList();
            cbxSpec.SelectedIndexChanged += (s, e) => RefreshList();
            LoadFilterCombos();
            RefreshList();
        }
        private string GetRole(string loginId)
        {
            using var c = new OleDbConnection(CONN);
            c.Open();
            var cmd = new OleDbCommand("SELECT R.RoleName FROM Account AS A INNER JOIN Roles AS R ON Val(A.RoleID)=Val(R.RoleID) WHERE Val(A.LoginID)=?", c);
            if (int.TryParse(loginId, out int idNum))
                cmd.Parameters.Add("?", OleDbType.Integer).Value = idNum;
            else
                cmd.Parameters.Add("?", OleDbType.VarWChar).Value = loginId;
            var res = cmd.ExecuteScalar();
            return res == null || res == DBNull.Value ? "User" : res.ToString();
        }
        private void LoadFilterCombos()
        {
            using var c = new OleDbConnection(CONN);
            c.Open();
            var da = new OleDbDataAdapter("SELECT DISTINCT RoleName FROM Roles", c);
            var dt = new DataTable();
            da.Fill(dt);
            cbxRole.Items.AddRange(dt.AsEnumerable().Select(r => r[0].ToString()).ToArray());
            da = new OleDbDataAdapter("SELECT DISTINCT Specialization FROM Doctors", c);
            dt.Clear();
            da.Fill(dt);
            cbxSpec.Items.AddRange(dt.AsEnumerable().Select(r => r[0].ToString()).ToArray());
        }
        private void RefreshList()
        {
            panelMain.Controls.Clear();
            foreach (var p in QueryPeople())
            {
                var card = new PeopleProfile();
                card.Bind(p, _isSecretary);
                card.ViewClicked += OnProfileViewClicked;
                panelMain.Controls.Add(card);
            }
        }
        private IEnumerable<Person> QueryPeople()
        {
            string likeName = "%" + tbxName.Text.Trim() + "%";
            using var c = new OleDbConnection(CONN);
            c.Open();
            string sql =
                @"SELECT 
                    A.LoginID,
                    I.Name,
                    R.RoleName,
                    IIf(IsNull(D.Specialization),'',D.Specialization) AS Spec,
                    IIf(IsNull(I.ProfileImagePath),'',I.ProfileImagePath) AS ImgPath,
                    I.ProfilePicture,
                    IIf(IsNull(A.ClinicID),0,A.ClinicID)             AS CID
                  FROM ((Account A
                         INNER JOIN Roles       R ON Val(A.RoleID)=Val(R.RoleID))
                        INNER JOIN Information I ON Val(A.LOGINID)=Val(I.LOGINID))
                         LEFT JOIN Doctors     D ON Val(A.LOGINID)=Val(D.LOGINID)
                 WHERE I.Name LIKE ?";
            var cmd = new OleDbCommand(sql, c);
            cmd.Parameters.Add("?", OleDbType.VarWChar).Value = likeName;
            if (!string.IsNullOrWhiteSpace(cbxRole.Text))
            {
                cmd.CommandText += " AND R.RoleName = ?";
                cmd.Parameters.Add("?", OleDbType.VarWChar).Value = cbxRole.Text;
            }
            if (!string.IsNullOrWhiteSpace(cbxSpec.Text))
            {
                cmd.CommandText += " AND D.Specialization = ?";
                cmd.Parameters.Add("?", OleDbType.VarWChar).Value = cbxSpec.Text;
            }
            if (myRole == "User")
                cmd.CommandText += " AND R.RoleName = 'Doctor'";
            if (myRole == "Doctor")
            {
                var ids = GetMyClinicIds();
                string inList = ids.Count == 0 ? "0" : string.Join(",", ids);
                cmd.CommandText +=
                    " AND (R.RoleName = 'Doctor' OR (R.RoleName = 'User' AND EXISTS(" +
                    "SELECT 1 FROM Appointments AP WHERE AP.UserInfoID = I.UserInfoID AND AP.ClinicID IN(" + inList + "))))";
            }
            using var r = cmd.ExecuteReader();
            while (r.Read())
            {
                yield return new Person
                {
                    LoginId = r["LoginID"].ToString(),
                    Name = r["Name"].ToString(),
                    Role = r["RoleName"].ToString(),
                    Specialization = r["Spec"].ToString(),
                    ImagePath = r["ImgPath"].ToString(),
                    ImageBlob = r["ProfilePicture"] as byte[],
                    ClinicId = r["CID"].ToString()
                };
            }
        }
        private List<string> GetMyClinicIds()
        {
            using var c = new OleDbConnection(CONN);
            c.Open();
            var da = new OleDbDataAdapter("SELECT ClinicID FROM Doctors WHERE LoginID=?", c);
            da.SelectCommand.Parameters.Add("?", OleDbType.Integer).Value = int.Parse(myLoginId);
            var dt = new DataTable();
            da.Fill(dt);
            return dt.AsEnumerable().Select(r => r[0].ToString()).ToList();
        }
        private void OnProfileViewClicked(Person p)
        {
            if (_isSecretary && p.Role != "Doctor")
            {
                MessageBox.Show(
                    "Secretaries are not allowed to view patient medical histories.",
                    "Access Denied",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }
            var host = FindForm();
            if (host is HomePage_Doctor hDoc)
            {
                if (p.Role == "Doctor")
                    hDoc.LoadControl(new DoctorDetail(p.LoginId));
                else
                    hDoc.LoadControl(new MedicalHistory(p.LoginId, p.Name, p.ImagePath, p.ClinicId, myLoginId));
            }
            else if (host is HomePage_User hUsr)
            {
                hUsr.LoadControl(new DoctorDetail(p.LoginId));
            }
        }
    }
}
