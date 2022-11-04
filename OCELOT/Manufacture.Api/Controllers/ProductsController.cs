using Manufacture.Api.Data;
using Manufacture.Api.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Manufacture.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController:ControllerBase
    {
        private readonly ManufactureContext _manufactureContext;

        public ProductsController(ManufactureContext manufactureContext)
        {
            _manufactureContext = manufactureContext;    
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var product = await _manufactureContext.Products.FindAsync(id);
            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync(Products newProduct)
        {
            _manufactureContext.Products.Add(newProduct);
            await _manufactureContext.SaveChangesAsync();
            return CreatedAtAction("Get", new { id = newProduct.Id }, newProduct);
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var products = await _manufactureContext.Products.ToListAsync();
            return Ok(products);
        }

    }
}
