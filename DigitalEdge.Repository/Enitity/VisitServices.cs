using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DigitalEdge.Repository.Enitity
{
    public class VisitServices
    {
        [Key]
        public long ServiceTypedId { get; set; }

        public string ServiceTypeName { get; set; }

    }
}
