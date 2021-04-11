using System;
using System.Collections.Generic;

namespace Classics.Data.Models
{
    public class Address
    {
        public Address()
        {
            Users = new List<User>();
        }
        public Guid AddressId { get; set; }     
        public string ZipCode { get; set; }
        public string StateCode { get; set; }
        public string City { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
