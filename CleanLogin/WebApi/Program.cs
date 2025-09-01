using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Swagger (explorador + generador)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Habilitar Swagger SIEMPRE (no lo metas en if (Development))
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

// Endpoint mínimo para probar que la app responde (opcional)
app.MapGet("/ping", () => Results.Ok(new { ok = true, at = DateTime.UtcNow }));

app.Run();
