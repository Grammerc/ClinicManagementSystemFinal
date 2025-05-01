using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace ClinicManagementSystemFinal.UserControls_User
{
    public partial class Dashboard_User : UserControl
    {

        private string userLoginId;
        public Dashboard_User(string loginId)
        {
            InitializeComponent();
            userLoginId = loginId;
            LoadUserName(userLoginId);
        }

        private void guna2Panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void LoadUserName(string loginId)
        {
            string connStr = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\Raphael\source\repos\ClinicManagementSystemFinal\ClinicManagementSystemFinal\Login.accdb;";
            using (OleDbConnection conn = new OleDbConnection(connStr))
            {
                conn.Open();
                string query = "SELECT Name FROM Information WHERE LoginID = @loginId";
                OleDbCommand cmd = new OleDbCommand(query, conn);
                cmd.Parameters.AddWithValue("@loginId", loginId);

                object result = cmd.ExecuteScalar();
                if (result != null && result != DBNull.Value)
                {
                    lblName.Text = result.ToString();
                }

                conn.Close();
            }
        }
    }
}
