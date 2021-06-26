using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace DigitalEdge.Domain
{
    public class RegistrationModel
    {

        #region Client Properties 
public long ClientId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string ClientPhoneNo { get; set; }
        public string DateOfBirth { get; set; }
        public int? Age { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateEdit { get; set; }
        public long CreatedBy { get; set; }
        public string PhysicalAddress { 
            get 
            {
                if (Zone == null)
                {
                    return "*" + "," + Village + "," + HouseNo + "," + GISLocation;
                }
                else if (Village == null)
                {
                    return Zone + "," + "*" + "," + HouseNo + "," + GISLocation;
                }
                else if (HouseNo == null)
                {
                return Zone + "," + Village + "," + "*" + "," + GISLocation;
                    
                }
                else if (GISLocation == null)
                {
                    return Zone + "," + Village + "," + HouseNo + "," + "*";
                }
                else if (Zone == null && Village == null && HouseNo == null && GISLocation == null)
                {
                    return "*" + "," + "*" + "," + "*" + "," + "*";
                }
                else
                {
                    return Zone + "," + Village + "," + HouseNo + "," + GISLocation;
                }
            }    
        }
        public string GeneralComment { get; set; }
        public string EnrolledByName { get; set; }
        public string AlternativePhoneNumber1 { get; set; }
        public bool PhoneVerifiedByAnalyst { get; set; }
        public bool PhoneVerifiedByFacilityStaff { get; set; }
        public string EnrollmentDate { get; set; }
        public string EnrolledByPhone { get; set; }
        public string ArtNo { get; set; }
        public long? LanguageId { get; set; }
        public long ClientTypeId { get; set; }
        public long StatusCommentId { get; set; }
        public long SexId { get; set; }
        public long ClientStatusId { get; set; }
        public int? ClientRelationship { get; set; }
        public int? EnrollmentType { get; set; }

        public bool AccessToPhone { get; set; }

        public int? HamornizedMobilePhone { get; set; }

        public int? HarmonizedPhysicalAddress { get; set; }

        //Physical Address Fields
        public string Zone { get; set; }

        public string Village { get; set; }

        public string HouseNo { get; set; }

        public string GISLocation { get; set; }




        #endregion

        #region Appointment Properties 
        public string AppointmentDate { get; set; }
        public string AppointmentTime { get; set; }
        public int AppointmentStatus { get; set; }
        public string InteractionDate { get; set; }
        public string InteractionTime { get; set; }
        public DateTime DateEdited { get; set; }
        public long EditedBy { get; set; }

        public long FacilityId { get; set; }      

        public long? ServicePointId { get; set; }

        public long ServiceTypeId { get; set; }

        // Newly added properties from CTS
        
        public long AppointmentId { get; set; }

        public string Comment { get; set; }

        #endregion            

        #region Method Extras

        public DateTime GetCreatedDate()
        {

            return DateTime.Now;
        }

        public DateTime GetBirthDate()
        {
            return DateTime.Parse(string.Format("{0}", DateOfBirth));
        }

        public DateTime GetEnrollmentDate()
        {
            return DateTime.Parse(string.Format("{0}", EnrollmentDate));
        }

        public DateTime GetAppointmentDateAndTime()
        {
            return DateTime.Parse(string.Format("{0} {1}", AppointmentDate, AppointmentTime));
        }
        public DateTime GetInteractionDateAndTime()
        {
            return DateTime.Parse(string.Format("{0} {1}", InteractionDate, InteractionTime));
        }

        public DateTime GetPriorAppointmentDate() { 
        
            return DateTime.Parse(string.Format("{0}", AppointmentDate));

        }

        public DateTime GetDateCreated() { return DateTime.Now; }

        public DateTime GetDateEdited() {

            DateTime today = DateTime.Today.Date;

            DateTime createdDate = DateCreated.Date;

            var editedDate = createdDate - today;
               
            return Convert.ToDateTime(editedDate);

        }

        public int CalculateAge()
        {            
                var today = DateTime.Today;

                var dob = Convert.ToDateTime(DateOfBirth);

                var age = today.Year - dob.Year;

                if (dob > today.AddYears(-age)) age--;
                return age;
        }

        public int CalculateDaysLate()
        {
            var today = DateTime.Today;

            var appointmentDate = Convert.ToDateTime(AppointmentDate);

            var noDaysLate = today.Year - appointmentDate.Year;

            return noDaysLate;
        }
        #endregion

    }
}
