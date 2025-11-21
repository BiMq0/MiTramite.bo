using MiTramite_Back.Handlers;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using MiTramite_Back.Acceso_A_Datos.Context;
using MiTramite_Back.Middleware.Tokens;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

var builder = WebApplication.CreateBuilder(args);

//Adicion de Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo { Title = "MiTramite API", Description = "Documentacion de API para MiTramite", Version = "v1" }); });

//Database
builder.Services.AddDbContext<MiTramiteDbContext>(options =>
{
   options.UseNpgsql(builder.Configuration.GetConnectionString("DatabaseConnectionString"));
});

builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
       options.TokenValidationParameters = new TokenValidationParameters
       {
          ValidateIssuer = true,
          ValidateAudience = true,
          ValidateLifetime = true,
          ValidateIssuerSigningKey = true,
          ValidIssuer = builder.Configuration["Jwt:Issuer"],
          ValidAudience = builder.Configuration["Jwt:Audience"],
          IssuerSigningKey = new SymmetricSecurityKey(
               Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!)
           ),
          ClockSkew = TimeSpan.FromMinutes(5)
       };

       options.Events = new JwtBearerEvents
       {
          OnMessageReceived = context =>
          {
             if (context.Request.Cookies.ContainsKey("token"))
             {
                context.Token = context.Request.Cookies["token"];
             }
             return Task.CompletedTask;
          }
       };
    });

builder.Services.AddAuthorization();

// Scopes y Repositories
builder.Services.AddScopedRepositories();
builder.Services.AddScopedServices();
builder.Services.AddScoped<ITokenService, TokenService>();

// Middlewares
builder.Services.AddCors(options =>
{
   options.AddPolicy("AllowFrontend", policy =>
   {
      policy.WithOrigins("http://localhost:5080")
             .AllowCredentials()
             .AllowAnyHeader()
             .AllowAnyMethod();
   });
});



var app = builder.Build();

if (app.Environment.IsDevelopment())
{
   app.UseSwagger();
   app.UseSwaggerUI(c =>
   {
      c.SwaggerEndpoint("/swagger/v1/swagger.json", "PharmAPI V1");
   });
}

app.UseCors("AllowFrontend");



app.UseRouting();
app.UseCookiePolicy(new CookiePolicyOptions
{
   MinimumSameSitePolicy = SameSiteMode.None,
   HttpOnly = HttpOnlyPolicy.Always,
   Secure = CookieSecurePolicy.None
});

app.UseAuthentication();
app.UseAuthorization();

app.MapEndpoints();
app.MapGet("/", () => Results.Redirect("/swagger"));
app.MapGet("/api/verify", async () => Results.Ok("Token disponible y válido")).RequireAuthorization(new AuthorizeAttribute
{
   AuthenticationSchemes = "Bearer"
});


app.MapPost("/api/logout", (HttpContext context) =>
{
   context.Response.Cookies.Delete("token");
   return Results.Ok("Sesión cerrada");
}).AllowAnonymous();

app.Run();
