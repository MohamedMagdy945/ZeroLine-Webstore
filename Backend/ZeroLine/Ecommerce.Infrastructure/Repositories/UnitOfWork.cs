
using ZeroLine.Core.Interfaces;
using ZeroLine.Infrastructure.Data;

namespace ZeroLine.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private ICategoryRepository _categoryRepository;
        private IProductRepository _productRepository;
        private IPhotoRepository _photoRepository;
        public UnitOfWork(AppDbContext context)
        {
            _context = context;
 
        }

        public ICategoryRepository CategoryRepository
            => _categoryRepository ??= new CategoryRepository(_context);

        public IProductRepository ProductRepository
            => _productRepository ??= new ProductRepository(_context);

        public IPhotoRepository PhotoRepository
            => _photoRepository ??= new PhotoRepository(_context);
    }
}
