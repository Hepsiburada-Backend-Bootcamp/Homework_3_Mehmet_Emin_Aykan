using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ECommerce.Application.Services;
using ECommerce.Application.Services.Brand;
using ECommerce.Application.Services.Category;
using Microsoft.Extensions.DependencyInjection;
using ECommerce.Infrastructure;
using Microsoft.Extensions.Configuration;
using ECommerce.Application.Services.Customer;
using ECommerce.Application.Services.Product;

namespace ECommerce.Application
{
    public static class ApplicationModuleExtensions
    {
        public static IServiceCollection AddApplicationModule(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddScoped<ICustomerService, CustomerService>();

            services.AddScoped<ICategoryService, CategoryService>();

            services.AddScoped<IBrandService, BrandService>();

            services.AddScoped<IProductService, ProductService>();

            services.AddInfrastructureModule(configuration);

            return services;
        }
    }
}
