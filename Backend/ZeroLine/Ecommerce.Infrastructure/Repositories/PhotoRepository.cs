
using Microsoft.EntityFrameworkCore;
using ZeroLine.Core.Entities.Product;
using ZeroLine.Core.Interfaces;

namespace ZeroLine.Infrastructure.Repositories
{
    public class PhotoRepository : GenericRepository<Photo>, IPhotoRepository
    {
        public PhotoRepository(DbContext context) : base(context)
        {
        }
    }
}
