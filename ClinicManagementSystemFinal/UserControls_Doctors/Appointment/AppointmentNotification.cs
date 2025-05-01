using System;
using System.Data.OleDb;
using System.Net.Mail;
using System.Timers;
using System.Diagnostics;
using System.Data;

namespace ClinicManagementSystemFinal.UserControls_Doctors.Appointment
{
    public class AppointmentNotification
    {
        private readonly System.Timers.Timer _notificationTimer;
        private const string CONN =
            @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\Raphael\source\repos\ClinicManagementSystemFinal\ClinicManagementSystemFinal\Login.accdb;Persist Security Info=False;";

        public AppointmentNotification()
        {
            Debug.WriteLine("AppointmentNotification system initialized");
            InitializeNotificationTable();
            _notificationTimer = new System.Timers.Timer(60000); // Check every minute
            _notificationTimer.Elapsed += CheckAppointments;
            _notificationTimer.Start();
            Debug.WriteLine("Notification timer started");
        }

        private void VerifyDatabaseStructure()
        {
            try
            {
                Debug.WriteLine("\n=== DATABASE STRUCTURE VERIFICATION ===");
                using var conn = new OleDbConnection(CONN);
                conn.Open();

                // Get all tables
                var schema = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });
                Debug.WriteLine("\nAvailable Tables:");
                foreach (DataRow row in schema.Rows)
                {
                    Debug.WriteLine($"- {row["TABLE_NAME"]}");
                }

                // Verify each table's structure
                VerifyTableStructure(conn, "Appointments");
                VerifyTableStructure(conn, "Account");
                VerifyTableStructure(conn, "Clinics");
                VerifyTableStructure(conn, "AppointmentNotifications");

