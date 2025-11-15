using System.Text.RegularExpressions;
using TransformApi.Model;

namespace TransformApi.Transformations;
public class IterationRefreshSwedenToSanitizeProductNumbers : ITransformation<Payload>
{
    private const string SanitizePattern = "[^0-9]";
    private const string TransformationArea = "SE1";
    private const string TransformationIteration = "Refresh";

    public Payload Transform(Payload payload)
    {
        if (payload.Area?.Name != TransformationArea || payload.Iteration != TransformationIteration)
        {
            return payload;
        }
        //TODO: How to handle null values in a more generic and cleaner way?
        var sanitizedProducts = payload.Products?.Where(p => p.ProductNumber?.Value is not null)
            .Select(p => p with
            {
                ProductNumber = p.ProductNumber! with
                {
                    Value = Regex.Replace(p.ProductNumber.Value!, SanitizePattern, "")
                }
            }).ToList();

        return payload with { Products = sanitizedProducts };
    }
}
