using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Store.Application;
using Store.Infrastructure.Data;

namespace Store.Infrastructure
{
    public class DbContextConfigurator : IDbContextConfigurator
    {
        public void ConfigureDbContext(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AdventureWorksDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("AdventureWorks")));
        }
    }
}
