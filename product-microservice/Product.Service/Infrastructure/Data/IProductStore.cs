namespace Product.Service.Infrastructure.Data;
internal interface IProductStore
{
  Task<ApiModels.Product?> GetById(int id);

  Task CreateProduct(ApiModels.Product product);

  Task UpdateProduct(ApiModels.Product product);
}