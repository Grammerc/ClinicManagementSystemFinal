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
            @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\Raphael\Downloads\Login.accdb;Persist Security Info=False;";

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
            _loginId = loginId;   // store for later refresh

            var clinics = new List<(int id, string name, string address)>();
            const string sql =
                @"SELECT C.ClinicID, C.ClinicName, C.Address
                  FROM Doctors AS D
                  INNER JOIN Clinics AS C ON D.ClinicID = C.ClinicID
                 WHERE D.LoginID = ?";

            try
            {
                using var conn = new OleDbConnection(CONN);
                conn.Open();
                using var cmd = new OleDbCommand(sql, conn);
                cmd.Parameters.Add("?", OleDbType.Integer).Value = int.Parse(_loginId);
                using var rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    clinics.Add((
                        rdr.GetInt32(0),
                        rdr.GetString(1),
                        rdr.GetString(2)
                    ));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error loading clinics:\n" + ex.Message + "\n\nSQL was:\n" + sql,
                    "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            flpClinics.Controls.Clear();

            foreach (var (id, name, address) in clinics)
            {
                var card = CreateCardFromTemplate();
                card.Visible = true;

                if (card.Controls["pbxClinic"] is PictureBox pbx)
                {
                    string png = Path.Combine(imageFolderPath, name + ".png");
                    string jpg = Path.Combine(imageFolderPath, name + ".jpg");
                    if (File.Exists(png)) pbx.Image = Image.FromFile(png);
                    else if (File.Exists(jpg)) pbx.Image = Image.FromFile(jpg);
                    else pbx.Image = null;
                    pbx.SizeMode = PictureBoxSizeMode.Zoom;
                }

                if (card.Controls["lblName"] is Label lblName)
                    lblName.Text = name;

                if (card.Controls["lblLocation"] is Label lblLoc)
                    lblLoc.Text = address;

                if (card.Controls["btnEdit"] is Button btn)
                {
                    btn.Tag = id;
                    btn.Click -= BtnEdit_Click;
                    btn.Click += BtnEdit_Click;
                }

                flpClinics.Controls.Add(card);
            }
        }

        private void BtnEdit_Click(object sender, EventArgs e)
        {
            if (sender is Button btn && btn.Tag is int clinicId)
                EditRequested?.Invoke(clinicId);
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
            var popup = new Form { /* … your sizing/styling … */ };

            // pass in the stored loginId (_loginId) so AddClinic can link it
            var addCtl = new AddClinic(int.Parse(_loginId));
            addCtl.Dock = DockStyle.Fill;
            popup.ClientSize = addCtl.PreferredSize;
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