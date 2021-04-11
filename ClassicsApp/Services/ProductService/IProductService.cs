
using ClassicsApp.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;

namespace ClassicsApp.Services
{
    public interface IProductService
    {
        List<UserProduct> GetAllProducts();
        List<UserProduct> GetByUserId(Guid userId);
        void Edit(Product editProduct);
        void Create(Product newProduct, Guid ownerId);
        void ManageProduct(ManageProduct manageProduct);
        void DisableProductPhoto(Guid productId);
    }
}
