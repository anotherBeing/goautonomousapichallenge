using System.Text.Json;
using TransformApi.Model;
using TransformApi.Transformations;
using Xunit;

namespace Tests;
public class IterationFirstToFutureDeliveryDatesTests
{
    [Fact]
    public void GivenIterationFirst_WhenAllDeliveryDatesInThePast_ThenAllDeliveryDatesShouldBeTwoDaysInFuture()
    {
        var payload = JsonSerializer.Deserialize<Payload>(GetJson());

        var adjustedDatesProducts = payload.Products.Select(x => x with
        {
            DeliveryDate = x.DeliveryDate with
            {
                Value = DateTime.UtcNow.AddDays(-1).ToString("yyyy-MM-dd")
            }
        });
        payload = payload with { Products = adjustedDatesProducts.ToList() };

        payload = payload with { Iteration = "first" };

        var transformer = new IterationFirstToFutureDeliveryDates();

        var result = transformer.Transform(payload);

        var deliveryDatesAfterTransform = result.Products
            .Select(x => x.DeliveryDate.Value)
            .ToArray();
        var twoDaysInFuture = DateTime.UtcNow.AddDays(2).ToString("yyyy-MM-dd");

        Assert.All(deliveryDatesAfterTransform, x => x.Equals(twoDaysInFuture));
    }

    [Fact]
    public void GivenIterationFirst_WhenOneDeliveryDatesInThePast_ThenThatDeliveryDateShouldBeTwoDaysInFuture()
    {
        var payload = JsonSerializer.Deserialize<Payload>(GetJson());

        var products = payload.Products.ToList();

        var productDeliveryInThePast = products.First() with
        {
            DeliveryDate = products.First().DeliveryDate with
            {
                Value = DateTime.UtcNow.AddDays(-1).ToString("yyyy-MM-dd")
            }
        };

        products[0] = productDeliveryInThePast;

        var productDeliveryToday = products.Last() with
        {
            DeliveryDate = products.Last().DeliveryDate with
            {
                Value = DateTime.UtcNow.ToString("yyyy-MM-dd")
            }
        };

        products[1] = productDeliveryToday;

        payload = payload with
        {
            Products = products
        };

        payload = payload with { Iteration = "first" };

        var transformer = new IterationFirstToFutureDeliveryDates();

        var result = transformer.Transform(payload);

        var deliveryDatesAfterTransform = result.Products
            .Select(x => x.DeliveryDate.Value)
            .ToArray();
        var twoDaysInFuture = DateTime.UtcNow.AddDays(2).ToString("yyyy-MM-dd");

        Assert.Equal(1, deliveryDatesAfterTransform.Count(x => x == twoDaysInFuture));
        Assert.Equal(1, deliveryDatesAfterTransform.Count(x => x != twoDaysInFuture));
        Assert.Equal(1, result.Products.Count(x => x == products[1]));
    }

    [Fact]
    public void GivenIterationNotFirst_WhenAllDeliveryDatesInThePast_ThenAllNoTransform()
    {
        var payload = JsonSerializer.Deserialize<Payload>(GetJson());

        var adjustedDatesProducts = payload.Products.Select(x => x with
        {
            DeliveryDate = x.DeliveryDate with
            {
                Value = DateTime.UtcNow.AddDays(-1).ToString("yyyy-MM-dd")
            }
        });
        payload = payload with { Products = adjustedDatesProducts.ToList() };

        payload = payload with { Iteration = "not_first" };

        var transformer = new IterationFirstToFutureDeliveryDates();

        var result = transformer.Transform(payload);

        Assert.Equal(payload, result);
    }

    private string GetJson()
    {
        string json = @"{
    ""mail_id"": ""11111111-1111-1111-1111-111111111111"",
    ""iteration"": ""first"",
    ""intent"": ""request_simple_order"",
    ""area"": {
        ""name"": ""CA1""
    },
    ""products"": [
        {
            ""line_number"": 1,
            ""product_number"": {
                ""id"": ""1"",
                ""value"": ""BP47283429""
            },
            ""unit_price"": {
                ""id"": ""2"",
                ""value"": ""100""
            },
            ""delivery_date"": {
                ""id"": ""3"",
                ""value"": ""2025-12-12""
            }
        },
        {
            ""line_number"": 2,
            ""product_number"": {
                ""id"": ""4"",
                ""value"": ""BP3819238""
            },
            ""unit_price"": {
                ""id"": ""5"",
                ""value"": ""200""
            },
            ""delivery_date"": {
                ""id"": ""6"",
                ""value"": ""2025-12-12""
            }
        }
    ],
    ""header"": {
        ""order_number"": {
            ""value"": ""4712371"",
            ""id"": ""7""
        },
        ""account_id"": {
            ""value"": ""9000028128"",
            ""id"": ""8""
        }
    }
}";

        return json;
    }
}
