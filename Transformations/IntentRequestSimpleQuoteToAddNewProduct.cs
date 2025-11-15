using System.Data.SqlTypes;
using TransformApi.Model;

namespace TransformApi.Transformations;
public class IntentRequestSimpleQuoteToAddNewProduct : ITransformation<Payload>
{
    private const string TransformationTrigger = "request_simple_quote";

    public Payload Transform(Payload payload)
    {
        if (payload.Intent is null)
            return payload;

        var shouldTransform = payload.Intent.Equals(TransformationTrigger);

        if (shouldTransform)
        {
            
        }

        return payload;
    }
}
