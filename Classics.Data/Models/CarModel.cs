using System;
using System.Collections.Generic;

namespace Classics.Data.Models
{
    public class CarModel
    {
        public CarModel()
        {
            Series = new List<Serie>();
            Products = new List<Product>();
        }

        public Guid CarModelId { get; set; }
        public Guid BrandId { get; set; }
        public string Name { get; set; }
        public DateTime CreatedOn { get; set; }
        public Guid CreatedBy { get; set; }
        public Enums.CarModel.CarModelStatus Status { get; set; }
        public virtual User User { get; set; }
        public virtual Brand Brand { get; set; }
        public virtual ICollection<Serie> Series { get; set; }
        public virtual ICollection<Product> Products { get; set; }

    }
}
