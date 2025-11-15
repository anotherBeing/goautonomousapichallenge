using System.Text.Json.Serialization;

namespace Transform.API.Model;

public class Payload
{
    [JsonPropertyName("mail_id")]
    public Guid? MailId { get; set; }

    [JsonPropertyName("iteration")]
    public string? Iteration { get; set; }

    [JsonPropertyName("intent")]
    public string? Intent { get; set; }

    [JsonPropertyName("area")]
    public Area? Area { get; set; }

    [JsonPropertyName("products")]
    public List<Product>? Products { get; set; }

    [JsonPropertyName("header")]
    public Header? Header { get; set; }
}

public class Area
{
    [JsonPropertyName("name")]
    public string? Name { get; set; }
}

public class Product
{
    [JsonPropertyName("line_number")]
    public int? LineNumber { get; set; }

    [JsonPropertyName("product_number")]
    public ProductField? ProductNumber { get; set; }

    [JsonPropertyName("unit_price")]
    public ProductField? UnitPrice { get; set; }

    [JsonPropertyName("delivery_date")]
    public ProductField? DeliveryDate { get; set; }
}

public class ProductField
{
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    [JsonPropertyName("value")]
    public string? Value { get; set; }
}

public class Header
{
    [JsonPropertyName("order_number")]
    public ProductField? OrderNumber { get; set; }

    [JsonPropertyName("account_id")]
    public ProductField? AccountId { get; set; }
}
