using System.Text.Json.Serialization;

namespace Carbon.Interface.Models.Flight.Request;

public class FlightEstimateRequest
{
    public string Type { get; set; } = "flight";
    public int Passengers { get; set; }
    public List<FlightLegs> Legs { get; set; } = [];
    [JsonPropertyName("distance_unit")]
    public string? DistanceUnit { get; set; }
}