using System;

namespace DigitalEdge.Domain
{
    public class DistrictModel
    {

        public DistrictModel()
        {
        }

        public DistrictModel(long districtId , string districtName , long? provinceId )
        {
            this.DistrictId = districtId;
            this.DistrictName = districtName;
            this.ProvinceId = provinceId;
        }

        public long DistrictId { get; set; }
        public string DistrictName { get; set; }
        public long? ProvinceId { get; set; }
        public ProvinceModel Provinces { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateEdited { get; set; }
        public long EditedBy { get; set; }
        public long CreatedBy { get; set; }
    }
}
