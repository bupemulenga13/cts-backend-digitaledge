using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DigitalEdge.Repository.Enitity
{
    public class Sex
    {
        [Key]
        public long SexId { get; set; }

        public string SexName { get; set; }


    }
}
