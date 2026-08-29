using System.Text.Json.Serialization;

namespace Carbon.Interface.Models;

public class Attributes
{
    [JsonPropertyName("estimated_at")]
    public DateTime EstimatedAt { get; set; }
    [JsonPropertyName("carbon_g")]
    public decimal Grams { get; set; }
    [JsonPropertyName("carbon_lb")]
    public decimal Pounds { get; set; }
    [JsonPropertyName("carbon_kg")]
    public decimal Kilograms { get; set; }
    [JsonPropertyName("carbon_mt")]
    public decimal MetricTon { get; set; }
}