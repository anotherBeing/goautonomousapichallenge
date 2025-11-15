using System.Text.Json;

namespace TransformApi.API.Model;

public static class PayloadDeserializer
{
    private static readonly JsonSerializerOptions Options = new()
    {
        PropertyNameCaseInsensitive = true,
        AllowTrailingCommas = true,
    };

    public static Payload? Deserialize(string json)
    {
        return JsonSerializer.Deserialize<Payload>(json, Options);
    }
}
