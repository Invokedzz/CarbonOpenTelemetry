using Carbon.Domain.Models;
using Infrastructure.Data.Configuration.Reader;
using Infrastructure.Data.Configuration.Reader.Maps;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configuration;

public class AirportTypeConfiguration : IEntityTypeConfiguration<Airport>
{
    public void Configure(EntityTypeBuilder<Airport> builder)
    {
        builder.ToTable("CarbonAirports");
        builder.HasData(GetAirports());
    }

    private static List<Airport> GetAirports()
    {
        var reader = CarbonCsvReader
            .ReadCsvInChunks<Airport, AirportFileMap>(
                Path.Combine(Directory.GetCurrentDirectory(),
                    "Util", "Files", "airports.dat"));

        return reader
            .SelectMany(e => e)
            .ToList();
    }
}