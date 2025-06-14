using Microsoft.EntityFrameworkCore;
using Store.API.Profiles;
using Store.Application;
using Store.Application.Interfaces;
using Store.Infrastructure;
using Store.Infrastructure.Data;
using Store.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<AdventureWorksDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("AdventureWorksConnection")));
// builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddAutoMapper(typeof(ProductProfile), typeof(CategoryProfile));
// Add services to the container.
builder.Services.AddSingleton<IDbContextConfigurator, DbContextConfigurator>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();

// Use dependency injection to resolve the configurator instead of calling BuildServiceProvider
builder.Services.AddScoped(provider =>
{
    var configurator = provider.GetRequiredService<IDbContextConfigurator>();
    configurator.ConfigureDbContext(builder.Services, builder.Configuration);
    return configurator;
});

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();