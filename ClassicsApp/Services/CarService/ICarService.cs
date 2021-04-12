using ClassicsApp.ObjectValue;
using ClassicsApp.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClassicsApp.Services
{
    public interface ICarService
    {
        List<BrandViewModel> GetBrandViewModels();
        List<ModelViewModel> GetModelViewModels();
        List<SerieViewModel> GetSerieViewModels();
        List<SelectListItem> GetBrandsSelectList();       
        List<SelectListItem> GetModelSelectListByBrandId(Guid brandId);
        List<SelectListItem> GetSeriesSelectListByModelId(Guid modelId);
        string AddSerie(string serieName, Guid modelId, Guid userId);
        string AddBrand(string brandName, Guid userId);
        string AddCarModel(string modelName, Guid brandId, Guid userId);
        public string EditSerie(Guid serieId, Enums.Serie.SerieStatus status, string name);
        public string EditModel(Guid modelId, Enums.CarModel.CarModelStatus status, string name);
        public string EditBrand(Guid brandId, Enums.Brand.BrandStatus status, string name);
        List<SelectListItem> GetActiveBrandsSelectList();
        List<SelectListItem> GetActiveModelSelectListByBrandId(Guid brandId);
        List<SelectListItem> GetActiveSeriesSelectListByModelId(Guid modelId);
        List<SelectListItem> GetAllModels();
        List<SelectListItem> GetModels();
        List<SelectListItem> GetUserCars(Guid userId);

    }
}
