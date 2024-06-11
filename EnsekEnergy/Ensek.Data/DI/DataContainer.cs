using Ensek.Data.Repository;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ensek.Data.DI
{
    public class DataContainer
    {
        public static void Register(IServiceCollection services)
        {
            services.AddScoped<IMeterReadingRepo, MeterReadingRepo>();
            services.AddScoped<IAccountRepo, AccountRepo>();

        }
    }
}
