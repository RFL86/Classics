using Classics.Data.UnitOfWork;
using Classics.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClassicsApp.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ClassicsApp.Services
{
    public class CarService : ICarService
    {
        private readonly IBaseUnitOfWork _unitOfWork;


        public CarService(IBaseUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public List<SelectListItem> GetBrandsSelectList()
        {
            var brandsList = _unitOfWork.BrandRepository.Get().Select(b => new SelectListItem
            {
                Text = b.Name,
                Value = b.BrandId.ToString()
            }).OrderBy(b => b.Text).ToList();

            return brandsList;
        }

        public List<SelectListItem> GetModelSelectListByBrandId(Guid brandId)
        {
            var modelsList = _unitOfWork.CarModelRepository.Get(m => m.BrandId == brandId).Select(m => new SelectListItem
            {
                Text = m.Name,
                Value = m.CarModelId.ToString()
            }).OrderBy(m => m.Text).ToList();

            return modelsList;
        }

        public List<SelectListItem> GetAllModels()
        {
            var modelsList = _unitOfWork.CarModelRepository.Get().Select(m => new SelectListItem
            {
                Text = string.Concat(m.Name, " - ", m.Brand.Name),
                Value = m.CarModelId.ToString()
            }).OrderBy(m => m.Text).ToList();

            return modelsList;
        }

        public List<SelectListItem> GetSeriesSelectListByModelId(Guid modelId)
        {
            var seriesList = _unitOfWork.SerieRepository.Get(s => s.CarModelId == modelId).Select(s => new SelectListItem
            {
                Text = s.Name,
                Value = s.SerieId.ToString()
            }).OrderBy(s => s.Text).ToList();

            return seriesList;
        }


        public List<BrandViewModel> GetBrandViewModels()
        {
            var brands = _unitOfWork.BrandRepository.Get().Select(b => new BrandViewModel
            {
                BrandId = b.BrandId,
                Name = b.Name,
                Status = (int)b.Status,
                CreatedOn = b.CreatedOn
            }).OrderBy(b => b.Name).ToList();

            return brands;
        }

        public List<ModelViewModel> GetModelViewModels()
        {
            var models = _unitOfWork.CarModelRepository.Get().Select(m => new ModelViewModel
            {
                ModelId = m.CarModelId,
                Name = m.Name,
                Brand = m.Brand.Name,
                Status = (int)m.Status,
                CreatedOn = m.CreatedOn
            }).OrderBy(m => m.Name).ToList();

            return models;
        }

        public List<SerieViewModel> GetSerieViewModels()
        {
            var series = _unitOfWork.SerieRepository.Get().Select(s => new SerieViewModel
            {
                SerieId = s.SerieId,
                Name = s.Name,
                Model = s.CarModel.Name,
                Brand = s.CarModel.Brand.Name,
                Status = (int)s.Status,
                CreatedOn = s.CreatedOn
            }).OrderBy(s => s.Name).ToList();

            return series;
        }


        //public List<CarModel> GetCarModels()
        //{
        //    var carModels = _unitOfWork.CarModelRepository.Get(c => c.Status == Data.Enums.CarModel.CarModelStatus.Enable).ToList();
        //    return carModels;
        //}

        //public List<Brand> GetBrands()
        //{
        //    var brands = _unitOfWork.BrandRepository.Get(c => c.Status == Brand.BrandStatus.Enable).ToList();
        //    return brands;
        //}

        //public List<Serie> GetSeries()
        //{
        //    var series = _unitOfWork.SerieRepository.Get(c => c.Status == Serie.SerieStatus.Enable).ToList();
        //    return series;
        //}

        public string AddCarModel(string modelName, Guid brandId)
        {
            if (string.IsNullOrEmpty(modelName))
                return "Tamanho do nome inválido.";
            if (modelName.Length < 3)
                return "Tamanho do nome inválido.";
            if (!CheckModel(modelName, brandId))
                return "Modelo já cadastrado.";

            var model = new CarModel()
            {
                CarModelId = Guid.NewGuid(),
                BrandId = brandId,
                Name = modelName.Trim(),
                CreatedOn = DateTime.Now,
                CreatedBy = new Guid("7BF0E9EB-4D12-4070-98FC-06DBDB703BE0"),
                Status = Enums.CarModel.CarModelStatus.Enabled
            };

            _unitOfWork.CarModelRepository.Add(model);
            _unitOfWork.Commit();

            return string.Empty;
        }

        public string AddBrand(string brandName)
        {
            if (string.IsNullOrEmpty(brandName))
                return "Tamanho do nome inválido.";
            if (brandName.Length < 3)
                return "Tamanho do nome inválido.";
            if (!CheckBrand(brandName))
                return "Marca já cadastrada.";

            var brand = new Brand()
            {
                BrandId = Guid.NewGuid(),
                Name = brandName.Trim(),
                CreatedOn = DateTime.Now,
                CreatedBy = new Guid("7BF0E9EB-4D12-4070-98FC-06DBDB703BE0"),
                Status = Enums.Brand.BrandStatus.Enabled
            };

            _unitOfWork.BrandRepository.Add(brand);
            _unitOfWork.Commit();
            return string.Empty;
        }

        public string AddSerie(string serieName, Guid modelId)
        {
            if (string.IsNullOrEmpty(serieName))
                return "Tamanho do nome inválido.";
            if (serieName.Length < 2)
                return "Tamanho do nome inválido.";
            if (!CheckSerie(serieName, modelId))
                return "Série já cadastrada.";

            var serie = new Serie()
            {
                SerieId = Guid.NewGuid(),
                CarModelId = modelId,
                Name = serieName.Trim(),
                CreatedOn = DateTime.Now,
                CreatedBy = new Guid("7BF0E9EB-4D12-4070-98FC-06DBDB703BE0"),
                Status = Enums.Serie.SerieStatus.Enabled
            };

            _unitOfWork.SerieRepository.Add(serie);
            _unitOfWork.Commit();

            return string.Empty;
        }

        public void EditCarModel(CarModel carmodel)
        {
            _unitOfWork.CarModelRepository.Edit(carmodel);
            _unitOfWork.Commit();
        }

        public void EditBrand(Brand brand)
        {
            _unitOfWork.BrandRepository.Edit(brand);
            _unitOfWork.Commit();
        }

        public void EditSerie(Serie serie)
        {
            _unitOfWork.SerieRepository.Edit(serie);
            _unitOfWork.Commit();
        }

        public bool CheckBrand(string name)
        {
            var existsBrand = _unitOfWork.BrandRepository.GetIQueryable(b => b.Name.ToLower().Equals(name.ToLower().Trim())).Any();
            if (existsBrand)
                return false;

            return true;
        }

        public bool CheckModel(string name, Guid brandId)
        {
            var existsModel = _unitOfWork.CarModelRepository.GetIQueryable(m => m.Name.ToLower().Equals(name.ToLower().Trim()) && m.BrandId == brandId).Any();
            if (existsModel)
                return false;

            return true;
        }

        public bool CheckSerie(string name, Guid modelId)
        {
            var existsSerie = _unitOfWork.SerieRepository.GetIQueryable(s => s.Name.ToLower().Equals(name.ToLower().Trim()) && s.CarModelId == modelId).Any();
            if (existsSerie)
                return false;

            return true;
        }

        public string EditBrand(Guid brandId, Enums.Brand.BrandStatus status, string name)
        {
            var nameError = CheckBrandName(name);
            if (!string.IsNullOrEmpty(nameError))
                return nameError;

            var brand = _unitOfWork.BrandRepository.Get(b => b.BrandId == brandId).FirstOrDefault();
            brand.Name = name;
            brand.Status = status;
            _unitOfWork.BrandRepository.Edit(brand);
            _unitOfWork.Commit();

            return string.Empty;
        }

        public string EditModel(Guid modelId, Enums.CarModel.CarModelStatus status, string name)
        {
            var nameError = CheckModelName(name);
            if (!string.IsNullOrEmpty(nameError))
                return nameError;

            var model = _unitOfWork.CarModelRepository.Get(b => b.CarModelId == modelId).FirstOrDefault();
            model.Name = name;
            model.Status = status;
            _unitOfWork.CarModelRepository.Edit(model);
            _unitOfWork.Commit();

            return string.Empty;
        }


        public string EditSerie(Guid serieId, Enums.Serie.SerieStatus status, string name)
        {
            var nameError = CheckSerieName(name);
            if (!string.IsNullOrEmpty(nameError))
                return nameError;

            var serie = _unitOfWork.SerieRepository.Get(b => b.SerieId == serieId).FirstOrDefault();
            serie.Name = name;
            serie.Status = status;
            _unitOfWork.SerieRepository.Edit(serie);
            _unitOfWork.Commit();

            return string.Empty;
        }


        public string CheckBrandName(string name)
        {
            var existsSerie = _unitOfWork.SerieRepository.GetIQueryable(s => s.Name.ToLower().Equals(name.ToLower().Trim())).Any();
            if (existsSerie)
                return "Item já cadastrado.";

            if (string.IsNullOrEmpty(name))
                return "Tamanho do nome inválido.";
            if (name.Length < 3)
                return "Tamanho do nome inválido.";

            return string.Empty;
        }

        public string CheckModelName(string name)
        {
            var existsSerie = _unitOfWork.CarModelRepository.GetIQueryable(s => s.Name.ToLower().Equals(name.ToLower().Trim())).Any();
            if (existsSerie)
                return "Item já cadastrado.";

            if (string.IsNullOrEmpty(name))
                return "Tamanho do nome inválido.";
            if (name.Length < 3)
                return "Tamanho do nome inválido.";

            return string.Empty;
        }

        public string CheckSerieName(string name)
        {
            var existsSerie = _unitOfWork.SerieRepository.GetIQueryable(s => s.Name.ToLower().Equals(name.ToLower())).Any();
            if (existsSerie)
                return "Item já cadastrado.";

            if (string.IsNullOrEmpty(name))
                return "Tamanho do nome inválido.";
            if (name.Length < 3)
                return "Tamanho do nome inválido.";

            return string.Empty;
        }

        public List<SelectListItem> GetActiveBrandsSelectList()
        {
            var brandsList = _unitOfWork.BrandRepository.Get(b => b.Status == Enums.Brand.BrandStatus.Enabled).Select(b => new SelectListItem
            {
                Text = b.Name,
                Value = b.BrandId.ToString()
            }).OrderBy(b => b.Text).ToList();

            return brandsList;
        }

        public List<SelectListItem> GetActiveModelSelectListByBrandId(Guid brandId)
        {
            var modelsList = _unitOfWork.CarModelRepository.Get(m => m.BrandId == brandId && m.Status == Enums.CarModel.CarModelStatus.Enabled).Select(m => new SelectListItem
            {
                Text = m.Name,
                Value = m.CarModelId.ToString()
            }).OrderBy(m => m.Text).ToList();

            return modelsList;
        }

        public List<SelectListItem> GetModels()
        {
            var modelsList = _unitOfWork.CarModelRepository.Get(m => m.Status == Enums.CarModel.CarModelStatus.Enabled)
                .Select(m => new SelectListItem
                {
                    Text = m.Name,
                    Value = m.CarModelId.ToString()
                }).OrderBy(m => m.Text).ToList();

            return modelsList;
        }

        public List<SelectListItem> GetActiveSeriesSelectListByModelId(Guid modelId)
        {
            var seriesList = _unitOfWork.SerieRepository.Get(s => s.CarModelId == modelId
            && s.Status == Enums.Serie.SerieStatus.Enabled).Select(s => new SelectListItem
            {
                Text = s.Name,
                Value = s.SerieId.ToString()
            }).OrderBy(s => s.Text).ToList();

            return seriesList;
        }



    }
}
