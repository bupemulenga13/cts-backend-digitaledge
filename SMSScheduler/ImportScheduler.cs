using Quartz;
using Quartz.Impl;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace DigitalEdge.SMSScheduler
{
    public class ImportScheduler
    {
        public ImportScheduler()
        {

        }

        public void Start()
        {
            try
            {
                ScheduleDataImportJob().GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {
                ErrorLogger.LogErrorToFile(ex);
            }
        }

        public void Stop()
        {
        }

        public static async Task ScheduleDataImportJob()
        {
            IScheduler scheduler;
            var schedulerFactory = new StdSchedulerFactory();
            scheduler = schedulerFactory.GetScheduler().Result;
            scheduler.Start().Wait();
            int ScheduleIntervalInMinute = 1;//job will run every minute
            JobKey jobKey = JobKey.Create("SMSScheduler");
            var GetJobDetail = scheduler.GetJobDetail(jobKey);
            IJobDetail job = JobBuilder.Create<ImportJob>().WithIdentity(jobKey).Build();
            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("JobTrigger")
                .StartNow()
                .WithSimpleSchedule(x => x.WithIntervalInMinutes(ScheduleIntervalInMinute).RepeatForever())
                .Build();
            await scheduler.ScheduleJob(job, trigger);
        }

    }
}
