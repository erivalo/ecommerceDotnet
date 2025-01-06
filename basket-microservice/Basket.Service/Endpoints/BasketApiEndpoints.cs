using Basket.Service.Infrastructure.Data;
using Basket.Service.Models;
using Basket.Service.ApiModels;
using Microsoft.AspNetCore.Mvc;

namespace Basket.Service.Endpoints;
public static class BasketApiEndpoints
{
  public static void RegisterEndpoints(this IEndpointRouteBuilder routeBuilder)
  {
    routeBuilder.MapGet("/{customerId}", (
      [FromServices] IBasketStore basketStore,
      string customerId)
        => basketStore.GetBasketByCustomerId(customerId));

    routeBuilder.MapPost("/{customerId}", ([FromServices] IBasketStore basketStore, string customerId, CreateBasketRequest createBasketRequest) =>
    {
      var customerBasket = new CustomerBasket { CustomerId = customerId };

      customerBasket.AddBasketproduct(new BasketProduct(createBasketRequest.ProductId, createBasketRequest.ProductName));
      basketStore.CreateCustomerBasket(customerBasket);

      return TypedResults.Created();
    });

    routeBuilder.MapPut("/{customerId}", ([FromServices] IBasketStore basketStore, string customerId, AddBasketProductRequest addProductRequest) =>
    {
      var customerBasket = basketStore.GetBasketByCustomerId(customerId);

      customerBasket.AddBasketproduct(new BasketProduct(addProductRequest.ProductId, addProductRequest.ProductName, addProductRequest.Quantity));
      basketStore.UpdateCustomerBasket(customerBasket);

      return TypedResults.NoContent();
    });

    routeBuilder.MapDelete("/{customerId}", ([FromServices] IBasketStore basketStore, string customerId) =>
    {
      basketStore.DeleteCustomerBasket(customerId);

      return TypedResults.NoContent();
    });

    routeBuilder.MapDelete("/{customerId}/{productId}", ([FromServices] IBasketStore basketStore, string customerId, string productId) =>
    {
      var customerBasket = basketStore.GetBasketByCustomerId(customerId);

      customerBasket.RemoveBasketProduct(productId);
      basketStore.UpdateCustomerBasket(customerBasket);

      return TypedResults.NoContent();
    });
  }
}