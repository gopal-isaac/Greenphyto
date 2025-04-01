using GreenServices.Models;
using GreenServices.Repository;
using GreenServices.Services.Plant;
using GreenServices.ThirdPartyIntegration.PlantConfigurationApi;
using GreenServices.ThirdPartyIntegration.SensorReadingApi;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace GreenServices
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IGetSensorReadings, GetSensorReadings>();
            services.AddScoped<IGetPlantConfigurations, GetPlantConfigurations>();
            services.AddScoped<IPlantServices, PlantServices>();
            services.AddTransient<IPlantRepository, PlantRepository>();


            return services;
        }

        public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<PlantContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

            return services;
        }

        public static IServiceCollection AddHttpClients(this IServiceCollection services)
        {
            services.AddHttpClient<IGetSensorReadings, GetSensorReadings>();
            services.AddHttpClient<IGetPlantConfigurations, GetPlantConfigurations>();

            return services;
        }

    }
}
