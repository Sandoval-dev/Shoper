using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shoper.Application.Dtos.OrderItemDtos;
using Shoper.Application.Usecasess.OrderItemServices;

namespace Shoper.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderItemController : ControllerBase
    {
        private readonly IOrderItemServices _orderItemServices;

        public OrderItemController(IOrderItemServices orderItemServices)
        {
            _orderItemServices = orderItemServices;
        }

        [HttpGet("getallorderitems")]
        public async Task<IActionResult> GetAllOrderItem()
        {
            var orderItems =await _orderItemServices.GetAllOrderItemAsync();
            return Ok(orderItems);
        }

        [HttpGet("getbyidorderitem")]
        public async Task<IActionResult> GetByIdOrderItem(int id)
        {
            var orderItem = await _orderItemServices.GetByIdOrderItemAsync(id);
            return Ok(orderItem);
        }

        [HttpPost("createorderitem")]
        public async Task<IActionResult> CreateOrderItem(CreateOrderItemDto dto)
        {
            await _orderItemServices.CreateOrderItemAsync(dto);
            return Ok("Create process successful");
        }

        [HttpDelete("deleteorderitem")]
        public async Task<IActionResult> DeleteOrderItem(int id)
        {
            await _orderItemServices.DeleteOrderItemAsync(id);
            return Ok("Delete process successful");
        }

        [HttpPut("updateorderitem")]
        public async Task<IActionResult> UpdateOrderItem(UpdateOrderItemDto dto)
        {
            await _orderItemServices.UpdateOrderItemAsync(dto);
            return Ok("Update process successful");
        }
    }
}
