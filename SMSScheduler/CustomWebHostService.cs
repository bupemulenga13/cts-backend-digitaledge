using System;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.WindowsServices;

namespace DigitalEdge.SMSScheduler
{
    internal class CustomWebHostService1 : WebHostService
    {
        private IWebHost host;

        public CustomWebHostService1(IWebHost host) : base(host)
        {
            this.host = host;
        }
        protected override void OnStarting(string[] args)
        {
            base.OnStarting(args);
        }
        protected override void OnStarted()
        {
            var scheduler = new ImportScheduler();
            scheduler.Start();
            base.OnStarted();
        }

        protected override void OnStopping()
        {
            base.OnStopping();
        }
    }
    public class CustomWebHostService : WebHostService
    {
        private IWebHost host;
        public CustomWebHostService(IWebHost host) : base(host)
        {
            this.host = host;
        }

        protected override void OnStarting(string[] args)
        {
            base.OnStarting(args);

        }

        protected override void OnStarted()
        {
            var scheduler = new ImportScheduler();
            scheduler.Start();
            base.OnStarted();
        }

        protected override void OnStopping()
        {
            base.OnStopping();
        }
    }
}