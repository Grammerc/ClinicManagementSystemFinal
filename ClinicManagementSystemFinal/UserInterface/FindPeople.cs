using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ClinicManagementSystemFinal.UserControls_Doctors;

namespace ClinicManagementSystemFinal.UserInterface
{
    public partial class FindPeople : UserControl
    {
        readonly string myLoginId;
        readonly string myRole;
        const string CONN = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=B:\Downloads\Login.accdb;Persist Security Info=False;";

        public FindPeople(string loginId)
        {
            InitializeComponent();
            myLoginId = loginId;
            myRole = GetRole(loginId);
            tbxName.TextChanged += (s, e) => RefreshList();
            cbxRole.SelectedIndexChanged += (s, e) => RefreshList();
            cbxSpec.SelectedIndexChanged += (s, e) => RefreshList();
            LoadFilterCombos();
            RefreshList();
        }

        string GetRole(string loginId)
        {
            using var c = new OleDbConnection(CONN);
            c.Open();

            var cmd = new OleDbCommand(
                "SELECT R.RoleName " +
                "FROM   Account AS A INNER JOIN Roles AS R ON Val(A.RoleID)=Val(R.RoleID) " +
                "WHERE  Val(A.LoginID)=?", c);

            if (int.TryParse(loginId, out int idNum))
                cmd.Parameters.Add("?", OleDbType.Integer).Value = idNum;
            else
                cmd.Parameters.Add("?", OleDbType.VarWChar).Value = loginId;

            try
            {
                var res = cmd.ExecuteScalar();
                return (res == null || res == DBNull.Value)
                    ? "User"
                    : res.ToString();
            }
            catch (OleDbException ex)
            {
   
                MessageBox.Show(
                    "SQL Error in GetRole:\n" +
                    cmd.CommandText + "\n\n" +
                    $"Param[0] = {cmd.Parameters[0].Value} ({cmd.Parameters[0].OleDbType})\n\n" +
                    ex.Message,
                    "Database Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );

                return "User";
            }
        }

        void LoadFilterCombos()
        {
            using var c = new OleDbConnection(CONN); c.Open();
            var da = new OleDbDataAdapter("SELECT DISTINCT RoleName FROM Roles", c);
            var dt = new DataTable(); da.Fill(dt);
            cbxRole.Items.AddRange(dt.AsEnumerable().Select(r => r[0].ToString()).ToArray());
            da = new OleDbDataAdapter("SELECT DISTINCT Specialization FROM Doctors", c); dt.Clear(); da.Fill(dt);
            cbxSpec.Items.AddRange(dt.AsEnumerable().Select(r => r[0].ToString()).ToArray());
        }

        void RefreshList()
        {
            panelMain.Controls.Clear();
            foreach (var p in QueryPeople())
            {
                var card = new PeopleProfile(); card.Bind(p); card.ViewClicked += Card_ViewClicked;
                panelMain.Controls.Add(card);
            }
        }

        IEnumerable<Person> QueryPeople()
        {
            string likeName = "%" + tbxName.Text.Trim() + "%";
            using var c = new OleDbConnection(CONN);
            c.Open();

            // 1) Sql with Val(...) on every potentially mismatched join
            string sql =
        @"SELECT A.LoginID,
       I.Name,
       R.RoleName,
       IIf(IsNull(D.Specialization),'',D.Specialization)    AS Spec,
       IIf(IsNull(I.ProfileImagePath),'',I.ProfileImagePath) AS ImgPath,
       I.ProfilePicture,
       IIf(IsNull(A.ClinicID),0,A.ClinicID)               AS CID
  FROM ((Account A 
          INNER JOIN Roles R    ON Val(A.RoleID)    = Val(R.RoleID))
         INNER JOIN Information I ON Val(A.LOGINID) = Val(I.LOGINID))
         LEFT  JOIN Doctors D  ON Val(A.LOGINID)   = Val(D.LOGINID)
 WHERE I.Name LIKE ?";

            var cmd = new OleDbCommand(sql, c);
            cmd.Parameters.Add("?", OleDbType.VarWChar).Value = likeName;

            // 2) Apply filters exactly as before
            if (cbxRole.Text != "")
            {
                cmd.CommandText += " AND R.RoleName = ?";
                cmd.Parameters.Add("?", OleDbType.VarWChar).Value = cbxRole.Text;
            }
            if (cbxSpec.Text != "")
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

                cmd.CommandText += @"
      AND (
           R.RoleName = 'Doctor'
        OR ( R.RoleName = 'User'
             AND EXISTS(
                 SELECT 1 
                   FROM Appointments AP
                  WHERE AP.UserInfoID  = I.UserInfoID
                    AND AP.ClinicID   IN(" + inList + @")
             )
           )
      )";
            }

            // 3) Run with try/catch to pinpoint any remaining mismatch
            OleDbDataReader r = null;
            try
            {
                r = cmd.ExecuteReader();
            }
            catch (OleDbException ex)
            {
                var msg = new StringBuilder();
                msg.AppendLine("SQL Error in QueryPeople()");
                msg.AppendLine("----------- CommandText -----------");
                msg.AppendLine(cmd.CommandText);
                msg.AppendLine("--------- Parameters ---------");
                for (int i = 0; i < cmd.Parameters.Count; i++)
                {
                    var p = cmd.Parameters[i];
                    msg.AppendLine($"Param[{i}]: '{p.Value}'  Type={p.OleDbType}");
                }
                msg.AppendLine();
                msg.AppendLine("Exception:");
                msg.AppendLine(ex.Message);
                MessageBox.Show(msg.ToString(), "Database Type Mismatch", MessageBoxButtons.OK, MessageBoxIcon.Error);
                yield break;
            }

            // 4) If we’ve made it here, read rows
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

            r.Close();
        }

        List<string> GetMyClinicIds()
        {
            using var c = new OleDbConnection(CONN); c.Open();
            var da = new OleDbDataAdapter("SELECT ClinicID FROM Doctors WHERE LoginID=?", c);
            da.SelectCommand.Parameters.Add("?", OleDbType.Integer).Value = int.Parse(myLoginId);
            var dt = new DataTable(); da.Fill(dt);
            return dt.AsEnumerable().Select(r => r[0].ToString()).ToList();
        }

        void Card_ViewClicked(Person p)
        {
            if (FindForm() is HomePage_Doctor hDoc)
            {
                if (p.Role == "Doctor")
                    hDoc.LoadControl(new DoctorDetail(p.LoginId));
                else
                    hDoc.LoadControl(new MedicalHistory(p.LoginId, p.Name, p.ImagePath, p.ClinicId, myLoginId));
                return;
            }

            if (FindForm() is HomePage_User hUser)
            {
                hUser.LoadControl(new DoctorDetail(p.LoginId));   // users can view only doctors
            }
        }
    }
}