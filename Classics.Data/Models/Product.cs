using System;
using System.Collections.Generic;

namespace Classics.Data.Models
{
    public class Product
    {
        public Product()
        {
            BlobFiles = new List<BlobFile>();
        }

        public Guid ProductId { get; set; }
        public Guid OwnerId { get; set; }        
        public Guid? CarModelId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public DateTime CreatedOn { get; set; }
        public Enums.Product.ProductStatus Status { get; set; }
        public virtual User Owner { get; set; }
        public virtual CarModel CarModel { get; set; }
        public virtual ICollection<BlobFile> BlobFiles { get; set; }

    }
}
