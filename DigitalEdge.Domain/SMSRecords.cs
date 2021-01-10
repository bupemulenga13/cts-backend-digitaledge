using System;

namespace DigitalEdge.Domain
{
   public class SMSRecords
    {
        public SMSRecords()
        {
        }
        public SMSRecords(long clientId, string firstName, string middleName, string lastName, long phoneNo, DateTime NextAppointmentDate,DateTime AppointmenDateTime)
        {
            this.ClientId = clientId;
            this.FirstName = firstName;
            this.MiddleName = middleName;
            this.LastName = lastName;
            this.PhoneNumber = phoneNo;
            this.NextAppointmentDate = NextAppointmentDate;
            this.AppointmenDateTime = AppointmenDateTime;
        }
        public long ClientId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string FullName
        {
            get { return FirstName + " " + MiddleName + " " + LastName; }
        }
        public long PhoneNumber { get; set; }
        public DateTime AppointmenDateTime { get; set; }
        public DateTime AppointmenTime { get; set; }
        public DateTime NextAppointmentDate { get; set; }
        public string FacilityName { get; set; }
        public long? FacilityId { get; set; }
        public string ServicePointName { get; set; }
        public long? ServicePointId { get; set; }
        public string Message { get; set; }
        public string FacilityContactNumber { get; set; }
    }
}
