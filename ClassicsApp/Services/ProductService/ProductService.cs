using Classics.Data.UnitOfWork;
using Classics.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using ClassicsApp.ViewModels;

namespace ClassicsApp.Services
{
    public class ProductService : IProductService
    {
        private readonly IBaseUnitOfWork _unitOfWork;

        public ProductService(IBaseUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public List<UserProduct> GetByUserId(Guid userId)
        {
            var products = _unitOfWork.ProductRepository.Get(c => c.OwnerId == userId).Select(p => new UserProduct
            {
                ProductId = p.ProductId,
                Title = p.Title,
                Description = p.Description,
                ShortDescription = p.Description.Substring(0, Math.Min(p.Description.Length, 60)) +
                (Math.Min(p.Description.Length, 60) == 60 ? "(...)" : ""),
                Price = p.Price,
                CreatedOn = p.CreatedOn,
                Status = Helpers.EnumHelper.GetDescription(p.Status),
                StatusValue = p.Status.GetHashCode(),
                PhotoUrl = p.BlobFiles.FirstOrDefault(b => b.Status == Enums.BlobFile.BlobFileStatus.Enabled)?.Url,
                Phone = string.IsNullOrEmpty(p.Owner.MobilePhone) ? "Não informado" : p.Owner.MobilePhone,
                Email = p.Owner.Email,
                Address = p.Owner.Address.City + "-" + p.Owner.Address.StateCode,
                CarModelId = p.CarModelId,
                CarModel = p.CarModel?.Name ?? "Não especificado"
            }).OrderByDescending(p => p.CreatedOn).ToList();
            return products;
        }

        public List<UserProduct> GetAllProducts()
        {
            var products = _unitOfWork.ProductRepository.Get().Select(p => new UserProduct
            {
                ProductId = p.ProductId,
                Title = p.Title,
                Description = p.Description,
                ShortDescription = p.Description.Substring(0, Math.Min(p.Description.Length, 60)) +
                (Math.Min(p.Description.Length, 60) == 60 ? "(...)" : ""),
                Price = p.Price,
                CreatedOn = p.CreatedOn,
                Status = Helpers.EnumHelper.GetDescription(p.Status),
                StatusValue = p.Status.GetHashCode(),
                PhotoUrl = p.BlobFiles.FirstOrDefault(b => b.Status == Enums.BlobFile.BlobFileStatus.Enabled)?.Url,
                Phone = string.IsNullOrEmpty(p.Owner.MobilePhone) ? "Não informado" : p.Owner.MobilePhone,
                Email = p.Owner.Email,
                Address = p.Owner.Address.City + "-" + p.Owner.Address.StateCode,
                CarModelId = p.CarModelId,
                CarModel = p.CarModel?.Name ?? "Não especificado"
            }).OrderByDescending(p => p.CreatedOn).Take(1000).ToList();

            return products;
        }

        public void Edit(ViewModels.Product editProduct)
        {
            var product = _unitOfWork.ProductRepository.FirstOrDefault(p => p.ProductId == editProduct.ProductId);

            product.Title = editProduct.Title;
            product.Status = (Enums.Product.ProductStatus)editProduct.StatusValue == Enums.Product.ProductStatus.Enable ? Enums.Product.ProductStatus.PendingApproval : (Enums.Product.ProductStatus)editProduct.StatusValue;
            product.Description = editProduct.Description;
            product.Price = editProduct.Price;
            if (editProduct.CarModelId != null || editProduct.CarModelId != Guid.Empty)
                product.CarModelId = editProduct.CarModelId;

            _unitOfWork.ProductRepository.Edit(product);
            _unitOfWork.Commit();
        }

        public void Create(ViewModels.Product newProduct, Guid ownerId)
        {
            var product = new Classics.Data.Models.Product();
            product.ProductId = Guid.NewGuid();
            product.Title = newProduct.Title;
            product.Status = (Enums.Product.ProductStatus)newProduct.StatusValue;
            product.Description = newProduct.Description;
            product.Price = newProduct.Price;
            product.CreatedOn = DateTime.Now;
            product.OwnerId = ownerId;
            if (newProduct.CarModelId != null || newProduct.CarModelId != Guid.Empty)
                product.CarModelId = newProduct.CarModelId;

            _unitOfWork.ProductRepository.Add(product);
            _unitOfWork.Commit();
        }

        public void ManageProduct(ManageProduct manageProduct)
        {
            var product = _unitOfWork.ProductRepository.FirstOrDefault(p => p.ProductId == manageProduct.ProductId);

            var alertMessage = string.Empty;
            if (product.Status == Enums.Product.ProductStatus.Enable)
                alertMessage = string.Concat("O produto ", product.Title, " foi aprovado pela moderação e está disponível nas buscas");
            else
                alertMessage = string.Concat("O produto ", product.Title, " foi reprovado pela moderação, verifique possíveis inconsistências no anúncio");

            var alert = new Classics.Data.Models.Alert
            {
                AlertId = Guid.NewGuid(),
                CreatedBy = new Guid("EA89F53A-8D92-4C40-8288-D26F01D7B516"),
                Receiver = Enums.Alert.Receiver.Client,
                Subject = product.Title,
                Message = alertMessage,
                Status = Enums.Alert.AlertStatus.Available
            };

            var userAlert = new Classics.Data.Models.UserAlert
            {
                UserAlertId = Guid.NewGuid(),
                UserId = product.OwnerId,
                AlertId = alert.AlertId,
                ReadingStatus = Enums.UserAlert.ReadingStatus.NotRead
            };

            product.Status = (Enums.Product.ProductStatus)manageProduct.Status;

            _unitOfWork.ProductRepository.Edit(product);
            _unitOfWork.AlertRepository.Add(alert);
            _unitOfWork.UserAlertRepository.Add(userAlert);
            _unitOfWork.Commit();
        }

        public void DisableProductPhoto(Guid productId)
        {
            var files = _unitOfWork.BlobFileRepository.Get(p => p.ReferId == productId);

            foreach (var item in files)
                item.Status = Enums.BlobFile.BlobFileStatus.Disabled;

            _unitOfWork.BlobFileRepository.EditAll(files);
            _unitOfWork.Commit();
        }
    }
}
