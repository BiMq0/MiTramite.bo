using MiTramite_Back.Handlers;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using MiTramite_Back.Acceso_A_Datos.Context;

var builder = WebApplication.CreateBuilder(args);

//Adicion de Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo { Title = "MiTramite API", Description = "Documentacion de API para MiTramite", Version = "v1" }); });
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
   options.IdleTimeout = TimeSpan.FromMinutes(480);
   options.Cookie.HttpOnly = true;
   options.Cookie.IsEssential = true;
});

builder.Services.AddHttpContextAccessor(); // Necesario para acceder a HttpContext en servicios


//Database
builder.Services.AddDbContext<MiTramiteDbContext>(options =>
{
   options.UseNpgsql(builder.Configuration.GetConnectionString("DatabaseConnectionString"));
});


//Scopes
builder.Services.AddScopedRepositories();
builder.Services.AddScopedServices();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
   app.UseSwagger();
   app.UseSwaggerUI(c =>
   {
      c.SwaggerEndpoint("/swagger/v1/swagger.json", "PharmAPI V1");
   });
}
app.UseSession();
app.MapEndpoints();
app.MapGet("/", () => Results.Redirect("/swagger"));
app.Run();
