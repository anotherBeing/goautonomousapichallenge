using System.Data.SqlTypes;
using TransformApi.Model;

namespace TransformApi.Transformations;
public class IntentRequestSimpleQuoteToAddNewProduct : ITransformation<Payload>
{
    private const string TransformationTrigger = "request_simple_quote";

    public Payload Transform(Payload payload)
    {
        var shouldTransform = payload.Intent.Equals(TransformationTrigger, StringComparison.OrdinalIgnoreCase);

        if (shouldTransform)
        {
            var lastProduct = payload.Products.Last();

            var newProduct = new Product()
            {
                LineNumber = lastProduct.LineNumber + 1,
                ProductNumber = new ProductField() { Value = "FREIGHT" },
                UnitPrice = new ProductField() { Value = "50" },
            };

            payload.Products.Add(newProduct);
        }

        return payload;
    }
}
