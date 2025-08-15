
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ZeroLine.Core.Entities.Product;

namespace ZeroLine.Infrastructure.Data.Config
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(30);
            builder.Property(x => x.Id).IsRequired();

            builder.HasData(new Category { Id = 1, Name = "test" , Description = "test" });
        }
    }
}
