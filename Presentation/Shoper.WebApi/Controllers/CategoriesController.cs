using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shoper.Application.Dtos.CategoryDtos;
using Shoper.Application.Usecasess.CategoryServices;

namespace Shoper.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryServices _categoryServices;

        public CategoriesController(ICategoryServices categoryServices)
        {
            _categoryServices = categoryServices;
        }

        [HttpGet("getallcategories")]
        public async Task<IActionResult> GetAllCategories()
        {
            var categories=await _categoryServices.GetAllCategoryAsync();
            return Ok(categories);
        }

        [HttpGet("getbyidcategory")]
        public async Task<IActionResult> GetByIdCategory(int id) 
        {
            var category=await _categoryServices.GetByIdCategoryAsync(id);
            return Ok(category);
        }

        [HttpPost("createcategory")]
        public async Task<IActionResult> CreateCategory(CreateCategoryDto dto)
        {
            await _categoryServices.CreateCategoryAsync(dto);
            return Ok("Create process successful");
        }

        [HttpPut("updatecategory")]
        public async Task<IActionResult> UpdateCategory(UpdateCategoryDto dto) 
        {
            await _categoryServices.UpdateCategoryAsync(dto);
            return Ok("Update process successful");
        }

        [HttpDelete("deletecategory")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            await _categoryServices.DeleteCategoryAsync(id);
            return Ok("Delete process successful");
        }
    }
}
