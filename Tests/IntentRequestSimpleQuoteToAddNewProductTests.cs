using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransformApi.Domain.Transformations;
using TransformApi.Domain.Model;
using Xunit;

namespace Tests;
public class IntentRequestSimpleQuoteToAddNewProductTests : TransformationTestsBase
{
    [Fact]
    public void GivenIntentRequestSimpleQuote_ThenNewProductShouldBeAdded()
    {
        var payload = GetPayload();

        payload = payload with { Intent = "request_simple_quote" };

        var lastProduct = payload.Products.Last();

        var expectedAddedProduct = new Product()
        {
            LineNumber = lastProduct.LineNumber + 1,
            ProductNumber = new ProductField() { Value = "FREIGHT" },
            UnitPrice = new ProductField() { Value = "50" },
        };

        var transformer = new IntentRequestSimpleQuoteToAddNewProduct();

        var result = transformer.Transform(payload);

        Assert.True(payload.Products.All(item => result.Products.Contains(item)));
        Assert.Contains(expectedAddedProduct, result.Products);
    }

    [Fact]
    public void GivenIntentNotRequestSimpleQuote_ThenShouldNotTransform()
    {
        var payload = GetPayload();

        payload = payload with { Intent = "not_request_simple_quote" };

        var transformer = new IntentRequestSimpleQuoteToAddNewProduct();

        var result = transformer.Transform(payload);
        
        Assert.Equal(payload, result);
    }
}
