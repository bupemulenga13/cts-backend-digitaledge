using DigitalEdge.Repository.Enitity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DigitalEdge.Repository
{
   public class Client
    {
        public Client()
        {
        }
        public Client(long id, string firstName, string lastName, string artNo, long sexId, long clientTypeId, long clientStatusId, long statusCommentId, long facilityId, DateTime dateOfBirth, int? age, DateTime enrollmentDate, 
            string clientPhoneNo, string alternativePhoneNumber1, bool verifiedByAnalyst, bool verifiedByStaff, string zone, string village, string houseNo, string location, string enrolledByPhone, long? servicePointId, long? languageId, string enrolledByName, string generalComment, int? enrollmentType, int? clientRelationship, bool accessToPhone, 
            int? harmonizedPhone, int? harmonizedAddress, DateTime dateCreated)
        {
            ClientId = id;
            FirstName = firstName;
            LastName = lastName;
            ArtNo = artNo;
            SexId = sexId;
            ClientTypeId = clientTypeId;
            ClientStatusId = clientStatusId;
            StatusCommentId = statusCommentId;
            FacilityId = facilityId;
            DateOfBirth = dateOfBirth;
            Age = age;
            EnrollmentDate = enrollmentDate;
            ClientPhoneNo = clientPhoneNo;
            AlternativePhoneNumber1 = alternativePhoneNumber1;
            PhoneVerifiedByAnalyst = verifiedByAnalyst;
            PhoneVerifiedByFacilityStaff = verifiedByStaff;
            Zone = zone;
            Village = village;
            HouseNo = houseNo;
            GISLocation = location;
            EnrolledByPhone = enrolledByPhone;
            ServicePointId = servicePointId;
            LanguageId = languageId;
            EnrolledByName = enrolledByName;
            GeneralComment = generalComment;
            EnrollmentType = enrollmentType;
            ClientRelationship = clientRelationship;
            AccessToPhone = accessToPhone;
            HamornizedMobilePhone = harmonizedPhone;
            HarmonizedPhysicalAddress = harmonizedAddress;
            DateCreated = dateCreated;            
        }

        public Client(long id, string firstName, string lastName, string artNo, long sexId, long clientTypeId, long clientStatusId, long statusCommentId, long facilityId, DateTime dateOfBirth, int? age, DateTime enrollmentDate,
            string clientPhoneNo, string alternativePhoneNumber1, bool verifiedByAnalyst, bool verifiedByStaff, string zone, string village, string houseNo, string location, string enrolledByPhone, long? servicePointId, long? languageId, string enrolledByName, string generalComment, int? enrollmentType, int? clientRelationship, bool accessToPhone,
            int? harmonizedPhone, int? harmonizedAddress, DateTime dateCreated, DateTime dateEdited)
        {
            ClientId = id;
            FirstName = firstName;
            LastName = lastName;
            ArtNo = artNo;
            SexId = sexId;
            ClientTypeId = clientTypeId;
            ClientStatusId = clientStatusId;
            StatusCommentId = statusCommentId;
            FacilityId = facilityId;
            DateOfBirth = dateOfBirth;
            Age = age;
            EnrollmentDate = enrollmentDate;
            ClientPhoneNo = clientPhoneNo;
            AlternativePhoneNumber1 = alternativePhoneNumber1;
            PhoneVerifiedByAnalyst = verifiedByAnalyst;
            PhoneVerifiedByFacilityStaff = verifiedByStaff;
            Zone = zone;
            Village = village;
            HouseNo = houseNo;
            GISLocation = location;
            EnrolledByPhone = enrolledByPhone;
            ServicePointId = servicePointId;
            LanguageId = languageId;
            EnrolledByName = enrolledByName;
            GeneralComment = generalComment;
            EnrollmentType = enrollmentType;
            ClientRelationship = clientRelationship;
            AccessToPhone = accessToPhone;
            HamornizedMobilePhone = harmonizedPhone;
            HarmonizedPhysicalAddress = harmonizedAddress;
            DateCreated = dateCreated;
            DateEdit = dateEdited;
        }        

        [Key]
        public long ClientId  { get; set; }

        [MaxLength(50)]
        public string FirstName { get; set; }

        [MaxLength(50)]
        public string MiddleName { get; set; }

        [MaxLength(50)]
        public string LastName { get; set; }

        public string ArtNo { get; set; }

        public string ClientPhoneNo { get; set; } 

        public  DateTime DateOfBirth { get; set; } 

        public int? Age { get; set; } 

        public DateTime? DateCreated { get; set; } 
        public DateTime? DateEdit { get; set; } 

        public long EditBy { get; set; } 
        public long CreatedBy { get; set; }


        //Physical Address
        public string Zone { get; set; }

        public string Village { get; set; }

        public string HouseNo { get; set; }

        public string GISLocation { get; set; }
        public string GeneralComment { get; set; }
        public string EnrolledByName { get; set; }
        public string AlternativePhoneNumber1 { get; set; }
        public bool PhoneVerifiedByAnalyst { get; set; }
        public bool PhoneVerifiedByFacilityStaff { get; set; }
        public DateTime EnrollmentDate { get; set; }
        public string EnrolledByPhone { get; set; }

        public long FacilityId { get; set; }

        [ForeignKey("FacilityId")]
        public virtual Facility Facilities { get; set; }

        public long ClientTypeId { get; set; }

        [ForeignKey("ClientTypeId")]
        public virtual ClientType ClientTypes { get; set; }

        public virtual Appointment ClientAppointments { get; set; }

        public virtual Visit ClientVisits { get; set; }

        public long? ServicePointId { get; set; }

        [ForeignKey("ServicePointId")]
        public virtual ServicePoint ServicePoints  { get; set; }

        public long? LanguageId { get; set; }

        [ForeignKey("LanguageId")]
        public virtual Language Languages { get; set; }

        public long ClientStatusId { get; set; }

        [ForeignKey("ClientStatusId")]
        public virtual ClientStatus ClientStatuses { get; set; }

        public long StatusCommentId { get; set; }

        [ForeignKey("StatusCommentId")]
        public virtual StatusComments StatusComments { get; set; }

        public long SexId { get; set; }

        [ForeignKey("SexId")]
        public virtual Sex Sex { get; set; }

        //New fields for live db
        public int? ClientRelationship { get; set; }

        public int? EnrollmentType { get; set; }

        public bool AccessToPhone { get; set; }

        public int? HamornizedMobilePhone { get; set; }

        public int? HarmonizedPhysicalAddress { get; set; }


        





    }

}
