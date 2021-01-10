using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DigitalEdge.Repository.Enitity
{
    public class FacilityType
    {
        [Key]
        public long FacilityTypeId { get; set; }

        public string FacilityTypeName  { get; set; }
    }
}
