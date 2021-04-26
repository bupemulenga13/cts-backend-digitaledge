 using System;
using System.Data;
using System.IO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using DigitalEdge.Domain;
using DigitalEdge.Services;
using DigitalEdge.Utility;

namespace DigitalEdge.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class VisitController : ControllerBase
    {
        private readonly IVisitService _visitService;
        private readonly ISMSSchedulerService _smsSchedulerService;
        private readonly IConfiguration _config;
        public VisitController(IVisitService visitService, IConfiguration config, ISMSSchedulerService smsSchedulerService)
        {
            this._smsSchedulerService = smsSchedulerService;
            this._visitService = visitService;
            this._config = config;
        }
        [HttpPost]
        [Route("[action]")]
        [Authorize]
        public IActionResult ImportCSVData([FromForm]FormFileData file)
        {
            {
                if (file.FileToUpload[0].FileName == null)
                    return BadRequest();
                string extension = Path.GetExtension(file.FileToUpload[0].FileName);
                DataTable dataTable = null;
                if (extension != ".csv")
                    return StatusCode(400, "Please verify .CSV sheet and upload again.");
                dataTable = DataImporter.CSVDataImport(file.FileToUpload[0]);
                if (dataTable.Rows.Count == 0)
                    return StatusCode(400, "Column heading is missing for any column. Please verify sheet and upload again.");
                var response = _visitService.CSVImportFile(dataTable);
                return Ok(response);
            }
        }
        [HttpPost]
        [Route("CreateVisit")]
        [AllowAnonymous]
        public ActionResult CreateVisit([FromBody] VisitModel model)
        {
            if (model == null)
            {
                return Ok(new ServiceResponse() { StatusCode = 400 });
            }
            else
            {
                var result = this._visitService.AddVisit(model);
                if (result != null)
                {
                    return Ok(new ServiceResponse() { Success = true, StatusCode = 200, Message = "Visit created successfully!" });

                }
                return Ok(new ServiceResponse() { Success = true, StatusCode = 400, Message = "Error: Visit not created" });
            }
        }
        [HttpGet]
        [Route("GetAppointmentsDetails")]
        [Authorize]
        public ActionResult GetAppointmentsDetails()
        {
            var user = _visitService.getAppointmentsDetails();
            return Ok(user);
        }

        [HttpGet]
        [Route("GetAppointments")]
        [Authorize]
        public ActionResult GetAppointments()
        {
            var appointments = _visitService.GetAppointments();
            return Ok(appointments);
        }
        [HttpPost]
        [Route("GetAppointmentsCheck")]
        [Authorize]
        public IActionResult GetAppointmentsCheck([FromBody]AppointmentsModel model)
        {
            var user = _visitService.getAppointmentCheck(model);
            return Ok(user);
        }
        [HttpGet]
        [Route("GetAppointmentsDetailsMissed")]
        [Authorize]
        public ActionResult GetAppointmentsDetailsMissed()
        {
            var user = _visitService.getAppointmentsDetailsMissed();
            return Ok(user);
        }
        [HttpPost]
        [Route("ViewAppointmentsMissedFilter")]
        [Authorize]
        public IActionResult ViewAppointmentsMissedFilter([FromBody]VisitsModel data)
        {
            if (data.ServicePointId > 1)
            {
                var userfilter = _visitService.getAppointmentsMissedFilter(data);
                return Ok(userfilter);
            }
            else {
                var user = _visitService.getAppointmentsDetailsMissed();
                return Ok(user);
            }
        }
        [HttpGet]
        [Route("GetUpcommingVisitsDetails")]
        [Authorize]
        public ActionResult GetUpcommingVisitsDetails()
        {
            var user = _visitService.getUpcommingVisitsDetails();
            return Ok(user);
        }  
        [HttpPost]
        [Route("ViewUpcommingVisits")]
        [Authorize]
        public IActionResult ViewUpcommingVisits(VisitsModel data)
        {
            if (data.ServicePointId > 1)
            {
                var user = _visitService.getUpcommingVisitsDetails(data);
                return Ok(user);
            }
            else {
                var user = _visitService.getUpcommingVisitsDetails();
                return Ok(user);
            }
        }
        [HttpGet]
        [Route("GetVisitHistory")]
        [Authorize]
        public ActionResult GetVisitHistory()
        {
            var user = _visitService.getVisitHistory();
            return Ok(user);
        }
        [HttpPost]
        [Route("GetVisitHistoryByServicePoint")]
        [Authorize]
        public IActionResult GetVisitHistoryByServicePoint(VisitsModel data)
        {
            if (data.ServicePointId > 1)
            {
                var user = _visitService.getVisitHistoryByServicePoint(data);
                return Ok(user);
            }
            else {
                var user = _visitService.getUpcommingVisitsDetails();
                return Ok(user);
            }
        }
        [HttpGet]
        [Route("GetMissedVisitsDetails")]
        [Authorize]
        public ActionResult GetMissedVisitsDetails()
        {
            var user = _visitService.getMissedVisitsDetails();
            return Ok(user);
        }
        [HttpPost]
        [Route("ViewVisitsMissedFilter")]
        [Authorize]
        public IActionResult ViewVisitsMissedFilter([FromBody]VisitsModel data)
        {
            if (data.ServicePointId > 1)
            {
                var user = _visitService.getVisitsMissedFilter(data);
                return Ok(user);
            }
            else {
                var user = _visitService.getMissedVisitsDetails();
                return Ok(user);
            }           
        }
        [HttpGet]
        [Route("ViewClientDetails")]
        [Authorize]
        public ActionResult ViewClientDetails()
        {
            var user = _visitService.getClientDetails();
            return Ok(user);
        }
        [HttpPost]
        [Route("GetClientDetails")]
        [Authorize]
        public IActionResult GetClientDetails()
        {
            var user = _visitService.getClientDetails();
            return Ok(user);
        }

        [HttpPost]
        [Route("GetClientInfo")]
        [Authorize]
        public IActionResult GetClientInfo([FromForm]ClientModel model)
        {
            
            if (model.FirstName != null && model.FirstName == model.FirstName)
            {
                var userA = _visitService.getClientDetails(model.FirstName);
                return Ok(userA);
            }
            if (model.LastName != null && model.LastName == model.LastName){

                var userB = _visitService.getClientDetails(model.LastName);
                return Ok(userB);
            }
            if (model.ArtNo != null && model.ArtNo == model.ArtNo)
            {
                var user = _visitService.getClientDetails(model.ArtNo);
                return Ok(user);
            }
            
                return StatusCode(404, "Please enter valid client details!");


         }

        [HttpGet]
        [Route("GetClients")]
        [Authorize]
        public ActionResult GetClients()
        {
            var clients = _visitService.getClients();
            return Ok(clients);
        }
        
        [HttpGet]
        [Route("GetFacilityTypes")]
        [Authorize]
        public ActionResult GetFacilityTypes()
        {
            var clients = _visitService.GetFacilityTypes();

            return Ok(clients);
        }
        
        [HttpGet]
        [Route("GetFacilities")]
        [Authorize]
        public ActionResult GetFacilities()
        {
            var facilities = _visitService.GetFacilities();

            return Ok(facilities);
        }
        
        [HttpGet]
        [Route("GetServiceTypes")]
        [Authorize]
        public ActionResult GetServiceTypes()
        {   
            var serviceTypes = _visitService.GetServiceTypes();

            return Ok(serviceTypes);
        }
        
        [HttpGet]
        [Route("GetServicePoints")]
        [Authorize]
        public ActionResult GetServicePoints()
        {
            var servicePoints = _visitService.GetServicePoints();

            return Ok(servicePoints);
        }
        
        [HttpGet]
        [Route("GetVisits")]
        [Authorize]
        public ActionResult GetVisits()
        {
            var visits = _visitService.GetVisits();

            return Ok(visits);
        }


        [HttpGet]
        [Route("GetClientById")]
        [Authorize]
        public ActionResult GetClientById(long id)
        {
            var client = _visitService.GetClientById(id);

            return Ok(client);
        }
        
        [HttpGet]
        [Route("GetAppointment")]
        [Authorize]
        public ActionResult GetAppointment(long id)
        {
            var client = _visitService.GetAppointmentById(id);


            return Ok(client);
        }
        
        [HttpGet]
        [Route("GetVisit")]
        [Authorize]
        public ActionResult GetVisit(long id)
        {
            var visit = _visitService.GetVisitById(id);

            return Ok(visit);
        }

        [HttpPost]
        [Route("ViewClientDetailsFilters")]
        [Authorize]
        public IActionResult ViewClientDetailsFilters([FromBody]VisitsModel data)
        { 
            var userfilter = _visitService.getClientDetailsFilters(data);
                return Ok(userfilter);
            
        }
        [HttpPost]
        [Route("ViewActiveClientFilter")]
        [Authorize]
        public IActionResult ViewActiveClientFilter([FromBody]VisitsModel data)
        {
            var userfilter = _visitService.getActiveClientFilter(data);
            return Ok(userfilter);
        }
        [HttpPost]
        [Route("ViewUpcommingAppointment")]
        [Authorize]
        public IActionResult ViewUpcommingAppointment([FromBody]VisitsModel data)
        {
            if (data.ServicePointId > 1)
            {
                var userfilter = _visitService.getUpcommingAppointment(data);
                return Ok(userfilter);
            }
            else
            {
                var user = _visitService.getAppointmentsDetails();
                return Ok(user);
            }
        }

        [HttpGet]
        [Route("ClientVisitPastDetails")]
        [Authorize]
        public ActionResult ClientVisitPastDetails()
        {
            var user = _visitService.getClientVisitPastDetails();
            return Ok(user);
        }       
        [HttpGet]
        [Route("SMSSendScheduler")]
        [AllowAnonymous]
        public ActionResult SMSSendScheduler()
        {
            var user = _smsSchedulerService.ClientsList();
            return Ok(user);
        }
        [HttpGet]
        [Route("ViewDetails/{id}")]
        [Authorize]
        public ActionResult ViewDetails(long id)
        {
            var user = _visitService.viewDetails(id);
            return Ok(user);
        }
        [HttpPost]
        [Route("SendReminder")]
        [Authorize]
        public ActionResult SendReminder(AppointmentsModel appointmentsModel)
        {
            string user = string.Empty;
            if (appointmentsModel.VisitsId == 0)
                user = _visitService.smsRecords(appointmentsModel.Id, true);
            else
                user = _visitService.smsRecords(appointmentsModel.VisitsId, false);
            return Ok(new ServiceResponse() { StatusCode = 200, Message = user });
        }
        [HttpGet]
        [Route("GetFacility")]
        [Authorize]
        public ActionResult GetFacility(long id)
        {
            return Ok(_visitService.GetFacility(id));
        }

        
        


        [HttpGet]
        [Route("GetDistrict/{id}")]
        [Authorize]
        public ActionResult GetDistrict(long id)
        {
            return Ok(_visitService.getDistrict(id));
        }
        [HttpGet]
        [Route("GetProvince")]
        [Authorize]
        public ActionResult GetProvince()
        {
            return Ok(_visitService.GetProvince());
        }
        [HttpGet]
        [Route("GetServicePoint/{id}")]
        [Authorize]
        public ActionResult GetServicePoint(long id)
        {
            return Ok(_visitService.GetServicePoint(id));
        }
        [HttpPost]
        [Route("GetMessage")]
        [Authorize]
        public ActionResult GetMessage(MessageTemplateModel messageTemplateModel)
        {
            if (messageTemplateModel == null)
                return null;
            return Ok(_visitService.GetMessage(messageTemplateModel));
        }
        [HttpPost]
        [Route("GetBulkMessage")]
        [Authorize]
        public ActionResult GetBulkMessage([FromForm]CSVBulkData bulkData)
        {
            if (bulkData.FileToUpload == null)
                return Ok(new ServiceResponse() { StatusCode = 400, Message = "Please Upload CSV" });
            string extension = Path.GetExtension(bulkData.FileToUpload[0].FileName);
            DataTable dataTable = null;
            if (extension != ".csv")
                return StatusCode(400, "Please verify .CSV sheet and upload again.");
            dataTable = DataImporter.CSVDataImport(bulkData.FileToUpload[0]);
            if (dataTable.Rows.Count == 0)
                return StatusCode(400, "Column heading is missing for any column. Please verify sheet and upload again.");
            var response = _visitService.CSVImportFile(dataTable);
            var result = _visitService.SendBulkSMS(response, bulkData);
            return Ok(new ServiceResponse() { StatusCode = 200, Message = result });
        }
        [HttpPost]
        [Route("DeleteFacility")]
        [Authorize]
        public ActionResult DeleteFacility(FacilityModel facilityModel)
        {
            string isfacility = "Facility  ";
            try
            {
                if (facilityModel.AssignedFacilityId == 0 || facilityModel.FacilityId == 0)
                    return null;
                _visitService.DeleteFacility(facilityModel , isfacility);
                return Ok(new ServiceResponse() { StatusCode = 200, Message = "Delete Successfully" });
            }
            catch (Exception ex)
            {
                return Ok(new ServiceResponse() { StatusCode = 500, Message = ex.Message.ToString() });
            }

        }
        [HttpPost]
        [Route("DeleteServicePoint")]
        [Authorize]
        public ActionResult DeleteServicePoint(ServicePointModel servicePoint)
        {
            string isservicePoint="servicepoint";
            try
            {
                if (servicePoint.AssignedServicePointId == 0 || servicePoint.ServicePointId == 0)
                    return null;
                _visitService.DeleteServicePoint(servicePoint , isservicePoint);
                return Ok(new ServiceResponse() { StatusCode = 200, Message = "Delete Successfully" });
            }
            catch (Exception ex)
            {
                return Ok(new ServiceResponse() { StatusCode = 500, Message = ex.Message.ToString() });
            }

        }
    }
}
