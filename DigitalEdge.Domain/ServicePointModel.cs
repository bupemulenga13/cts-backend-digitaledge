using System;

namespace DigitalEdge.Domain
{
   public class ServicePointModel
    {
        public ServicePointModel()
        {
           
        }

        public ServicePointModel(long servicePointId, string servicePointName)
        {
            this.ServicePointId = servicePointId;
            this.ServicePointName = servicePointName;
        }
        //public ServicePointModel(string servicePointName, long? facilityId)
        //{
        //    this.ServicePointName = servicePointName;
        //    this.FacilityId = facilityId;
        //}
        //public ServicePointModel(string facilityName, string ServiceName, long servicepointId)
        //{
        //    this.FacilityName = facilityName;
        //    this.ServicePointName = ServiceName;
        //    this.ServicePointId = servicepointId;
        //}

        public long ServicePointId { get; set; }
        public long AssignedServicePointId { get; set; }
        public string ServicePointName { get; set; }
        public long FacilityId { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateEdited { get; set; }
        public long EditedBy { get; set; }
        public long CreatedBy { get; set; }
        public string FacilityName { get; set; }
    }
}
