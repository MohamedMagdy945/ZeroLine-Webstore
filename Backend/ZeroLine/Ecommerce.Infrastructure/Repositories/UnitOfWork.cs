
using AutoMapper;
using ZeroLine.API.Services;
using ZeroLine.Core.Interfaces;
using ZeroLine.Infrastructure.Data;

namespace ZeroLine.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IImageManagmentService _imageManagmentService;
        private ICategoryRepository _categoryRepository;
        private IProductRepository _productRepository;
        private IPhotoRepository _photoRepository;
        public UnitOfWork(AppDbContext context, IImageManagmentService imageManagmentService, IMapper mapper)
        {
            _context = context;
            _imageManagmentService = imageManagmentService;
            _mapper = mapper;
        }

        public ICategoryRepository CategoryRepository
            => _categoryRepository ??= new CategoryRepository(_context);

        public IProductRepository ProductRepository
            => _productRepository ??= new ProductRepository(_context, _mapper, _imageManagmentService);

        public IPhotoRepository PhotoRepository
            => _photoRepository ??= new PhotoRepository(_context);
    }
}
