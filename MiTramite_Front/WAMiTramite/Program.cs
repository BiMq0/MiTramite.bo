using System.Net;
using Microsoft.AspNetCore.Components;
using WAMiTramite.Components;
using WAMiTramite.Handlers;

var builder = WebApplication.CreateBuilder(args);

// Adicion y manejo de Cookies
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


builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddScopedServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();


app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
