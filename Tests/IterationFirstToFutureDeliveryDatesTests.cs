using System.Text.Json;
using TransformApi.Domain.Transformations;
using TransformApi.Domain.Model;
using Xunit;

namespace Tests;
public class IterationFirstToFutureDeliveryDatesTests : TransformationTestsBase
{
    [Fact]
    public void GivenIterationFirst_WhenAllDeliveryDatesInThePast_ThenAllDeliveryDatesShouldBeTwoDaysInFuture()
    {
        var payload = GetPayload();

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
        var payload = GetPayload();

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
        var payload = GetPayload();

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

}
