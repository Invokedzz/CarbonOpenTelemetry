namespace Carbon.Interface.Models.Flight;

public class FlightAttributes : Attributes
{
    public int Passengers { get; set; }
    public List<FlightLegs> Legs { get; set; } = [];
}