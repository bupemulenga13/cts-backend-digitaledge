using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DigitalEdge.Repository
{
   public class UserFacility
    {

        public UserFacility()
        {
        }      
        public UserFacility(long facilityId, long? userId, bool isactive , long? userfacilityId , long? servicepointId)
        {
            this.FacilityId = facilityId;
            this.UserId = userId;
            this.IsActive = isactive;
            this.UserFacilityId = userfacilityId;
            this.ServicePointId = servicepointId;
        }      

        [Key]
        public long? UserFacilityId { get; set; }
        public long? UserId { get; set; }
        public long? FacilityId { get; set; }    
        public long? ServicePointId { get; set; }    
        public bool IsActive { get; set; }
        [ForeignKey("FacilityId")]
        public virtual Facility facility { get; set; }
        [ForeignKey("UserId")]
        public virtual Users Users { get; set; } 
        [ForeignKey("ServicePointId")]
        public virtual ServicePoint ServicePoints { get; set; }

    }
}
