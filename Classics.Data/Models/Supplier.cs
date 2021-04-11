using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classics.Data.Models
{
    public class Supplier
    {
        public Guid SupplierId { get; set; }
        public Guid CreatedBy { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Cnpj { get; set; }
        public Enums.Supplier.SupplierStatus Status { get; set; }
        public virtual User User { get; set; }

    }
}
