using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace DigitalEdge.Domain
{
    public class RegistrationModel
    {
        public long ClientId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string PhoneNo { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int Age { get; set; }
        public int CurrentAge { get; set; }
        public string NextOfKinName { get; set; }
        public long NextOfKinContact { get; set; }
        public long NextOfClientID { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateEdit { get; set; }
        public long EditBy { get; set; }
        public long CreatedBy { get; set; }
        public DateTime? PriorAppointmentDate { get; set; }
        public string AppointmentDate { get; set; }
        public DateTime AppointmentTime { get; set; }
        public DateTime? NextAppointmentDate { get; set; }
        public string ReasonOfVisit { get; set; }
        public string AdviseNotes { get; set; }
        public DateTime DateEdited { get; set; }
        public long EditedBy { get; set; }

        public long Id { get; set; }
        public long FacilityId { get; set; }

        

        public long ServicePointId { get; set; }

        // Newly added properties from CTS
        public string  Address { get; set; }
        public string GeneralComment { get; set; }
        public string EnrolledBy { get; set; }
        public string AlternativePhoneNumber1 { get; set; }
        public string AlternativePhoneNumber2 { get; set; }
        public bool PhoneVerifiedByAnalyst { get; set; }
        public bool PhoneVerifiedByFacilityStaff { get; set; }
        public DateTime EnrollmentDate { get; set; }
        public string EnrolledByPhone { get; set; }

        public string ArtNo { get; set; }

        public long LanguageId { get; set; }

        public long ClientTypeId { get; set; }

        public long StatusCommentId { get; set; }

        public long SexId { get; set; }

        public long ClientStatusId { get; set; }




        // Section for drop down listsd




    }
}
