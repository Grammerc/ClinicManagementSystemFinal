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

namespace ClinicManagementSystemFinal.UserControls_User
{
    public partial class Services : UserControl
    {

        private List<string> serviceNames = new List<string>();
        private int currentPage = 0;
        private const int servicesPerPage = 9;
        private string imageFolderPath = @"C:\Users\Raphael\Source\Repos\ClinicManagementSystemFinal\ClinicManagementSystemFinal\Pictures\ServiceImages\";
        public Services()
        {
            InitializeComponent();
            this.Load += new EventHandler(Services_Load);


        }

        private void LoadAllServices()
        {
            serviceNames.Clear();

            string connStr = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\Raphael\Downloads\Login.accdb;";
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
                var btn = Controls.Find($"btnImage{i + 1}", true).FirstOrDefault() as Guna.UI2.WinForms.Guna2ImageButton;

                if (lbl == null)
                    MessageBox.Show($"Label 'serviceName{i + 1}' not found");

                if (btn == null)
                    MessageBox.Show($"Guna2ImageButton 'btnImage{i + 1}' not found");

                if (lbl != null && btn != null)
                {
                    if (index < serviceNames.Count)
                    {
                        string service = serviceNames[index];
                        lbl.Text = service;

                        string imagePath = Path.Combine(imageFolderPath, $"{service}.png");
                        if (!File.Exists(imagePath))
                            imagePath = Path.Combine(imageFolderPath, $"{service}.jpg");

                        if (File.Exists(imagePath))
                        {
                            btn.Image = Image.FromFile(imagePath);
                            btn.ImageSize = btn.Size;
                            btn.HoverState.ImageSize = btn.Size; 
                            btn.PressedState.ImageSize = btn.Size; 
                        }
                        else
                        {
                            btn.Image = null;
                        }

                        lbl.Visible = true;
                        btn.Visible = true;
                        btn.ImageSize = new Size(btn.Width, btn.Height); 
                        btn.ImageRotate = 0;
                        btn.ImageOffset = new Point(0, 0);
                        btn.BackColor = Color.Transparent;
                        btn.UseTransparentBackground = true;
                    }
                    else
                    {
                        lbl.Text = "";
                        lbl.Visible = false;
                        btn.Image = null;
                        btn.Visible = false;
                    }
                }
            }
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
