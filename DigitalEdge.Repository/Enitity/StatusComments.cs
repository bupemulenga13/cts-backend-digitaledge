using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DigitalEdge.Repository.Enitity
{
    public class StatusComments
    {
        [Key]
        public long StatusCommentId { get; set; }

        public string StatusCommentName { get; set; }

        public long ClientStatusId { get; set; }

        [ForeignKey("ClientStatusId")]
        public virtual ClientStatus ClientStatuses { get; set; }

    }
}
