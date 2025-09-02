using Application.Interfaces;
using Application.Services;
using Infrastructure.Data;
using Infrastructure.Persistence;
using Infrastructure.Repositories;
using Infrastructure.Security;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
var cfg = builder.Configuration;

// EF Core
builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseSqlServer(cfg.GetConnectionString("Default")));

// Application
builder.Services.AddScoped<ILoginService, LoginService>();
builder.Services.AddScoped<IRegisterService, RegisterService>();
builder.Services.AddControllers();
// Infrastructure
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IPasswordHasher, BcryptPasswordHasher>();
builder.Services.Configure<JwtOptions>(cfg.GetSection("Jwt"));
builder.Services.AddScoped<IJwtTokenService, JwtTokenService>();

// Auth (JWT)
var jwt = cfg.GetSection("Jwt");
var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt["Key"]!));
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(o =>
    {
        o.TokenValidationParameters = new()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwt["Issuer"],
            ValidAudience = jwt["Audience"],
            IssuerSigningKey = key
        };
    });
builder.Services.AddAuthorization();

// Swagger
builder.Services.AddEndpointsApiExplorer();


builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Login API", Version = "v1" });

    // Soporte para Bearer en Swagger UI
    var scheme = new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Ingrese: Bearer {token}"
    };
    c.AddSecurityDefinition("Bearer", scheme);
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        { new OpenApiSecurityScheme { Reference = new OpenApiReference {
              Type = ReferenceType.SecurityScheme, Id = "Bearer" } }, Array.Empty<string>() }
    });
});


var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();
app.MapControllers();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();



app.Run();
