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
using Guna.UI2.WinForms;

namespace ClinicManagementSystemFinal.UserControls_Doctors
{
    public partial class MyClinics : UserControl
    {

        private readonly string imageFolderPath =
    @"C:\Users\Raphael Perocho\source\repos\ClinicManagementSystemFinal\ProjectClinic\ClinicManagementSystemFinal\Pictures\ClinicPictures\";
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
            // 1. pull clinic names — you already do this
            List<string> clinicNames = new List<string>();

            using (var conn = new OleDbConnection(
                   @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=B:\Downloads\Login.accdb;Persist Security Info=False;"))
            {
                conn.Open();
                var cmd = new OleDbCommand(@"
            SELECT C.ClinicName
            FROM   Doctors D INNER JOIN Clinics C ON D.ClinicID = C.ClinicID
            WHERE  D.LoginID = @loginId", conn);
                cmd.Parameters.AddWithValue("@loginId", loginId);

                using (var rdr = cmd.ExecuteReader())
                    while (rdr.Read())
                        clinicNames.Add(rdr["ClinicName"].ToString());
            }

            // 2. hide all 9 slots first
            HideAllPanels();

            // 3. populate up to 9 clinics
            for (int i = 0; i < clinicNames.Count && i < 9; i++)
            {
                string name = clinicNames[i];

                // panel + label
                var pnl = Controls.Find($"panelClinic{i + 1}", true).FirstOrDefault();
                var lbl = Controls.Find($"clinicName{i + 1}", true).FirstOrDefault() as Label;
                var imgB = Controls.Find($"imgClinic{i + 1}", true).FirstOrDefault() as Guna2ImageButton;

                if (pnl == null || lbl == null || imgB == null) continue;

                pnl.Visible = true;
                lbl.Text = name;

                // picture (png then jpg)
                string imgPath = Path.Combine(imageFolderPath, $"{name}.png");
                if (!File.Exists(imgPath))
                    imgPath = Path.Combine(imageFolderPath, $"{name}.jpg");

                if (imgB.Image != null) { var old = imgB.Image; imgB.Image = null; old.Dispose(); }
                imgB.Image = File.Exists(imgPath) ? Image.FromFile(imgPath) : null;

                // keep size fixed – no hover zoom
                imgB.ImageSize = imgB.Size;
                imgB.HoverState.ImageSize = imgB.Size;
                imgB.PressedState.ImageSize = imgB.Size;

                // (optional) store the name for later clicks
                imgB.Tag = name;
                imgB.Click -= imgClinic_Click;
                imgB.Click += imgClinic_Click;
            }
        }

        private void imgClinic_Click(object sender, EventArgs e)
        {
            if (sender is not Guna2ImageButton btn || btn.Tag == null) return;
            string clinicName = btn.Tag.ToString();

            // do whatever action you need when a doctor clicks their clinic
            MessageBox.Show($"Clicked: {clinicName}");
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
