using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Store.Application.Infrastructure;
using Store.Application.Interfaces;
using Store.Infrastructure.Data;
using Store.Infrastructure.Repositories;

namespace Store.Infrastructure.Configuration
{
    public class DatabaseServiceConfiguration: IDatabaseServiceConfiguration
    {
        public IServiceCollection AddDatabaseServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AdventureWorksDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IProductRepository, ProductRepository>();
            // This method can be used to configure database services if needed
            return services;
        }
    }
    
    
}
