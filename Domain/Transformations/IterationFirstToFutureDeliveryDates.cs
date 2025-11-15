using TransformApi.Domain.Model;

namespace TransformApi.Domain.Transformations;
public class IterationFirstToFutureDeliveryDates : ITransformation<Payload>
{
    private const string TransformationTrigger = "first";

    public Payload Transform(Payload payload)
    {
        var shouldTransform = payload.Iteration.Equals(TransformationTrigger, StringComparison.OrdinalIgnoreCase);

        if (!shouldTransform)
            return payload;

        var today = DateTime.UtcNow.Date; //TODO: Should we use local time?

        var updatedProducts = payload.Products?
            .Select(product =>
            {
                if (product?.DeliveryDate is not null
                    && DateTime.TryParse(product.DeliveryDate.Value, out var deliveryDate)
                    && deliveryDate.Date < today)
                {
                    var newDeliveryDate = today.AddDays(2).ToString("yyyy-MM-dd");
                    var updatedDeliveryDate = product.DeliveryDate with { Value = newDeliveryDate };

                    return product with { DeliveryDate = updatedDeliveryDate };
                }

                return product;
            })
            .ToList();

        if (updatedProducts is null)
            return payload;

        return payload with { Products = updatedProducts };
    }
}