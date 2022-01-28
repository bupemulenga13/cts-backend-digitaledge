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

        public string InitialViralLoadCount { get; set; }

        public string CurrentViralLoadCount { get; set; }

        public string NextVLDueDate { get; set; }

        public DateTime? DateCreated { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string ArtNo { get; set; }
 

        public DateTime GetCreatedDate()
        {
            return DateTime.Now;
        }



    }
}
