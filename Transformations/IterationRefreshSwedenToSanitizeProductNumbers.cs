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
        var sanitizedProducts = payload.Products?
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
