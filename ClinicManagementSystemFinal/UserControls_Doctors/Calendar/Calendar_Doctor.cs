using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Globalization;
using System.Windows.Forms;
using AppointmentUC = ClinicManagementSystemFinal.UserControls_Doctors.Appointment.AppointmentView_Doctors;

namespace ClinicManagementSystemFinal.UserControls_Doctors
{
    public partial class Calendar_Doctor : UserControl
    {
        readonly string doctorLoginId;
        readonly Dictionary<DateTime, int> dayTotals = new Dictionary<DateTime, int>();
        public static int _year, _month;
        public event Action<DateTime> DayClicked;

        const string CONN =
            @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\Raphael\source\repos\ClinicManagementSystemFinal\ClinicManagementSystemFinal\Login.accdb;Persist Security Info=False;";
        
        readonly AppointmentUC apptView;

        public Calendar_Doctor(string loginId, AppointmentUC appointmentUC)
        {
            InitializeComponent();



            doctorLoginId = loginId;
            apptView = appointmentUC;               // passed in by the host form
            btnPrevious.Click += (s, e) => ShiftMonth(-1);
            btnNext.Click += (s, e) => ShiftMonth(1);
            Load += (s, e) => ShiftMonth(0);
        }

        void ShiftMonth(int delta)
        {
            DateTime refDate = _year == 0 ? DateTime.Now : new DateTime(_year, _month, 1);
            DateTime newDate = refDate.AddMonths(delta);
            _year = newDate.Year;
            _month = newDate.Month;
            LoadAppointmentDates();
            ShowDays();
        }

        void LoadAppointmentDates()
        {
            dayTotals.Clear();

            DateTime first = new DateTime(_year, _month, 1);
            DateTime last = first.AddMonths(1).AddDays(-1);

            using var conn = new OleDbConnection(CONN);
            conn.Open();
            var cmd = new OleDbCommand(
                @"SELECT DateValue(A.AppointmentDate) AS ApptDate,
                 COUNT(*)                      AS Total
          FROM   Appointments A
                 INNER JOIN Doctors D ON A.DoctorID = D.DoctorID
          WHERE  D.LoginID       = ?
            AND  A.[Status]      = 'Approved'
            AND  A.AppointmentDate BETWEEN ? AND ?
          GROUP  BY DateValue(A.AppointmentDate)", conn);

            cmd.Parameters.AddWithValue("?", Convert.ToInt32(doctorLoginId));
            cmd.Parameters.AddWithValue("?", first);
            cmd.Parameters.AddWithValue("?", last);

            using var rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                DateTime d = Convert.ToDateTime(rdr["ApptDate"]).Date;
                int c = Convert.ToInt32(rdr["Total"]);
                dayTotals[d] = c;
            }
        }

        void ShowDays()
        {

            flowLayoutPanel1.Controls.Clear();
            lbMonth.Text = $"{new DateTimeFormatInfo().GetMonthName(_month).ToUpper()} {_year}";

            int blanks = (int)new DateTime(_year, _month, 1).DayOfWeek;
            for (int i = 0; i < blanks; i++)
                flowLayoutPanel1.Controls.Add(new DynamicCalendar("", false));

            int daysInMonth = DateTime.DaysInMonth(_year, _month);
            for (int d = 1; d <= daysInMonth; d++)
            {
                DateTime thisDate = new DateTime(_year, _month, d);
                bool highlight = dayTotals.ContainsKey(thisDate);
                int total = highlight ? dayTotals[thisDate] : 0;

                var cell = new DynamicCalendar(d.ToString(), highlight, total);
                cell.DatePicked += apptView.SelectDate;   
                flowLayoutPanel1.Controls.Add(cell);
                cell.DatePicked += d => DayClicked?.Invoke(d);
            }
        }
    }
}