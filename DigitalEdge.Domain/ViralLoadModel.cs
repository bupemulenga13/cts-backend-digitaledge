using System;
using System.Collections.Generic;
using System.Text;

namespace DigitalEdge.Domain
{
    public class ViralLoadModel
    {
        public ViralLoadModel()
        {
        }

        public long ViralLoadId { get; set; }

        public long ClientId { get; set; }

        public int InitialViralLoadCount { get; set; }

        public int CurrentViralLoadCount { get; set; }

        public DateTime NextVLDueDate { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime GetCreatedDate()
        {
            return DateTime.Now;
        }



    }
}
