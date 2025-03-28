using Shoper.Application.Dtos.OrderDtos;
using Shoper.Application.Dtos.OrderItemDtos;
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
        private readonly IRepository<OrderItem> _orderItemRepository;
        private readonly IRepository<Customer> _customerRepository;
        private readonly IRepository<Products> _productRepository;

        public OrderServices(IRepository<Order> orderRepository, IRepository<OrderItem> orderItemRepository, IRepository<Customer> customerRepository, IRepository<Products> productRepository)
        {
            _orderRepository = orderRepository;
            _orderItemRepository = orderItemRepository;
            _customerRepository = customerRepository;
            _productRepository = productRepository;
        }

        public async Task CreateOrderAsync(CreateOrderDto dto)
        {
            decimal sum = 0;
            var order = new Order
            {
                OrderDate = dto.OrderDate,
                CustomerId = dto.CustomerId,
                //Customer = dto.Customer,
                //BillingAddress = dto.BillingAddress,
                ShippingAddress = dto.ShippingAddress,
                //OrderItems = dto.OrderItems,
                OrderStatus = dto.OrderStatus,
                //PaymentMethod = dto.PaymentMethod,
                TotalAmount = sum
            };

            await _orderRepository.CreateAsync(order);

            foreach (var item in dto.OrderItems)
            {

                await _orderItemRepository.CreateAsync(new OrderItem
                {
                    OrderId = order.OrderId,
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    TotalPrice = item.TotalPrice
                });
                sum=sum + item.TotalPrice;
            }
            order.TotalAmount = sum;
            await _orderRepository.UpdateAsync(order);
        }

        public async Task DeleteOrderAsync(int id)
        {
            var order=await _orderRepository.GetByIdAsync(id);
            foreach (var item in order.OrderItems)
            {
                var orderItems=await _orderItemRepository.GetByIdAsync(item.OrderItemId);
                await _orderItemRepository.DeleteAsync(orderItems);
            }
            await _orderRepository.DeleteAsync(order);
        }

        public async Task<List<ResultOrderDto>> GetAllOrdersAsync()
        {
            var orders=await _orderRepository.GetAllAsync();
            var orderitem= await _orderItemRepository.GetAllAsync();
            var result= new List<ResultOrderDto>();

            foreach (var item in orders)
            {
                var orderCustomer=await _customerRepository.GetByIdAsync(item.CustomerId);
                var orderDto = new ResultOrderDto
                {
                    OrderId = item.OrderId,
                    OrderDate = item.OrderDate,
                    TotalAmount = item.TotalAmount,
                    OrderStatus = item.OrderStatus,
                    //BillingAddress = x.BillingAddress,
                    ShippingAddress = item.ShippingAddress,
                    OrderItems = new List<ResultOrderItemDto>(),
                    //PaymentMethod = x.PaymentMe,
                    CustomerId = item.CustomerId,
                    Customer = orderCustomer,
                };

                foreach (var item1 in orderitem)
                {
                    var orderItemProduct=await _productRepository.GetByIdAsync(item1.ProductId);
                    var orderItemDto= new ResultOrderItemDto
                    {
                        OrderId = item.OrderId,
                        OrderItemId = item1.OrderItemId,
                        ProductId = item1.ProductId,
                        Quantity = item1.Quantity,
                        TotalPrice = item1.TotalPrice,
                        Product = orderItemProduct

                    };
                    orderDto.OrderItems.Add(orderItemDto);
                }
                result.Add(orderDto);
            }
            return result;
        }

        public async Task<GetByIdOrderDto> GetByIdOrderAsync(int id)
        {
            var order= await _orderRepository.GetByIdAsync(id);
            var orderCustomer=await _customerRepository.GetByIdAsync(order.CustomerId);
            var newOrder = new GetByIdOrderDto
            {
                OrderId = order.OrderId,
                OrderDate = order.OrderDate,
                CustomerId = order.CustomerId,
                //Customer = order.Customer,
                //BillingAddress = order.BillingAddress,
                ShippingAddress = order.ShippingAddress,
                //OrderItems = order.OrderItems,
                OrderStatus = order.OrderStatus,
                //PaymentMethod = order.PaymentMethod,
                TotalAmount = order.TotalAmount,
                Customer = orderCustomer,
                OrderItems = new List<ResultOrderItemDto>()
            };
            foreach (var item in newOrder.OrderItems)
            {
                var orderItemProduct = await _productRepository.GetByIdAsync(item.ProductId);
                var orderItemDto = new ResultOrderItemDto
                {
                    OrderId = item.OrderId,
                    OrderItemId = item.OrderItemId,
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    TotalPrice = item.TotalPrice,
                    Product = orderItemProduct
                };
                newOrder.OrderItems.Add(orderItemDto);
            }
            return newOrder;
        }

        public async Task UpdateOrderAsync(UpdateOrderDto dto)
        {
            var order=await _orderRepository.GetByIdAsync(dto.OrderId);
            var orderItems=await _orderItemRepository.GetAllAsync();
            order.OrderStatus = dto.OrderStatus;
            decimal sum = 0;
            foreach (var item in dto.OrderItems)
            {
                foreach (var item1 in order.OrderItems)
                {
                    var orderItemdto = await _orderItemRepository.GetByIdAsync(item1.OrderItemId);
                    if (item.OrderItemId==item1.OrderItemId)
                    {
                        orderItemdto.Quantity = item.Quantity;
                        orderItemdto.TotalPrice = item.TotalPrice;  
                    }
                    sum = sum + item1.TotalPrice;
                }

            }
            order.TotalAmount = sum;

            await _orderRepository.UpdateAsync(order);
        }
    }
}
