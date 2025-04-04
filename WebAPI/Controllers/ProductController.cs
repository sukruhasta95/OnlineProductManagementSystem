using Application.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("getAllProducts")]
        public async Task<IActionResult> GetProducts()
        {
            var products = await _productService.GetAllAsync();
            return Ok(products);
        }

        [Authorize]
        [HttpGet("getProductById")] 
        public async Task<IActionResult> GetProductById(Guid id)
        {
            var product = await _productService.GetByIdAsync(id);
            if (product == null)
                return NotFound("Ürün bulunamadı.");

            return Ok(product);
        }

        [Authorize(Roles = "admin")]
        [HttpPost("addProduct")]
        public async Task<IActionResult> AddProduct([FromBody] Product product)
        {
            await _productService.AddAsync(product);
            return Ok(product);
        }

        [Authorize(Roles = "admin")]
        [HttpPatch("updateProduct")]
        public async Task<IActionResult> UpdateProduct([FromBody] Product product)
        {

            await _productService.UpdateAsync(product);
            return Ok(product);
        }

        [Authorize(Roles = "admin")]
        [HttpDelete("deleteProduct")]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            await _productService.DeleteAsync(id);
            return Ok();
        }
    }
}
