using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ensek.Service.DI
{
    public class ServiceContainer
    {
        public static void Register(IServiceCollection services)
        {
            services.AddScoped<IUploadFileService, UploadFileService>();
            services.AddScoped<IValidate, Validate>();

        }
    }
}
