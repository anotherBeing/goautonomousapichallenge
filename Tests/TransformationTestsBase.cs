using System.Text.Json;
using TransformApi.API.Model;
using Payload = TransformApi.Model.Payload;

namespace Tests;
public class TransformationTestsBase
{
    protected Payload GetPayload()
    {
        //TODO: The tests have dependency on the mapper. Remove this dependency
        // by creating the Payload object directly in the tests. 
        var apiPayload = JsonSerializer.Deserialize<TransformApi.API.Model.Payload>(GetJson());

        return apiPayload.ToDomain();
    }

    protected string GetJson()
    {
        string json = @"{
    ""mail_id"": ""11111111-1111-1111-1111-111111111111"",
    ""iteration"": ""first"",
    ""intent"": ""request_simple_order"",
    ""area"": {
        ""name"": ""CA1""
    },
    ""products"": [
        {
            ""line_number"": 1,
            ""product_number"": {
                ""id"": ""1"",
                ""value"": ""BP47283429""
            },
            ""unit_price"": {
                ""id"": ""2"",
                ""value"": ""100""
            },
            ""delivery_date"": {
                ""id"": ""3"",
                ""value"": ""2025-12-12""
            }
        },
        {
            ""line_number"": 2,
            ""product_number"": {
                ""id"": ""4"",
                ""value"": ""BP3819238""
            },
            ""unit_price"": {
                ""id"": ""5"",
                ""value"": ""200""
            },
            ""delivery_date"": {
                ""id"": ""6"",
                ""value"": ""2025-12-12""
            }
        }
    ],
    ""header"": {
        ""order_number"": {
            ""value"": ""4712371"",
            ""id"": ""7""
        },
        ""account_id"": {
            ""value"": ""9000028128"",
            ""id"": ""8""
        }
    }
}";

        return json;
    }
}
