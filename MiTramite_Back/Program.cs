using MiTramite_Back.Handlers;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using MiTramite_Back.Acceso_A_Datos.Context;
using MiTramite_Back.Middleware;

var builder = WebApplication.CreateBuilder(args);

//Adicion de Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo { Title = "MiTramite API", Description = "Documentacion de API para MiTramite", Version = "v1" }); });

//Database
builder.Services.AddDbContext<MiTramiteDbContext>(options =>
{
   options.UseNpgsql(builder.Configuration.GetConnectionString("DatabaseConnectionString"));
});

// JWT Configuration
builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("Jwt"));

// Scopes y Repositories
builder.Services.AddScopedRepositories();
builder.Services.AddScopedServices();

// Middlewares
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
   app.UseSwagger();
   app.UseSwaggerUI(c =>
   {
      c.SwaggerEndpoint("/swagger/v1/swagger.json", "PharmAPI V1");
   });
}

app.AddMiddleware();
app.MapEndpoints();
app.MapGet("/", () => Results.Redirect("/swagger"));
app.MapGet("/verify", () => Results.Ok("Token disponible y válido"));
app.Run();
