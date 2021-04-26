using System;
using System.Collections.Generic;
using System.Text;

namespace DigitalEdge.Domain
{
    public class VisitsServiceModel
    {
        public VisitsServiceModel() { }

        public VisitsServiceModel(long serviceTypeId, string serviceTypeName)
        {
            this.ServiceTypeId = serviceTypeId;
            this.ServiceTypeName = serviceTypeName;
        }

        public long ServiceTypeId { get; set; }

        public string ServiceTypeName { get; set; }


    }
}
