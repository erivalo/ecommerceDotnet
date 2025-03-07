using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Product.Service.ApiModels;

namespace Product.Service.Infrastructure.Data.EntityFramework;

internal class ProductContext : DbContext, IProductStore
{
  public ProductContext(DbContextOptions<ProductContext> options)
    : base(options)
  {
  }

  public DbSet<ApiModels.Product> Products { get; set; }
  public DbSet<ProductType> ProductTypes { get; set; }

  public async Task CreateProduct(ApiModels.Product product)
  {
    Products.Add(product);
    await SaveChangesAsync();
  }

  public async Task<ApiModels.Product?> GetById(int id)
  {
    return await Products
      .Include(p => p.ProductType)
      .FirstOrDefaultAsync(p => p.Id == id);
  }

  public async Task UpdateProduct(ApiModels.Product product)
  {
    var existingProduct = await FindAsync<ApiModels.Product>(product.Id);

    if (existingProduct is not null)
    {
      existingProduct.Name = product.Name;
      existingProduct.Price = product.Price;
      existingProduct.Description = product.Description;

      await SaveChangesAsync();
    }
  }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.ApplyConfiguration(new ProductConfiguration());
    modelBuilder.ApplyConfiguration(new ProductTypeConfiguration());
  }
}