using System.Text;
using Basket.Service.ApiModels;
using Basket.Service.Endpoints;
using Basket.Service.Infrastructure.Data;
using Basket.Service.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Caching.Distributed;
using Moq;

namespace Basket.Tests.Endpoints;
public class BasketApiEndpointTests
{
  private readonly Mock<IBasketStore> _basketStore;
  private readonly Mock<IDistributedCache> _cache;

  public BasketApiEndpointTests()
  {
    _basketStore = new Mock<IBasketStore>();
    _cache = new Mock<IDistributedCache>();
  }

  [Fact]
  public async Task GivenExistingBasket_WhenCallingGetBasket_ThenReturnsBasket()
  {
    // Arrange
    const string customerId = "1";
    var customerBasket = new CustomerBasket { CustomerId = customerId };
    _basketStore
      .Setup(store => store.GetBasketByCustomerId(customerId))
      .ReturnsAsync(customerBasket);
    // Act
    var result = await BasketApiEndpoints.GetBasket(_basketStore.Object, customerId);
    // Assert
    Assert.NotNull(result);
    Assert.Equal(customerId, result.CustomerId);
  }

  [Fact]
  public async Task GivenNewBasketRequest_WhenCallingCreateBasket_ThenReturnsCreatedResult()
  {
    // Arrange
    const string customerId = "1";
    const string productId = "1";
    var createBasketRequest = new CreateBasketRequest(productId, "Test Name");
    _cache
      .Setup(cache => cache.GetAsync(productId, It.IsAny<CancellationToken>()))
      .ReturnsAsync(Encoding.UTF8.GetBytes("1.00"));
    // Act
    var result = await BasketApiEndpoints.CreateBasket(_basketStore.Object, _cache.Object,
        customerId, createBasketRequest);
    // Assert
    Assert.NotNull(result);
    var createdResult = (Created)result;
    Assert.NotNull(createdResult);
  }

  [Fact]
  public async Task GivenExistingBasket_WhenCallingAddBasketProduct_ThenReturnsNoContentResult()
  {
    // Arrange
    const string customerId = "1";
    var customerBasket = new CustomerBasket { CustomerId = customerId };
    _basketStore
      .Setup(store => store.GetBasketByCustomerId(customerId))
      .ReturnsAsync(customerBasket);

    const string productId = "1";
    const int quantity = 2;
    _cache
      .Setup(c => c.GetAsync(productId, It.IsAny<CancellationToken>()))
      .ReturnsAsync(Encoding.UTF8.GetBytes("1.00"));
    var addBasketProductRequest = new AddBasketProductRequest(productId, "Test Name", quantity);
    // Act
    var result = await BasketApiEndpoints.AddBasketProduct(_basketStore.Object, _cache.Object,
        customerId, addBasketProductRequest);
    // Assert
    Assert.NotNull(result);
    var noContentResult = (NoContent)result;
    Assert.NotNull(noContentResult);
  }

  [Fact]
  public async Task GivenExistingBasketWithProducts_WhenCallingDeleteBasketProduct_ThenReturnsNoContentResult()
  {
    // Arrange
    const string customerId = "1";
    const string productId = "1";
    var customerBasket = new CustomerBasket { CustomerId = customerId };

    customerBasket.AddBasketProduct(new BasketProduct(productId, "Test Name", 9.99m));

    _basketStore
      .Setup(store => store.GetBasketByCustomerId(customerId))
      .ReturnsAsync(customerBasket);

    // Act
    var result = await BasketApiEndpoints.DeleteBasketProduct(_basketStore.Object, customerId, productId);

    // Assert
    Assert.NotNull(result);

    var noContentResult = (NoContent)result;
    Assert.NotNull(noContentResult);
  }

  [Fact]
  public async Task GivenExistingBasket_WhenCallingDeleteBasket_ThenReturnsNoContentResult()
  {
    // Arrange
    const string customerId = "1";
    var customerBasket = new CustomerBasket { CustomerId = customerId };

    _basketStore
      .Setup(store => store.GetBasketByCustomerId(customerId))
      .ReturnsAsync(customerBasket);

    // Act
    var result = await BasketApiEndpoints.DeleteBasket(_basketStore.Object, customerId);

    // Assert
    Assert.NotNull(result);

    var noContentResult = (NoContent)result;
    Assert.NotNull(noContentResult);
  }
}