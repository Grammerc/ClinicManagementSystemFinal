using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using ClinicManagementSystemFinal.UserControls_Doctors.Clinics;

namespace ClinicManagementSystemFinal.UserControls_Doctors
{
    public partial class MyClinics : UserControl
    {
        private string _loginId;           // remembers who we're loading clinics for
        public event Action<int> EditRequested;
        private readonly Panel _templatePanel;
        private readonly string imageFolderPath =
            @"C:\Users\Raphael\source\repos\ClinicManagementSystemFinal\ClinicManagementSystemFinal\Pictures\ClinicPictures";
        private const string CONN =
            @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\Raphael\source\repos\ClinicManagementSystemFinal\ClinicManagementSystemFinal\Login.accdb;Persist Security Info=False;";

        public MyClinics()
        {
            InitializeComponent();
            _templatePanel = pnlTemplate;
            _templatePanel.Visible = false;
        }

        /// <summary>
        /// Call this to load (or reload) the list of clinics for a given user.
        /// </summary>
        public void LoadMyClinics(string loginId)
        {
            _loginId = loginId;
            flpClinics.Controls.Clear();

            using var conn = new OleDbConnection(CONN);
            conn.Open();

            using var cmd = new OleDbCommand(
                "SELECT C.ClinicID, C.ClinicName, C.Address, C.Picture " +
                "FROM Doctors D " +
                "INNER JOIN Clinics C ON D.ClinicID = C.ClinicID " +
                "WHERE D.LoginID = ?", conn);
            cmd.Parameters.AddWithValue("?", int.Parse(loginId));

            using var rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                var card = CreateCardFromTemplate();
                card.Visible = true;

                if (card.Controls["pbxClinic"] is PictureBox pbx)
                {
                    // Handle the image from the database
                    object pictureData = rdr["Picture"];
                    if (pictureData != DBNull.Value)
                    {
                        try
                        {
                            if (pictureData is byte[] bytes)
                            {
                                using (var ms = new MemoryStream(bytes))
                                {
                                    pbx.Image?.Dispose(); // Dispose of previous image if any
                                    pbx.Image = Image.FromStream(ms);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Error loading image: {ex.Message}", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    pbx.SizeMode = PictureBoxSizeMode.Zoom;
                }

                if (card.Controls["lblName"] is Label lblName)
                    lblName.Text = rdr["ClinicName"]?.ToString() ?? "Unnamed Clinic";

                if (card.Controls["lblLocation"] is Label lblLoc)
                    lblLoc.Text = rdr["Address"]?.ToString() ?? "No address provided";

                if (card.Controls["btnEdit"] is Button btn)
                {
                    btn.Tag = rdr.GetInt32(0);
                    btn.Click -= BtnEdit_Click;
                    btn.Click += BtnEdit_Click;
                }

                flpClinics.Controls.Add(card);
            }
        }

        private void BtnEdit_Click(object sender, EventArgs e)
        {
            if (sender is Button btn && btn.Tag is int clinicId)
            {
                var popup = new Form();
                var editCtl = new EditClinic(clinicId);
                editCtl.Dock = DockStyle.Fill;
                popup.ClientSize = editCtl.PreferredSize;
                popup.Controls.Add(editCtl);

                // Subscribe to the ChangesSaved event
                editCtl.ChangesSaved += (s, args) =>
                {
                    popup.Close();
                    LoadMyClinics(_loginId); // Refresh the clinic list
                };

                popup.ShowDialog(this);
            }
        }

        private Panel CreateCardFromTemplate()
        {
            var t = _templatePanel;
            var copy = new Panel
            {
                Size = t.Size,
                Margin = t.Margin,
                Padding = t.Padding,
                BackColor = t.BackColor
            };

            foreach (Control c in t.Controls)
            {
                Control cc = null;
                if (c is PictureBox pb)
                    cc = new PictureBox { SizeMode = pb.SizeMode };
                else if (c is Label lb)
                    cc = new Label { AutoSize = lb.AutoSize };
                else if (c is Button bt)
                    cc = new Button();
                else
                    continue;

                cc.Name = c.Name;
                cc.Size = c.Size;
                cc.Location = c.Location;
                cc.Font = c.Font;
                cc.ForeColor = c.ForeColor;
                if (cc is Label || cc is Button)
                    cc.Text = c.Text;

                copy.Controls.Add(cc);
            }

            return copy;
        }

        private void label1_Click(object sender, EventArgs e)
        {
            var popup = new Form
            {
                StartPosition = FormStartPosition.CenterParent,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MaximizeBox = false,
                MinimizeBox = false,
                ClientSize = new Size(800, 600)  // Standard size for the AddClinic form
            };

            // pass in the stored loginId (_loginId) so AddClinic can link it
            var addCtl = new AddClinic(int.Parse(_loginId));
            addCtl.Dock = DockStyle.Fill;
            popup.Controls.Add(addCtl);

            // when AddClinic tells us it's done, close & reload
            addCtl.ClinicAdded += (s, e2) =>
            {
                popup.Close();
                LoadMyClinics(_loginId);
            };

            popup.ShowDialog(this);
        }
    }
}