using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClassicsApp.Services;

namespace ClassicsApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupplierController : ControllerBase
    {
        private readonly ISupplierService _supplierService;

        public SupplierController(ISupplierService supplierService)
        {
            _supplierService = supplierService;
        }

        [HttpGet("GetSuppliers")]
        public ActionResult GetSuppliers()
        {
            var suppliers = _supplierService.GetAll();
            return Ok(suppliers);
        }

        [HttpGet("GetById")]
        public ActionResult GetById(Guid supplierId)
        {
            var supplier = _supplierService.GetById(supplierId);
            return Ok(supplier);
        }

        [HttpPost("AddSupplier")]
        public IActionResult AddSupplier([FromForm] ViewModels.Supplier supplier)
        {
            if(_supplierService.CheckIfExists(supplier.Cnpj))
                return Ok("O fornecedor já foi cadastrado anteriormente.");

            //TODO: pegar da sessão
            supplier.CreatedBy = new Guid("7BF0E9EB-4D12-4070-98FC-06DBDB703BE0");

             _supplierService.Create(supplier);
            return Ok();
        }

        [HttpPost("EditSupplier")]
        public IActionResult EditSupplier([FromForm] ViewModels.Supplier supplier)
        {
            _supplierService.Edit(supplier);
            return Ok();
        }
    }
}
