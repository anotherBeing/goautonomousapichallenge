using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransformApi.Model;
using TransformApi.Transformations;
using Xunit;

namespace Tests;
public class IntentRequestSimpleQuoteToAddNewProductTests : TransformationTestsBase
{
    [Fact]
    public void GivenIntentRequestSimple_ThenNewProductShouldBeAdded()
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
}
