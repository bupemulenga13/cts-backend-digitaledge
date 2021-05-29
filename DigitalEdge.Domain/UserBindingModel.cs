
namespace DigitalEdge.Domain
{
    public class UserBindingModel
    {   
        public long UserId { get; set; }
        public string FacilityName { get; set; }
        public string UserName { get; set; }
        public bool IsAvailable { get; set; }
        public string Facility { get; set; }
        public string ServicePoint { get; set; }
        public long? UserFacilityId { get; set; }
        public long FacilityId { get; set; }
        public long ServicePointId { get; set; }
        public string ServicePointName { get; set; }
        public string DistrictId { get; set; }
        public string ProvinceId { get; set; }
        public string ProvinceName { get; set; }
        public int FacilityContactNumber { get; set; }
        public long FacilityTypeId { get; set; }
        public string Address { get; set; }


    }

}
