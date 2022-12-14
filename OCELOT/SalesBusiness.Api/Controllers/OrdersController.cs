using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SalesBusiness.Api.Data;
using SalesBusiness.Api.Data.Entities;

namespace SalesBusiness.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrdersController:ControllerBase
    {
        private readonly SalesBusinessContext _salesBusinessContext;

        public OrdersController(SalesBusinessContext salesBusinessContext)
        {
            _salesBusinessContext = salesBusinessContext;   
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var order = await _salesBusinessContext.Orders.FindAsync(id);
            return Ok(order);
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync(Orders newOrder)
        {
            _salesBusinessContext.Orders.Add(newOrder);
            await _salesBusinessContext.SaveChangesAsync();
            return CreatedAtAction("Get", new { id = newOrder.Id }, newOrder);
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var orders = await _salesBusinessContext.Orders.ToListAsync();
            return Ok(orders);
        }
    }
}
