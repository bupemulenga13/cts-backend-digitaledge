using System;
using System.IO;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace DigitalEdge.SMSScheduler
{
    class Program
    {
        static void Main(string[] args)
        {
            var host = WebHost.CreateDefaultBuilder(args)
    .UseContentRoot(AppContext.BaseDirectory)
    .UseStartup<Startup>()
    .Build();

            host.RunAsCustomService();
        }
    }
}
