using ClassicsApp.ObjectValue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClassicsApp.Services
{
    public interface ISupplierService
    {
        List<ViewModels.SupplierViewModel> GetAll();
        void Create(ViewModels.Supplier supplier);
        void Edit(ViewModels.Supplier supplier);
        ViewModels.SupplierViewModel GetById(Guid supplierId);
        bool CheckIfExists(string cnpj);
    }
}
