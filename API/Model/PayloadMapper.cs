using System;
using System.Collections.Generic;
using TransformApi.Model;

namespace TransformApi.API.Model;

public static class PayloadMapper
{
    //TODO: Figure out which properties can be null and adjust accordingly.
    public static TransformApi.Model.Payload ToDomain(this Payload apiPayload)
    {
        if (apiPayload is null) throw new ArgumentNullException(nameof(apiPayload));
        return new TransformApi.Model.Payload
        {
            MailId = apiPayload.MailId ?? throw new ArgumentNullException(nameof(apiPayload.MailId)),
            Iteration = apiPayload.Iteration ?? throw new ArgumentNullException(nameof(apiPayload.Iteration)),
            Intent = apiPayload.Intent ?? throw new ArgumentNullException(nameof(apiPayload.Intent)),
            Area = ToDomain(apiPayload.Area),
            Products = ToDomain(apiPayload.Products),
            Header = ToDomain(apiPayload.Header)
        };
    }

    private static TransformApi.Model.Area ToDomain(Area? apiArea)
    {
        if (apiArea is null) throw new ArgumentNullException(nameof(apiArea));
        return new TransformApi.Model.Area { Name = apiArea.Name ?? throw new ArgumentNullException(nameof(apiArea.Name)) };
    }

    private static List<TransformApi.Model.Product> ToDomain(List<Product>? apiProducts)
    {
        if (apiProducts is null) throw new ArgumentNullException(nameof(apiProducts));
        var list = new List<TransformApi.Model.Product>();
        foreach (var p in apiProducts)
        {
            list.Add(ToDomain(p));
        }
        return list;
    }

    private static TransformApi.Model.Product ToDomain(Product? apiProduct)
    {
        if (apiProduct is null) throw new ArgumentNullException(nameof(apiProduct));
        return new TransformApi.Model.Product
        {
            LineNumber = apiProduct.LineNumber ?? throw new ArgumentNullException(nameof(apiProduct.LineNumber)),
            ProductNumber = ToDomain(apiProduct.ProductNumber),
            UnitPrice = ToDomain(apiProduct.UnitPrice),
            DeliveryDate = ToDomain(apiProduct.DeliveryDate)
        };
    }

    private static TransformApi.Model.ProductField ToDomain(ProductField? apiField)
    {
        if (apiField is null) throw new ArgumentNullException(nameof(apiField));
        return new TransformApi.Model.ProductField
        {
            Id = apiField.Id ?? throw new ArgumentNullException(nameof(apiField.Id)),
            Value = apiField.Value ?? throw new ArgumentNullException(nameof(apiField.Value))
        };
    }

    private static TransformApi.Model.Header ToDomain(Header? apiHeader)
    {
        if (apiHeader is null) throw new ArgumentNullException(nameof(apiHeader));
        return new TransformApi.Model.Header
        {
            OrderNumber = ToDomain(apiHeader.OrderNumber),
            AccountId = ToDomain(apiHeader.AccountId)
        };
    }
}
