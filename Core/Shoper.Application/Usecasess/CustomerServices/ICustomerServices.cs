using Shoper.Application.Dtos.CustomerDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoper.Application.Usecasess.CustomerServices
{
    public interface ICustomerServices
    {
        Task<List<ResultCustomerDto>> GetAllCustomerAsync();
        Task<GetByIdCustomerDto> GetByIdCustomerAsync(int id);
        Task CreateCustomerAsync(CreateCustomerDto dto);
        Task UpdateCustomerAsync(UpdateCustomerDto dto);
        Task DeleteCustomerAsync(int id);
    }
}
