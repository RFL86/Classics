using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClassicsApp.Services;
using ClassicsApp.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.IO;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace ClassicsApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarController : ControllerBase
    {
        private readonly ICarService _carService;

        public CarController(ICarService carService)
        {
            _carService = carService;
        }

        [HttpGet("GetBrandsTable")]
        public IActionResult GetBrandViewModels()
        {
            var brands = _carService.GetBrandViewModels();
            return Ok(brands);

        }

        [HttpGet("GetModelsTable")]
        public IActionResult GetModelViewModels()
        {

            var models = _carService.GetModelViewModels();
            return Ok(models);
        }

        [HttpGet("GetSeriesTable")]
        public IActionResult GetSerieViewModels()
        {
            var series = _carService.GetSerieViewModels();
            return Ok(series);
        }

        [HttpGet("GetBrands")]
        public IActionResult GetBrandsSelectList()
        {
            var brands = _carService.GetBrandsSelectList();
            return Ok(brands);

        }


        [HttpGet("GetModels")]
        public ActionResult GetModelSelectListByBrandId(Guid brandId)
        {
            var models = _carService.GetModelSelectListByBrandId(brandId);
            return Ok(models);
        }

        [HttpGet("GetAllModels")]
        public ActionResult GetAllModels()
        {
            var models = _carService.GetAllModels();
            return Ok(models);
        }


        [HttpPost("AddSerie")]
        [Authorize]
        public IActionResult AddSerie([FromForm] NewSerie newSerie)
        {
            var user = User.Claims.Where(u => u.Type == ClaimTypes.UserData).FirstOrDefault().Value;
            var userId = new Guid(user);
            var result = _carService.AddSerie(newSerie.Name, newSerie.CarModelId, userId);
            return Ok(result);
        }


        [HttpPost("AddCarModel")]
        [Authorize]
        public IActionResult AddCarModel([FromForm] NewCarModel newCarModel)
        {
            var user = User.Claims.Where(u => u.Type == ClaimTypes.UserData).FirstOrDefault().Value;
            var userId = new Guid(user);
            var result = _carService.AddCarModel(newCarModel.Name, newCarModel.BrandId, userId);
            return Ok(result);
        }

        [HttpPost("AddBrand")]
        [Authorize]
        public IActionResult AddBrand([FromForm] NewBrand newBrand)
        {
            var user = User.Claims.Where(u => u.Type == ClaimTypes.UserData).FirstOrDefault().Value;
            var userId = new Guid(user);
            var result = _carService.AddBrand(newBrand.Name, userId);
            return Ok(result);
        }

        [HttpPost("EditItem")]
        public IActionResult EditItem([FromForm] EditItem item)
        {
            var status = item.Status ? 1 : 0;
            string result = string.Empty;

            switch(item.Type)
            {
                case "Marca":
                    result = _carService.EditBrand(item.ItemId, (Enums.Brand.BrandStatus)status, item.Name);
                    break;
                case "Modelo":
                    result = _carService.EditModel(item.ItemId, (Enums.CarModel.CarModelStatus)status, item.Name);
                    break;
                case "Série":
                    result = _carService.EditSerie(item.ItemId, (Enums.Serie.SerieStatus)status, item.Name);
                    break;
            }
           
            return Ok(result);
        }

        [HttpGet("GetActiveBrands")]
        public IActionResult GetActiveBrands()
        {
            var brands = _carService.GetActiveBrandsSelectList();
            return Ok(brands);
        }


        [HttpGet("GetActiveModels")]
        public ActionResult GetActiveModelstByBrandId(Guid brandId)
        {
            var models = _carService.GetActiveModelSelectListByBrandId(brandId);
            return Ok(models);
        }

        [HttpGet("GetModelList")]
        public ActionResult GetModels()
        {
            var models = _carService.GetModels();
            return Ok(models);
        }

        [HttpGet("GetActiveSeries")]
        public ActionResult GetActiveSeriestByBrandId(Guid modelId)
        {
            var series = _carService.GetActiveSeriesSelectListByModelId(modelId);
            return Ok(series);
        }

        [HttpGet("GetUserCars")]
        [AllowAnonymous]
        public IActionResult GetUserCars()
        {
            var hasLoggedUser = User.Claims.Any();
            var userId = Guid.Empty;
            if (hasLoggedUser)
            {
                var user = User.Claims.Where(u => u.Type == ClaimTypes.UserData).FirstOrDefault().Value;
                userId = new Guid(user);            }
      
            var cars = _carService.GetUserCars(userId);
            return Ok(cars);
        }

    }
}
