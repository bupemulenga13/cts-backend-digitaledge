using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DigitalEdge.Repository.Enitity
{
    public class ClientType
    {
        [Key]
        public long ClientTypeId { get; set; }

        public string ClientTypeName { get; set; }
    }
}
