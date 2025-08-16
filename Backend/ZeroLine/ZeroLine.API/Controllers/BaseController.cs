using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ZeroLine.Core.Interfaces;

namespace ZeroLine.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        protected readonly IUnitOfWork unOfWork;
        protected readonly IMapper mapper;
        public BaseController(IUnitOfWork unOfWork, IMapper mapper)
        {
            this.unOfWork = unOfWork;
            this.mapper = mapper;
        }
    }
}
