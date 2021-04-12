using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClassicsApp.ViewModels
{
    public class UserProduct
    {
        public Guid ProductId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ShortDescription { get; set; }
        public decimal Price { get; set; }
        public DateTime CreatedOn { get; set; }
        public string Status { get; set; }
        public int StatusValue { get; set; }
        public string PhotoUrl { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public Guid? CarModelId { get; set; }
        public string CarModel { get; set; }

    }

    public class ProductPhoto
    {
        public IFormFile PhotoContent { get; set; }
        public Guid ProductId { get; set; }
    }

    public class Product
    {
        public Guid? ProductId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int StatusValue { get; set; }
        public Guid? CarModelId { get; set; }
    }

    public class ManageProduct
    {
        public Guid ProductId { get; set; }
        public int Status { get; set; }

    }
  
}
