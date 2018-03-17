using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sales.Models;

namespace Trips.Persistence.Sales.EntityConfigurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products", "dbo").HasKey(c => c.Id);
            builder.Property(p => p.Name).IsRequired().HasMaxLength(255);
            builder.Property(p => p.Category).IsRequired().HasMaxLength(255);
            builder.Property(p => p.ImageURL).HasMaxLength(1024);
            builder.Property(p => p.ThumbnailURL).HasMaxLength(1024);
        }
    }
}