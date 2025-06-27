using Microsoft.EntityFrameworkCore;
using TeiasProxy.Data;
using TeiasProxy.Helpers;
using TeiasProxy.Middleware;
using TeiasProxy.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

builder.Services.AddDbContext<ProxyDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("PostgreSql")));

builder.Services.AddControllers();
builder.Services.AddHttpClient<TeiasProxyService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseMiddleware<LagosAuthMiddleware>();

app.MapControllers();
DummyDataSeeder.SeedIfNeeded(app.Services);
app.Run();
 
