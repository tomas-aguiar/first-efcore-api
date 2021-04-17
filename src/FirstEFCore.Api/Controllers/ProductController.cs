using System.Threading.Tasks;
using FirstEFCore.Api.Data;
using FirstEFCore.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace FirstEFCore.Api.Controllers
{
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly DataContext _context;

        public ProductController()
        {
            _context = new DataContext();
        }
        
        [HttpPost]
        [Route("api/v1/CreateProduct")]
        public async Task<IActionResult> CreateProduct([FromBody] Product newProduct)
        {
            if (newProduct == null)
                return BadRequest();

            try
            {
                var addedProduct = await _context.Products.AddAsync(newProduct);
                await _context.SaveChangesAsync();

                return Ok(addedProduct);
            }
            catch
            {
                return StatusCode(500);
            }
        }
    }
}