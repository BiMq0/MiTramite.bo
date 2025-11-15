using Microsoft.AspNetCore.Components;
using WAMiTramite.Components;
using WAMiTramite.Handlers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped(sp =>
{
    return new HttpClient { BaseAddress = new Uri(Config.ApiUrl) };
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
