using System.Data.OleDb;
using System.Data;
using Guna.Charts.WinForms;

namespace ClinicManagementSystemFinal.UserControls_Doctors
{
    public partial class Dashboard_Doctors : UserControl
    {
        private readonly string _doctorLoginId;
        private const string CONN =
            @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=B:\Downloads\Login.accdb;Persist Security Info=False;";

        class AppointmentInfo
        {
            public DateTime Date;
            public string PatientName;
            public string Reason;
        }
        public Dashboard_Doctors(string doctorLoginId)
        {
            InitializeComponent();
            _doctorLoginId = doctorLoginId;

            // wire-up our period-toggle radio buttons
            rdoDay.CheckedChanged += (s, e) => RefreshStats();
            rdoMonth.CheckedChanged += (s, e) => RefreshStats();
            rdoYear.CheckedChanged += (s, e) => RefreshStats();

            // initial draw
            RefreshStats();
        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void label21_Click(object sender, EventArgs e)
        {

        }

        private void cboPeriod_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void RefreshStats()
        {
            // 1) decide period
            string period = rdoDay.Checked ? "DAY"
                         : rdoMonth.Checked ? "MONTH"
                         : "YEAR";

            // 2) pull labels & raw series data
            var labels = GetTimeLabels(period);
            var completed = GetCounts("Completed", period);
            var pending = GetCounts("Pending", period);
            var newPats = GetNewPatientCounts(period);
            // assume each completed = 1h
            var hours = completed.Select(c => (double)c).ToList();

            // 3) KPI totals
            lblAppointments.Text = completed.Sum().ToString();
            lblPending.Text = pending.Sum().ToString();
            lblNewPatients.Text = newPats.Sum().ToString();
            lblHoursWorked.Text = hours.Sum().ToString("0");

            // 4) build four line‐datasets
            //    (you can tweak colors, point style etc in code or in designer)
            var dsComp = new GunaLineDataset { Label = "Completed" };
            var dsPend = new GunaLineDataset { Label = "Pending" };
            var dsNew = new GunaLineDataset { Label = "New Patients" };
            var dsHrs = new GunaLineDataset { Label = "Hours Worked" };

            for (int i = 0; i < labels.Count; i++)
            {
                var lbl = labels[i];
                dsComp.DataPoints.Add(new LPoint { Label = lbl, Y = completed[i] });
                dsPend.DataPoints.Add(new LPoint { Label = lbl, Y = pending[i] });
                dsNew.DataPoints.Add(new LPoint { Label = lbl, Y = newPats[i] });
                dsHrs.DataPoints.Add(new LPoint { Label = lbl, Y = hours[i] });
            }

            // 5) clear & add them to your chart
            chartStats.Datasets.Clear();
            chartStats.Datasets.Add(dsComp);
            chartStats.Datasets.Add(dsPend);
            chartStats.Datasets.Add(dsNew);
            chartStats.Datasets.Add(dsHrs);

            chartStats.Update();
            LoadLastPatients();
        }

        private List<string> GetTimeLabels(string period)
        {
            // format string & date-filter per period
            string fmt = period == "DAY" ? "yyyy-mm-dd"
                       : period == "MONTH" ? "yyyy-mm"
                                         : "yyyy";
            string dateFilter = period == "DAY"
                ? "DateAdd('d',-6,Date())"
                : period == "MONTH"
                    ? "DateAdd('m',-11,Date())"
                    : "DateAdd('yyyy',-4,Date())";

            var list = new List<string>();
            using var conn = new OleDbConnection(CONN);
            conn.Open();
            var sql = $@"
SELECT DISTINCT
    Format(A.AppointmentDate,'{fmt}') AS g
  FROM Appointments AS A
       INNER JOIN Doctors D ON A.DoctorID=D.DoctorID
 WHERE D.LoginID  = ?
   AND A.AppointmentDate >= {dateFilter}
 ORDER BY Format(A.AppointmentDate,'{fmt}')";
            using var cmd = new OleDbCommand(sql, conn);
            cmd.Parameters.AddWithValue("?", _doctorLoginId);
            using var r = cmd.ExecuteReader();
            while (r.Read()) list.Add(r.GetString(0));
            return list;
        }

        private List<int> GetCounts(string status, string period)
        {
            // same fmt & dateFilter as above
            string fmt = period == "DAY" ? "yyyy-mm-dd"
                       : period == "MONTH" ? "yyyy-mm"
                                         : "yyyy";
            string dateFilter = period == "DAY"
                ? "DateAdd('d',-6,Date())"
                : period == "MONTH"
                    ? "DateAdd('m',-11,Date())"
                    : "DateAdd('yyyy',-4,Date())";

            var labels = GetTimeLabels(period);
            var counts = labels.Select(_ => 0).ToList();
            using var conn = new OleDbConnection(CONN);
            conn.Open();
            var sql = $@"
SELECT
    Format(A.AppointmentDate,'{fmt}') AS g,
    COUNT(*)                 AS cnt
  FROM Appointments AS A
       INNER JOIN Doctors D ON A.DoctorID=D.DoctorID
 WHERE D.LoginID        = ?
   AND A.Status         = ?
   AND A.AppointmentDate>= {dateFilter}
 GROUP BY Format(A.AppointmentDate,'{fmt}')";
            using var cmd = new OleDbCommand(sql, conn);
            cmd.Parameters.AddWithValue("?", _doctorLoginId);
            cmd.Parameters.AddWithValue("?", status);
            using var r = cmd.ExecuteReader();
            while (r.Read())
            {
                var g = r.GetString(0);
                var cnt = Convert.ToInt32(r[1]);
                int idx = labels.IndexOf(g);
                if (idx >= 0) counts[idx] = cnt;
            }
            return counts;
        }

        private List<int> GetNewPatientCounts(string period)
        {
            // same fmt & dateFilter
            string fmt = period == "DAY" ? "yyyy-mm-dd"
                       : period == "MONTH" ? "yyyy-mm"
                                         : "yyyy";
            string dateFilter = period == "DAY"
                ? "DateAdd('d',-6,Date())"
                : period == "MONTH"
                    ? "DateAdd('m',-11,Date())"
                    : "DateAdd('yyyy',-4,Date())";

            var labels = GetTimeLabels(period);
            var counts = labels.Select(_ => 0).ToList();
            using var conn = new OleDbConnection(CONN);
            conn.Open();
            var sql = $@"
SELECT
    s.g,
    COUNT(*) AS cnt
  FROM (
         SELECT DISTINCT
             A.UserInfoID,
             Format(A.AppointmentDate,'{fmt}') AS g
           FROM Appointments A
                INNER JOIN Doctors D ON A.DoctorID=D.DoctorID
          WHERE D.LoginID        = ?
            AND A.AppointmentDate>= {dateFilter}
       ) AS s
 GROUP BY s.g";
            using var cmd = new OleDbCommand(sql, conn);
            cmd.Parameters.AddWithValue("?", _doctorLoginId);
            using var r = cmd.ExecuteReader();
            while (r.Read())
            {
                var g = r.GetString(0);
                var cnt = Convert.ToInt32(r[1]);
                int idx = labels.IndexOf(g);
                if (idx >= 0) counts[idx] = cnt;
            }
            return counts;
        }

        private List<AppointmentInfo> GetLastCompletedAppointments(int howMany)
        {
            var list = new List<AppointmentInfo>();
            using var conn = new OleDbConnection(CONN);
            conn.Open();
            using var cmd = new OleDbCommand($@"
        SELECT TOP {howMany}
               A.AppointmentDate,
               I.Name,
               A.ReasonForVisit
          FROM Appointments A
          INNER JOIN Doctors     D ON A.DoctorID    = D.DoctorID
          INNER JOIN Information I ON A.UserInfoID   = I.UserInfoID
         WHERE D.LoginID  = ?
           AND A.Status   = 'Completed'
         ORDER BY A.AppointmentDate DESC
    ", conn);
            cmd.Parameters.AddWithValue("?", _doctorLoginId);
            using var rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                list.Add(new AppointmentInfo
                {
                    Date = rdr.GetDateTime(0),
                    PatientName = rdr.GetString(1),
                    Reason = rdr.GetString(2)
                });
            }
            return list;
        }

        private void LoadLastPatients()
        {
            flpLastPatients.Controls.Clear();
            foreach (var appt in GetLastCompletedAppointments(5))
            {
                card.Dock = DockStyle.Top;
                card.lblDate.Text = appt.Date.ToString("g");       // short datetime
                card.lblName.Text = appt.PatientName;
                card.lblReason.Text = appt.Reason;
                flpLastPatients.Controls.Add(card);
            }
        }
    }
}

