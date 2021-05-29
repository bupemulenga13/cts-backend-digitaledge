using System;
using System.Collections.Generic;
using System.Text;



namespace DigitalEdge.Domain
{
   public class ClientModel
    {
        public ClientModel() { 
        
        }
        public ClientModel(long clientId , string firstName , string lastName, string artNo) {
            this.ClientId = clientId;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.ArtNo = artNo;
        } 
       
        public long ClientId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string ClientPhoneNo { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int? Age { get; set; }
        public int CurrentAge { get; set; }
        public string NextOfKinName { get; set; }
        public long NextOfKinContact { get; set; }
        public long NextOfClientID { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateEdit { get; set; }
        public long EditBy { get; set; }
        public long CreatedBy { get; set; }
        public string ArtNo { get; set; }
        public string Address { get; set; }
        public string GeneralComment { get; set; }
        public string EnrolledBy { get; set; }
        public string AlternativePhoneNumber1 { get; set; }
        public string AlternativePhoneNumber2 { get; set; }
        public bool PhoneVerifiedByAnalyst { get; set; }
        public bool PhoneVerifiedByFacilityStaff { get; set; }
        public DateTime EnrollmentDate { get; set; }
        public string EnrolledByPhone { get; set; }

        public long ClientTypeId { get; set; }
        public string ClientType { get; set; }

        public long FacilityId { get; set; }
        public string Facility { get; set; }
     
        public long ClientStatusId { get; set; }
        public string ClientStatus { get; set; }

        public long StatusCommentId { get; set; }
        public string StatusComment { get; set; }

        public long? ServicePointId { get; set; }
        public string ServicePoint { get; set; }

        public long? LanguageId { get; set; }
        public string Language { get; set; }

        public long SexId { get; set; }
        public string Sex { get; set; }

        





    }
}
