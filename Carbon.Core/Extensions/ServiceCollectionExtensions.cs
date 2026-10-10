using Carbon.Application.Contracts;
using Carbon.Application.UseCases;
using Carbon.Core.Contracts;
using Carbon.Domain.Contracts.Services.Authentication;
using Carbon.Domain.Contracts.Services.Emissions;
using Carbon.Domain.Services;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.OpenApi.Models;

namespace Carbon.Core.Extensions
{
    public static class Extensions
    {
        public static void AddCarbonServices(this IServiceCollection services)
        {
            services.AddTransient<IAuthService, AuthService>();
            services.AddTransient<IEmissionsService, EmissionsService>();
        }

        public static void AddCarbonUseCases(this IServiceCollection services)
        {
            services.AddTransient<IAuthUseCase, AuthUseCase>();
            services.AddTransient<IEmissionsUseCase, EmissionsUseCase>();
        }

        public static void AddCarbonRateLimiter(this IServiceCollection services)
        {
            services.AddRateLimiter(options =>
            {
                options.AddFixedWindowLimiter(nameof(RateLimiterPolicies.SessionPolicy), opt =>
                {
                    opt.PermitLimit = 5;
                    opt.Window = TimeSpan.FromSeconds(40);
                    opt.AutoReplenishment = true;
                });

                options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
    
                options.OnRejected = async (context, token) =>
                {
                    context.HttpContext.Response.StatusCode = StatusCodes.Status429TooManyRequests;
                    context.HttpContext.Response.ContentType = "application/json";
                    
                    var response = new CarbonProblemDetails
                    {
                        OperationId = Guid.NewGuid(),
                        Title = "Too many requests!",
                        Status = context.HttpContext.Response.StatusCode,
                        Detail = $"Try again later. Error: {context.HttpContext.Response.StatusCode}",
                        Extensions = new Dictionary<string, object?>()
                    };
                    
                    await context.HttpContext.Response.WriteAsJsonAsync(response, token);
                };
            });
        }

        public static void AddSwaggerAuthorization(this IServiceCollection services)
        {
            services.AddSwaggerGen(e =>
            {
                e.AddSecurityDefinition(nameof(Utility.Bearer), new OpenApiSecurityScheme
                {
                    Name = nameof(Utility.Authorization),
                    Type = SecuritySchemeType.Http,
                    Scheme = nameof(Utility.Bearer).ToLowerInvariant(),
                    BearerFormat = nameof(Utility.Jwt).ToUpperInvariant(),
                    In = ParameterLocation.Header,
                });
                
                e.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = nameof(Utility.Bearer)
                            }
                        },
                        []
                    }
                });
            });
        }
    }
}