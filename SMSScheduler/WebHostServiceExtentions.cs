using System;
using System.Collections.Generic;
using System.ServiceProcess;
using System.Text;
using Microsoft.AspNetCore.Hosting;

namespace DigitalEdge.SMSScheduler
{
    public static class WebHostServiceExtentions
    {
        public static void RunAsCustomService(this IWebHost host)
        {
            var webHostService = new CustomWebHostService(host);
            ServiceBase.Run(webHostService);
        }
    }
}
