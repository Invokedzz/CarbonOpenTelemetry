namespace Carbon.Interface.Models.Flight;

public class FlightData : Estimate
{
    public required FlightAttributes Attributes { get; set; }
}