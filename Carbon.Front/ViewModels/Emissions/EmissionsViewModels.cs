namespace Carbon.Front.ViewModels.Emissions;
 
public record FlightLeg(string Origin, string Destination, string CarrierCode, int FlightNumber, DateOnly DepartureDate);
public record EmissionsRequest(Guid UserId, List<FlightLeg> Flights, string Cabin, int Passengers, bool RoundTrip);
 
public record EmissionsResponse(Guid Id, FlightInformation Information, TotalEmissions TotalEmissions, DateOnly ModelDate, DateTimeOffset CalculatedAt);
public record FlightInformation(string Cabin, int Passengers, int FlightsCount, bool RoundTrip);
public record TotalEmissions(decimal KgPerPassenger, decimal TotalKg, decimal TotalTonnes, TotalImpact Impact);
public record TotalImpact(string Level, string ContrailsImpact);
 
public static class ApiRoutes
{
    // [HttpPost("/Register")] etc. use a leading slash, which bypasses the "Carbon/[controller]" prefix.
    public const string Register = "/Register";
    public const string Login = "/Login";
    public const string EmissionsCalculate = "/Carbon/Emissions/Calculate";
}