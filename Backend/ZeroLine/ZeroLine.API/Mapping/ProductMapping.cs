using AutoMapper;
using ZeroLine.Core.DTO;
using ZeroLine.Core.Entities.Product;

namespace ZeroLine.API.Mapping
{
    public class ProductMapping : Profile
    {
        public ProductMapping()
        {
            CreateMap<Product, ProductDto>().ForMember(x => x.CategoryName , op =>
            {
                op.MapFrom(src => src.Category.Name);
            }).ReverseMap();
            CreateMap<Photo, PhotoDto>().ReverseMap();
            CreateMap<AddProductDto, Product>()
                .ForMember(dest => dest.Photos, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<UpdateProductDto, Product>()
              .ForMember(dest => dest.Photos, opt => opt.Ignore())
              .ReverseMap();
        } 
    }
}
