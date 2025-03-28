using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shoper.Application.Dtos.CartItemDtos;
using Shoper.Application.Usecasess.CartItemServices;

namespace Shoper.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartItemsController : ControllerBase
    {
       private readonly ICartItemServices _cartItemServices;
        public CartItemsController(ICartItemServices cartItemServices)
        {
            _cartItemServices = cartItemServices;
        }
        [HttpGet("getallcartitems")]
        public async Task<IActionResult> GetAllCartItems()
        {
            var cartItems = await _cartItemServices.GetAllCartItemAsync();
            return Ok(cartItems);
        }
        [HttpGet("getbyidcartitem")]
        public async Task<IActionResult> GetByIdCartItem(int id)
        {
            var cartItem = await _cartItemServices.GetByIdCartItemAsync(id);
            return Ok(cartItem);
        }
        [HttpPost("createcartitem")]
        public async Task<IActionResult> CreateCartItem(CreateCartItemDto dto)
        {
            await _cartItemServices.CreateCartItemAsync(dto);
            return Ok("Create process successful");
        }
        [HttpPut("updatecartitem")]
        public async Task<IActionResult> UpdateCartItem(UpdateCartItemDto dto)
        {
            await _cartItemServices.UpdateCartItemAsync(dto);
            return Ok("Update process successful");
        }
        [HttpDelete("deletecartitem")]
        public async Task<IActionResult> DeleteCartItem(int id)
        {
            await _cartItemServices.DeleteCartItemAsync(id);
            return Ok("Delete process successful");
        }
    }
}
