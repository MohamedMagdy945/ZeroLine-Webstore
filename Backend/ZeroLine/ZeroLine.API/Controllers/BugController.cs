using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ZeroLine.Core.Interfaces;

namespace ZeroLine.API.Controllers
{
    public class BugController : BaseController
    {
        public BugController(IUnitOfWork unOfWork, IMapper mapper) : base(unOfWork, mapper)
        {
        }
        [HttpGet("Not-Found")]
        public async Task<IActionResult> GetNotFound()
        {
            try
            {
                var category = await unOfWork.CategoryRepository.GetByIdAsync(9999);
                if (category == null)
                {
                    return NotFound();
                }
                return Ok(category);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("Server-Error")]
        public async Task<IActionResult> GetServerError()
        {
            var category = await unOfWork.CategoryRepository.GetByIdAsync(9999);
            category.Name = "Updated Name";
            return Ok(category);
        }
        [HttpGet("Bad-Request/{Id}")]
        public async Task<IActionResult> GetBadRequest(int id)
        {

            return Ok();
        }

        [HttpGet("Bad-Request/")]
        public async Task<IActionResult> GetBadRequest()
        {
            return BadRequest();
        }
    }

}
