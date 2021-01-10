using System;
using System.ComponentModel.DataAnnotations;

namespace DigitalEdge.Repository
{
  public  class Province
    {
        [Key]
        public long ProvinceId { get; set; }
        public string ProvinceName { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateEdited { get; set; }
        public long EditedBy { get; set; }
        public long CreatedBy { get; set; }
    }
}
