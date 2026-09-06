using System.Text.Json.Serialization;

namespace Carbon.Travel.Impact.Provider.Contracts
{
    public class FlightEmissionsRequest
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
    
    public class FlightEmissionsResponse
    {
        [JsonPropertyName("flightEmissions")]
        public List<FlightWithEmissions>? Flights { get; set; }
        [JsonPropertyName("modelVersion")]
        public ModelVersion? ModelVersion { get; set; }
    }

    public class FlightWithEmissions
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
        public long First { get; set; }
        [JsonPropertyName("business")]
        public long Business { get; set; }
        [JsonPropertyName("premiumEconomy")]
        public long PremiumEconomy { get; set; }
        [JsonPropertyName("economy")]
        public long Economy { get; set; }
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