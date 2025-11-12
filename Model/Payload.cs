using System.Text.Json.Serialization;
using TransformApi.Transformations;

namespace TransformApi.Model;

public record Payload : ITransformable<Payload>
{
    [JsonPropertyName("mail_id")]
    public Guid? MailId { get; init; }
    [JsonPropertyName("iteration")]
    public string? Iteration { get; init; }
    [JsonPropertyName("intent")]
    public string? Intent { get; init; }
    [JsonPropertyName("area")]
    public Area? Area { get; init; }
    [JsonPropertyName("products")]
    public List<Product>? Products { get; init; }
    [JsonPropertyName("header")]
    public Header? Header { get; init; }
    public Payload TransformBy(Payload payload, IEnumerable<ITransformation<Payload>> transformations)
    {
        return transformations.Aggregate(payload, (current, transformation) => transformation.Transform(current));
    }
}

public record Area
{
    [JsonPropertyName("name")]
    public string? Name { get; init; }
}

public record Product
{
    [JsonPropertyName("line_number")]
    public int? LineNumber { get; init; }
    [JsonPropertyName("product_number")]
    public ProductField? ProductNumber { get; init; }
    [JsonPropertyName("unit_price")]
    public ProductField? UnitPrice { get; init; }
    [JsonPropertyName("delivery_date")]
    public ProductField? DeliveryDate { get; init; }
}

public record ProductField
{
    [JsonPropertyName("id")]
    public string? Id { get; init; }
    [JsonPropertyName("value")]
    public string? Value { get; init; }
}

public record Header
{
    [JsonPropertyName("order_number")]
    public ProductField? OrderNumber { get; init; }
    [JsonPropertyName("account_id")]
    public ProductField? AccountId { get; init; }
}
