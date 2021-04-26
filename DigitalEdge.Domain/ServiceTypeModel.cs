using System;
using System.Collections.Generic;
using System.Text;

namespace DigitalEdge.Domain
{
    public class ServiceTypeModel
    {
        public ServiceTypeModel()
        {

        }

        public long ServiceTypeId { get; set; }
        public string ServiceTypeName { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateEdited { get; set; }
        public long EditedBy { get; set; }
        public long CreatedBy { get; set; }
    }
}
