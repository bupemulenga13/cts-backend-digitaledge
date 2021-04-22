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
            ServiceTypeId = serviceTypeId;
            ServiceTypeName = serviceTypeName;
        }

        public long ServiceTypeId { get; set; }

        public string ServiceTypeName { get; set; }


    }
}
