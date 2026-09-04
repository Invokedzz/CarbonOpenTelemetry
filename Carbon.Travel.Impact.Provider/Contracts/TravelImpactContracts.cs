using System.Text.Json.Serialization;

namespace Carbon.Travel.Impact.Provider.Contracts
{
    public class DetailedFlightEmissionsRequest
    {
        [JsonPropertyName("flights")]
        public List<Flight> Flights { get; set; } = [];
    }

    public class Flight
    {
        [JsonPropertyName("origin")]
        public string Origin { get; set; } = string.Empty;
        [JsonPropertyName("destination")]
        public string Destination { get; set; } = string.Empty;
        [JsonPropertyName("operatingCarrierCode")]
        public string OperatingCarrierCode { get; set; } = string.Empty;
        [JsonPropertyName("flightNumber")]
        public int FlightNumber { get; set; }
        [JsonPropertyName("departureDate")]
        public required DepartureDate Date { get; set; }
    }

    public class DepartureDate
    {
        [JsonPropertyName("year")]
        public int Year { get; set; }
        [JsonPropertyName("month")]
        public int Month { get; set; }
        [JsonPropertyName("day")]
        public int Day { get; set; }
    }
    
    public class DetailedFlightEmissionsResponse
    {
        [JsonPropertyName("flightEmissions")]
        public List<FlightWithDetailedEmissions>? Flights { get; set; }
        [JsonPropertyName("modelVersion")]
        public ModelVersion? ModelVersion { get; set; }
    }

    public class FlightWithDetailedEmissions
    {
        [JsonPropertyName("flight")]
        public required Flight Flight { get; set; }
        [JsonPropertyName("emissionsGramsPerPax")]
        public EmissionsGramsPerPax? Details { get; set; }
        [JsonPropertyName("contrailsImpactBucket")]
        public string? ContrailsImpactBucket { get; set; }
        [JsonPropertyName("source")]
        public string? Source { get; set; }
    }   

    public class EmissionsGramsPerPax
    {
        [JsonPropertyName("first")]
        public int First { get; set; }
        [JsonPropertyName("business")]
        public int Business { get; set; }
        [JsonPropertyName("premiumEconomy")]
        public int PremiumEconomy { get; set; }
        [JsonPropertyName("economy")]
        public int Economy { get; set; }
    }
    
    public class ModelVersion
    {
        [JsonPropertyName("major")]
        public int Major { get; set; }

        [JsonPropertyName("minor")]
        public int Minor { get; set; }

        [JsonPropertyName("patch")]
        public int Patch { get; set; }

        [JsonPropertyName("dated")]
        public string? Dated { get; set; }
    }
}