using Carbon.Domain.Models;

namespace Carbon.Domain.Contracts.Data.Repositories;

public interface IAirportRepository : ICarbonRepository<Airport, int>
{
    Task<Airport?> GetAirportByIataAsync(string iata, CancellationToken ct);
}