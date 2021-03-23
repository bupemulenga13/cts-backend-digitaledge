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

        public Facility(long facilityId, string facilityName, string facilityContactNumber, bool isAvailable, string address)
        {
            FacilityId = facilityId;
            FacilityName = facilityName;
            FacilityContactNumber = facilityContactNumber;
            IsAvailable = isAvailable;
            Address = address;
        }

        public Facility(long facilityId, string facilityName, string facilityContactNumber, long facilityTypeId, bool isAvailable, string address)
        {
            FacilityId = facilityId;
            FacilityName = facilityName;
            FacilityContactNumber = facilityContactNumber;
            FacilityTypeId = facilityTypeId;
            IsAvailable = isAvailable;
            Address = address;
        }

        public Facility(long facilityId , long? districtId , string facilityName, string facilityContactNumber, long facilityTypeId, bool isAvailable, string address)
        {
            this.FacilityId = facilityId;
            this.FacilityTypeId = facilityTypeId;
            this.DistrictId = districtId;
            this.FacilityName = facilityName;
            this.FacilityContactNumber = facilityContactNumber;
            this.IsAvailable = isAvailable;
            this.Address = address;

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

        public DateTime DateCreated { get; set; }
        public DateTime DateEdited { get; set; }
        public long EditedBy { get; set; }
        public long CreatedBy { get; set; }
        public bool IsAvailable { get; set; }
        public string Address { get; set; }





    }
}
