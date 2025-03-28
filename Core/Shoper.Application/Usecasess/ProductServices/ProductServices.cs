using Shoper.Application.Dtos.Product;
using Shoper.Application.Interfaces;
using Shoper.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoper.Application.Usecasess.ProductServices
{
    public class ProductServices : IProductServices
    {
        private readonly IRepository<Products> _productsRepository;

        public ProductServices(IRepository<Products> productsRepository)
        {
            _productsRepository = productsRepository;
        }
        public async Task CreateProductAsync(CreateProductDto dto)
        {
            await _productsRepository.CreateAsync(new Products
            {
                ProductName = dto.ProductName,
                Description = dto.Description,
                ImageUrl = dto.ImageUrl,
                CategoryId = dto.CategoryId,
                Stock = dto.Stock,
                Price = dto.Price,
            });
        }

        public async Task DeleteProductAsync(int id)
        {
            var product =await _productsRepository.GetByIdAsync(id);
            await _productsRepository.DeleteAsync(product);
        }

        public async Task<List<ResultProductDto>> GetAllProductAsync()
        {
            var products=await _productsRepository.GetAllAsync();
            return products.Select(x => new ResultProductDto
            {
                Id = x.Id,
                Description = x.Description,
                ImageUrl = x.ImageUrl,
                CategoryId = x.CategoryId,
                Stock = x.Stock,
                ProductName = x.ProductName,
                Price = x.Price,
            }).ToList();
        }

        public async Task<GetByIdProductDto> GetByIdProductAsync(int id)
        {
           var product=await _productsRepository.GetByIdAsync(id);
            var newProduct = new GetByIdProductDto
            {
                Description = product.Description,
                ImageUrl = product.ImageUrl,
                CategoryId = product.CategoryId,
                Stock = product.Stock,
                ProductName = product.ProductName,
                Price = product.Price,
            };
            return newProduct;
        }

        public async Task UpdateProductAsync(UpdateProductDto dto)
        {
            var product = await _productsRepository.GetByIdAsync(dto.Id);
            product.ProductName = dto.ProductName;
            product.Description = dto.Description;
            product.ImageUrl = dto.ImageUrl;
            product.CategoryId = dto.CategoryId;
            product.Stock = dto.Stock;
            product.Price = dto.Price;

            await _productsRepository.UpdateAsync(product);
        }
    }
}
