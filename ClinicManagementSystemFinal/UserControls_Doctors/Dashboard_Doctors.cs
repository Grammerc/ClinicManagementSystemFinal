using System.Data;
using System.Data.OleDb;
using Guna.Charts.WinForms;

namespace ClinicManagementSystemFinal.UserControls_Doctors
{
    public partial class Dashboard_Doctors : UserControl
    {
        private readonly string _doctorLoginId;
        private const string CONN =
            @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\Raphael\Downloads\Login.accdb;Persist Security Info=False;";
        private readonly Panel _lastPatientTemplate;
        private const int _maxLastPatients = 5;
        class AppointmentInfo
        {
            public DateTime Date;
            public string PatientName;
            public string Reason;
            public string UserInfoId;
            public string ProfileImagePath;
            public byte[] ProfileImageBlob;
            public string ClinicId;
        }
        public Dashboard_Doctors(string doctorLoginId)
        {
            InitializeComponent();
            btnView.Click += BtnView_Click;
            _doctorLoginId = doctorLoginId;
            rdoDay.Checked = true;

            _lastPatientTemplate = LastPatientCard;
            _lastPatientTemplate.Visible = false;
            // wire-up our period-toggle radio buttons
            rdoDay.CheckedChanged += (s, e) => RefreshStats();
            rdoMonth.CheckedChanged += (s, e) => RefreshStats();
            rdoYear.CheckedChanged += (s, e) => RefreshStats();
            this.VisibleChanged += (s, e) => {
                if (this.Visible)
                    RefreshStats();
        };
            RefreshStats();
        }

        private void BtnView_Click(object sender, EventArgs e)
        {
            var host = this.FindForm() as HomePage_Doctor;
            if (host != null)
                host.LoadControl(new LastPatient(_doctorLoginId));
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

            var dsComp = new GunaLineDataset { Label = "Completed" };
            var dsPend = new GunaLineDataset { Label = "Pending" };
            var dsNew = new GunaLineDataset { Label = "New Patients" };
            var dsHrs = new GunaLineDataset { Label = "Hours Worked" };

    
            dsComp.BorderColor = Color.Green;
            //dsComp.PointFillColors = Color.Green;
            dsPend.BorderColor = Color.Orange;
            //dsPend.PointFillColors = Color.Orange;
            dsNew.BorderColor = Color.MediumPurple;
            //dsNew.PointFillColors = Color.MediumPurple;
            dsHrs.BorderColor = Color.Blue;
           // dsHrs.PointFillColors = Color.Blue;

            for (int i = 0; i < labels.Count; i++)
            {
                var lbl = labels[i];
                dsComp.DataPoints.Add(new LPoint { Label = lbl, Y = completed[i] });
                dsPend.DataPoints.Add(new LPoint { Label = lbl, Y = pending[i] });
                dsNew.DataPoints.Add(new LPoint { Label = lbl, Y = newPats[i] });
                dsHrs.DataPoints.Add(new LPoint { Label = lbl, Y = hours[i] });
            }

            chartStats.Datasets.Clear();
            chartStats.Datasets.Add(dsComp);
            chartStats.Datasets.Add(dsPend);
            chartStats.Datasets.Add(dsNew);
            chartStats.Datasets.Add(dsHrs);

            chartStats.Update();

            LoadLastPatients();
            LoadTodaysAppointments();
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

            var sql = $@"
SELECT TOP {howMany}
       A.AppointmentDate,
       I.UserInfoID,
       IIf(IsNull(I.ProfileImagePath), '', I.ProfileImagePath) AS ImgPath,
       I.ProfilePicture,
       A.ClinicID,
       I.Name,
       A.ReasonForVisit
  FROM (Appointments AS A
        INNER JOIN Doctors     AS D 
          ON A.DoctorID    = D.DoctorID)
       INNER JOIN Information AS I 
          ON A.UserInfoID  = I.UserInfoID
 WHERE D.LoginID  = ?
   AND A.Status   = 'Completed'
 ORDER BY A.AppointmentDate DESC";

            using var cmd = new OleDbCommand(sql, conn);
            cmd.Parameters.AddWithValue("?", _doctorLoginId);

            using var rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                list.Add(new AppointmentInfo
                {
                    Date = rdr.GetDateTime(0),
                    PatientName = rdr.GetString(5),
                    Reason = rdr.GetString(6),
                    ProfileImagePath = rdr.GetString(2),               // now comes through as ImgPath
                    ProfileImageBlob = rdr["ProfilePicture"] as byte[],
                    ClinicId = rdr.GetInt32(4).ToString(),
                    UserInfoId = rdr.GetInt32(1).ToString()
                });
            }

