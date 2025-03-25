using Shoper.Application.Dtos.OrderDtos;
using Shoper.Application.Interfaces;
using Shoper.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoper.Application.Usecasess.OrderServices
{
    public class OrderServices : IOrderServices
    {
        private readonly IRepository<Order> _orderRepository;

        public OrderServices(IRepository<Order> orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task CreateOrderAsync(CreateOrderDto dto)
        {
           await _orderRepository.CreateAsync(new Order
           {
               OrderDate = dto.OrderDate,
               CustomerId = dto.CustomerId,
               //Customer = dto.Customer,
               BillingAddress = dto.BillingAddress,
               ShippingAddress = dto.ShippingAddress,
               //OrderItems = dto.OrderItems,
               OrderStatus = dto.OrderStatus,
               PaymentMethod = dto.PaymentMethod,
               TotalAmount = dto.TotalAmount
           });
        }

        public async Task DeleteOrderAsync(int id)
        {
            var order=await _orderRepository.GetByIdAsync(id);
            await _orderRepository.DeleteAsync(order);
        }

        public async Task<List<ResultOrderDto>> GetAllOrdersAsync()
        {
            var orders=await _orderRepository.GetAllAsync();
            return orders.Select(x => new ResultOrderDto
            {
                OrderId = x.OrderId,
                OrderDate = x.OrderDate,
                CustomerId = x.CustomerId,
                Customer = x.Customer,
                BillingAddress = x.BillingAddress,
                ShippingAddress = x.ShippingAddress,
                OrderItems = x.OrderItems,
                OrderStatus = x.OrderStatus,
                PaymentMethod = x.PaymentMethod,
                TotalAmount=x.TotalAmount
            }).ToList();
        }

        public async Task<GetByIdOrderDto> GetByIdOrderAsync(int id)
        {
            var order= await _orderRepository.GetByIdAsync(id);
            var newOrder = new GetByIdOrderDto
            {
                OrderId = order.OrderId,
                OrderDate = order.OrderDate,
                CustomerId = order.CustomerId,
                Customer = order.Customer,
                BillingAddress = order.BillingAddress,
                ShippingAddress = order.ShippingAddress,
                OrderItems = order.OrderItems,
                OrderStatus = order.OrderStatus,
                PaymentMethod = order.PaymentMethod,
                TotalAmount = order.TotalAmount
            };
            return newOrder;
        }

        public async Task UpdateOrderAsync(UpdateOrderDto dto)
        {
            var order=await _orderRepository.GetByIdAsync(dto.OrderId);
            order.OrderDate = dto.OrderDate;
            order.CustomerId = dto.CustomerId;
            order.Customer = dto.Customer;
            order.BillingAddress = dto.BillingAddress;
            order.ShippingAddress = dto.ShippingAddress;
            order.OrderItems = dto.OrderItems;
            order.OrderStatus = dto.OrderStatus;
            order.PaymentMethod = dto.PaymentMethod;
            order.TotalAmount = dto.TotalAmount;

            await _orderRepository.UpdateAsync(order);
        }
    }
}
