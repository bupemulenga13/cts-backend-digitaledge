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
        public AccountController(IAccountService accountService, IConfiguration config , IVisitService visitService)
        {
            this._accountService = accountService;
            this._visitService = visitService;
            this._config = config;
        }

        [HttpPost]
        [Route("Login")]
        [AllowAnonymous]
        public ActionResult Login([FromBody]UserModel model)
        {
            if (model == null)
                return Ok(new ServiceResponse() { StatusCode = 400 });
            var user = _accountService.ValidateUser(model.Email, model.Password);
            if (user == null)
                return Ok(new ServiceResponse() { StatusCode = 401 });
            var token = _accountService.GetToken(user);
            return Ok(new { user, token });
        }
        [HttpPost]
        [Route("CreateAppointment")]
        [AllowAnonymous]
        public ActionResult CreateAppointment([FromBody]RegistrationModel model)
        {
            if (model == null)
            {
                return Ok(new ServiceResponse() { StatusCode = 400 });
            }
            var user = _accountService.ValidateClient(model);            
            if (user == null)
            {
                 this._accountService.AddClient(model);
                var userdata = _visitService.GetClient();
                if (userdata != null)
                {
                    model.ClientId = (userdata.ClientId);
                    this._accountService.AddAppointment(model);
                }
                return Ok(new ServiceResponse() { StatusCode = 200 });
            }
            else
            {
                model.ClientId = (user.ClientId);
              string result =  this._accountService.AddAppointment(model);
                if (result == "null")
                    return Ok(new ServiceResponse() { Success = true, StatusCode = 500 });
                else
                    return Ok(new ServiceResponse() { Success = true, StatusCode = 200 });
             }            
        }
        [HttpPost]
        [Route("EditAppointment")]
        [AllowAnonymous]
        public ActionResult EditAppointment([FromBody]RegistrationModel model)
        {           
            this._accountService.UpdateAppointment(model);
            return Ok(new ServiceResponse() { Success = true, StatusCode = 200 });
        }
        [HttpGet]
        [Route("GetData")]
        [Authorize]
        public ActionResult GetData()
        {
            var user = _accountService.getData();
            return Ok(user);
        }      
        [HttpGet]
        [Route("GetData/{id}")]
        [Authorize]
        public ActionResult GetData(long id)
        {
            var user = _accountService.getData(id);
            return Ok(user);
        }
        [HttpPost]
        [Route("Create")]
        [Authorize]
        public ActionResult Create([FromBody]UserModel userdetail)
        {
            this._accountService.AddUser(userdetail);
            return Ok(new ServiceResponse() { Success = true, StatusCode = 200 });
        }
        [HttpPut]
        [Route("Edit")]
        [Authorize]
        public ActionResult Edit([FromBody]UserModel updateuser)
        {
            this._accountService.UpdateUser(updateuser);
            return Ok(new ServiceResponse() { Success = true, StatusCode = 200 });
        }
        [HttpPost]
        [Route("Delete")]
        [Authorize]
        public ActionResult Delete([FromBody]UserModel deleteuser)
        {
            this._accountService.UpdateUser(deleteuser);
            return Ok(new ServiceResponse() { Success = true, StatusCode = 200 });
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
        [Route("FacilityCreate")]
        [Authorize]
        public ActionResult FacilityCreate(UserBindingModel userFacility)
        {
            string resultdata = this._accountService.AddFacilityUser(userFacility);
            if (resultdata == "null")
                return Ok(new ServiceResponse() { Success = true, StatusCode = 500 });
           else
                return Ok(new ServiceResponse() { Success = true, StatusCode = 200 });

        }       
        [HttpPost]
        [Route("ServicePointCreate")]
        [Authorize]
        public ActionResult ServicePointCreate(ServicePointModel servicePoint)
        {
            string resultdata = this._accountService.AddServicePoint(servicePoint);
            if (resultdata == "null")
                return Ok(new ServiceResponse() { Success = true, StatusCode = 500 });
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
        public ActionResult DeleteFacility([FromBody]UserBindingModel deleteuser)
        {
            this._accountService.UpdateFacilityUser(deleteuser);

            return Ok(new ServiceResponse() { Success = true, StatusCode = 200 });
        }
        [HttpPost]
        [Route("ServicePointUpdate")]
        [Authorize]
        public ActionResult ServicePointUpdate([FromBody]ServicePointModel updateservice)
        {
            this._accountService.UpdateServicePoint(updateservice);

            return Ok(new ServiceResponse() { Success = true, StatusCode = 200 });
        }  
        
        [HttpPost]
        [Route("UpdatefacilityUser")]
        [Authorize]
        public ActionResult UpdatefacilityUser([FromBody]FacilityModel updatefacility)
        {
             string resultdata = this._accountService.UpdateFacility(updatefacility);

            if (resultdata == "null")
                return Ok(new ServiceResponse() { Success = true, StatusCode = 500 });
            else
                return Ok(new ServiceResponse() { Success = true, StatusCode = 200 });
        }
    }
}