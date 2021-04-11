using ClassicsApp.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClassicsApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlertController : ControllerBase
    {
        private readonly IAlertService _alertService;

        public AlertController(IAlertService alertService)
        {
            _alertService = alertService;
        }

        [HttpGet("GetAlerts")]
        public ActionResult GetAlerts()
        {
            var alerts = _alertService.GetAll();
            return Ok(alerts);
        }

        [HttpGet("GetUserAlerts")]
        public ActionResult GetUserAlerts()
        {
            var userId = new Guid("3FBBD0D1-299C-48FF-BB0C-FE8E7003C766");
            var alerts = _alertService.GetUserAlerts(userId);
            return Ok(alerts);
        }
        

       [HttpPost("AddAlert")]
        public IActionResult AddAlert([FromForm] ViewModels.NewAlert alert)
        {
            //TODO: pegar da sessão
            alert.CreatedBy = new Guid("7BF0E9EB-4D12-4070-98FC-06DBDB703BE0");

            _alertService.Create(alert);
            return Ok();
        }

        [HttpPost("ChangeAlertStatus")]
        public IActionResult ChangeAlertStatus([FromForm] ViewModels.AlertStatus alert)
        {
            _alertService.ChangeAlertStatus(alert);
            return Ok();
        }

        [HttpPost("ChangeUserAlertStatus")]
        public IActionResult ChangeUserAlertStatus([FromForm] Guid UserAlertId)
        {
            _alertService.ChangeUserAlertStatus(UserAlertId);
            return Ok();
        }
    }
}