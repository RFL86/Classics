using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClassicsApp.ViewModels
{ 
    public class BrandViewModel
    {
        public Guid BrandId { get; set; }
        public string Name { get; set; }
        public int Status { get; set; }
        public DateTime CreatedOn { get; set; }
    }

    public class ModelViewModel
    {
        public Guid ModelId { get; set; }
        public string Brand { get; set; }
        public string Name { get; set; }
        public int Status { get; set; }
        public DateTime CreatedOn { get; set; }
    }

    public class SerieViewModel
    {
        public Guid SerieId { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string Name { get; set; }
        public int Status { get; set; }
        public DateTime CreatedOn { get; set; }
    } 

    public class NewBrand
    {
        public string Name { get; set; }
    }

    public class NewCarModel
    {
        public string Name { get; set; }
        public Guid BrandId { get; set; }

    }

    public class NewSerie
    {
        public string Name { get; set; }
        public Guid CarModelId { get; set; }

    }

    public class EditItem
    {
        public Guid ItemId { get; set; }
        public string Name { get; set; }
        public bool Status { get; set; }
        public string Type { get; set; }
    }

    public class NewCar
    {
        public Guid SerieId { get; set; }
        public string Name { get; set; }
    }

    public class MyCars
    {
        public Guid MyCarId { get; set; }
        public string NickName { get; set; }
        public string BrandName { get; set; }
        public string ModelName { get; set; }
        public string SerieName { get; set; }
    }

    public class EditMyCar
    {
        public Guid MyCarId { get; set; }
        public string NickName { get; set; }
    }

}
