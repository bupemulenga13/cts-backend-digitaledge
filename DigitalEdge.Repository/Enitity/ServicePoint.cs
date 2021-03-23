using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DigitalEdge.Repository
{
   public class ServicePoint
    {
        public ServicePoint()
        {
        }
        public ServicePoint(long servicePointId, string servicePointName, long? facilityId)
        {
            this.FacilityId = facilityId;
            this.ServicePointId = servicePointId;
            this.ServicePointName = servicePointName;
        }

        [Key]
        public long ServicePointId { get; set; }
        public string ServicePointName { get; set; }
        public long? FacilityId { get; set; }

        [ForeignKey("FacilityId")]
        public virtual Facility Facility { get; set;}
        public DateTime DateCreated { get; set; }
        public DateTime DateEdited { get; set; }
        public long EditedBy { get; set; }
        public long CreatedBy { get; set; }
    }
}
