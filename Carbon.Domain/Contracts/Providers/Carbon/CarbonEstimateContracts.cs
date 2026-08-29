namespace Carbon.Domain.Contracts.Providers.Carbon
{
    public class FlightCarbonEstimateRequest
    {
        public int Passengers { get; set; }
        public List<FlightLegs> Legs { get; set; } = [];
        public string? DistanceUnit { get; set; }
    }

    public class FlightCarbonEstimateResponse
    {
        public int Passengers { get; set; }
        public List<FlightLegs> Legs { get; set; } = [];
        public DateTime EstimatedAt { get; set; }
        public decimal Grams { get; set; }
        public decimal Kilograms { get; set; }
        public decimal Pounds { get; set; }
        public decimal MetricTon { get; set; }
        public string Unit { get; set; } = string.Empty;
        public decimal Distance { get; set; }
    }

    public class FlightLegs
    {
        public string DepartureAirport { get; set; } = string.Empty;
        public string DestinationAirport { get; set; } = string.Empty;
        public string CabinClass { get; set; } = string.Empty;
    }
}