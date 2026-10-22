namespace Carbon.Front.ViewModels.Emissions
{
    public class EmissionsRequestViewModel
    {
        public List<FlightLegViewModel>? FlightLegs { get; set; }
        public string? Cabin { get; set; }
        public int? Passengers { get; set; }
        public bool RoundTrip { get; set; }
    }

    public class FlightLegViewModel
    {
        public string? Origin { get; set; }
        public string? Destination { get; set; }
        public string? CarrierCode { get; set; }
        public int? FlightNumber { get; set; }
        public DateOnly? DepartureDate { get; set; }
    }

    public class EmissionsResponseViewModel
    {
        public string? Cabin { get; set; }
        public int Passengers { get; set; }
        public int FlightsCount { get; set; }
        public bool RoundTrip { get; set; }
        public decimal KgPerPassenger { get; set; }
        public decimal TotalKg { get; set; }
        public decimal TotalTonnes { get; set; }
        public string? Level { get; set; }
        public string? ContrailsImpact { get; set; }
    }
}