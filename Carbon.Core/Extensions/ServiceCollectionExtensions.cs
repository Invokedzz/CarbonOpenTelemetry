using Carbon.Application.Contracts;
using Carbon.Application.UseCases;
using Carbon.Domain.Contracts.Services.Authentication;
using Carbon.Domain.Services;

namespace Carbon.Core.Extensions
{
    public static class Extensions
    {
        public static void AddCarbonServices(this IServiceCollection services)
        {
            services.AddTransient<IAuthService, AuthService>();
        }

        public static void AddCarbonUseCases(this IServiceCollection services)
        {
            services.AddTransient<IAuthUseCase, AuthUseCase>();
        }
    }
}