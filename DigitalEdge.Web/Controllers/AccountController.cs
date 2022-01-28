namespace DigitalEdge.Web.Controllers
{
    using DigitalEdge.Domain;
    using DigitalEdge.Services;
    using DigitalEdge.Utility;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using System;

    /// <summary>
    /// Defines the <see cref="AccountController" />.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]

    public class AccountController : ControllerBase
    {
        /// <summary>
        /// Defines the _accountService.
        /// </summary>
        private readonly IAccountService _accountService;

        /// <summary>
        /// Defines the _visitService.
        /// </summary>
        private readonly IVisitService _visitService;

        /// <summary>
        /// Defines the _config.
        /// </summary>
        private readonly IConfiguration _config;

        /// <summary>
        /// Initializes a new instance of the <see cref="AccountController"/> class.
        /// </summary>
        /// <param name="accountService">The accountService<see cref="IAccountService"/>.</param>
        /// <param name="config">The config<see cref="IConfiguration"/>.</param>
        /// <param name="visitService">The visitService<see cref="IVisitService"/>.</param>
        public AccountController(IAccountService accountService, IConfiguration config, IVisitService visitService)
        {
            this._accountService = accountService;
            this._visitService = visitService;
            this._config = config;
        }

        /// <summary>
        /// The Login.
        /// </summary>
        /// <param name="model">The model<see cref="UserModel"/>.</param>
        /// <returns>The <see cref="ActionResult"/>.</returns>
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

        /// <summary>
        /// The CreateAppointment.
        /// </summary>
        /// <param name="model">The model<see cref="RegistrationModel"/>.</param>
        /// <returns>The <see cref="ActionResult"/>.</returns>
        [HttpPost]
        [Route("CreateAppointment")]
        [Authorize]
        public ActionResult CreateAppointment([FromBody] RegistrationModel model)
        {
            var newAppoinmentResponse = Ok(new ServiceResponse() { StatusCode = 400 });
            var appointmentIsValid = true;
            if (model == null)
            {
                return newAppoinmentResponse;
            }

            var user = _accountService.ValidateClient(model);
            if (user.ArtNo != model.ArtNo)
            {
                return NotFound(new ServiceResponse()
                {
                    Success = false,
                    StatusCode = 400,
                    Message = "Client not found, enroll client before creating appointment!"
                });
            }
            else
            {
                var appointments = _visitService.getAppointmentsDetailsByClientId(model);
                var dtFormat = string.Format("{0} {1}", model.AppointmentDate, model.AppointmentTime);
                DateTime appDate = DateTime.Parse(dtFormat);
                foreach (var appointment in appointments)
                {
                    // for threading sake, only appointments occuring on this day will be checked first
                    if (appDate == appointment.AppointmentDate)
                    {
                        
                        if(model.AppointmentStatus == appointment.AppointmentStatus && model.ServiceTypeId == appointment.ServiceTypeId)
                        {
                            appointmentIsValid = false;
                        }
                    }
                }
            }
            if (appointmentIsValid == true)
            {
                // safe to create new appointment 
                model.ClientId = (user.ClientId);
                string result = this._accountService.AddAppointment(model);
                if (result == "null")
                    return BadRequest(new ServiceResponse()
                    { Success = false, StatusCode = 400, Message = "Appointment model empty" });
                else
                    return Ok(new ServiceResponse()
                    { Success = true, StatusCode = 200, Message = "Client appointment successfully created!" });
            }
            else
            {
                return BadRequest(new ServiceResponse()
                {
                    Success = false,
                    StatusCode = 401,
                    Message = "Appointment for this service and date already exists."
                });
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
            this._accountService.EditAppointment(model);
            return Ok(new ServiceResponse() { Success = true, StatusCode = 200, Message = "Appoitnment updated successfully!" });

        }

        /// <summary>
        /// The AddAttendance.
        /// </summary>
        /// <param name="model">The model<see cref="RegistrationModel"/>.</param>
        /// <returns>The <see cref="ActionResult"/>.</returns>
        [HttpPost]
        [Route("AddAttendance")]
        [Authorize]
        public ActionResult AddAttendance([FromBody] RegistrationModel model)
        {
            if (model == null)
            {
                return BadRequest(new ServiceResponse() { Success = false, StatusCode = 400, Message = "Error: Could not add appointment attendance!" });
            }
            this._accountService.AddAttendance(model);
            return Ok(new ServiceResponse() { Success = true, StatusCode = 200, Message = "Appoitnment attendance added successfully!" });
        }

        /// <summary>
        /// The CreateClient.
        /// </summary>
        /// <param name="model">The model<see cref="RegistrationModel"/>.</param>
        /// <returns>The <see cref="ActionResult"/>.</returns>
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


        /// <summary>
        /// The EditClient.
        /// </summary>
        /// <param name="model">The model<see cref="RegistrationModel"/>.</param>
        /// <returns>The <see cref="ActionResult"/>.</returns>
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

        /// <summary>
        /// The GetData.
        /// </summary>
        /// <returns>The <see cref="ActionResult"/>.</returns>
        [HttpGet]
        [Route("GetData")]
        [Authorize]
        public ActionResult GetData()
        {
            var user = _accountService.GetData();
            return Ok(user);
        }

        /// <summary>
        /// The GetUsersByFacility.
        /// </summary>
        /// <param name="facilityId">The facilityId<see cref="long"/>.</param>
        /// <returns>The <see cref="ActionResult"/>.</returns>
        [HttpGet]
        [Route("GetUsersByFacility/{facilityId}")]
        [Authorize]
        public ActionResult GetUsersByFacility(long facilityId)
        {
            var user = _accountService.GetUsersByFacility(facilityId);
            return Ok(user);
        }

        /// <summary>
        /// The GetData.
        /// </summary>
        /// <param name="id">The id<see cref="long"/>.</param>
        /// <returns>The <see cref="ActionResult"/>.</returns>
        [HttpGet]
        [Route("GetData/{id}")]
        [Authorize]
        public ActionResult GetData(long id)
        {
            var user = _accountService.GetData(id);
            return Ok(user);
        }

        /// <summary>
        /// The GetAppointment.
        /// </summary>
        /// <param name="id">The id<see cref="long"/>.</param>
        /// <returns>The <see cref="ActionResult"/>.</returns>
        [HttpGet]
        [Route("GetAppointment/{id}")]
        [Authorize]
        public ActionResult GetAppointment(long id)
        {
            var user = _accountService.GetAppointment(id);
            return Ok(user);
        }

        /// <summary>
        /// The Create.
        /// </summary>
        /// <param name="user">The user<see cref="UserModel"/>.</param>
        /// <returns>The <see cref="ActionResult"/>.</returns>
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

        /// <summary>
        /// The AddViralLoadResult.
        /// </summary>
        /// <param name="addVLresult">The addVLresult<see cref="ViralLoadModel"/>.</param>
        /// <returns>The <see cref="ActionResult"/>.</returns>
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

        
        [HttpPost]
        [Route("EditViralLoadResult")]
        [Authorize]
        public ActionResult EditViralLoadResult([FromBody] ViralLoadModel addVLresult)
        {
            if (addVLresult == null)
            {
                return BadRequest(new ServiceResponse() { Success = false, StatusCode = 400, Message = "Error: Failed to edit viral load result" });
            }
            this._accountService.EditViralLoad(addVLresult);    
            return Ok(new ServiceResponse() { Success = true, StatusCode = 200, Message = "Client viral Load updated successfully" });
        }

        /// <summary>
        /// The Edit.
        /// </summary>
        /// <param name="updateuser">The updateuser<see cref="UserModel"/>.</param>
        /// <returns>The <see cref="ActionResult"/>.</returns>
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

        /// <summary>
        /// The Delete.
        /// </summary>
        /// <param name="deleteuser">The deleteuser<see cref="UserModel"/>.</param>
        /// <returns>The <see cref="ActionResult"/>.</returns>
        [HttpPost]
        [Route("Delete")]
        [Authorize]
        public ActionResult Delete([FromBody] UserModel deleteuser)
        {
            this._accountService.UpdateUser(deleteuser);
            return Ok(new ServiceResponse() { Success = true, StatusCode = 200, Message = "User successfully deleted" });
        }

        /// <summary>
        /// The GetRoles.
        /// </summary>
        /// <returns>The <see cref="ActionResult"/>.</returns>
        [HttpGet]
        [Route("GetRoles")]
        [Authorize]
        public ActionResult GetRoles()
        {
            var userroles = _accountService.getRoles();
            return Ok(userroles);
        }

        /// <summary>
        /// The CreateFacility.
        /// </summary>
        /// <param name="userFacility">The userFacility<see cref="UserBindingModel"/>.</param>
        /// <returns>The <see cref="ActionResult"/>.</returns>
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

        /// <summary>
        /// The ServicePointCreate.
        /// </summary>
        /// <param name="servicePoint">The servicePoint<see cref="ServicePointModel"/>.</param>
        /// <returns>The <see cref="ActionResult"/>.</returns>
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

        /// <summary>
        /// The GetFacilityData.
        /// </summary>
        /// <returns>The <see cref="ActionResult"/>.</returns>
        [HttpGet]
        [Route("GetFacilityData")]
        [Authorize]
        public ActionResult GetFacilityData()
        {
            var user = _accountService.getFacilityData();
            return Ok(user);
        }

        /// <summary>
        /// The GetFacilityUserData.
        /// </summary>
        /// <returns>The <see cref="ActionResult"/>.</returns>
        [HttpGet]
        [Route("GetFacilityUserData")]
        [Authorize]
        public ActionResult GetFacilityUserData()
        {
            var user = _accountService.getFacilityUserData();
            return Ok(user);
        }

        /// <summary>
        /// The GetServiceData.
        /// </summary>
        /// <returns>The <see cref="ActionResult"/>.</returns>
        [HttpGet]
        [Route("GetServiceData")]
        [Authorize]
        public ActionResult GetServiceData()
        {
            var user = _accountService.getServiceData();
            return Ok(user);
        }

        /// <summary>
        /// The DeleteFacility.
        /// </summary>
        /// <param name="deleteuser">The deleteuser<see cref="UserBindingModel"/>.</param>
        /// <returns>The <see cref="ActionResult"/>.</returns>
        [HttpPost]
        [Route("DeleteFacility")]
        [Authorize]
        public ActionResult DeleteFacility([FromBody] UserBindingModel deleteuser)
        {
            this._accountService.UpdateFacilityUser(deleteuser);

            return Ok(new ServiceResponse() { Success = true, StatusCode = 200 });
        }

        /// <summary>
        /// The ServicePointUpdate.
        /// </summary>
        /// <param name="updateservice">The updateservice<see cref="ServicePointModel"/>.</param>
        /// <returns>The <see cref="ActionResult"/>.</returns>
        [HttpPost]
        [Route("ServicePointUpdate")]
        [Authorize]
        public ActionResult ServicePointUpdate([FromBody] ServicePointModel updateservice)
        {
            this._accountService.UpdateServicePoint(updateservice);

            return Ok(new ServiceResponse() { Success = true, StatusCode = 200 });
        }

        /// <summary>
        /// The UpdatefacilityUser.
        /// </summary>
        /// <param name="facilityModel">The facilityModel<see cref="FacilityModel"/>.</param>
        /// <returns>The <see cref="ActionResult"/>.</returns>
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

        /// <summary>
        /// The CountUsers.
        /// </summary>
        /// <returns>The <see cref="int"/>.</returns>
        [HttpGet]
        [Route("CountUsers")]
        [Authorize]
        public int CountUsers()
        {
            return _accountService.CountUsers();
        }

        /// <summary>
        /// The CountUsersInFacility.
        /// </summary>
        /// <param name="facilityId">The facilityId<see cref="long"/>.</param>
        /// <returns>The <see cref="int"/>.</returns>
        [HttpGet]
        [Route("CountUsersInFacility")]
        [Authorize]
        public int CountUsersInFacility(long facilityId)
        {
            return _accountService.CountUsersInFacility(facilityId);
        }

        /// <summary>
        /// The ActiveUsers.
        /// </summary>
        /// <returns>The <see cref="int"/>.</returns>
        [HttpGet]
        [Route("ActiveUsers")]
        [Authorize]
        public int ActiveUsers()
        {
            return _accountService.ActiveUsers();
        }
    }
}
