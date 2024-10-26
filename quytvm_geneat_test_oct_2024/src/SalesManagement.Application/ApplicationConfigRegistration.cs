using Microsoft.Extensions.DependencyInjection;
using SalesManagement.Application.IService;
using SalesManagement.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SalesManagement.Application
{
    public static class ApplicationConfigRegistration
    {
        public static IServiceCollection ConfigureApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddScoped<IProductService, ProductService>();

            return services;
        }
    }
}
