
using Microsoft.EntityFrameworkCore;
using ZeroLine.Core.Entities.Product;

namespace ZeroLine.Infrastructure.Data.Config
{
    public class PhotoConfiguration : IEntityTypeConfiguration<Photo>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Photo> builder)
        {
            builder.HasData(new Photo {  Id =1, ImageName = "product-3.jpg", ProductId = 1 });
        }
    }
}
