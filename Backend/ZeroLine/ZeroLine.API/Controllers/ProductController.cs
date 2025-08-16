using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ZeroLine.API.Helper;
using ZeroLine.Core.DTO;
using ZeroLine.Core.Interfaces;

namespace ZeroLine.API.Controllers
{

    public class ProductController : BaseController
    {
        public ProductController(IUnitOfWork unOfWork, IMapper mapper) : base(unOfWork, mapper)
        {
        }

        [HttpGet("Get-All")]
        public async Task<IActionResult> get()
        {
            try
            {
                var Product = await unOfWork.ProductRepository
                    .GetAllAsync(p => p.Category, p => p.Photos);

                var result = mapper.Map<List<ProductDto>>(Product);

                if (Product is null)
                {
                    return BadRequest(new ResponseAPI(400, "No Products Exist"));
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("Get-By-Id/{id}")]
        public async Task<IActionResult> getById(int id)
        {
            try
            {
                var product = unOfWork.ProductRepository.GetByIdAsync(id, p => p.Category, p => p.Photos);
                if (product is null) return BadRequest(new ResponseAPI(400, $"not found product id= {id}"));
                var result = mapper.Map<ProductDto>(product);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("Add-Product")]
        public async Task<IActionResult> add(AddProductDto productDto)
        {
            try
            {
                await unOfWork.ProductRepository.AddAsync(productDto);
                return Ok(new ResponseAPI(200, "Product Added Successfully"));
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseAPI(400,ex.Message));
            }
        }

        [HttpPut("Update-Product")]
        public async Task<IActionResult> Update(UpdateProductDto productDto)
        {
            try
            {
                await unOfWork.ProductRepository.UpdateAsync(productDto);
                return Ok(new ResponseAPI(200, "Product Updated Successfully"));
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseAPI(400, ex.Message));
            }
        }
        [HttpDelete("Delete-Product/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var product = await unOfWork.ProductRepository.GetByIdAsync(id);
                if (product is null) return BadRequest(new ResponseAPI(400, $"not found product id= {id}"));
                await unOfWork.ProductRepository.DeleteAsync(product);
                return Ok(new ResponseAPI(200, "Product Deleted Successfully"));
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseAPI(400, ex.Message));
            }
        }

    }
}
