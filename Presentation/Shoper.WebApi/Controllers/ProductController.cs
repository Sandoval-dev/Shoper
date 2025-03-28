using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shoper.Application.Dtos.Product;
using Shoper.Application.Usecasess.ProductServices;

namespace Shoper.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductServices _productServices;

        public ProductController(IProductServices productServices)
        {
            _productServices = productServices;
        }

        [HttpGet("getallproducts")]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _productServices.GetAllProductAsync();
            return Ok(products);
        }

        [HttpGet("getproductbyid/{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var product = await _productServices.GetByIdProductAsync(id);
            return Ok(product);
        }

        [HttpPost("createproduct")]
        public async Task<IActionResult> CreateProduct(CreateProductDto dto)
        {
            await _productServices.CreateProductAsync(dto);
            return Ok("Create process successful");
        }

        [HttpPut("updateproduct")]
        public async Task<IActionResult> UpdateProduct(UpdateProductDto dto)
        {
            await _productServices.UpdateProductAsync(dto);
            return Ok("Update process successful");
        }

        [HttpDelete("deleteproduct")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            await _productServices.DeleteProductAsync(id);
            return Ok("Delete process successful");
        }

    }
}
