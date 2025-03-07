using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Product.Service.ApiModels;

namespace Product.Service.Infrastructure.Data.EntityFramework;
internal class ProductTypeConfiguration : IEntityTypeConfiguration<ProductType>
{
  public void Configure(EntityTypeBuilder<ProductType> builder)
  {
    builder.HasKey(t => t.Id);

    builder.Property(p => p.Type)
        .IsRequired()
        .HasMaxLength(100);
  }
}