using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Persistence;
using Repositories;
using Services.AutomapperProfiles;
using Services.Factory;

namespace API.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services,
            IConfiguration config)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });
            });

            services.AddDbContext<DataContext>(opt =>
            {
                opt.UseSqlServer(config.GetConnectionString("DefaultConnection"));
            });


            services.AddAutoMapper(typeof(MappingProfiles).Assembly);
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IServiceFactory, ServiceFactory>();

            return services;
        }
    }
}
