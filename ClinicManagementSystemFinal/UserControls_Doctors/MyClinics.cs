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

namespace ClinicManagementSystemFinal.UserControls_Doctors
{
    public partial class MyClinics : UserControl
    {
        public MyClinics()
        {
            InitializeComponent();
        }

        private void tableLayoutPanel11_Paint(object sender, PaintEventArgs e)
        {

        }

        private void imgClinic1_Click(object sender, EventArgs e)
        {

        }

        public void LoadMyClinics(string loginId)
        {
            List<string> clinicNames = new List<string>();

            OleDbConnection conn = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=B:\Downloads\Login.accdb;Persist Security Info=False;");
            conn.Open();

            OleDbCommand cmd = new OleDbCommand(@"
        SELECT C.ClinicName
        FROM Doctors D
        INNER JOIN Clinics C ON D.ClinicID = C.ClinicID
        WHERE D.LoginID = @loginId", conn);

            cmd.Parameters.AddWithValue("@loginId", loginId);
            OleDbDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                clinicNames.Add(reader["ClinicName"].ToString());
            }

            reader.Close();
            conn.Close();

            HideAllPanels();

            for (int i = 0; i < clinicNames.Count; i++)
            {
                Control panel = this.Controls.Find("panelClinic" + (i + 1), true).FirstOrDefault();
                if (panel != null)
                {
                    panel.Visible = true;
                }

                Label label = this.Controls.Find("clinicName" + (i + 1), true).FirstOrDefault() as Label;
                if (label != null)
                {
                    label.Text = clinicNames[i];
                }
            }
        }

        private void HideAllPanels()
        {
            for (int i = 1; i <= 9; i++)
            {
                Control panel = this.Controls.Find("panelClinic" + i, true).FirstOrDefault();
                if (panel != null)
                {
                    panel.Visible = false;
                }
            }
        }
    }
}
