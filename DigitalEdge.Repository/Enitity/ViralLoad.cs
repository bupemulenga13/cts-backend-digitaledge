using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DigitalEdge.Repository.Enitity
{
    public class ViralLoad
    {
        public ViralLoad()
        {

        }

        public ViralLoad(long id, int initlaVlCount, int currentVLcount, DateTime nextVLDueDate, DateTime dateCreated)
        {
            ViralLoadId = id;
            InitialViralLoadCount = initlaVlCount;
            CurrentViralLoadCount = currentVLcount;
            NextVLDueDate = nextVLDueDate;
            DateCreated = dateCreated;

        }

        [Key]
        public long ViralLoadId { get; set; }

        public long ClientId { get; set; }

        [ForeignKey("ClientId")]
        public virtual Client Clients { get; set; }


        public int InitialViralLoadCount { get; set; }

        public int CurrentViralLoadCount { get; set; }

        public DateTime? NextVLDueDate { get; set; }

        public DateTime DateCreated { get; set; }

    }
}
