using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shoper.Application.Dtos.CustomerDtos;
using Shoper.Application.Usecasess.CustomerServices;

namespace Shoper.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerServices _customerServices;

        public CustomerController(ICustomerServices customerServices)
        {
            _customerServices = customerServices;
        }

        [HttpGet("getallcustomers")]
        public async Task<IActionResult> GetAllCustomers()
        {
            var customers=await _customerServices.GetAllCustomerAsync();
            return Ok(customers);
        }

        [HttpGet("getbyidcustomer")]
        public async Task<IActionResult> GetByIdCustomer(int id)
        {
            var customer = await _customerServices.GetByIdCustomerAsync(id);
            return Ok(customer);
        }

        [HttpPost("createcustomer")]
        public async Task<IActionResult> CreateCustomer(CreateCustomerDto dto)
        {
            await _customerServices.CreateCustomerAsync(dto);
            return Ok("Create process successful");
        }

        [HttpPut("updatecustomer")]
        public async Task<IActionResult> UpdateCustomer(UpdateCustomerDto dto)
        {
            await _customerServices.UpdateCustomerAsync(dto);
            return Ok("Update process successful");
        }

        [HttpDelete("deletecustomer")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            await _customerServices.DeleteCustomerAsync(id);
            return Ok("Delete process successful");
        }
    }
}
