using Shoper.Application.Dtos.CartItemDtos;
using Shoper.Application.Interfaces;
using Shoper.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoper.Application.Usecasess.CartItemServices
{
    public class CartItemServices : ICartItemServices
    {
        private readonly IRepository<CartItem> _caritemRepository;

        public CartItemServices(IRepository<CartItem> caritemRepository)
        {
            _caritemRepository = caritemRepository;
        }
        public Task CreateCartItemAsync(CreateCartItemDto dto)
        {
            var cartItem= new CartItem
            {
                ProductId = dto.ProductId,
                Quantity = dto.Quantity,
                //CartId = dto.CartId,
                TotalPrice = dto.TotalPrice
            };
            return _caritemRepository.CreateAsync(cartItem);
        }

        public async Task DeleteCartItemAsync(int id)
        {
            var cartItem=await _caritemRepository.GetByIdAsync(id);
            await _caritemRepository.DeleteAsync(cartItem);
        }

        public async Task<List<ResultCartItemDto>> GetAllCartItemAsync()
        {
            var cartItems = await _caritemRepository.GetAllAsync();
            return cartItems.Select(x => new ResultCartItemDto { CartItemId = x.CartItemId,
                ProductId = x.ProductId,
                Quantity = x.Quantity,
                CartId = x.CartId, 
                TotalPrice = x.TotalPrice }).ToList();
        }

        public Task<List<ResultCartItemDto>> GetByCartIdCartItemAsync(int cartId)
        {
            return null;
        }

        public async Task<GetByIdCartItemDto> GetByIdCartItemAsync(int id)
        {
            var cartItem=await _caritemRepository.GetByIdAsync(id);
            var newCartItem = new GetByIdCartItemDto { CartItemId = cartItem.CartItemId,
                ProductId = cartItem.ProductId,
                Quantity = cartItem.Quantity,
                CartId = cartItem.CartId,
                TotalPrice = cartItem.TotalPrice 
            };
            return newCartItem;
        }

        public async Task UpdateCartItemAsync(UpdateCartItemDto dto)
        {
            var cartItem=await _caritemRepository.GetByIdAsync(dto.CartItemId);
            cartItem.Quantity = dto.Quantity;
            //cartItem.CartId = dto.CartId;
            //cartItem.TotalPrice = dto.TotalPrice;
            cartItem.CartItemId = dto.CartItemId;
            cartItem.ProductId = dto.ProductId;
            await _caritemRepository.UpdateAsync(cartItem);
        }
    }
}
