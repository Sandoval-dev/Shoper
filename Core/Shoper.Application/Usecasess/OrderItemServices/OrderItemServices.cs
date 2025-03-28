using Shoper.Application.Dtos.OrderItemDtos;
using Shoper.Application.Interfaces;
using Shoper.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoper.Application.Usecasess.OrderItemServices
{
    public class OrderItemServices : IOrderItemServices
    {
        private readonly IRepository<OrderItem> _orderItemRepsitory;

        public OrderItemServices(IRepository<OrderItem> orderItemRepsitory)
        {
            _orderItemRepsitory = orderItemRepsitory;
        }
        public Task CreateOrderItemAsync(CreateOrderItemDto dto)
        {
            var orderItem = new OrderItem
            {
                TotalPrice = dto.TotalPrice,
                ProductId = dto.ProductId,
                Quantity = dto.Quantity,
                //OrderId = dto.OrderId
            };
            return _orderItemRepsitory.CreateAsync(orderItem);
        }

        public async Task DeleteOrderItemAsync(int id)
        {
            var orderItem=await _orderItemRepsitory.GetByIdAsync(id);
            await _orderItemRepsitory.DeleteAsync(orderItem);
        }

        public async Task<List<ResultOrderItemDto>> GetAllOrderItemAsync()
        {
            var orderItems= await _orderItemRepsitory.GetAllAsync();
            return orderItems.Select(x => new ResultOrderItemDto
            {
                OrderItemId = x.OrderItemId,
                ProductId = x.ProductId,
                Quantity = x.Quantity,
                OrderId = x.OrderId
            }).ToList();
        }

        public async Task<GetByIdOrderItemDto> GetByIdOrderItemAsync(int id)
        {
            var orderItem= await _orderItemRepsitory.GetByIdAsync(id);
            var newOrderItem = new GetByIdOrderItemDto
            {
                OrderItemId = orderItem.OrderItemId,
                ProductId = orderItem.ProductId,
                Quantity = orderItem.Quantity,
                OrderId = orderItem.OrderId
            };
            return newOrderItem;
        }

        public async Task UpdateOrderItemAsync(UpdateOrderItemDto dto)
        {
            var orderItem= await _orderItemRepsitory.GetByIdAsync(dto.OrderItemId);
            orderItem.ProductId = dto.ProductId;
            orderItem.Quantity = dto.Quantity;
            //orderItem.OrderId = dto.OrderId;
            orderItem.TotalPrice = dto.TotalPrice;

            await _orderItemRepsitory.UpdateAsync(orderItem);
        }
    }
}
