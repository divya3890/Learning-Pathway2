using Ensek.Data;
using Ensek.Data.DI;
using Ensek.Service.DI;
using Microsoft.EntityFrameworkCore;

namespace Ensek.API.Configuration
{
    public static class RegisterServiceExtensions
    {
        public static void RegisterServices(this IServiceCollection services, string configuration)
        {
            new EnsekDbContext(configuration);
            DataContainer.Register(services);
            ServiceContainer.Register(services);
            services.AddDbContext<EnsekDbContext>(options =>
            {
                options.UseSqlServer(configuration, b => b.MigrationsAssembly("Ensek.Data"));
            });

            var serviceProvider = services.BuildServiceProvider();
            var configContext = serviceProvider.GetRequiredService<EnsekDbContext>();
            configContext.Database.Migrate();
        }
    }
}
