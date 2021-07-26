using System;

namespace DigitalEdge.Domain
{
    public class FacilityModel
    {
        public FacilityModel()
        {
        }
        public FacilityModel(long facilityId, string facilityName)
        {
            this.FacilityId = facilityId;
            this.FacilityName = facilityName;
        }
        public FacilityModel(long facilityId, string facilityName, string districtName, long facilityTypeId, bool isAvailable, string address)
        {
            this.FacilityId = facilityId;
            this.FacilityName = facilityName;
            this.DistrictName = districtName;
            this.FacilityTypeId = facilityTypeId;
            this.IsAvailable = isAvailable;
            this.Address = address;

        }
        public long FacilityId { get; set; }
        public long AssignedFacilityId { get; set; }
        public string FacilityName { get; set; }
        public string FacilityContactNumber { get; set; }
        public long DistrictId { get; set; }
        public string DistrictName { get; set; }
        public string ProvinceId { get; set; }
        public string ProvinceName { get; set; }
        public DistrictModel Districts { get; set; }
        public string FacilityTypeName { get; set; }

        public DateTime DateCreated { get; set; }
        public DateTime DateEdited { get; set; }
        public long? EditedBy { get; set; }
        public long? CreatedBy { get; set; }
        public long FacilityTypeId { get; set; }
        public bool IsAvailable { get; set; }
        public string Address { get; set; }


    }
}
