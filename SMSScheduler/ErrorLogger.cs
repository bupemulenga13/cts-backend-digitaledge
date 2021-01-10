using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DigitalEdge.SMSScheduler
{
    public static class ErrorLogger
    {
        public static void LogErrorToFile(Exception ex)
        {
            string baseDir = @"C:\DigitalEdge.SMSScheduler.ErrorLogs";
            string dirToLogError = "";
            if (!Directory.Exists(baseDir))
            {
                Directory.CreateDirectory(baseDir);
            }
            dirToLogError = Path.Combine(baseDir, DateTime.UtcNow.ToString("MMM-dd-yyyy"));
            if (!Directory.Exists(dirToLogError))
            {
                Directory.CreateDirectory(dirToLogError);
            }
            string filePath = Path.Combine(dirToLogError, "Error.txt");
            using (StreamWriter writer = new StreamWriter(filePath, true))
            {
                writer.WriteLine("Message :" + ex.Message + Environment.NewLine + "StackTrace :" + ex.StackTrace +
                   "" + Environment.NewLine + "Date :" + DateTime.UtcNow.ToString());
                writer.WriteLine(Environment.NewLine + "-----------------------------------------------------------------------------" + Environment.NewLine);
            }
        }
    }
}
