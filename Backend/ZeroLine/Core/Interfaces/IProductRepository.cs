
using ZeroLine.Core.DTO;
using ZeroLine.Core.Entities.Product;

namespace ZeroLine.Core.Interfaces
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        Task<bool> AddAsync(AddProductDto productDto);
        Task<bool> UpdateAsync(UpdateProductDto productDto);
    }   
}
