
using ZeroLine.Core.Entities.Product;
using ZeroLine.Core.Interfaces;
using ZeroLine.Infrastructure.Data;

namespace ZeroLine.Infrastructure.Repositories
{
    internal class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(AppDbContext context) : base(context)
        {
        }
    }
   
}
