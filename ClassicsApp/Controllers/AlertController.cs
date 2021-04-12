using ClassicsApp.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

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
        [Authorize]
        public ActionResult GetUserAlerts()
        {
            var user = User.Claims.Where(u => u.Type == ClaimTypes.UserData).FirstOrDefault().Value;
            var userId = new Guid(user);
            var alerts = _alertService.GetUserAlerts(userId);
            return Ok(alerts);
        }

        [HttpPost("AddAlert")]
        [Authorize]
        public IActionResult AddAlert([FromForm] ViewModels.NewAlert alert)
        {
            var user = User.Claims.Where(u => u.Type == ClaimTypes.UserData).FirstOrDefault().Value;
            var userId = new Guid(user);
            alert.CreatedBy = userId;
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