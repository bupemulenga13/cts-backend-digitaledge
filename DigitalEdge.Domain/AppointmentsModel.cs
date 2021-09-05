using System;

namespace DigitalEdge.Domain
{
 public class AppointmentsModel
    {
        public AppointmentsModel()
        {
        }

        public AppointmentsModel(long appointmentId, long? clientId, long? facilityId, long? servicePointId, DateTime? appointmentDate, DateTime? dateCreated, DateTime? dateEdited, long? editedBy, long? createdBy, int? appointmentStatus)
        {
            this.AppointmentId = appointmentId;
            this.ClientId = clientId;
            FacilityId = facilityId;
            ServicePointId = servicePointId;
            AppointmentDate = appointmentDate;
            DateCreated = dateCreated;
            DateEdited = dateEdited;
            EditedBy = editedBy;
            CreatedBy = createdBy;
            AppointmentStatus = appointmentStatus;
        }

        public AppointmentsModel(long id,long? clientId,long visitsId,string firstName, string lastName, string middleName, DateTime? priorAppointmentDate,DateTime? appointmentDate,
                DateTime? appointmentTime, DateTime? nextAppointmentDate , int? age)
        {
            this.Id = id;
            this.ClientId = clientId;
            this.VisitsId = visitsId;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.MiddleName = middleName;
            this.PriorAppointmentDate = priorAppointmentDate;
            this.AppointmentDate = appointmentDate;
            this.AppointmentTime = appointmentTime;
            this.NextAppointmentDate = nextAppointmentDate;
            this.Age = age;
        }
        public AppointmentsModel(long id, string firstName, string lastName, string middleName, string clientPhoneNo,DateTime? dateOfBirth, DateTime? priorAppointmentDate, 
           DateTime? nextAppointmentDate, DateTime? visitDate, string visitType, string reasonOfVisit, string adviseNotes)
        {
            this.Id = id;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.MiddleName = middleName;
            this.ClientPhoneNo= clientPhoneNo;
            this.DateOfBirth = dateOfBirth;
            this.PriorAppointmentDate = priorAppointmentDate;
            this.NextAppointmentDate = nextAppointmentDate;
            this.VisitDate = visitDate;
            this.VisitType = visitType;
            this.ReasonOfVisit = reasonOfVisit;
            this.AdviseNotes = adviseNotes;           
        }

       


        public long Id { get; set; }
        public long? ClientId { get; set; }
        public long VisitsId { get; set; }
        
        public long? FacilityId { get; set; }
        public long? ServicePointId { get; set; }

        public long? ServiceTypeId { get; set; }

        public string FullName {
            get
            {
                return FirstName + "" + LastName;
            }       
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ArtNo { get; set; }
        public string MiddleName { get; set; }
        public ClientModel ClientModel { get; set; }
        public FacilityModel FacilityModel { get; set; }
        public ServiceTypeModel ServiceTypeModel { get; set; }

        public string ClientPhoneNo { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public DateTime? VisitDate { get; set; }
        public string VisitType { get; set; } 
        public DateTime? PriorAppointmentDate { get; set; }
        public DateTime? AppointmentDate { get; set; }
        public DateTime? AppointmentTime { get; set; }
        public DateTime? NextAppointmentDate { get; set; }
        public string ReasonOfVisit { get; set; }
        public string AdviseNotes { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateEdited { get; set; }
        public long? EditedBy { get; set; }
        public long? CreatedBy { get; set; }
        public string FacilityName { get; set; }
        public string DistrictName { get; set; }
        public string ProvinceName { get; set; }
        public long? ProvinceId { get; set; }
        public long? DistrictId { get; set; }
        public int? Age { get; set; }
        public int? AppointmentStatus { get; set; }
        public DateTime? InteractionDate { get; set; }  
        public long AppointmentId { get; }
        public string Comment { get; set; }
        public DateTime GetCreatedDate()
        {
            return DateTime.Now;
        }


    }
   
}
