using Carbon.Domain.Contracts.Data;
using Carbon.Domain.Contracts.Data.Repositories;
using Carbon.Domain.Contracts.Providers.Carbon;
using Carbon.Interface;
using Carbon.Interface.Clients;
using Infrastructure.Data;
using Infrastructure.Data.Repositories;
using Infrastructure.Providers;
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

    public static void AddCarbonInterface(this IServiceCollection services)
    {
        services.AddTransient<ICarbonInterfaceClient, CarbonInterfaceClient>();
        services.AddTransient<ICarbonInterfaceSession, CarbonInterfaceSession>();
        services.AddTransient<ICarbonEstimateProvider, CarbonEstimateProvider>();
    }
}