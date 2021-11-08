
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

        //Add Appointment
        public Appointment(long id, long clientId, long facilityId, long serviceTypeId, DateTime appointmentDate, int appointmentStatus, string detail, DateTime? dateCreated, long? createdBy)
        {
            AppointmentId = id;
            ClientId = clientId;
            FacilityId = facilityId;
            ServiceTypeId = serviceTypeId;
            AppointmentDate = appointmentDate;
            AppointmentStatus = appointmentStatus;
            Comment = detail;
            DateCreated = dateCreated;
            CreatedBy = createdBy;
        }

        //Edit Appointment
        public Appointment(long id, long clientId , long facilityId, long serviceTypeId, DateTime appointmentDate, DateTime? appointmentDateFulfilled, int appointmentStatus, string detail, DateTime? dateCreated, DateTime? dateEdited, long? createdBy, long? editedBy)
        {
            AppointmentId = id;
            ClientId = clientId;
            FacilityId = facilityId;
            ServiceTypeId = serviceTypeId;
            AppointmentDate = appointmentDate;
            InteractionDate = appointmentDateFulfilled;
            AppointmentStatus = appointmentStatus;
            Comment = detail;
            DateEdited = dateEdited;
            DateCreated = dateCreated;
            CreatedBy = createdBy;
            EditedBy = editedBy;
        }
                

        [Key]
        public long AppointmentId { get; set; } 
        public long ClientId { get; set; }

        [ForeignKey("ClientId")]
        public virtual Client ClientModel { get; set; }

        public long FacilityId { get; set; }

        [ForeignKey("FacilityId")]
        public virtual Facility FacilityModel { get; set; }

        public long ServiceTypeId { get; set; }

        [ForeignKey("ServiceTypeId")]
        public virtual VisitServices ServiceTypeModel { get; set; }

        public DateTime AppointmentDate { get; set; }
        public int AppointmentStatus { get; set; }
        public DateTime? InteractionDate { get; set; }
        public string Comment { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateEdited { get; set; }
        public long? EditedBy { get; set; }
        public long? CreatedBy { get; set; }



    }
}
