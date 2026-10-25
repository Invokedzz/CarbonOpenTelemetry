using Carbon.Front.Components;
using Carbon.Front.Util;
using Microsoft.AspNetCore.Components.Authorization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    // A foto da webcam passa pelo SignalR; o limite padrão (32 KB) é pequeno demais
    .AddHubOptions(o => o.MaximumReceiveMessageSize = 5 * 1024 * 1024);

builder.Services.AddAuthorizationCore();
builder.Services.AddCascadingAuthenticationState();
 
builder.Services.AddScoped<SessionStore>();
builder.Services.AddScoped<AuthenticationStateProvider, JwtAuthStateProvider>();
 
builder.Services.AddHttpClient<ApiClient>(c =>
    c.BaseAddress = new Uri(builder.Configuration["Api:BaseUrl"]
                            ?? throw new InvalidOperationException("Api:BaseUrl is not configured.")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
