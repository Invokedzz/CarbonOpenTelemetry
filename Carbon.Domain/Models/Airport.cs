namespace Carbon.Domain.Models;

public class Airport
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Country { get; set; }
    public string? Iata { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
}