            return list;
        }
        private void LoadLastPatients()
        {
            flpLastPatients.Controls.Clear();



            foreach (var appt in GetLastCompletedAppointments(_maxLastPatients))
            {
                // 1) Clone the template panel
                var card = new Panel
                {
                    Size = _lastPatientTemplate.Size,
                    Margin = _lastPatientTemplate.Margin,
                    Padding = _lastPatientTemplate.Padding,
                    BackColor = _lastPatientTemplate.BackColor
                };

                // 2) Copy each Label from the template
                foreach (Control ctl in _lastPatientTemplate.Controls)
                {
                    Control copy = null;
                    if (ctl is Label src)
                    {
                        copy = new Label
                        {
                            Name = src.Name,
                            Size = src.Size,
                            Location = src.Location,
                            Font = src.Font,
                            ForeColor = src.ForeColor,
                            TextAlign = src.TextAlign,
                            BackColor = Color.Transparent
                        };
                    }
                    else if (ctl is PictureBox srcPbx)
                    {
                        var pb = new PictureBox
                        {
                            Name = srcPbx.Name,
                            Size = srcPbx.Size,
                            Location = srcPbx.Location,
                            BackColor = srcPbx.BackColor,
                            BorderStyle = srcPbx.BorderStyle,
                            SizeMode = PictureBoxSizeMode.StretchImage
                        };
                        copy = pb;
                    }

                    if (copy != null)
                        card.Controls.Add(copy);
                }

                // 3) Fill in the data
                var lblDate = card.Controls.Find("lblDate", false).FirstOrDefault() as Label;
                var lblName = card.Controls.Find("lblName", false).FirstOrDefault() as Label;
                var lblReason = card.Controls.Find("lblReason", false).FirstOrDefault() as Label;

                if (lblDate != null) lblDate.Text = appt.Date.ToString("g");
                if (lblName != null) lblName.Text = appt.PatientName;
                if (lblReason != null) lblReason.Text = appt.Reason;

                var pbxProfile = card.Controls
    .OfType<PictureBox>()
    .FirstOrDefault(p => p.Name == "pbxProfile");
                if (pbxProfile != null)
                {
                    Image pic = null;
                    if (!string.IsNullOrEmpty(appt.ProfileImagePath) && File.Exists(appt.ProfileImagePath))
                    {
                        pic = Image.FromFile(appt.ProfileImagePath);
                    }
                    else if (appt.ProfileImageBlob?.Length > 0)
                    {
                        using var ms = new MemoryStream(appt.ProfileImageBlob);
                        pic = Image.FromStream(ms);
                    }
                    pbxProfile.Image = pic;
                    pbxProfile.SizeMode = PictureBoxSizeMode.StretchImage;
                    pbxProfile.Cursor = Cursors.Hand;
                    pbxProfile.Click += (s, e) =>
                    {
                        var host = this.FindForm() as HomePage_Doctor;
                        if (host != null)
                            host.LoadControl(new MedicalHistory(
                                appt.UserInfoId,
                                appt.PatientName,
                                appt.ProfileImagePath,
                                appt.ClinicId,
                                _doctorLoginId
                            ));
                    };
                }
                flpLastPatients.Controls.Add(card);
            }
        }

        private void LoadTodaysAppointments()
        {
            dgvToday.Rows.Clear();

            using var conn = new OleDbConnection(CONN);
            conn.Open();

            // only Approved, today
            var cmd = new OleDbCommand(@"
        SELECT 
            A.AppointmentDate,
            A.TimeSlot,
            I.Name,
            A.ReasonForVisit
        FROM 
            (Appointments AS A
             INNER JOIN Doctors     AS D ON A.DoctorID    = D.DoctorID)
             INNER JOIN Information AS I ON A.UserInfoID   = I.UserInfoID
        WHERE 
            D.LoginID      = ?
          AND A.[Status]    = 'Approved'
          AND A.AppointmentDate >= Date()
          AND A.AppointmentDate <  DateAdd('d',1,Date())
        ORDER BY A.AppointmentDate, A.TimeSlot;", conn);
            cmd.Parameters.AddWithValue("?", _doctorLoginId);

            using var rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                var date = ((DateTime)rdr["AppointmentDate"]).ToShortDateString();
                var time = rdr["TimeSlot"].ToString();
                var name = rdr["Name"].ToString();
                var reason = rdr["ReasonForVisit"].ToString();

                dgvToday.Rows.Add(date, time, name, reason);
            }
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void rdoYear_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}

