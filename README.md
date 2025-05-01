# dotnetMicroservices

## Steps to tun the app:

### 1 Publish Package

Open Ecommerce.Shared folder at the same level as our project file, and run **dotnet pack**, then push the generate nuget package to the folder where local nuget packages are:

dotnet nuget push ECommerce.Shared.1.1.0.nupkg -s C:/MyLocaFolder

### 2 Build and Run dockers images

We need to build some Docker images:

> docker run -it --rm -p 8001:8080 -e RabbitMq**HostName=host.docker.internal order.service:v3.0
> docker run -it --rm -p 8000:8080 -e RabbitMq**HostName=host.docker.internal basket.service:v4.0

Then run the docker images:

> docker run -it --rm -p 8001:8080 -e RabbitMq**HostName=host.docker.internal order.service:v3.0
> docker run -it --rm -p 8000:8080 -e RabbitMq**HostName=host.docker.internal basket.service:v4.0

Ensure that we have the rabbitmq server running

### 3 Test

Now, let’s hop over to Postman and create a basket for a new customer with ID 789:

```
{
    "ProductId": "3434",
    "ProductName": "New Balance Rebel V3"
}
```

We can go straight to creating our Order for this customer:

```
{
    "orderProducts": [
        {
            "productId": "3434",
            "quantity": 1
        }
    ]
}
```

Finally, we’ll make a GET request to the Basket microservice and verify the basket has been deleted:

```
{
    "products": [],
    "customerId": "789"
}
```
