using System;
using System.Collections.Generic;
using System.Text;

namespace DigitalEdge.Domain
{
    public class SearchModel
    {
        public SearchModel()
        {
               
        }

        #region ClientProperties
        public long ClientId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ArtNo { get; set; }
        public string ClientPhoneNo { get; set; }
        public DateTime? EnrollmentDate { get; set; }


        #endregion

        #region AppointmentProperties
        public long AppointmentId { get; set; }
        public string AppointmentDate { get; set; }
        public string AppointmentTime { get; set; }
        public int AppointmentStatus { get; set; }
        public string InteractionDate { get; set; }
        public string InteractionTime { get; set; }        
        public long ServiceTypeId { get; set; }
        public long? FacilityId { get; set; }
        public string Comment { get; set; }

        public string ServiceTypeName { get; set; }
        public string FacilityName { get; set; }



        #endregion

        #region SystemProperties
        public DateTime DateCreated { get; set; }

        public DateTime DateEdited { get; set; }
        public long CreatedBy { get; set; }
        public long EditedBy { get; set; }

        #endregion

        #region Method Extras
        public DateTime GetInteractionDateandTime()
        {
            return DateTime.Parse(string.Format("{0} {1}", InteractionDate, InteractionTime));
        }
        #endregion


    }
}
