using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Store.API.Profiles;
using Store.Application;
using Store.Application.Interfaces;
using Store.Application.Services;
using Store.Infrastructure;
using Store.Infrastructure.Data;
using Store.Infrastructure.Repositories;
using Store.Infrastructure.Services;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<AdventureWorksDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("AdventureWorksConnection")));
// builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddAutoMapper(typeof(ProductProfile), typeof(CategoryProfile));
// Add services to the container.
builder.Services.AddSingleton<IDbContextConfigurator, DbContextConfigurator>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();
builder.Services.AddSingleton<ITokenService, TokenService>();
builder.Services.AddScoped<Store.Application.Interfaces.IAuthService, AuthService>();
// Add BCrypt (if not using the enhanced version)
builder.Services.AddSingleton<IPasswordHasher>(_ =>
    new PasswordHasher(BCrypt.Net.BCrypt.GenerateSalt(12)));

// Use dependency injection to resolve the configurator instead of calling BuildServiceProvider
builder.Services.AddScoped(provider =>
{
    var configurator = provider.GetRequiredService<IDbContextConfigurator>();
    configurator.ConfigureDbContext(builder.Services, builder.Configuration);
    return configurator;
});

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Product API", Version = "v1" });

    // Include XML comments
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// --- JWT Authentication Configuration Start 

var jwtSettings = builder.Configuration.GetSection("JwtSettings");
var secretKey = jwtSettings["Secret"] ?? 
    throw new InvalidOperationException("JWT Secret is not configured.");
var issuer = jwtSettings["Issuer"] ?? 
    throw new InvalidOperationException("JWT Issuer is not configured.");
var audience = jwtSettings["Audience"] ?? 
    throw new InvalidOperationException("JWT Audience is not configured.");

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidIssuer = issuer,
        ValidAudience = audience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))

    };
});

// --- JWT Authentication Configuration End -- 

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
// -- Add Authentication and Authorization Middleware
app.UseAuthentication(); 
app.UseAuthorization();
// -- End Middleware

app.MapControllers();

app.Run();