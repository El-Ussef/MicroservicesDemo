using System.Text.Json.Serialization;

namespace PurchaseManagementApi.Entities;

public class ProductVM
{
    [JsonPropertyName("id")]
    public Guid Id { get; set; }

    [JsonPropertyName("label")]
    public string Label { get; set; }
    
    [JsonPropertyName("quantity")]
    public int Quantity { get; set; }
}