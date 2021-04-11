using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClassicsApp.ViewModels
{
    public class Supplier
    {
        public Guid SupplierId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Cnpj { get; set; }
        public Guid CreatedBy { get; set; }
        public int Status { get; set; }
    }

    public class SupplierBanner
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }

    public class SupplierViewModel
    {
        public Guid SupplierId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ShortDescription { get; set; }
        public string Cnpj { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string CreatedBy { get; set; }
        public string StatusText { get; set; }
        public int StatusValue { get; set; }
    }
}
