using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text.RegularExpressions;
using DigitalEdge.Domain;
using DigitalEdge.Services;
using Microsoft.Data.SqlClient;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DigitalEdgeTest.Services
{
    [TestClass()]
    public class VisitServiceTest
    {
        private readonly string ctsConnStr =
            @"Server=; Database=CTSMigrationDB; User ID=sa; Password=m7r@n$4mAz; Trusted_Connection=True;";


        private readonly string _updateAppointmentStatusSql = File.ReadAllText(
            @"C:\TheLab\CTS\backend\cts-backend-digitaledge\DigitalEdge.Domain\SeedData\UpdateAppointmentStatusProc.sql");


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

        [TestMethod()]
        public void UpdateAppointmentStatusTest()
        {
            AppointmentsModel appointments = new AppointmentsModel(
                10,
                841,
                1180,
                2,
                new DateTime(2021, 11, 12),
                new DateTime(2021, 10, 17),
                DateTime.Now,
                8,
                8,
                0 // -1-> Missed, 0 -> Pending, 1 -> Attended
            );


            var appointmentId = appointments.AppointmentId;
            var providerId = appointments.EditedBy;
            using SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ctsConnStr;
            conn.Open();
            SqlCommand cmd =
                new SqlCommand("UpdateAppointmentStatus", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            var outParameter = cmd.Parameters.Add("@Result", SqlDbType.Bit);
            outParameter.Direction = ParameterDirection.Output;


            var inParameters = cmd.Parameters.Add("@AppointmentId", SqlDbType.Int);
            inParameters.Direction = ParameterDirection.Input;
            inParameters.Value = appointmentId;

            inParameters = cmd.Parameters.Add("@ProviderId", SqlDbType.Int);
            inParameters.Direction = ParameterDirection.Input;
            inParameters.Value = providerId;

            var returnValParameter = cmd.Parameters.Add("return_value", SqlDbType.Bit);
            returnValParameter.Direction = ParameterDirection.ReturnValue;

            cmd.CommandTimeout = 30000;
            cmd.ExecuteNonQuery();
            var result = returnValParameter.Value;
            conn.Close();

            Assert.AreEqual(result, 1);
        }
    }
}