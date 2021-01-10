using System;

namespace DigitalEdge.Domain
{
 public class AppointmentsModel
    {
        public AppointmentsModel()
        {
        }

            public AppointmentsModel(long id,long clientId,long visitsId,string firstName, string lastName, string middleName, DateTime? priorAppointmentDate,DateTime appointmentDate,
                DateTime appointmentTime, DateTime? nextAppointmentDate , long age)
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
        public AppointmentsModel(long id, string firstName, string lastName, string middleName, long clientPhoneNo,DateTime dateOfBirth, DateTime? priorAppointmentDate, 
           DateTime? nextAppointmentDate, DateTime visitDate, DateTime visitType, string reasonOfVisit, string adviseNotes)
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

        public AppointmentsModel(long id, long ClientId,long FacilityId, long ServicePointID, string firstName, string lastName, string middleName,
            long clientPhoneNo, DateTime dateOfBirth, DateTime? priorAppointmentDate,
           DateTime? nextAppointmentDate, DateTime visitDate, DateTime visitType, string reasonOfVisit, string adviseNotes)
        {
            this.Id = id;
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
        }


        public long Id { get; set; }
        public long ClientId { get; set; }
        public long VisitsId { get; set; }
        
        public long? FacilityId { get; set; }
        public long? ServicePointId { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string FullName
        {
            get { return FirstName + " " + MiddleName + " " + LastName; }
        }

        public ClientModel clientModel { get; set; }
        public FacilityModel FacilityModel { get; set; }
        public ServicePointModel ServicePointModel { get; set; }
        public long ClientPhoneNo { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime VisitDate { get; set; }
        public DateTime VisitType { get; set; } 
        public DateTime? PriorAppointmentDate { get; set; }
        public DateTime AppointmentDate { get; set; }
        public DateTime AppointmentTime { get; set; }
        public DateTime? NextAppointmentDate { get; set; }
        public string ReasonOfVisit { get; set; }
        public string AdviseNotes { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateEdited { get; set; }
        public long EditedBy { get; set; }
        public long CreatedBy { get; set; }
        public string BuldingName { get; set; }
        public string DistrictName { get; set; }
        public string ProvinceName { get; set; }
        public long ProvinceId { get; set; }
        public long DistrictId { get; set; }
        public long Age { get; set; }      


    }
   
}
