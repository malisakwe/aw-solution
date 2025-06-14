using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Store.Application.Infrastructure
{
    public interface IDatabaseServiceConfiguration
    {
        IServiceCollection AddDatabaseServices(IServiceCollection services, IConfiguration configuration); 
    }
}
