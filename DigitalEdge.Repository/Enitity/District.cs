using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DigitalEdge.Repository
{
   public class District
    {
        [Key]
        public long DistrictId { get; set; }
        public string DistrictName { get; set; }
        public long ProvinceId { get; set; }
        [ForeignKey("ProvinceId")]
        public virtual Province Provinces { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateEdited { get; set; }
        public long EditedBy { get; set; }
        public long CreatedBy { get; set; }

     

    }
}
