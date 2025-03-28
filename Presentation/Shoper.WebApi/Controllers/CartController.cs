using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shoper.Application.Dtos.CartDtos;
using Shoper.Application.Usecasess.CartServices;

namespace Shoper.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartServices _cartServices;

        public CartController(ICartServices cartServices)
        {
            _cartServices = cartServices;
        }

        [HttpGet("getallcarts")]
        public async Task<IActionResult> GetAllCarts()
        {
            var carts=await _cartServices.GetAllCartAsync();
            return Ok(carts);
        }

        [HttpGet("getbyidcart")]
        public async Task<IActionResult> GetCartById(int id)
        {
            var cart = await _cartServices.GetByIdCartAsync(id);
            return Ok(cart);
        }

        [HttpPost("createcart")]
        public async Task<IActionResult> CreateCart(CreateCartDto dto)
        {
            await _cartServices.CreateCartAsync(dto);
            return Ok("Create process successful");
        }

        [HttpPut("updatecart")]
        public async Task<IActionResult> UpdateCart(UpdateCartDto dto) 
        { 
            await _cartServices.UpdateCartAsync(dto);
            return Ok("Update process successful");
        }

        [HttpDelete("deletecart")]
        public async Task<IActionResult> DeleteCart(int id)
        {
            await _cartServices.DeleteCartAsync(id);
            return Ok("Delete process successful");
        }
    }
}
