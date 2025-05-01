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
using ServiceStack;

namespace ClinicManagementSystemFinal.UserControls_User
{
    public partial class Clinics : UserControl
    {
        private string userLoginId;
        private string preselectedService;
        private string imageFolderPath = @"C:\Users\Raphael\source\repos\ClinicManagementSystemFinal\ClinicManagementSystemFinal\Pictures\ClinicPictures";
        
        public Clinics(string loginId) : this(loginId, null) {  }
        public Clinics(string loginId, string serviceFilters)
        {
            InitializeComponent();
            preselectedService = serviceFilters;

            this.Load += new System.EventHandler(this.Clinics_Load);
            cbxSpecialization.SelectedIndexChanged += AnyFilterChanged;
            cbxInsuranceCoverage.SelectedIndexChanged += AnyFilterChanged;
            cbxLocation.SelectedIndexChanged += AnyFilterChanged;
            cbxTimeSlot.SelectedIndexChanged += AnyFilterChanged;
            cbxServices.SelectedIndexChanged += AnyFilterChanged;
            userLoginId = loginId;
        }

        private void Clinics_Load(object sender, EventArgs e)
        {
            PopulateFilterComboBoxes();

            //—auto‑select the incoming Service filter—
            if (!string.IsNullOrEmpty(preselectedService))
            {
                int idx = cbxServices.Items.IndexOf(preselectedService);
                if (idx >= 0) cbxServices.SelectedIndex = idx;
                else cbxServices.Text = preselectedService;
            }

            ApplyClinicFilters();
        }

        private void AnyFilterChanged(object sender, EventArgs e)
        {
            ApplyClinicFilters();
        }

        private void PopulateFilterComboBoxes()
        {
            cbxSpecialization.Items.Clear();
            cbxInsuranceCoverage.Items.Clear();
            cbxLocation.Items.Clear();
            cbxTimeSlot.Items.Clear();
            cbxServices.Items.Clear();

            string connStr = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\Raphael\source\repos\ClinicManagementSystemFinal\ClinicManagementSystemFinal\Login.accdb;";
            using (OleDbConnection conn = new OleDbConnection(connStr))
            {
                conn.Open();

                OleDbCommand cmd = new OleDbCommand("SELECT Specialization, [Insurance Coverage], Address, TimeSlot, Services FROM Clinics", conn);
                OleDbDataReader reader = cmd.ExecuteReader();

                HashSet<string> specs = new HashSet<string>();
                HashSet<string> insurances = new HashSet<string>();
                HashSet<string> locations = new HashSet<string>();
                HashSet<string> times = new HashSet<string>();
                HashSet<string> services = new HashSet<string>();

                while (reader.Read())
                {
                    foreach (var item in reader["Specialization"].ToString().Split(','))
                        specs.Add(item.Trim());
                    foreach (var item in reader["Insurance Coverage"].ToString().Split(','))
                        insurances.Add(item.Trim());
                    foreach (var item in reader["Services"].ToString().Split(','))
                        services.Add(item.Trim());
                    string address = reader["Address"].ToString();
                    if (address.Contains(","))
                    {
                        string city = address.Split(',').Last().Trim();
                        if (!string.IsNullOrEmpty(city))
                            locations.Add(city);
                    }

                    times.Add(reader["TimeSlot"].ToString().Trim());
                }

                cbxSpecialization.Items.AddRange(specs.ToArray());
                cbxInsuranceCoverage.Items.AddRange(insurances.ToArray());
                cbxLocation.Items.AddRange(locations.ToArray());
                cbxTimeSlot.Items.AddRange(times.ToArray());
                cbxServices.Items.AddRange(services.ToArray());

                conn.Close();
            }
        }



        private void ApplyClinicFilters()
        {
            const int maxPanels = 6;
            string connStr = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\Raphael\source\repos\ClinicManagementSystemFinal\ClinicManagementSystemFinal\Login.accdb;";
            using (var conn = new OleDbConnection(connStr))
            {
                conn.Open();
                var rdr = new OleDbCommand("SELECT * FROM Clinics", conn).ExecuteReader();
                int i = 1;

                while (rdr.Read() && i <= maxPanels)
                {
                    if (!MatchesFilters(rdr)) { continue; }

                    // locate controls
                    var pnl = Controls.Find($"panelClinic{i}", true).FirstOrDefault() as Panel;
                    var lblN = Controls.Find($"clinicsName{i}", true).FirstOrDefault() as Label;
                    var lblT = Controls.Find($"clinicTag{i}", true).FirstOrDefault() as Label;
                    var img = Controls.Find($"clinicImage{i}", true).FirstOrDefault() as Guna2ImageButton;
                    if (pnl == null || lblN == null || lblT == null || img == null) break;

                    // show info
                    string clinicName = rdr["ClinicName"].ToString();
                    pnl.Visible = true;
                    lblN.Text = clinicName;
                    lblT.Text = rdr["Specialization"].ToString();

                    // load picture (png first, then jpg)
                    string imgPath = Path.Combine(imageFolderPath, $"{clinicName}.png");
                    if (!File.Exists(imgPath))
                        imgPath = Path.Combine(imageFolderPath, $"{clinicName}.jpg");

                    if (img.Image != null) { var old = img.Image; img.Image = null; old.Dispose(); }
                    img.Image = File.Exists(imgPath) ? Image.FromFile(imgPath) : null;

                    /* stop the hover‑shrink / zoom */
                    img.ImageSize = img.Size;          // normal state
                    img.HoverState.ImageSize = img.Size;          // hover = same size
                    img.PressedState.ImageSize = img.Size;          // pressed = same size
                    img.HoverState.ImageOffset = Point.Empty;       // no slide
                    img.PressedState.ImageOffset = Point.Empty;

                    // make clickable
                    img.Tag = clinicName;
                    img.Click -= ClinicImage_Click;
                    img.Click += ClinicImage_Click;

                    i++;
                }

                // hide unused panels
                for (int j = i; j <= maxPanels; j++)
                    (Controls.Find($"panelClinic{j}", true).FirstOrDefault() as Panel)?.Hide();
            }
        }

        private void ClinicImage_Click(object sender, EventArgs e)
        {
            if (sender is not Guna2ImageButton img || img.Tag == null) return;
            string clinicName = img.Tag.ToString();

            // find the host form (HomePage_User)
            var host = this.FindForm() as HomePage_User;
            if (host == null) return;          // safety check

            // swap in the appointment user‑control on the main panel
            host.LoadControl(new Appointments_Create(userLoginId, clinicName));
        }

        private bool MatchesFilters(OleDbDataReader r)
        {
            bool spec = string.IsNullOrEmpty(cbxSpecialization.Text) || r["Specialization"].ToString().Contains(cbxSpecialization.Text);
            bool ins = string.IsNullOrEmpty(cbxInsuranceCoverage.Text) || r["Insurance Coverage"].ToString().Contains(cbxInsuranceCoverage.Text);
            bool loc = string.IsNullOrEmpty(cbxLocation.Text) || r["Address"].ToString().Contains(cbxLocation.Text);
            bool t = string.IsNullOrEmpty(cbxTimeSlot.Text) || r["TimeSlot"].ToString() == cbxTimeSlot.Text;
            return spec && ins && loc && t;
        }

        private void cbxLocation_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cbxServices_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}