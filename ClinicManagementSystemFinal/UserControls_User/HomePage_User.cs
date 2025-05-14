using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClinicManagementSystemFinal.UserControls_User;
using ClinicManagementSystemFinal.UserControls_User.Appointments_Control;
using ClinicManagementSystemFinal.UserInterface;

namespace ClinicManagementSystemFinal
{
    public partial class HomePage_User : Form
    {
        private int borderSize = 2;
        private Size formSize;
        private string userLoginId;
        private const string CONN =
            @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\Raphael\source\repos\ClinicManagementSystemFinal\ClinicManagementSystemFinal\Login.accdb;Persist Security Info=False;";

        public HomePage_User()
        {
            InitializeComponent();
            userLoginId = userLoginId;
            formSize = this.ClientSize;
            this.Padding = new Padding(borderSize);
            this.BackColor = Color.FromArgb(114, 137, 218);
        }

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

        private void panelDesktop_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panelTitleBar_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void panelTitleBar_Paint(object sender, PaintEventArgs e)
        {

        }

        public HomePage_User(string loginId)
        {
            InitializeComponent();
            userLoginId = loginId;

            formSize = this.ClientSize;
            this.Padding = new Padding(borderSize);
            this.BackColor = Color.FromArgb(114, 137, 218);

            this.MouseDown += (s, e) =>
            {
                if (e.Button == MouseButtons.Left)
                {
                    ReleaseCapture();
                    SendMessage(this.Handle, 0x112, 0xf012, 0);
                }
            };

            guna2Panel11.MouseDown += (s, e) =>
            {
                if (e.Button == MouseButtons.Left)
                {
                    ReleaseCapture();
                    SendMessage(this.Handle, 0x112, 0xf012, 0);
                }
            };
        }

        protected override void WndProc(ref Message m)
        {
            const int WM_NCCALCSIZE = 0x0083;
            const int WM_SYSCOMMAND = 0x0112;
            const int SC_MINIMIZE = 0xF020;
            const int SC_RESTORE = 0xF120;
            const int WM_NCHITTEST = 0x0084;
            const int resizeAreaSize = 10;
            const int HTCLIENT = 1;
            const int HTLEFT = 10;
            const int HTRIGHT = 11;
            const int HTTOP = 12;
            const int HTTOPLEFT = 13;
            const int HTTOPRIGHT = 14;
            const int HTBOTTOM = 15;
            const int HTBOTTOMLEFT = 16;
            const int HTBOTTOMRIGHT = 17;

            if (m.Msg == WM_NCHITTEST)
            {
                base.WndProc(ref m);
                if (this.WindowState == FormWindowState.Normal)
                {
                    if ((int)m.Result == HTCLIENT)
                    {
                        Point screenPoint = new Point(m.LParam.ToInt32());
                        Point clientPoint = this.PointToClient(screenPoint);
                        if (clientPoint.Y <= resizeAreaSize)
                        {
                            if (clientPoint.X <= resizeAreaSize)
                                m.Result = (IntPtr)HTTOPLEFT;
                            else if (clientPoint.X < (this.Size.Width - resizeAreaSize))
                                m.Result = (IntPtr)HTTOP;
                            else
                                m.Result = (IntPtr)HTTOPRIGHT;
                        }
                        else if (clientPoint.Y <= (this.Size.Height - resizeAreaSize))
                        {
                            if (clientPoint.X <= resizeAreaSize)
                                m.Result = (IntPtr)HTLEFT;
                            else if (clientPoint.X > (this.Width - resizeAreaSize))
                                m.Result = (IntPtr)HTRIGHT;
                        }
                        else
                        {
                            if (clientPoint.X <= resizeAreaSize)
                                m.Result = (IntPtr)HTBOTTOMLEFT;
                            else if (clientPoint.X < (this.Size.Width - resizeAreaSize))
                                m.Result = (IntPtr)HTBOTTOM;
                            else
                                m.Result = (IntPtr)HTBOTTOMRIGHT;
                        }
                    }
                }
                return;
            }


            if (m.Msg == WM_NCCALCSIZE && m.WParam.ToInt32() == 1)
            {
                return;
            }

            if (m.Msg == WM_SYSCOMMAND)
            {
                int wParam = (m.WParam.ToInt32() & 0xFFF0);
                if (wParam == SC_MINIMIZE)
                    formSize = this.ClientSize;
                if (wParam == SC_RESTORE)
                    this.Size = formSize;
            }
            base.WndProc(ref m);
        }

        private void HomePage_Resize(object sender, EventArgs e)
        {
            AdjustForm();
        }

        private void AdjustForm()
        {
            switch (this.WindowState)
            {
                case FormWindowState.Maximized:
                    this.Padding = new Padding(8, 8, 8, 0);
                    break;
                case FormWindowState.Normal:
                    if (this.Padding.Top != borderSize)
                        this.Padding = new Padding(borderSize);
                    break;
            }
        }

        private void btnMenu_Click(object sender, EventArgs e)
        {
            CollapseMenu();
        }

        private void CollapseMenu()
        {
            if (this.panelMenu.Width > 200)
            {
                panelMenu.Width = 100;
                btnMenu.Dock = DockStyle.Top;
                foreach (Button menuButton in panelMenu.Controls.OfType<Button>())
                {
                    menuButton.Text = "";
                    menuButton.ImageAlign = ContentAlignment.MiddleCenter;
                    menuButton.Padding = new Padding(0);
                }
            }
            else
            {
                panelMenu.Width = 230;
                btnMenu.Dock = DockStyle.None;
                foreach (Button menuButton in panelMenu.Controls.OfType<Button>())
                {
                    menuButton.Text = "   " + menuButton.Tag.ToString();
                    menuButton.ImageAlign = ContentAlignment.MiddleLeft;
                    menuButton.Padding = new Padding(10, 0, 0, 0);
                }
            }
        }

