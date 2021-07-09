using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zino.Core.DI;
using Zino.Service.Enums;
using Zino.Service.Services;

namespace Zino.Api.Extensions
{
    public static class StartupExtension
    {
        public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IServiceConvert, ServiceConvert>();
            services.AddScoped<IUnitFurmola, UnitFurmola>();
            ServiceLocator.SetLocatorProvider(services.BuildServiceProvider());
            services.RegisterSwagger();
        }
        private static void RegisterSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Unit Convertion" });
            });
        }
        public static void UseZinoSwagger(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None);
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Unit Convertion API");
            });
        }
    }
}
