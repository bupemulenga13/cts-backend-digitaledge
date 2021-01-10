using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DigitalEdge.Repository
{
  public  class ClientIdentity
    {
        [Key]
        public long IdentityId { get; set; }
        public byte[] FingerPrintImage { get; set; }
        public long? ClientId { get; set; }
        [ForeignKey("ClientId")]
        public virtual Client Clients { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateEdited { get; set; }
        public long EditedBy { get; set; }
        public long CreatedBy { get; set; }
    }
}
