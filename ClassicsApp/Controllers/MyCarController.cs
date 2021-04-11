using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClassicsApp.Services;
using ClassicsApp.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClassicsApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MyCarController : ControllerBase
    {
        private readonly IMyCarService _myCarService;
        private readonly ICorreiosWS _correiosWS;
        public MyCarController(IMyCarService myCarService, ICorreiosWS correiosWS)
        {
            _myCarService = myCarService;
            _correiosWS = correiosWS;
        }


        [HttpGet("GetMyCars")]
        public ActionResult GetMyCars()
        {
            var userid = new Guid("7BF0E9EB-4D12-4070-98FC-06DBDB703BE0");
            var series = _myCarService.GetMyCars(userid);
            return Ok(series);
        }


        [HttpPost("AddMyCar")]
        public IActionResult AddMyCar([FromForm] NewCar newCar)
        {
            var userid = new Guid("7BF0E9EB-4D12-4070-98FC-06DBDB703BE0");
            var result = _myCarService.AddMyCar(newCar, userid);
            return Ok(result);
        }

        [HttpPost("EditMyCar")]
        public IActionResult EditMyCar([FromForm] EditMyCar editMyCar)
        {
            var result = _myCarService.EditMyCar(editMyCar);
            return Ok(result);
        }

        [HttpPost("RemoveMyCar")]
        public IActionResult RemoveMyCar([FromForm] Guid MyCarId)
        {
            _myCarService.RemoveMyCar(MyCarId);
            return Ok();
        }
        


    }
}
