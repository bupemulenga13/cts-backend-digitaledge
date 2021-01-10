using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using DigitalEdge.Repository;
using DigitalEdge.Services;
using DigitalEdge.Utility;

namespace DigitalEdge.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TemplateController : ControllerBase
    {
        private readonly ITemplateService _templateService;
        private readonly IConfiguration _config;
        public TemplateController(ITemplateService templateService, IConfiguration config)
        {
            this._templateService = templateService;
            this._config = config;
        }

        [HttpGet]
        [Route("GetTemplates")]
        [Authorize]
        public ActionResult GetTemplates()
        {
            var user = _templateService.GetMessageTemplates();
            return Ok(user);
        }
        [HttpGet]
        [Route("GetTemplates/{id}")]
        [Authorize]
        public ActionResult GetTemplateById(long id)
        {
            var user = _templateService.GetMessageTemplate(id);
            return Ok(user);
        }
        [HttpPost]
        [Route("CreateTemplate")]
        [Authorize]
        public ActionResult CreateTemplate([FromBody]MessageTemplate messageTemplate)
        {
            this._templateService.AddMessageTemplate(messageTemplate);
            return Ok(new ServiceResponse() { Success = true, StatusCode = 200 });
        }
        [HttpPost]
        [Route("DeleteTemplate")]
        [Authorize]
        public ActionResult DeleteTemplate([FromBody]MessageTemplate messageTemplate)
        {
            if (messageTemplate.MessageTemplateId != 0)
            {
                this._templateService.DeleteMessageTemplate(messageTemplate);
                return Ok(new ServiceResponse() { Success = true, StatusCode = 200 });
            }
            return Ok(new ServiceResponse() { Success = false, StatusCode = 400 });
        }
    }
}