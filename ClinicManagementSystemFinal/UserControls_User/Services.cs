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
using System.IO;
using Guna.UI2.WinForms;

namespace ClinicManagementSystemFinal.UserControls_User
{
    
    public partial class Services : UserControl
    {
        private string userLoginId;
        private List<string> serviceNames = new List<string>();
        private int currentPage = 0;
        private const int servicesPerPage = 9;
        private string imageFolderPath = @"C:\Users\Raphael\source\repos\ClinicManagementSystemFinal\ClinicManagementSystemFinal\Pictures\ServiceImages\";
        public Services(string loginId)
        {
            InitializeComponent();
            this.Load += new EventHandler(Services_Load);
            userLoginId = loginId;

        }

        private void LoadAllServices()
        {
            serviceNames.Clear();

            string connStr = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\Raphael\source\repos\ClinicManagementSystemFinal\ClinicManagementSystemFinal\Login.accdb;";
            using (OleDbConnection conn = new OleDbConnection(connStr))
            {
                conn.Open();
                OleDbCommand cmd = new OleDbCommand("SELECT ServiceName FROM Services ORDER BY ServiceName ASC", conn);
                OleDbDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    serviceNames.Add(reader["ServiceName"].ToString());
                }

                conn.Close();
            }
            DisplayServices();
        }
        private void DisplayServices()
        {
            for (int i = 0; i < servicesPerPage; i++)
            {
                int index = currentPage * servicesPerPage + i;

                var lbl = Controls.Find($"serviceName{i + 1}", true).FirstOrDefault() as Label;
                var btn = Controls.Find($"btnImage{i + 1}", true).FirstOrDefault() as Guna2ImageButton;
                if (lbl == null || btn == null) continue;

                if (index < serviceNames.Count)
                {
                    string service = serviceNames[index];
                    lbl.Text = service;

                    string imgPath = Path.Combine(imageFolderPath, $"{service}.png");
                    if (!File.Exists(imgPath))
                        imgPath = Path.Combine(imageFolderPath, $"{service}.jpg");
                    btn.Image = File.Exists(imgPath) ? Image.FromFile(imgPath) : null;

                    btn.ImageSize = btn.Size;
                    btn.HoverState.ImageSize = btn.Size;   
                    btn.PressedState.ImageSize = btn.Size;

                    btn.Tag = service;
                    btn.Click -= ServiceImage_Click;
                    btn.Click += ServiceImage_Click;

                    lbl.Visible = btn.Visible = true;
                }
                else
                {
                    lbl.Visible = btn.Visible = false;
                    btn.Image = null;
                }
            }
        }

        private void ServiceImage_Click(object sender, EventArgs e)
        {
            if (sender is not Guna2ImageButton btn || btn.Tag == null) return;

            string serviceName = btn.Tag.ToString();

            var host = this.FindForm() as HomePage_User;
            if (host == null) return;

            host.LoadControl(new Clinics(userLoginId, serviceName));
        }



        private Control GetControlRecursive(Control parent, string name)
        {
            foreach (Control c in parent.Controls)
            {
                if (c.Name == name) return c;
                var found = GetControlRecursive(c, name);
                if (found != null) return found;
            }
            return null;
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if ((currentPage + 1) * servicesPerPage < serviceNames.Count)
            {
                currentPage++;
                DisplayServices();
            }
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            if (currentPage > 0)
            {
                currentPage--;
                DisplayServices();
            }
        }




        private void Services_Load(object sender, EventArgs e)
        {

            LoadAllServices();
            

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
