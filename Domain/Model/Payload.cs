using TransformApi.Domain.Transformations;

namespace TransformApi.Domain.Model;

public record Payload : ITransformable<Payload>
{
    public Guid MailId { get; init; }
    public string Iteration { get; init; }
    public string Intent { get; init; }
    public Area Area { get; init; }
    public List<Product> Products { get; init; }
    public Header Header { get; init; }
    public Payload TransformBy(Payload payload, IEnumerable<ITransformation<Payload>> transformations)
    {
        return transformations.Aggregate(payload, (current, transformation) => transformation.Transform(current));
    }
}

public record Area
{
    public string Name { get; init; }
}

public record Product
{
    public int LineNumber { get; init; }
    public ProductField ProductNumber { get; init; }
    public ProductField UnitPrice { get; init; }
    public ProductField DeliveryDate { get; init; }
}

public record ProductField
{
    public string Id { get; init; }
    public string Value { get; init; }
}

public record Header
{
    public ProductField OrderNumber { get; init; }
    public ProductField AccountId { get; init; }
}