        public void LoadControl(Control c)
        {
            if (panelMainDesktop.Controls.Count > 0)
                panelMainDesktop.Controls.RemoveAt(0);


            if (c is Form childForm)
            {
                childForm.TopLevel = false;
                childForm.FormBorderStyle = FormBorderStyle.None;
                childForm.Dock = DockStyle.Fill;
                panelMainDesktop.Controls.Add(childForm);
                panelMainDesktop.Tag = childForm;
                childForm.Show();
            }
            else
            {
                c.Dock = DockStyle.Fill;
                panelMainDesktop.Controls.Add(c);
                panelMainDesktop.Tag = c;
                c.BringToFront();
            }
        }

        private void btnSwitch_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void btnAppointment_Click(object sender, EventArgs e)
        {
            LoadControl(new Appointments_Create(userLoginId));
        }

        private void btnDoctor_Click(object sender, EventArgs e)
        {
        }

        private void btnServices_Click(object sender, EventArgs e)
        {

        }

        private void btnSupport_Click(object sender, EventArgs e)
        {
        }

        private void btnLogOut_Click(object sender, EventArgs e)
        {
            this.Hide();
            SignIn loginForm = new SignIn();
            loginForm.Show();
        }

        private void HomePage_User_Load(object sender, EventArgs e)
        {
            guna2ShadowForm1.SetShadowForm(this);
            LoadProfilePicture(userLoginId);


            menuContainer.Height = 53;
            LoadUserName(userLoginId);
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

        private void LoadProfilePicture(string loginId)
        {
            const string sql = @"
SELECT 
    I.ProfileImagePath,
    I.ProfilePicture,
    I.Name
FROM Information I
WHERE I.LoginID = ?";

            using var conn = new OleDbConnection(CONN);
            try
            {
                conn.Open();
                using var cmd = new OleDbCommand(sql, conn);
                cmd.Parameters.Add("?", OleDbType.Integer).Value = Convert.ToInt32(loginId);

                using var rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    Image img = null;
                    var path = rdr["ProfileImagePath"]?.ToString();
                    if (!string.IsNullOrEmpty(path) && File.Exists(path))
                    {
                        img = Image.FromFile(path);
                    }
                    else if (rdr["ProfilePicture"] != DBNull.Value)
                    {
                        var blob = (byte[])rdr["ProfilePicture"];
                        using var ms = new MemoryStream(blob);
                        img = Image.FromStream(ms);
                    }
                    
                    if (img != null)
                    {
                        pbxProfilePic.Image = CropToCircle(img);
                        pbxProfilePic.SizeMode = PictureBoxSizeMode.Zoom;
                        pbxProfilePic.BackColor = Color.Transparent;
                    }
                    else
                    {
                        pbxProfilePic.Image = null;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading profile picture: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                pbxProfilePic.Image = null;
            }
            finally
            {
                conn.Close();
            }
        }

        private Image CropToCircle(Image srcImage)
        {
            if (srcImage == null) return null;

            Bitmap dstImage = new Bitmap(srcImage.Width, srcImage.Height);
            using (Graphics g = Graphics.FromImage(dstImage))
            {
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (Brush brush = new TextureBrush(srcImage))
                {
                    GraphicsPath path = new GraphicsPath();
                    path.AddEllipse(0, 0, srcImage.Width, srcImage.Height);
                    g.FillPath(brush, path);
                }
            }
            return dstImage;
        }

        private void btnDashboard_Click(object sender, EventArgs e)
        {
            LoadControl(new Dashboard_User(userLoginId));
        }

        private void btnAppointments_Click(object sender, EventArgs e)
        {
            if (menuContainer.Height == 53)
            {
                menuContainer.Height = 215;
            }
            else
            {
                menuContainer.Height = 53;
            }

            LoadControl(new Appointments_Create(userLoginId));
        }

        private void btnClinics_Click(object sender, EventArgs e)
        {
            LoadControl(new Clinics(userLoginId));
        }

        private void btnService_Click(object sender, EventArgs e)
        {
            LoadControl(new Services(userLoginId));
        }

        private void guna2ImageButton3_Click(object sender, EventArgs e)
        {

        }

        private void guna2Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        bool menuExpand = false;
        private void menuTransition_Tick(object sender, EventArgs e)
        {
            if (menuExpand == false)
            {
                menuContainer.Height += 10;
                if (menuContainer.Height >= 215)
                {
                    menuExpand = true;
                }
            }
            else
            {
                menuContainer.Height -= 10;
                if (menuContainer.Height <= 53)
                {
                    menuExpand = false;
                }
            }
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            LoadControl(new Appointments_Read(userLoginId));
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            LoadControl(new Appointments_Update(userLoginId));
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            LoadControl(new Appointments_Remove(userLoginId));
        }

        private void pbxProfilePic_Click(object sender, EventArgs e)
        {
            LoadControl(new UserInformation(userLoginId));
        }

        private void guna2Separator1_Click(object sender, EventArgs e)
        {

        }

        private void btnFindDoctors_Click(object sender, EventArgs e)
        {
            LoadControl(new FindPeople(userLoginId, isSecretary:false));
        }
    }
}

