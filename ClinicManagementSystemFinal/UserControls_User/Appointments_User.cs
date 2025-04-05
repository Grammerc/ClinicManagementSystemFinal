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
        public Appointments_User()
        {
            InitializeComponent();
            conn.ConnectionString = @"Provider = Microsoft.ACE.OLEDB.12.0; Data Source = ""B:\Downloads\Login.accdb"";
            Persist Security Info=False;";
            LoadClinicNames();
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
    }
}
