using System.Net;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using WAMiTramiteGestion.Components;
using WAMiTramiteGestion.Handlers;
using WAMiTramiteGestion.Services.Notificaciones;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient("ApiClient", client =>
{
    client.BaseAddress = new Uri(Config.ApiUrl);
})
.ConfigurePrimaryHttpMessageHandler(() =>
{
    return new HttpClientHandler
    {
        UseCookies = true,
        CookieContainer = new CookieContainer()
    };
});
builder.Services.AddScoped<INotificacionService, NotificacionService>();
// Blazor híbrido
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

// Registrar servicios
builder.Services.AddSingleton<LoginStateService>();
builder.Services.AddScoped<ProtectedLocalStorage>();
builder.Services.AddScopedServices();

var app = builder.Build();

// Pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode();

app.Run();
