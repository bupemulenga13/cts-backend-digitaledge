using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using Microsoft.Data.SqlClient;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DigitalEdgeTest.Services
{
    [TestClass()]
    public class VisitServiceTest
    {
        private readonly string ctsConnStr =
            @"Server=.\SMARTCARE40; Database=CTSMigrationDB; User ID=sa; Password=m7r@n$4mAz; Trusted_Connection=True;";

        private readonly string _updateAppointmentStatusSql = File.ReadAllText(
            @"C:\TheLab\CTS\backend\cts-backend-digitaledge\DigitalEdge.Domain\SeedData\UpdateAppointmentStatusProc.sql");

        [TestMethod()]
        public void UpdateAppointmentStatusTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void SeedStoredProcedure()
        {
            using SqlConnection conn = new SqlConnection(ctsConnStr);
            int affectedRows = -1;
            //split into individual sql at each "GO" keyword
            IEnumerable<string> cmdStrings = Regex.Split(_updateAppointmentStatusSql, @"^\s*GO\s*$",
                RegexOptions.Multiline | RegexOptions.IgnoreCase);
            foreach (string cmdStr in cmdStrings)
            {
                if (!string.IsNullOrWhiteSpace(cmdStr.Trim()))
                {
                    using var appointmentStatusUpdateCmd = new SqlCommand(cmdStr, conn);
                    conn.Open();
                    try
                    {
                        appointmentStatusUpdateCmd.CommandTimeout = 30000;
                        appointmentStatusUpdateCmd.CommandText = cmdStr;
                        affectedRows = appointmentStatusUpdateCmd.ExecuteNonQuery();
                        Console.WriteLine("Completed seed with" + affectedRows + "rows affected.");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Error seeding stored procedure: " + e.StackTrace);
                        throw;
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
            }

            Assert.AreNotSame(affectedRows, -1, "Error occurred running appointment tests");
        }
    }
}