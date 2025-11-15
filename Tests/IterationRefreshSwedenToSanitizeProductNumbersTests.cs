using System.Text.RegularExpressions;
using TransformApi.Domain.Model;
using TransformApi.Domain.Transformations;
using Xunit;

namespace Tests;
public class IterationRefreshSwedenToSanitizeProductNumbersTests : TransformationTestsBase
{
    [Fact]
    public void GivenIterationRefresh_WhenAreaSe1_ThenSanitizeProductNumbers()
    {
        var payload = GetPayload();
        payload = payload with { Area = new Area { Name = "SE1" }, Iteration = "Refresh" };

        var transformation = new IterationRefreshSwedenToSanitizeProductNumbers();

        var transformedPayload = transformation.Transform(payload);

        var expectedProducts = payload.Products?.Select(p => p with
        {
            ProductNumber = p.ProductNumber with
            {
                Value = Regex.Replace(p.ProductNumber.Value, "[^0-9]", "")
            }
        });

        Assert.NotEqual(payload, transformedPayload);
        Assert.Equal(expectedProducts, transformedPayload.Products);
    }

    [Fact]
    public void GivenIterationRefresh_WhenAreaNotSe1_ThenNoTransformation()
    {
        var payload = GetPayload();
        payload = payload with { Area = new Area { Name = "US1" }, Iteration = "Refresh" };

        var transformation = new IterationRefreshSwedenToSanitizeProductNumbers();

        var transformedPayload = transformation.Transform(payload);

        Assert.Equal(payload, transformedPayload);
    }

    [Fact]
    public void GivenIterationNotRefresh_WhenAreaSe1_ThenNoTransformation()
    {
            var payload = GetPayload();
        payload = payload with { Area = new Area { Name = "SE1" }, Iteration = "NotRefresh" };

        var transformation = new IterationRefreshSwedenToSanitizeProductNumbers();

        var transformedPayload = transformation.Transform(payload);

        Assert.Equal(payload, transformedPayload);
    }

    [Fact]
    public void GivenIterationNotRefresh_WhenAreaNotSe1_ThenNoTransformation()
    {
        var payload = GetPayload();
        payload = payload with { Area = new Area { Name = "US1" }, Iteration = "NotRefresh" };

        var transformation = new IterationRefreshSwedenToSanitizeProductNumbers();

        var transformedPayload = transformation.Transform(payload);

        Assert.Equal(payload, transformedPayload);
    }
}
