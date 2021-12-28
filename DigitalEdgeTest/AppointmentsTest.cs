using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using Microsoft.Data.SqlClient;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DigitalEdgeTest
{
    [TestClass()]
    class AppointmentsTest
    {
        private readonly string ctsConnStr =
            @"Server=.\SMARTCARE40; Database=CTSMigrationDB; User ID=sa; Password=m7r@n$4mAz; Trusted_Connection=True;";

        private readonly string _updateAppointmentStatusSql = File.ReadAllText(
            @"C:\TheLab\CTS\backend\cts-backend-digitaledge\DigitalEdge.Domain\SeedData\UpdateAppointmentStatusProc.sql");

        [TestMethod()]
        public bool SeedStoredProcedure()
        {
            using SqlConnection conn = new SqlConnection(ctsConnStr);
            //split into individual sql at each "GO" keyword
            IEnumerable<string> cmdStrings = Regex.Split(_updateAppointmentStatusSql, @"^\s*GO\s*$",
                RegexOptions.Multiline | RegexOptions.IgnoreCase);
            conn.Open();
            var transaction = conn.BeginTransaction("execScript");
            foreach (string cmdStr in cmdStrings)
            {
                if (!string.IsNullOrWhiteSpace(cmdStr.Trim()))
                {
                    using var appointmentStatusUpdateCmd = new SqlCommand(cmdStr, conn);
                    appointmentStatusUpdateCmd.Transaction = transaction;
                    try
                    {
                        appointmentStatusUpdateCmd.CommandTimeout = 30000;
                        appointmentStatusUpdateCmd.CommandText = cmdStr;
                        int affectedRows = appointmentStatusUpdateCmd.ExecuteNonQuery();
                        transaction.Commit();
                        Console.WriteLine("Completed seed with" + affectedRows + "rows affected.");
                    }
                    catch (Exception e)
                    {
                        transaction.Rollback();
                        Console.WriteLine("Error seeding stored procedure: " + e.StackTrace);
                        throw;
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
            }

            return true;
        }
    }
}