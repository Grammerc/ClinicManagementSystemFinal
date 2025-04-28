using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClinicManagementSystemFinal.UserControls_Doctors
{
    public partial class DynamicCalendar : UserControl
    {
        string _day, date, weekday;
        public event Action<DateTime> DatePicked;
        readonly DateTime thisDate;
        public DynamicCalendar(string day, bool highlight, int total = 0)
        {
            InitializeComponent();

            if (string.IsNullOrWhiteSpace(day))
            {
                lblDateToday.Text = "";
                checkBox1.Visible = false;
                panel1.Enabled = false;
                return;
            }

            thisDate = new DateTime(Calendar_Doctor._year, Calendar_Doctor._month, int.Parse(day));

            lblDateToday.Text = total > 0 ? $"{day}\n({total})" : day;

            checkBox1.Visible = highlight;
            if (thisDate.DayOfWeek == DayOfWeek.Sunday) lblDateToday.ForeColor = Color.FromArgb(255, 128, 128);

            panel1.Click += (s, e) =>
            {
                Toggle();
                DatePicked?.Invoke(thisDate);
            };
        }

        void Toggle()
        {
            bool state = !checkBox1.Checked;
            checkBox1.Checked = state;
            BackColor = state ? Color.FromArgb(255, 150, 79) : Color.White;
        }

        private void sundays()
        {
            try
            {
                DateTime day = DateTime.Parse(date);
                weekday = day.ToString("ddd");
                if (weekday == "Sun")
                {
                    lblDateToday.ForeColor = Color.FromArgb(255, 128, 128);
                }
                else
                {
                    lblDateToday.ForeColor = Color.FromArgb(64, 64, 64);
                }
            }
            catch (Exception) { }
        }

        private void panel1_click(object sender, EventArgs e)
        {
            if (checkBox1.Checked == false)
            {
                checkBox1.Checked = true;
                this.BackColor = Color.FromArgb(255, 150, 79);
            }
            else
            {
                checkBox1.Checked = false;
                this.BackColor = Color.White;
            }
        }

        private void panel1_Click_1(object sender, EventArgs e)
        {
            sundays();
        }

        private void DynamicCalendar_Load(object sender, EventArgs e)
        {

        }
    }
}
