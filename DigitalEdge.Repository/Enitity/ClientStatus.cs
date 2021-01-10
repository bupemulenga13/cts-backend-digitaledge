using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DigitalEdge.Repository.Enitity
{
    public class ClientStatus
    {
        [Key]
        public long ClientStatusId { get; set; }

        public string ClientStatusName { get; set; }

    }
}
