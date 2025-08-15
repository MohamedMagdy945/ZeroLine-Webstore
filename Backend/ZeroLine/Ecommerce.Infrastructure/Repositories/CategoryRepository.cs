using ZeroLine.Core.Entities.Product;
using ZeroLine.Core.Interfaces;
using ZeroLine.Infrastructure.Data;

namespace ZeroLine.Infrastructure.Repositories
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(AppDbContext context) : base(context)
        {
        }
    }
}
