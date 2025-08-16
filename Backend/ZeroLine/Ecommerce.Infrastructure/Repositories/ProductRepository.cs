
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ZeroLine.API.Services;
using ZeroLine.Core.DTO;
using ZeroLine.Core.Entities.Product;
using ZeroLine.Core.Interfaces;
using ZeroLine.Infrastructure.Data;

namespace ZeroLine.Infrastructure.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        private readonly IMapper mapper;
        private readonly IImageManagmentService imageManagmentService;
        public ProductRepository(AppDbContext context, IMapper mapper, IImageManagmentService imageManagmentService) : base(context)
        {
            this.mapper = mapper;
            this.imageManagmentService = imageManagmentService;
        }

        public async Task<bool> AddAsync(AddProductDto productDto)
        {
            if (productDto == null)
            {
                return false;
            }
            var product = mapper.Map<Product>(productDto);
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();

            var ImagePath = await imageManagmentService.AddImageAsync(productDto.Photos, productDto.Name);

            var photos = ImagePath.Select(photoPath => new Photo
            {
                ProductId = product.Id,
                ImageName = photoPath,
            }).ToList();
            await _context.Photos.AddRangeAsync(photos);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateAsync(UpdateProductDto productDto)
        {
            if (productDto == null)
            {
                return false;
            }
            var findProduct = await _context.Products.Include(m => m.Category)
                .Include(m => m.Photos).FirstOrDefaultAsync( m => m.Id == productDto.Id);
            if (findProduct is null)
                return false;

            var product = mapper.Map<Product>(productDto);

            var FindPhoto = await _context.Photos.Where(m => m.ProductId == product.Id).ToListAsync();
            foreach( var item in FindPhoto)
            {
                await imageManagmentService.DeleteImageAsync(item.ImageName);
            }
            _context.Photos.RemoveRange(FindPhoto);

            var ImagePath = await imageManagmentService.AddImageAsync(productDto.Photos, productDto.Name);
            var photos = ImagePath.Select(photoPath => new Photo
            {
                ProductId = product.Id,
                ImageName = photoPath,
            }).ToList();
            await _context.Photos.AddRangeAsync(photos);
            await _context.SaveChangesAsync();
            return true;


        }
    }
   
}
