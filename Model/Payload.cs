using TransformApi.Transformations;

namespace TransformApi.Model;

public record Payload(
    Guid? MailId,
    string? Iteration,
    string? Intent,
    Area? Area,
    List<Product>? Products,
    Header? Header
) : ITransformable<Payload>
{
    public Payload TransformBy(Payload payload, IEnumerable<ITransformation<Payload>> transformations)
    {
        return transformations.Aggregate(payload, (current, transformation) => transformation.Transform(current));
    }
}

public record Area(
    string? Name
);

public record Product(
    int? LineNumber,
    ProductField? ProductNumber,
    ProductField? UnitPrice,
    ProductField? DeliveryDate
);

public record ProductField(
    string? Id,
    string? Value
);

public record Header(
    ProductField? OrderNumber,
    ProductField? AccountId
);
