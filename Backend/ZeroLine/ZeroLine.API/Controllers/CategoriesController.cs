using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ZeroLine.API.Helper;
using ZeroLine.Core.DTO;
using ZeroLine.Core.Entities.Product;
using ZeroLine.Core.Interfaces;

namespace ZeroLine.API.Controllers
{
    public class CategoriesController : BaseController
    {
        public CategoriesController(IUnitOfWork unOfWork, IMapper mapper) : base(unOfWork, mapper)
        {
        }
        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var categories = await unOfWork.CategoryRepository.GetAllAsync();
                if (categories is null)
                {
                    return BadRequest(new ResponseAPI(400,"No Categorys Exsists"));
                }
                return Ok(categories);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("get-by-id/{id}")]
        public async Task<IActionResult> getById(int id)
        {
            try
            {
                var catgeroy = await unOfWork.CategoryRepository.GetByIdAsync(id);
                if (catgeroy is null)
                {
                    return BadRequest(new ResponseAPI(400, $"not found category id= {id}"));
                }
                return Ok(catgeroy);
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("add-category")]
        public async Task<IActionResult> add(CategoryDto categoryDto)
        {
            try
            {
                var category = mapper.Map<Category>(categoryDto);
                await unOfWork.CategoryRepository.AddAsync(category);
                return Ok(new ResponseAPI(200 , "Item has been Added"));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("update-category")]
        public async Task<IActionResult> update(UpdateCategoryDto categoryDto)
        {
            try
            {
                var categoryExisits = await unOfWork.CategoryRepository.GetByIdAsync(categoryDto.Id);
                if (categoryExisits is null)
                {
                    return BadRequest("Category not found.");
                }
                var category = mapper.Map<Category>(categoryDto);

                await unOfWork.CategoryRepository.UpdateAsync(category);

                return Ok(new ResponseAPI(200, "Item has been Updated"));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("delete-category/{id}")]
        public async Task<IActionResult> delete(int id)
        {
            try
            {
                var category = await unOfWork.CategoryRepository.GetByIdAsync(id);
                if (category is null)
                {
                    return BadRequest("Category not found.");
                }
                await unOfWork.CategoryRepository.DeleteAsync(id);
                return Ok(new ResponseAPI(200, "Item has been Deleted"));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
