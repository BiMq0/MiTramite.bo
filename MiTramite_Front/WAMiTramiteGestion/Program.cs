using WAMiTramiteGestion.Components;
using WAMiTramiteGestion.Handlers;
using WAMiTramiteGestion.Services.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Configurar HttpClients para manejar cookies correctamente en Server y WebAssembly
builder.Services.AddApiHttpClients(Config.ApiUrl);

// Blazor híbrido
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

// Registrar servicios
builder.Services.AddSingleton<LoginStateService>();
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
