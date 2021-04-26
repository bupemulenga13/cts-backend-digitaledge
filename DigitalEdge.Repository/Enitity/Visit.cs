using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DigitalEdge.Repository
{
    public class Visit
    {

        public Visit()
        {
        }

        public Visit(long VisitId, long ClientId, long FacilityId, long ServicePointId, string firstName, string lastName, string middleName,
          long clientPhoneNo, DateTime? dateOfBirth, DateTime priorAppointmentDate,
         DateTime nextAppointmentDate, DateTime visitDate, string visitType, string reasonOfVisit, string adviseNotes, long age)
        {
            this.VisitId = VisitId;
            this.ClientId = ClientId;
            this.FacilityId = FacilityId;
            this.ServicePointId = ServicePointId;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.MiddleName = middleName;
            this.ClientPhoneNo = clientPhoneNo;
            this.DateOfBirth = dateOfBirth;
            this.VisitDate = visitDate;
            this.VisitType = visitType;
            this.PriorAppointmentDate = priorAppointmentDate;
            this.NextAppointmentDate = nextAppointmentDate;
            this.ReasonOfVisit = reasonOfVisit;
            this.AdviseNotes = adviseNotes;
            this.Age = age;
        }
        public Visit(long visitId, long clientId, long? appointmentId, long facilityId, long servicePointId, DateTime visitDate, string reasonOfVisit, string clinicRemarks, string diagnosis, string secondDiagnosis, string thirdDiagnosis, string therapy)
        {
            this.VisitId = visitId;
            this.ClientId = clientId;
            this.AppointmentId = appointmentId;
            this.FacilityId = facilityId;
            this.ServicePointId = servicePointId;
            this.VisitDate = visitDate;
            this.ReasonOfVisit = reasonOfVisit;
            this.ClinicRemarks = clinicRemarks;
            this.Diagnosis = diagnosis;
            this.SecondDiagnosis = secondDiagnosis;
            this.ThirdDiagnosis = thirdDiagnosis;
            this.Therapy = therapy;

        }

        [Key]
        public long VisitId { get; set; }

        public long ClientId { get; set; }

        [ForeignKey("ClientId")]
        public virtual Client Clients { get; set; }

        [ForeignKey("FacilityId")]

        public long FacilityId { get; set; }

        public virtual Facility Facility { get; set; }

        [ForeignKey("ServicePointId")]
        public long ServicePointId { get; set; }

        public virtual ServicePoint ServicePoints { get; set; }
        
        [ForeignKey("ServiceTypeId")]
        public long ServiceTypeId { get; set; }

        public virtual VisitServices ServiceTypes { get; set; }
        
        /*[ForeignKey("ServiceTypeId")]
        public long ServiceTypeId { get; set; }

        public virtual VisitServices ServiceTypes { get; set; } */

        [ForeignKey("AppointmentId")]
        public long? AppointmentId { get; set; }

        public virtual Appointment Appointments { get; set; }
        public DateTime VisitDate { get; set; }
        public string VisitType { get; set; }
        public string ClinicRemarks { get; set; }
        public string Diagnosis { get; set; }
        public string SecondDiagnosis { get; set; }
        public string ThirdDiagnosis { get; set; }
        public string Therapy { get; set; }
        public DateTime PriorAppointmentDate { get; set; }
        public DateTime NextAppointmentDate { get; set; }
        public long AppointmentStatus { get; set; }
        public string ReasonOfVisit { get; set; }
        public string AdviseNotes { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateEdited{ get; set; }
        public long EditedBy{ get; set; }
        public long CreatedBy{ get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public long ClientPhoneNo { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public long Age { get; set; }

        
       
    }
}
