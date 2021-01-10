using System;
using System.Collections.Generic;
using System.Text;
using DigitalEdge.Domain;

namespace DigitalEdge.Services
{ 
    public interface ISMSSchedulerService
    {
        List<SMSRecords> ClientsList();
    }
}
