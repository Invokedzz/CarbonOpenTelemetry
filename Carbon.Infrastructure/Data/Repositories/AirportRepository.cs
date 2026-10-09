using Carbon.Domain.Contracts.Data.Repositories;
using Carbon.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repositories;

public class AirportRepository : IAirportRepository
{
    private readonly CarbonDbContext _context;
    
    public AirportRepository(CarbonDbContext context)
    {
        _context = context;
    }
    
    public async Task AddAsync(Airport entity, CancellationToken ct = default)
    {
        await _context.Airports.AddAsync(entity, ct);
    }

    public async Task<IEnumerable<Airport>> GetAllAsync(CancellationToken ct = default)
    {
        return await _context.Airports
            .AsNoTracking()
            .AsSplitQuery()
            .OrderBy(e => e.Country)
            .ToListAsync(ct);
    }

    public async Task<Airport?> GetByIdAsync(int id, CancellationToken ct = default)
    {
        return await _context.Airports
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.Id == id, ct);
    }

    public async Task<Airport?> GetAirportByIataAsync(string iata, CancellationToken ct = default)
    {
        return await _context.Airports
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.Iata == iata, ct);
    }
}