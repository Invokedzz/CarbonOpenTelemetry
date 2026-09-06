namespace Carbon.Domain.Contracts.Providers.TravelImpact
{
    public class ImpactProviderRequest
    {
        public List<Flight> Flights { get; set; } = [];
    }

    public class Flight
    {
        public string Origin { get; set; } = string.Empty;
        public string Destination { get; set; } = string.Empty;
        public string CarrierCode { get; set; } = string.Empty;
        public int FlightNumber { get; set; }
        public DateOnly DepartureDate { get; set; }
    }

    public class EmissionsPerPax
    {
        public long First { get; set; }
        public long Business { get; set; }
        public long PremiumEconomy { get; set; }
        public long Economy { get; set; }
    }

    public class ImpactProviderResponse
    {
        public Guid Id { get; set; }
        public required List<Flight> Flights { get; set; }
        public required EmissionsPerPax Emissions { get; set; }
        public ImpactBucket ImpactBucket { get; set; }
        public DateOnly Dated { get; set; }
        public DateTimeOffset RequestedAt { get; set; }
    }
    
    public enum ImpactBucket
    {
        ContrailsImpactUnspecified,
        ContrailsImpactNegligible,
        ContrailsImpactModerate,
        ContrailsImpactSevere
    }
}