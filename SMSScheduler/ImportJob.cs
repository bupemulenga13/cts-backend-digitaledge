using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using Quartz;
using DigitalEdge.Domain;
using DigitalEdge.Repository;

namespace DigitalEdge.SMSScheduler
{
    public class ImportJob : IJob
    {
        public ImportJob()
        {
          
        }

        public Task Execute(IJobExecutionContext context)
        {
            try
            {
                var logPath = @"E:\TechbitSolution\Text.txt";
                File.AppendAllText(logPath, $"Job started   " + Environment.NewLine);
            }
            catch (Exception ex)
            {
                var logPath = @"E:\TechbitSolution\Text.txt";
                File.AppendAllText(logPath, $"Error on Job" + Environment.NewLine);
                ErrorLogger.LogErrorToFile(ex);
            }

            return Task.FromResult(0);

        }

    }
}
