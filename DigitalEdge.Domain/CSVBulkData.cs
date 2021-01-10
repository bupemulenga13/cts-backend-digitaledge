
using System;
using Microsoft.AspNetCore.Http;

namespace DigitalEdge.Domain
{
    public class CSVBulkData
    {
        public long? FacilityId { get; set; }
        public string Facility { get; set; }
        public string ServicePointName { get; set; }
        public long? ServicePointId { get; set; }
        public string AppointmentDate { get; set; }
        public string AppointmentTime { get; set; }
        public IFormFileCollection FileToUpload { get; set; }

        public string Message { get; set; }
    }
}
