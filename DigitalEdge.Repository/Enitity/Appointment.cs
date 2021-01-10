using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DigitalEdge.Repository
{
   public class Appointment
    {

        public Appointment()
        {
        }
        public Appointment(long id, long clientId , DateTime appointmentDate, DateTime dateCreated, DateTime dateEdited, long editedBy, long createdBy)
        {
            this.AppointmentId = id;
            this.ClientId = clientId;
            this.DateCreated = dateCreated;
            this.DateEdited = dateEdited;
            this.EditedBy = editedBy;
            this.CreatedBy = createdBy;
            this.AppointmentDate = appointmentDate;
        }
        public Appointment(long id, long clientId,long? facilityId, long? servicePointId , DateTime appointmentDate, DateTime dateCreated, DateTime dateEdited, long editedBy, long createdBy)
        {
            this.AppointmentId = id;
            this.ClientId = clientId;
            this.DateCreated = dateCreated;
            this.DateEdited = dateEdited;
            this.EditedBy = editedBy;
            this.CreatedBy = createdBy;
            this.FacilityId = facilityId;
            this.ServicePointId = servicePointId;
            this.AppointmentDate = appointmentDate;
        }
        [Key]
        public long AppointmentId { get; set; } 

        public long? ClientId { get; set; }

        [ForeignKey("ClientId")]
        public virtual Client Clients { get; set; }
        public long? FacilityId { get; set; }

        [ForeignKey("FacilityId")]
        public virtual Facility facility { get; set; }
        public long? ServicePointId { get; set; }

        [ForeignKey("ServicePointId")]
        public virtual ServicePoint ServicePoints { get; set; }

        public DateTime AppointmentDate { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateEdited { get; set; }
        public long EditedBy { get; set; }
        public long CreatedBy { get; set; }
    }
}
