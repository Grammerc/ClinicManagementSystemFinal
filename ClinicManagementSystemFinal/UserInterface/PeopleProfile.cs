using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace ClinicManagementSystemFinal.UserInterface
{
    public partial class PeopleProfile : UserControl
    {
        public event Action<Person> ViewClicked;
        private Person _p;
        public PeopleProfile()
        {
            InitializeComponent();
            btnView.Click += (s, e) => ViewClicked?.Invoke(_p);
        }
        public void Bind(Person p, bool isSecretary)
        {
            _p = p;
            lblName.Text = p.Name;
            lblRole.Text = p.Role;
            lblSpecialization.Text = p.Specialization;
            Image pic = null;
            if (!string.IsNullOrWhiteSpace(p.ImagePath) && File.Exists(p.ImagePath))
                pic = Image.FromFile(p.ImagePath);
            else if (p.ImageBlob != null && p.ImageBlob.Length > 0)
            {
                using (var ms = new MemoryStream(p.ImageBlob))
                    pic = Image.FromStream(ms);
            }
            pbxProfile.Image = pic;
            pbxProfile.SizeMode = PictureBoxSizeMode.Zoom;
            if (isSecretary && p.Role != "Doctor")
            {
                btnView.Enabled = false;
                btnView.Text = "-";
                btnView.Cursor = Cursors.Default;
            }
            else
            {
                btnView.Enabled = true;
                btnView.Text = "View";
                btnView.Cursor = Cursors.Hand;
            }
        }
    }
}