using Carbon.Domain.Contracts.Services.Authentication;
using Carbon.Domain.Contracts.Services.Users;

namespace Carbon.Core.Extensions;

public static class AdminSeedExtensions
{
    // Se "Seed:AdminEmail" estiver configurado, esse usuário vira Admin ao iniciar a API.
    // Serve para criar o primeiro Admin; depois ele troca os perfis pela tela de usuários.
    public static async Task PromoteConfiguredAdminAsync(this WebApplication app)
    {
        var email = app.Configuration["Seed:AdminEmail"];
        if (string.IsNullOrWhiteSpace(email)) return;

        try
        {
            using var scope = app.Services.CreateScope();
            var users = scope.ServiceProvider.GetRequiredService<IUsersService>();

            if (await users.SetRoleByEmailAsync(email.Trim(), nameof(Roles.Admin)))
                app.Logger.LogInformation("Seed: {Email} agora é Admin.", email);
            else
                app.Logger.LogWarning("Seed: usuário {Email} não encontrado. Cadastre a conta e reinicie a API.", email);
        }
        catch (Exception e)
        {
            app.Logger.LogWarning(e, "Seed: não foi possível promover {Email} a Admin.", email);
        }
    }
}
