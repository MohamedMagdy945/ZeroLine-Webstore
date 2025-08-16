using AutoMapper;
using ZeroLine.Core.DTO;
using ZeroLine.Core.Entities.Product;

namespace ZeroLine.API.Mapping
{
    public class CategoryMapping : Profile
    {
        public CategoryMapping()
        {
            CreateMap<CategoryDto, Category>().ReverseMap();
            CreateMap<UpdateCategoryDto, Category>().ReverseMap();
        }
    }
}
