using System;
using System.Collections.Generic;
using System.Text;

namespace DigitalEdge.Domain
{
    public class VisitModel
    {
        public VisitModel()
        {
        }
        public long VisitId { get; set; }
        public long FacilityId { get; set; }
        public long ServiceTypeId { get; set; }
        public long? AppointmentId { get; set; }
        public AppointmentsModel Appointments { get; set; }
        public long ClientId { get; set; }
        public DateTime VisitDate { get; set; }
        public string ReasonOfVisit { get; set; }        
        public string VisitType { get; set; }
        public string ClinicRemarks { get; set; }
        public string Diagnosis { get; set; }
        public string SecondDiagnosis { get; set; }
        public string ThirdDiagnosis { get; set; }
        public string Therapy { get; set; }
        public DateTime PriorAppointmentDate { get; set; }
        public DateTime NextAppointmentDate { get; set; }
        public long AppointmentStatus { get; set; }
        public string AdviseNotes { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateEdited { get; set; }
        public long EditedBy { get; set; }
        public long CreatedBy { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string ClientPhoneNo { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public long Age { get; set; }


    }
}
