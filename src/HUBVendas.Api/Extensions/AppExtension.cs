using System.Text.Json.Serialization;
using HUBVendas.Domain.Interfaces;
using HUBVendas.Infra.Repositories;
using HUBVendas.Service.Services;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace HUBVendas.Api.Extensions {
    public static class AppExtension {
        public static void ConfigureMvc(this WebApplicationBuilder builder) {
            builder.Services.AddControllers().ConfigureApiBehaviorOptions(options => {
                options.SuppressModelStateInvalidFilter = true;
            })
            .AddJsonOptions(x => {
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            });
        }

        public static void ConfigureServices(this WebApplicationBuilder builder) {
            builder.Services.TryAddScoped<ICategoryRepository, CategoryRepository>();
            builder.Services.TryAddScoped<ICategoryService, CategoryService>();
            builder.Services.TryAddScoped<IProductRepository, ProductRepository>();
            builder.Services.TryAddScoped<IProductService, ProductService>();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddCors(policeBuilder =>
                policeBuilder.AddDefaultPolicy(policy => policy.WithOrigins("*").AllowAnyHeader().AllowAnyMethod()));
        }
    }
}