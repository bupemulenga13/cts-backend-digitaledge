
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DigitalEdge.Repository
{
   public class Appointment
    {
        private long v1;
        private long v2;
        private long v3;
        private DateTime dateTime1;
        private DateTime dateTime2;

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

        public Appointment(long appointmentId, long clientId, long facilityId, long servicePointId, DateTime appointmentDate, DateTime interactionDate, DateTime priorAppointmentDate, int appointmentStatus, string detail, DateTime dateEdited, long editedBy)
        {
            AppointmentId = appointmentId;
            ClientId = clientId;
            FacilityId = facilityId;
            ServicePointId = servicePointId;
            AppointmentDate = appointmentDate;
            InteractionDate = interactionDate;
            PriorAppointmentDate = priorAppointmentDate;
            AppointmentStatus = appointmentStatus;
            Detail = detail;
            DateEdited = dateEdited;
            EditedBy = editedBy;
        }

        public Appointment(long id, long clientId,long facilityId, long? servicePointId , DateTime appointmentDate, int appointmentStatus, string detail, DateTime dateCreated, DateTime dateEdited, long editedBy, long createdBy)
        {
            this.AppointmentId = id;
            this.ClientId = clientId;
            this.FacilityId = facilityId;
            this.ServicePointId = servicePointId;
            this.AppointmentDate = appointmentDate;
            this.AppointmentStatus = appointmentStatus;
            this.Detail = detail;
            this.DateCreated = dateCreated;
            this.DateEdited = dateEdited;
            this.EditedBy = editedBy;
            this.CreatedBy = createdBy;
            this.FacilityId = facilityId;
            this.ServicePointId = servicePointId;
            this.AppointmentDate = appointmentDate;
        }

        //public Appointment(long id, long clientId, long facilityId, long servicePointId, DateTime dateTime, DateTime dateCreated, DateTime dateEdited, long editedBy, long createdBy, int appointmentStatus)
        //{
        //    AppointmentId = id;
        //    this.ClientId = clientId;
        //    this.FacilityId = facilityId;
        //    this.ServicePointId = servicePointId;
        //    this.DateCreated = dateCreated;
        //    this.DateEdited = dateEdited;
        //    this.EditedBy = editedBy;
        //    this.CreatedBy = createdBy;
        //    this.AppointmentStatus = appointmentStatus;
        //}

        public Appointment(long id, long clientId , long facilityId, long servicePointId, DateTime appointmentDate, DateTime appointmentDateFulfilled, int appointmentStatus, string detail, DateTime dateCreated, DateTime dateEdited, long editedBy, long createdBy, DateTime priorAppointmentDate)
        {
            AppointmentId = id;
            ClientId = clientId;
            FacilityId = facilityId;
            ServicePointId = servicePointId;
            AppointmentDate = appointmentDate;
            InteractionDate = appointmentDateFulfilled;
            AppointmentStatus = appointmentStatus;
            Detail = detail;
            DateCreated = dateCreated;
            DateEdited = dateEdited;
            EditedBy = editedBy;
            CreatedBy = createdBy;
            PriorAppointmentDate = priorAppointmentDate;
        }

        //public Appointment(long id, long clientId, long facilityId, long servicePointId, DateTime appointmentDate, int appointmentStatus, DateTime startDate, DateTime endDate, DateTime dateCreated, DateTime dateEdited, long editedBy, long createdBy)
        //{
        //    this.AppointmentId = id;
        //    this.ClientId = clientId;
        //    this.FacilityId = facilityId;
        //    this.ServicePointId = servicePointId;
        //    this.AppointmentDate = appointmentDate;
        //    this.AppointmentStatus = appointmentStatus;
        //    this.PriorAppointmentDate = startDate;
        //    this.InteractionDate = endDate;
        //    this.DateCreated = dateCreated;
        //    this.DateEdited = dateEdited;
        //    this.EditedBy = editedBy;
        //    this.CreatedBy = createdBy;
        //}

        [Key]
        public long AppointmentId { get; set; } 

        public long ClientId { get; set; }

        [ForeignKey("ClientId")]
        public virtual Client ClientModel { get; set; }

        public long FacilityId { get; set; }

        [ForeignKey("FacilityId")]
        public virtual Facility FacilityModel { get; set; }

        public long? ServicePointId { get; set; }

        [ForeignKey("ServicePointId")]
        public virtual ServicePoint ServicePointModel { get; set; }
        public DateTime AppointmentDate { get; set; }
        public int AppointmentStatus { get; set; }
        public DateTime PriorAppointmentDate { get; set; }
        public DateTime InteractionDate { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateEdited { get; set; }
        public long EditedBy { get; set; }
        public long CreatedBy { get; set; }
        public string Detail { get; set; }


    }
}
