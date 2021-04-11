using System;
using System.Collections.Generic;


namespace Classics.Data.Models
{
    public class User
    {
        public User()
        {      
            BlobFiles = new List<BlobFile>();
            Brands = new List<Brand>();
            CarModels = new List<CarModel>();
            MyCars = new List<MyCar>();
            Products = new List<Product>();
            Series = new List<Serie>();
            Suppliers = new List<Supplier>();
            Alerts = new List<Alert>();
            UserAlerts = new List<UserAlert>();
        }

        public Guid UserId { get; set; }
        public Guid ProfileId { get; set; }
        public Guid? AddressId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string MobilePhone { get; set; }
        public string Password1 { get; set; }
        public string Password2 { get; set; }
        public Enums.User.UserStatus Status { get; set; }
        public DateTime CreatedOn { get; set; }
        public virtual Profile Profile { get; set; }
        public virtual Address Address { get; set; }
        public virtual ICollection<BlobFile> BlobFiles { get; set; }
        public virtual ICollection<Brand> Brands { get; set; }
        public virtual ICollection<CarModel> CarModels { get; set; }
        public virtual ICollection<MyCar> MyCars { get; set; }
        public virtual ICollection<Product> Products { get; set; }
        public virtual ICollection<Serie> Series { get; set; }
        public virtual ICollection<Supplier> Suppliers { get; set; }
        public virtual ICollection<Alert> Alerts { get; set; }
        public virtual ICollection<UserAlert> UserAlerts { get; set; }

    }
}
