using Shoper.Application.Dtos.CartDtos;
using Shoper.Application.Dtos.CartItemDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoper.Application.Usecasess.CartItemServices
{
    public interface ICartItemServices
    {
        Task<List<ResultCartItemDto>> GetAllCartItemAsync();
        Task<GetByIdCartItemDto> GetByIdCartItemAsync(int id);
        Task CreateCartItemAsync(CreateCartItemDto dto);
        Task UpdateCartItemAsync(UpdateCartItemDto dto);
        Task DeleteCartItemAsync(int id);
        Task<List<ResultCartItemDto>> GetByCartIdCartItemAsync(int cartId);

    }
}
