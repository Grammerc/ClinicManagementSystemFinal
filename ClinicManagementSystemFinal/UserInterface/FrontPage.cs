
using System.Runtime.InteropServices;
using Microsoft.VisualBasic;

namespace ClinicManagementSystemFinal
{
    public partial class FrontPage : Form
    {

        private int borderSize = 2;
        private Size formSize;
        public FrontPage()
        {
            InitializeComponent();
            formSize = this.ClientSize;
            this.Padding = new Padding(borderSize);
        }

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);
        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            SignIn hm = new SignIn();
            hm.Show();
        }

        private void panelTitleBar_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
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

        private void FrontPage_Resize(object sender, EventArgs e)
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

        private void btnClinics_Click(object sender, EventArgs e)
        {

            loadform(new Clinic());
        }

        private void loadform(object Form)
        {
            if (this.panelDesktop.Controls.Count > 0)
                this.panelDesktop.Controls.RemoveAt(0);
            Form f = Form as Form;
            f.TopLevel = false;
            f.Dock = DockStyle.Fill;
            this.panelDesktop.Controls.Add(f);
            this.panelDesktop.Tag = f;
            f.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Create an account to access the software!");
        }
    }
}
