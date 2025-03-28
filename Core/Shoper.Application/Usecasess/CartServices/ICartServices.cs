using Shoper.Application.Dtos.CartDtos;
using Shoper.Application.Dtos.CategoryDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoper.Application.Usecasess.CartServices
{
    public interface ICartServices
    {
        Task<List<ResultCartDto>> GetAllCartAsync();
        Task<GetByIdCartDto> GetByIdCartAsync(int id);
        Task CreateCartAsync(CreateCartDto dto);
        Task UpdateCartAsync(UpdateCartDto dto);
        Task DeleteCartAsync(int id);
    }
}
