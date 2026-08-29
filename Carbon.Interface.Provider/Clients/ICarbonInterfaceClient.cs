using Carbon.Interface.Models.Flight.Request;
using Carbon.Interface.Models.Flight.Response;

namespace Carbon.Interface.Clients;

public interface ICarbonInterfaceClient
{
    Task<FlightEstimateResponse> FlightEstimateAsync(FlightEstimateRequest request, CancellationToken ct);
}