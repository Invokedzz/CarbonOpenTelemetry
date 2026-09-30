using Carbon.Domain.Models;
using CsvHelper.Configuration;

namespace Infrastructure.Data.Configuration.Reader.Maps;

public sealed class AirportFileMap : ClassMap<Airport>
{
    public AirportFileMap()
    {
        Map(x => x.Id).Index(0);
        Map(x => x.Name).Index(1);
        Map(x => x.Country).Index(3);
        Map(x => x.Iata).Index(4);
        Map(x => x.Latitude).Index(6);
        Map(x => x.Longitude).Index(7);
    }
}