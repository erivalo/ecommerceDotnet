using Basket.Service.Models;

namespace Basket.Service.Infrastructure.Data;
internal interface IBasketStore
{
  Task<CustomerBasket> GetBasketByCustomerId(string CustomerId);
  Task CreateCustomerBasket(CustomerBasket customerBasket);
  Task UpdateCustomerBasket(CustomerBasket customerBasket);
  Task DeleteCustomerBasket(string customerId);
}