using System;
using System.Collections.Generic;
using System.Text;

namespace DigitalEdge.Domain
{
    public class FacilityTypeModel
    { 
        public FacilityTypeModel() { }
        public FacilityTypeModel(long facilityTypeId, string facilityTypeName)
        {
            this.FacilityTypeId = facilityTypeId;
            this.FacilityTypeName = facilityTypeName;
        }

        public long FacilityTypeId { get; set; }
        public string FacilityTypeName { get; set; }


    }
        
}
