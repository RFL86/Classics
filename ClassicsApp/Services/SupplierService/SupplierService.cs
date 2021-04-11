using Classics.Data.UnitOfWork;
using Classics.Data.Models;
using ClassicsApp.ObjectValue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using ClassicsApp.ViewModels;

namespace ClassicsApp.Services
{
    public class SupplierService : ISupplierService
    {
        private readonly IBaseUnitOfWork _unitOfWork;

        public SupplierService(IBaseUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public List<SupplierViewModel> GetAll()
        {
            var allSuppliers = _unitOfWork.SupplierRepository.Get().Select(s => new SupplierViewModel
            {
                SupplierId = s.SupplierId,
                Title = s.Title,
                Description = s.Description,
                ShortDescription = s.Description.Substring(0, Math.Min(s.Description.Length, 60)) + 
                (Math.Min(s.Description.Length, 60) == 60 ? "(...)" : ""),
                Email = s.Email,
                PhoneNumber = s.PhoneNumber,
                CreatedBy = s.User.Name,
                StatusText = s.Status == Enums.Supplier.SupplierStatus.Disable ? "Inativo" : "Ativo",
                StatusValue = s.Status.GetHashCode(),
                Cnpj = s.Cnpj
            }).ToList();

            return allSuppliers;
        }

        public SupplierViewModel GetById(Guid supplierId)
        {
            var supplier = _unitOfWork.SupplierRepository.Get(s => s.SupplierId == supplierId).Select(s => new SupplierViewModel
            {
                SupplierId = s.SupplierId,
                Title = s.Title,
                Description = s.Description,
                Email = s.Email,
                Cnpj = s.Cnpj,
                PhoneNumber = s.PhoneNumber,
                CreatedBy = s.User.Name,
                StatusText = s.Status == Enums.Supplier.SupplierStatus.Disable ? "Inativo" : "Ativo",
                StatusValue = s.Status.GetHashCode()               
            }).First();

            return supplier;
        }

        public bool CheckIfExists(string cnpj)
        {
            return _unitOfWork.SupplierRepository.Get(s => s.Cnpj.Equals(cnpj)).Any();
        }

        public void Create(ViewModels.Supplier supplier)
        {
            var newSupplier = new Classics.Data.Models.Supplier()
            {
                SupplierId = Guid.NewGuid(),
                Title = supplier.Title,
                Description = supplier.Description,
                Email = supplier.Email,
                PhoneNumber = supplier.PhoneNumber,
                Cnpj = supplier.Cnpj,
                CreatedBy = supplier.CreatedBy,
                Status = Enums.Supplier.SupplierStatus.Enable
            };

            _unitOfWork.SupplierRepository.Add(newSupplier);
            _unitOfWork.Commit();
        }

        public void Edit(ViewModels.Supplier supplier)
        {
            var supplierToEdit = _unitOfWork.SupplierRepository
                .FirstOrDefault(s => s.SupplierId == supplier.SupplierId);

            supplierToEdit.Title = supplier.Title;
            supplierToEdit.Description = supplier.Description;
            supplierToEdit.Email = supplier.Email;
            supplierToEdit.PhoneNumber = supplier.PhoneNumber;
            supplierToEdit.Cnpj = supplier.Cnpj;
            supplierToEdit.Status = (Enums.Supplier.SupplierStatus)supplier.Status;
    
            _unitOfWork.SupplierRepository.Edit(supplierToEdit);
            _unitOfWork.Commit();
        }
    }
}
