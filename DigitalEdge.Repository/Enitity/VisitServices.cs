using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DigitalEdge.Repository
{
    public class VisitServices
    {
        [Key]
        public long ServiceTypeId { get; set; }

        public string ServiceTypeName { get; set; }

    }
}
