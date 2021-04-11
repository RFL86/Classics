﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Classics.Data.Models;
using ClassicsApp.Services;
using ClassicsApp.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace ClassicsApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IBlobFileService _blobFileService;


        public ProductController(IProductService productService, IBlobFileService blobFileService)
        {
            _productService = productService;
            _blobFileService = blobFileService;
        }

        [HttpGet("GetUserProducts")]
        public ActionResult GetUserProducts()
        {
            var owner = new Guid("7BF0E9EB-4D12-4070-98FC-06DBDB703BE0");
            var products = _productService.GetByUserId(owner);

            var teste = User.Claims;
            var testes = User;


            return Ok(products);
        }

        [HttpGet("GetAllProducts")]
        public ActionResult GetAllProducts()
        {         
            var products = _productService.GetAllProducts();

            return Ok(products);
        }


        [HttpPost("AddProduct")]
        public IActionResult AddProduct([FromForm] ViewModels.Product product)
        {
            var owner = new Guid("7BF0E9EB-4D12-4070-98FC-06DBDB703BE0");
            _productService.Create(product, owner);
            return Ok();
        }

        [HttpPost("DisableProductPhoto")]
        public IActionResult DisableProductPhoto([FromForm] Guid productId)
        {
            _productService.DisableProductPhoto(productId);
            return Ok(true);
        }

        

        [HttpPost("EditProduct")]
        public IActionResult EditProduct([FromForm] ViewModels.Product product)
        {
            _productService.Edit(product);
            return Ok();
        }

        [HttpPost("Manage")]
        public IActionResult Manage([FromForm] ManageProduct manageProduct)
        {
            _productService.ManageProduct(manageProduct);
            return Ok();
        }

        [HttpPost("UploadPhoto")]
        public IActionResult UploadRechargeReceipt([FromForm] ProductPhoto productPhoto)
        {
            try
            {
                var blobFileId = Guid.NewGuid();
                var blobFile = MapBlobFile(productPhoto, blobFileId);
                var fileBytes = GetFileBytes(productPhoto.PhotoContent);
                var url = _blobFileService.UploadToBlob(blobFile, fileBytes);
                blobFile.Url = url;
                _blobFileService.AddBlobFile(blobFile);
            }
            catch (Exception exc)
            {
                return Ok(false);
            }

            return Ok(true);
        }

        public BlobFile MapBlobFile(ProductPhoto productPhoto, Guid blobFileId)
        {
            var fileExtension = Helpers.File.GetDefaultExtension(productPhoto.PhotoContent.ContentType);

            return new BlobFile
            {
                BlobFileId = blobFileId,
                Container = "default",
                Action = "ProductPhoto",
                ReferId = productPhoto.ProductId,
                MIME = productPhoto.PhotoContent.ContentType,
                Name = string.Concat(blobFileId, fileExtension),
                CreatedOn = DateTime.Now,
                Status = Enums.BlobFile.BlobFileStatus.Enabled,
                CreatedBy = new Guid("7BF0E9EB-4D12-4070-98FC-06DBDB703BE0")
            };
        }

        public byte[] GetFileBytes(IFormFile file)
        {
            var documentMemoryStream = new MemoryStream();
            file.CopyTo(documentMemoryStream);
            var fileBytes = documentMemoryStream.ToArray();
            return fileBytes;
        }
    }
}
