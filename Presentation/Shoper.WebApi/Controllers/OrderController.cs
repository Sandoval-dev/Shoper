using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shoper.Application.Dtos.OrderDtos;
using Shoper.Application.Usecasess.OrderServices;

namespace Shoper.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderServices _orderServices;

        public OrderController(IOrderServices orderServices)
        {
            _orderServices = orderServices;
        }

        [HttpGet("getallorders")]
        public async Task<IActionResult> GetAllOrders()
        {
            var orders =await _orderServices.GetAllOrdersAsync();
            return Ok(orders);
        }

        [HttpGet("getbyidorder")]
        public async Task<IActionResult> GetByIdOrder(int id)
        {
            var order = await _orderServices.GetByIdOrderAsync(id);
            return Ok(order);
        }

        [HttpDelete("deleteorder")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            await _orderServices.DeleteOrderAsync(id);
            return Ok("Delete process successful");
        }

        [HttpPost("createorder")]
        public async Task<IActionResult> CreateOrder(CreateOrderDto dto)
        {
            await _orderServices.CreateOrderAsync(dto);
            return Ok("Create process successful");
        }

        [HttpPut("updateorder")]
        public async Task<IActionResult> UpdateOrder(UpdateOrderDto dto)
        {
            await _orderServices.UpdateOrderAsync(dto);
            return Ok("Update process successful");
        }
    }
}
