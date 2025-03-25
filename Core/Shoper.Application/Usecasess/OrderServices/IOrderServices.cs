using Shoper.Application.Dtos.CustomerDtos;
using Shoper.Application.Dtos.OrderDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoper.Application.Usecasess.OrderServices
{
    public interface IOrderServices
    {
        Task<List<ResultOrderDto>> GetAllOrdersAsync();
        Task<GetByIdOrderDto> GetByIdOrderAsync(int id);
        Task CreateOrderAsync(CreateOrderDto dto);
        Task UpdateOrderAsync(UpdateOrderDto dto);
        Task DeleteOrderAsync(int id);
    }
}
