using Shoper.Application.Dtos.CustomerDtos;
using Shoper.Application.Dtos.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoper.Application.Usecasess.ProductServices
{
    public interface IProductServices
    {
        Task<List<ResultProductDto>> GetAllProductAsync();
        Task<GetByIdProductDto> GetByIdProductAsync(int id);
        Task CreateProductAsync(CreateProductDto dto);
        Task UpdateProductAsync(UpdateProductDto dto);
        Task DeleteProductAsync(int id);
    }
}
