using Shoper.Application.Dtos.CartDtos;
using Shoper.Application.Dtos.CartItemDtos;
using Shoper.Application.Dtos.CustomerDtos;
using Shoper.Application.Dtos.Product;
using Shoper.Application.Interfaces;
using Shoper.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoper.Application.Usecasess.CartServices
{
    public class CartServices : ICartServices
    {
        private readonly IRepository<Cart> _cartRepository;
        private readonly IRepository<CartItem> _cartItemRepository;
        private readonly IRepository<Customer> _customerRepository;
        private readonly IRepository<Products> _productsRepository;

        public CartServices(IRepository<Cart> cartRepository, IRepository<CartItem> cartItemRepository, IRepository<Customer> customerRepository, IRepository<Products> productRepository)
        {
            _cartRepository = cartRepository;
            _cartItemRepository = cartItemRepository;
            _customerRepository = customerRepository;
            _productsRepository = productRepository;
        }
        public async Task CreateCartAsync(CreateCartDto dto)
        {
            var cart=new Cart
            {

                CreatedDate = DateTime.Now,
                CustomerId = dto.CustomerId,
                //TotalAmount = dto.TotalAmount
            };

            await _cartRepository.CreateAsync(cart);
            var sum = 0;
            foreach (var item in dto.CartItems)
            {
                var  cartItem=new CartItem 
                {   CartId = cart.CartId,
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    TotalPrice = item.TotalPrice
                };
                sum= sum + (item.TotalPrice);
                await _cartItemRepository.CreateAsync(cartItem);
            }
            cart.TotalAmount = sum;
            await _cartRepository.UpdateAsync(cart);
        }

        public async Task DeleteCartAsync(int id)
        {
            var cart= await _cartRepository.GetByIdAsync(id);
            var catItems=await _cartItemRepository.GetAllAsync();
            foreach (var item in catItems)
            {
                if (item.CartId == id)
                {
                    var cartItem= await _cartItemRepository.GetByIdAsync(item.CartItemId);
                    await _cartItemRepository.DeleteAsync(cartItem);
                }
            }
            await _cartRepository.DeleteAsync(cart);
        }

        public async Task<List<ResultCartDto>> GetAllCartAsync()
        {
            var carts=await _cartRepository.GetAllAsync();
            var cartItems = await _cartItemRepository.GetAllAsync();
            var product=await _productsRepository.GetAllAsync();
            var result=new List<ResultCartDto>();
            foreach (var x in carts)
            {
                var customerDto = await _customerRepository.GetByFilterAsync(cus => cus.CustomerId == x.CustomerId);
                var cartDto= new ResultCartDto
                {
                    CartId = x.CartId,
                    CreatedDate = x.CreatedDate,
                    CustomerId = x.CustomerId,
                    Customer = customerDto,
                    TotalAmount = x.TotalAmount,
                    CartItems = new List<ResultCartItemDto>()
                };
                foreach (var item in x.CartItems)
                {
                    var productDto=await _productsRepository.GetByFilterAsync(p => p.Id == item.ProductId);
                    var cartItemDto = new ResultCartItemDto
                    {
                        CartId = item.CartId,
                        CartItemId = item.CartItemId,
                        ProductId = item.ProductId,
                        Products = productDto,
                        Quantity = item.Quantity,
                        TotalPrice = item.TotalPrice
                    };
                    cartDto.CartItems.Add(cartItemDto);
                }
                result.Add(cartDto);
            }
            return result;
        }

        public async Task<GetByIdCartDto> GetByIdCartAsync(int id)
        {
            var cart = await _cartRepository.GetByIdAsync(id);
            var cartItem=await _cartItemRepository.GetAllAsync();
            var customer = await _customerRepository.GetByIdAsync(cart.CustomerId);

            var result = new GetByIdCartDto
            {
                CartId = cart.CartId,
                CartItems=new List<ResultCartItemDto>(),
                CreatedDate = cart.CreatedDate,
                CustomerId = cart.CustomerId,
                Customer=customer
            };
            foreach (var item in cart.CartItems)
            {
                var productDto = await _productsRepository.GetByFilterAsync(p => p.Id == item.ProductId);
                var cartItemDto = new ResultCartItemDto
                {
                    CartId = item.CartId,
                    CartItemId = item.CartItemId,
                    ProductId = item.ProductId,
                    Products = productDto,
                    Quantity = item.Quantity,
                    TotalPrice = item.TotalPrice
                };
                result.CartItems.Add(cartItemDto);
            }
            return result;
        }

        public async Task UpdateCartAsync(UpdateCartDto dto)
        {
            var cart = await _cartRepository.GetByIdAsync(dto.CartId);
            var cartItems = await _cartItemRepository.GetAllAsync();
            //cart.CreatedDate = dto.CreatedDate;
            //cart.CustomerId = dto.CustomerId;
            //cart.TotalAmount = dto.TotalAmount;
            var sum = 0;
            foreach (var item in cart.CartItems)
            {
                foreach (var item1 in dto.CartItems)
                {

                    var cartItem = await _cartItemRepository.GetByIdAsync(item.CartItemId);
                    if (item.CartItemId == item1.CartItemId)
                    {
                        cartItem.Quantity = item1.Quantity;
                        cartItem.TotalPrice = item1.TotalPrice;
                    }
                    sum = sum + (item1.TotalPrice);
                }
                
            }
            cart.TotalAmount = sum;
            await _cartRepository.UpdateAsync(cart);
        }
    }
}
