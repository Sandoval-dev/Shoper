using Shoper.Application.Dtos.CustomerDtos;
using Shoper.Application.Interfaces;
using Shoper.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoper.Application.Usecasess.CustomerServices
{
    public class CustomerServices : ICustomerServices
    {
        private readonly IRepository<Customer> _repository;

        public CustomerServices(IRepository<Customer> repository)
        {
            _repository = repository;
        }

        public async Task CreateCustomerAsync(CreateCustomerDto dto)
        {
            await _repository.CreateAsync(new Customer
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
            });
        }

        public async Task DeleteCustomerAsync(int id)
        {
            var customer=await _repository.GetByIdAsync(id);
            await _repository.DeleteAsync(customer);
        }

        public async Task<List<ResultCustomerDto>> GetAllCustomerAsync()
        {
            var customers=await _repository.GetAllAsync();
            return customers.Select(x => new ResultCustomerDto { 
                CustomerId = x.CustomerId, 
                FirstName=x.FirstName, 
                LastName=x.LastName,
                Email = x.Email,
                //Orders = x.Orders
            }).ToList();
        }

        public async Task<GetByIdCustomerDto> GetByIdCustomerAsync(int id)
        {
           var customer=await _repository.GetByIdAsync(id);
           var newCustomer = new GetByIdCustomerDto
           {
               CustomerId = customer.CustomerId,
               FirstName = customer.FirstName,
               LastName = customer.LastName,
               Email = customer.Email,
               //Orders = customer.Orders
           };
            return newCustomer;
        }

        public async Task UpdateCustomerAsync(UpdateCustomerDto dto)
        {
            var customer = await _repository.GetByIdAsync(dto.CustomerId);
            customer.FirstName = dto.FirstName;
            customer.LastName = dto.LastName;
            customer.Email = dto.Email;
            //customer.Orders = dto.Orders;
            await _repository.UpdateAsync(customer);
        }
    }
}
