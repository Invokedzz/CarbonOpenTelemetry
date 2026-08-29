using System.Text.Json.Serialization;

namespace Carbon.Interface.Models.Flight;

public class FlightLegs
{
    [JsonPropertyName("departure_airport")]
    public string DepartureAirport { get; set; } = string.Empty;
    [JsonPropertyName("destination_airport")]
    public string DestinationAirport { get; set; } = string.Empty;
    [JsonPropertyName("cabin_class")]
    public string CabinClass { get; set; } = "economy";
}