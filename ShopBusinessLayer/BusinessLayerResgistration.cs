using Microsoft.Extensions.DependencyInjection;
using ShopBusinessLayer.Common;
using ShopBusinessLayer.Services.Interfaces;
using ShopBusinessLayer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace ShopBusinessLayer
{
    public static class BusinessLayerResgistration
    {
        public static IServiceCollection AddBusinessLayerResgistration(this IServiceCollection services)
        {  
            services.AddAutoMapper(typeof(MappingProfile));
            services.AddScoped(typeof(IPaginationService<,>), typeof(PaginationService<,>));

            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IBrandService, BrandService>();
            services.AddScoped<ICategoryService,CategoryService>();
            services.AddScoped<IProductService, ProductService>();


            return services;
        }
     
    }
}
