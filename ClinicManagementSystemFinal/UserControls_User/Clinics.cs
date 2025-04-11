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
    public partial class Clinics : UserControl
    {
        public Clinics()
        {
            InitializeComponent();
            this.Load += new System.EventHandler(this.Clinics_Load);
            cbxSpecialization.SelectedIndexChanged += AnyFilterChanged;
            cbxInsuranceCoverage.SelectedIndexChanged += AnyFilterChanged;
            cbxLocation.SelectedIndexChanged += AnyFilterChanged;
            cbxTimeSlot.SelectedIndexChanged += AnyFilterChanged;
            cbxServices.SelectedIndexChanged += AnyFilterChanged;
        }

        private void Clinics_Load(object sender, EventArgs e)
        {
            PopulateFilterComboBoxes();
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

            string connStr = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\Raphael\Downloads\Login.accdb;";
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
            string connStr = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\Raphael\Downloads\Login.accdb;";
            using (OleDbConnection conn = new OleDbConnection(connStr))
            {
                conn.Open();

                string query = "SELECT * FROM Clinics";
                OleDbCommand cmd = new OleDbCommand(query, conn);
                OleDbDataReader reader = cmd.ExecuteReader();

                int panelIndex = 1;

                while (reader.Read() && panelIndex <= 6)
                {
                    string specialization = reader["Specialization"].ToString();
                    string insurance = reader["Insurance Coverage"].ToString();
                    string address = reader["Address"].ToString();
                    string timeSlot = reader["TimeSlot"].ToString();

                    if (!string.IsNullOrEmpty(cbxSpecialization.Text) && !specialization.Contains(cbxSpecialization.Text)) continue;
                    if (!string.IsNullOrEmpty(cbxInsuranceCoverage.Text) && !insurance.Contains(cbxInsuranceCoverage.Text)) continue;
                    if (!string.IsNullOrEmpty(cbxLocation.Text) && !address.Contains(cbxLocation.Text)) continue;
                    if (!string.IsNullOrEmpty(cbxTimeSlot.Text) && timeSlot != cbxTimeSlot.Text) continue;

                    Panel panel = Controls.Find($"panelClinic{panelIndex}", true).FirstOrDefault() as Panel;
                    Label name = Controls.Find($"clinicsName{panelIndex}", true).FirstOrDefault() as Label;
                    Label tag = Controls.Find($"clinicTag{panelIndex}", true).FirstOrDefault() as Label;

                    if (panel != null && name != null && tag != null)
                    {
                        panel.Visible = true;
                        name.Text = reader["ClinicName"].ToString();
                        tag.Text = reader["Specialization"].ToString();
                        panelIndex++;
                    }
                }
                for (int i = panelIndex; i <= 6; i++)
                {
                    Panel panel = Controls.Find($"panelClinic{i}", true).FirstOrDefault() as Panel;
                    if (panel != null) panel.Visible = false;
                }

                conn.Close();
            }
        }
        //
        private void cbxLocation_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cbxServices_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}