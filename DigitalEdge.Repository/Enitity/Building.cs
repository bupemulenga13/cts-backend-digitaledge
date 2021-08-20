using DigitalEdge.Repository.Enitity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DigitalEdge.Repository
{
  public class Facility
    {

        public Facility()
        {
        }


        public Facility(long facilityId, string facilityName, string facilityContactNumber, long facilityTypeId, bool isAvailable, string address, DateTime? dateCreated, long createdBy, long? districtId)
        {
            FacilityId = facilityId;
            FacilityName = facilityName;
            FacilityContactNumber = facilityContactNumber;
            FacilityTypeId = facilityTypeId;
            IsAvailable = isAvailable;
            Address = address;
            DateCreated = dateCreated;
            CreatedBy = createdBy;
            DistrictId = districtId;
        }

        public Facility(long facilityId , long? districtId , string facilityName, string facilityContactNumber, long facilityTypeId, bool isAvailable, string address, DateTime? dateCreated, DateTime? dateEdited, long? createdBy, long? editedBy)
        {
            Facility facility = this;
            facility.FacilityId = facilityId;
            facility.DistrictId = districtId;
            facility.FacilityName = facilityName;
            facility.FacilityContactNumber = facilityContactNumber;
            facility.FacilityTypeId = facilityTypeId;
            facility.IsAvailable = isAvailable;
            facility.Address = address;
            facility.DateCreated = dateCreated;
            facility.DateEdited = dateEdited;
            facility.CreatedBy = createdBy;
            facility.EditedBy = editedBy;


        }


        [Key]
        public long FacilityId { get; set; }
        public string FacilityName { get; set; }
        public string FacilityContactNumber { get; set; }

        public long FacilityTypeId { get; set; }

        [ForeignKey("FacilityTypeId")]
        public virtual FacilityType FacilityTypeModel { get; set; }
        public long? DistrictId { get; set; }

        [ForeignKey("DistrictId")]
        public virtual District Districts { get; set; }

        public DateTime? DateCreated { get; set; }
        public DateTime? DateEdited { get; set; }
        public long? EditedBy { get; set; }
        public long? CreatedBy { get; set; }
        public bool IsAvailable { get; set; }
        public string Address { get; set; }





    }
}
