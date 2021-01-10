using System;

namespace DigitalEdge.Domain
{
    public class ProvinceModel
    {
        public ProvinceModel()
        {
        }
        public ProvinceModel(long provinceId , string provinceName)
        {
            this.ProvinceId = provinceId;
            this.ProvinceName = provinceName;
        }
        public long ProvinceId { get; set; }
        public string ProvinceName { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateEdited { get; set; }
        public long EditedBy { get; set; }
        public long CreatedBy { get; set; }
    }
}
