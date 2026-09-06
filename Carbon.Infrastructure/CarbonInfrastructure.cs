using Carbon.Domain.Contracts.Data;
using Carbon.Domain.Contracts.Data.Repositories;
using Carbon.Domain.Contracts.Providers.TravelImpact;
using Carbon.Travel.Impact.Provider;
using Carbon.Travel.Impact.Provider.Clients;
using Infrastructure.Data;
using Infrastructure.Data.Repositories;
using Infrastructure.Providers;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class CarbonInfrastructure
{
    public static void AddDataLayer(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<IRoleRepository, RoleRepository>();
        services.AddTransient<IUserRepository, UserRepository>();
        services.AddTransient<IUnitOfWork, UnitOfWork>();

        services.AddDbContext<CarbonDbContext>(opt =>
            opt.UseSqlServer(configuration["ConnectionStrings:Development"]));
    }

    public static void AddTImpact(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<ITravelImpactClient, TravelImpactClient>();
        services.AddTransient<ITravelImpactSession, TravelImpactSession>();
        services.AddTransient<ITravelImpactProvider, TravelImpactClientProvider>();

        var parameters = new Dictionary<string, string?>
        {
            { "key", configuration["Providers:TImpact:Key"] ?? string.Empty }
        };

        var baseAddress = configuration["Providers:TImpact:Uri"] ?? string.Empty;
        var uri = new Uri(QueryHelpers.AddQueryString(baseAddress, parameters));
        
        services.AddHttpClient("TravelImpact", client =>
        {
            client.Timeout = TimeSpan.FromSeconds(60);
            client.BaseAddress = uri;
        });
    }
}