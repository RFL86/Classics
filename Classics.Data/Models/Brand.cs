using System;
using System.Collections.Generic;

namespace Classics.Data.Models
{
    public class Brand
    {
        public Brand()
        {
            CarModels = new List<CarModel>();
        }

        public Guid BrandId { get; set; }
        public string Name { get; set; }    
        public Guid CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public Enums.Brand.BrandStatus Status { get; set; }
        public virtual User User { get; set; }

        public virtual ICollection<CarModel> CarModels { get; set; }

    }
}