                Debug.WriteLine("=== DATABASE VERIFICATION COMPLETE ===\n");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error verifying database structure: {ex.Message}\n{ex.StackTrace}");
            }
        }

        private void VerifyTableStructure(OleDbConnection conn, string tableName)
        {
            try
            {
                Debug.WriteLine($"\nVerifying table: {tableName}");
                var schema = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Columns, new object[] { null, null, tableName, null });
                
                Debug.WriteLine($"Columns in {tableName}:");
                foreach (DataRow row in schema.Rows)
                {
                    Debug.WriteLine($"- {row["COLUMN_NAME"]} ({row["DATA_TYPE"]})");
                }

                // Check if table has data
                using var cmd = new OleDbCommand($"SELECT COUNT(*) FROM {tableName}", conn);
                var count = cmd.ExecuteScalar();
                Debug.WriteLine($"Total records in {tableName}: {count}");

                // Show sample data
                if (Convert.ToInt32(count) > 0)
                {
                    Debug.WriteLine($"Sample data from {tableName}:");
                    using var sampleCmd = new OleDbCommand($"SELECT TOP 1 * FROM {tableName}", conn);
                    using var reader = sampleCmd.ExecuteReader();
                    if (reader.Read())
                    {
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            Debug.WriteLine($"  {reader.GetName(i)}: {reader[i]}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error verifying table {tableName}: {ex.Message}");
            }
        }

        private void InitializeNotificationTable()
        {
            try
            {
                Debug.WriteLine("Initializing AppointmentNotifications table...");
                using var conn = new OleDbConnection(CONN);
                conn.Open();

                // First verify the database structure
                VerifyDatabaseStructure();

                // Check if table exists
                bool tableExists = false;
                try
                {
                    using var checkTableCmd = new OleDbCommand("SELECT COUNT(*) FROM AppointmentNotifications", conn);
                    checkTableCmd.ExecuteScalar();
                    tableExists = true;
                    Debug.WriteLine("AppointmentNotifications table exists");
                }
                catch
                {
                    Debug.WriteLine("AppointmentNotifications table does not exist, creating it...");
                    using var createTableCmd = new OleDbCommand(
                        "CREATE TABLE AppointmentNotifications (AppointmentID NUMBER, NotificationTime DATETIME)", conn);
                    createTableCmd.ExecuteNonQuery();
                    Debug.WriteLine("AppointmentNotifications table created");
                }

                // If table exists, check if it's empty
                if (tableExists)
                {
                    using var countCmd = new OleDbCommand("SELECT COUNT(*) FROM AppointmentNotifications", conn);
                    var count = (int)countCmd.ExecuteScalar();
                    Debug.WriteLine($"AppointmentNotifications table has {count} records");

                    // If table is empty, populate it with existing approved appointments
                    if (count == 0)
                    {
                        Debug.WriteLine("Populating AppointmentNotifications table with existing approved appointments...");
                        string sql = @"
                            INSERT INTO AppointmentNotifications (AppointmentID, NotificationTime)
                            SELECT a.AppointmentID, a.AppointmentDate
                            FROM Appointments a
                            WHERE a.Status = 'Approved'
                            AND NOT EXISTS (
                                SELECT 1 FROM AppointmentNotifications 
                                WHERE AppointmentID = a.AppointmentID
                            )";

                        using var cmd = new OleDbCommand(sql, conn);
                        int rowsAffected = cmd.ExecuteNonQuery();
                        Debug.WriteLine($"Added {rowsAffected} appointments to Notification table");
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error initializing notification table: {ex.Message}\n{ex.StackTrace}");
            }
        }

        private void CheckAppointments(object sender, ElapsedEventArgs e)
        {
            try
            {
                Debug.WriteLine($"\n=== CHECKING APPOINTMENTS AT {DateTime.Now} ===");
                Debug.WriteLine($"Attempting to connect to database: {CONN}");
                
                using var conn = new OleDbConnection(CONN);
                conn.Open();
                Debug.WriteLine("Database connection successful");

                // First verify the database structure
                VerifyDatabaseStructure();

                // Get appointments that are:
                // 1. Approved status
                // 2. Current time is within 1 hour before the appointment start time
                // 3. Haven't been notified yet
                string sql = @"
                    SELECT a.AppointmentID, a.AppointmentDate, a.Status, a.TimeSlot,
                           u.username as Email, u.Name as UserName,
                           c.ClinicName
                    FROM ((Appointments a
                    INNER JOIN Account u ON a.UserInfoID = u.LoginID)
                    INNER JOIN Clinics c ON a.ClinicID = c.ClinicID)
                    WHERE a.Status = 'Approved'
                    AND NOT EXISTS (
                        SELECT 1 FROM AppointmentNotifications 
                        WHERE AppointmentID = a.AppointmentID
                    )";

                Debug.WriteLine($"\nExecuting SQL query: {sql}");
                
                using var cmd = new OleDbCommand(sql, conn);
                var now = DateTime.Now;
                cmd.Parameters.AddWithValue("?", now);
                cmd.Parameters.AddWithValue("?", now);
                cmd.Parameters.AddWithValue("?", now);
                Debug.WriteLine($"Added parameter: Current time = {now}");

                // Let's also check if we have any approved appointments
                using var checkAppointmentsCmd = new OleDbCommand(
                    "SELECT COUNT(*) FROM Appointments WHERE Status = 'Approved'", conn);
                var approvedCount = checkAppointmentsCmd.ExecuteScalar();
                Debug.WriteLine($"Found {approvedCount} approved appointments in total");

                Debug.WriteLine("\nExecuting main query...");
                using var rdr = cmd.ExecuteReader();
                Debug.WriteLine("Query executed successfully");

                bool foundAppointments = false;
                while (rdr.Read())
                {
                    foundAppointments = true;
                    int appointmentId = rdr.GetInt32(0);
                    DateTime appointmentTime = rdr.GetDateTime(1);
                    string userEmail = rdr.GetString(3);
                    string userName = rdr.GetString(4);
                    string clinicName = rdr.GetString(5);

                    Debug.WriteLine($"Found appointment: ID={appointmentId}, Time={appointmentTime}, Email={userEmail}, Status={rdr.GetString(2)}");
                    SendAppointmentReminder(userEmail, userName, clinicName, appointmentTime);
                    MarkNotificationAsSent(appointmentId);
                }

                if (!foundAppointments)
                {
                    Debug.WriteLine("No approved appointments found where current time is within 1 hour before the appointment start time");
                }

                Debug.WriteLine("=== APPOINTMENT CHECK COMPLETE ===\n");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"\n=== ERROR CHECKING APPOINTMENTS ===");
                Debug.WriteLine($"Error message: {ex.Message}");
                Debug.WriteLine($"Error type: {ex.GetType().FullName}");
                Debug.WriteLine($"Stack trace: {ex.StackTrace}");
                
                if (ex.InnerException != null)
                {
                    Debug.WriteLine($"\nInner exception:");
                    Debug.WriteLine($"Message: {ex.InnerException.Message}");
                    Debug.WriteLine($"Type: {ex.InnerException.GetType().FullName}");
                    Debug.WriteLine($"Stack trace: {ex.InnerException.StackTrace}");
                }

                // Try to get more details about the error
                if (ex is OleDbException oleEx)
                {
                    Debug.WriteLine("\nOLE DB Error Details:");
                    Debug.WriteLine($"Error Code: {oleEx.ErrorCode}");
                    Debug.WriteLine($"Source: {oleEx.Source}");
                    Debug.WriteLine($"Message: {oleEx.Message}");
                }

                Debug.WriteLine("=== ERROR DETAILS END ===\n");
            }
        }

        private void SendAppointmentReminder(string userEmail, string userName, string clinicName, DateTime appointmentTime)
        {
            try
            {
                Debug.WriteLine($"Attempting to send email to {userEmail}");
                
                // Configure SMTP client with more reliable settings
                using var smtpClient = new SmtpClient("smtp.gmail.com", 587)
                {
                    EnableSsl = true,
                    UseDefaultCredentials = false,
                    Credentials = new System.Net.NetworkCredential("ClinicManagementSystemC@gmail.com", "hyop ejoi vhlm miss"),
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    Timeout = 30000 // 30 seconds timeout
                };

                var mailMessage = new MailMessage
                {
                    From = new MailAddress("ClinicManagementSystemC@gmail.com", "Clinic Management System"),
                    Subject = "Appointment Reminder",
                    Body = $@"Dear {userName},

This is a reminder that you have an appointment at {clinicName} on {appointmentTime:MMMM dd, yyyy} at {appointmentTime:hh:mm tt}.

Please arrive 15 minutes before your scheduled appointment time.

Best regards,
Clinic Management System",
                    IsBodyHtml = false,
                    Priority = MailPriority.High
                };

                mailMessage.To.Add(new MailAddress(userEmail));
                
                // Add error handling for the send operation
                try
                {
                    smtpClient.Send(mailMessage);
                    Debug.WriteLine($"Email sent successfully to {userEmail}");
                }
                catch (SmtpException smtpEx)
                {
                    Debug.WriteLine($"SMTP Error sending email: {smtpEx.StatusCode} - {smtpEx.Message}");
                    if (smtpEx.InnerException != null)
                    {
                        Debug.WriteLine($"SMTP Inner exception: {smtpEx.InnerException.Message}");
                    }
                    throw; // Re-throw to be caught by outer try-catch
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error sending email to {userEmail}: {ex.Message}\n{ex.StackTrace}");
                if (ex.InnerException != null)
                {
                    Debug.WriteLine($"Inner exception: {ex.InnerException.Message}\n{ex.InnerException.StackTrace}");
                }
            }
        }

        private void MarkNotificationAsSent(int appointmentId)
        {
            try
            {
                Debug.WriteLine($"Marking appointment {appointmentId} as notified");
                using var conn = new OleDbConnection(CONN);
                conn.Open();

                string sql = "INSERT INTO AppointmentNotifications (AppointmentID, NotificationTime) VALUES (@AppointmentID, @NotificationTime)";
                Debug.WriteLine($"Executing SQL: {sql} with AppointmentID={appointmentId}");
                
                using var cmd = new OleDbCommand(sql, conn);
                cmd.Parameters.AddWithValue("@AppointmentID", appointmentId);
                cmd.Parameters.AddWithValue("@NotificationTime", DateTime.Now);
                cmd.ExecuteNonQuery();
                Debug.WriteLine($"Successfully marked appointment {appointmentId} as notified");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error marking notification as sent: {ex.Message}\n{ex.StackTrace}");
                if (ex.InnerException != null)
                {
                    Debug.WriteLine($"Inner exception: {ex.InnerException.Message}\n{ex.InnerException.StackTrace}");
                }
            }
        }

        public void Stop()
        {
            Debug.WriteLine("Stopping appointment notification system");
            _notificationTimer.Stop();
            _notificationTimer.Dispose();
        }
    }
} 