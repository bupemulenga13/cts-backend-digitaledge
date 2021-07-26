using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using DigitalEdge.Services;
using DigitalEdge.Domain;
using Microsoft.AspNetCore.Authorization;
using DigitalEdge.Utility;

namespace DigitalEdge.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly IVisitService _visitService;
        private readonly IConfiguration _config;
        public AccountController(IAccountService accountService, IConfiguration config, IVisitService visitService)
        {
            this._accountService = accountService;
            this._visitService = visitService;
            this._config = config;
        }

        [HttpPost]
        [Route("Login")]
        [AllowAnonymous]
        public ActionResult Login([FromBody] UserModel model)
        {
            if (model == null)
                return BadRequest(new ServiceResponse() { StatusCode = 400 });
            var user = _accountService.ValidateUser(model.Email, model.Password);
            if (user == null)
                return NotFound(new ServiceResponse() { StatusCode = 404, Message = "User not found" });
            var token = _accountService.GetToken(user);
            return Ok(new { user, token });
        }

        [HttpPost]
        [Route("CreateAppointment")]
        [Authorize]
        public ActionResult CreateAppointment([FromBody] RegistrationModel model)
        {
            if (model == null)
            {
                return Ok(new ServiceResponse() { StatusCode = 400 });
            }
            var user = _accountService.ValidateClient(model);
            if (user.ArtNo != model.ArtNo)
            {
                return NotFound(new ServiceResponse() { Success = false, StatusCode = 400, Message = "Client not found, enroll client before creating appointment!" });

            }
            else
            {
                model.ClientId = (user.ClientId);
                string result = this._accountService.AddAppointment(model);
                if (result == "null")
                    return BadRequest(new ServiceResponse() { Success = false, StatusCode = 400, Message = "Appointment model empty" });
                else
                    return Ok(new ServiceResponse() { Success = true, StatusCode = 200, Message = "Client appointment successfully created!" });
            }
        }

        [HttpPost]
        [Route("CreateClient")]
        [Authorize]
        public ActionResult CreateClient([FromBody] RegistrationModel model)
        {
            if (model == null)
            {
                return BadRequest(new ServiceResponse() { StatusCode = 400, Message = "Enter valid client details!" });
            }

            else
            {
                var result = this._accountService.AddClient(model);
                if (result != null)
                {
                    return Ok(new ServiceResponse() { Success = true, StatusCode = 200, Message = "Client created successfully!" });

                }
                return BadRequest(new ServiceResponse() { Success = false, StatusCode = 400, Message = "Error: Client not saved" });
            }
        }


        [HttpPost]
        [Route("EditAppointment")]
        [Authorize]
        public ActionResult EditAppointment([FromBody] RegistrationModel model)
        {
            if (model == null)
            {
                return BadRequest(new ServiceResponse() { Success = false, StatusCode = 400, Message = "Error: Could not update appointment!" });
            }
            this._accountService.UpdateAppointment(model);
            return Ok(new ServiceResponse() { Success = true, StatusCode = 200, Message = "Appointment update successfull!" });

        }

        [HttpPost]
        [Route("EditClient")]
        [Authorize]
        public ActionResult EditClient([FromBody] RegistrationModel model)
        {
            if (model == null)
            {
                return BadRequest(new ServiceResponse() { Success = false, StatusCode = 400, Message = "Edit client failed!" });
            }
            this._accountService.UpdateClient(model);
            return Ok(new ServiceResponse() { Success = true, StatusCode = 200, Message = "Client details successfully updated!" });
        }




        [HttpGet]
        [Route("GetData")]
        [Authorize]
        public ActionResult GetData()
        {
            var user = _accountService.GetData();
            return Ok(user);
        }
        
        [HttpGet]
        [Route("GetUsersByFacility/{facilityId}")]
        [Authorize]
        public ActionResult GetUsersByFacility(long facilityId)
        {
            var user = _accountService.GetUsersByFacility(facilityId);
            return Ok(user);
        }

        [HttpGet]
        [Route("GetData/{id}")]
        [Authorize]
        public ActionResult GetData(long id)
        {
            var user = _accountService.GetData(id);
            return Ok(user);
        }
        [HttpGet]
        [Route("GetAppointment/{id}")]
        [Authorize]
        public ActionResult GetAppointment(long id)
        {
            var user = _accountService.GetAppointment(id);
            return Ok(user);
        }

        [HttpPost]
        [Route("Create")]
        [Authorize]
        public ActionResult Create([FromBody] UserModel user)
        {
            if (user == null)
            {
                return BadRequest(new ServiceResponse() { Success = false, StatusCode = 400, Message = "Error: Failed to add a user! " });
            }
            this._accountService.AddUser(user);
            return Ok(new ServiceResponse() { Success = true, StatusCode = 200, Message = "User successfully created" });
        }

        [HttpPost]
        [Route("AddViralLoadResult")]
        [Authorize]
        public ActionResult AddViralLoadResult([FromBody] ViralLoadModel addVLresult)
        {
            if (addVLresult == null)
            {
                return BadRequest(new ServiceResponse() { Success = false, StatusCode = 400, Message = "Error: Failed to add viral load result" });
            }
            this._accountService.AddViralLoad(addVLresult);
            return Ok(new ServiceResponse() { Success = true, StatusCode = 200, Message = "Viral Load result successfully added" });
        }

        [HttpPut]
        [Route("Edit")]
        [Authorize]
        public ActionResult Edit([FromBody] UserModel updateuser)
        {
            if (updateuser == null)
            {
                return BadRequest(new ServiceResponse() { Success = false, StatusCode = 400, Message = "Error: Failed to update a user details! " });
            }
            this._accountService.UpdateUser(updateuser);
            return Ok(new ServiceResponse() { Success = true, StatusCode = 200, Message = "User details successfully updated!" });
        }

        [HttpPost]
        [Route("Delete")]
        [Authorize]
        public ActionResult Delete([FromBody] UserModel deleteuser)
        {
            this._accountService.UpdateUser(deleteuser);
            return Ok(new ServiceResponse() { Success = true, StatusCode = 200, Message = "User successfully deleted" });
        }

        [HttpGet]
        [Route("GetRoles")]
        [Authorize]
        public ActionResult GetRoles()
        {
            var userroles = _accountService.getRoles();
            return Ok(userroles);
        }

        [HttpPost]
        [Route("CreateFacility")]
        [Authorize]
        public ActionResult CreateFacility(UserBindingModel userFacility)
        {
            string resultdata = this._accountService.CreateFacility(userFacility);
            if (resultdata == "null")
                return BadRequest(new ServiceResponse() { Success = false, StatusCode = 400, Message = "Error: Failed to create facility" });
            else
                return Ok(new ServiceResponse() { Success = true, StatusCode = 200, Message = "Facility successfully created!" });

        }
        [HttpPost]
        [Route("ServicePointCreate")]
        [Authorize]
        public ActionResult ServicePointCreate(ServicePointModel servicePoint)
        {
            string resultdata = this._accountService.AddServicePoint(servicePoint);
            if (resultdata == "null")
                return BadRequest(new ServiceResponse() { Success = false, Message = "Failed to create service point", StatusCode = 500 });
            else
                return Ok(new ServiceResponse() { Success = true, StatusCode = 200 });

        }
        [HttpGet]
        [Route("GetFacilityData")]
        [Authorize]
        public ActionResult GetFacilityData()
        {
            var user = _accountService.getFacilityData();
            return Ok(user);
        }

        [HttpGet]
        [Route("GetFacilityUserData")]
        [Authorize]
        public ActionResult GetFacilityUserData()
        {
            var user = _accountService.getFacilityUserData();
            return Ok(user);
        }
        [HttpGet]
        [Route("GetServiceData")]
        [Authorize]
        public ActionResult GetServiceData()
        {
            var user = _accountService.getServiceData();
            return Ok(user);
        }
        [HttpPost]
        [Route("DeleteFacility")]
        [Authorize]
        public ActionResult DeleteFacility([FromBody] UserBindingModel deleteuser)
        {
            this._accountService.UpdateFacilityUser(deleteuser);

            return Ok(new ServiceResponse() { Success = true, StatusCode = 200 });
        }
        [HttpPost]
        [Route("ServicePointUpdate")]
        [Authorize]
        public ActionResult ServicePointUpdate([FromBody] ServicePointModel updateservice)
        {
            this._accountService.UpdateServicePoint(updateservice);

            return Ok(new ServiceResponse() { Success = true, StatusCode = 200 });
        }

        [HttpPost]
        [Route("UpdatefacilityUser")]
        [Authorize]
        public ActionResult UpdatefacilityUser([FromBody] FacilityModel facilityModel)
        {

            if (facilityModel == null)
            {
                return BadRequest(new ServiceResponse() { Success = false, StatusCode = 400, Message = "Error: Update operation failed" });
            }
            this._accountService.UpdateFacility(facilityModel);
            return Ok(new ServiceResponse() { Success = true, StatusCode = 200, Message = "Facility details updated successfully!" });
        }

        [HttpGet]
        [Route("CountUsers")]
        [Authorize]
        public int CountUsers()
        {
            return _accountService.CountUsers();
        }
        
        [HttpGet]
        [Route("CountUsersInFacility")]
        [Authorize]
        public int CountUsersInFacility(long facilityId)
        {
            return _accountService.CountUsersInFacility(facilityId);
        }

        [HttpGet]
        [Route("ActiveUsers")]
        [Authorize]
        public int ActiveUsers()
        {
            return _accountService.ActiveUsers();
        }



    }
}