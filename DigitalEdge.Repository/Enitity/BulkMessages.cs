using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DigitalEdge.Repository
{
    public class BulkMessages
    {
        [Key]
        public long BulkMessagesId { get; set; }
        public string ClientNumber { get; set; }
        public long? FacilityId { get; set; }

        [ForeignKey("FacilityId")]
        public virtual Facility facility { get; set; }
        public long? ServicePointId { get; set; }

        [ForeignKey("ServicePointId")]
        public virtual ServicePoint ServicePoints { get; set; }
        public string Message { get; set; }
        public string MessageStatus { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateEdited { get; set; }
        public long EditedBy { get; set; }
        public long CreatedBy { get; set; }
    }
}
