using GreenServices.Models;
using Microsoft.Extensions.Logging;

namespace GreenServices.Repository
{
    public class PlantRepository : IPlantRepository
    {
        private readonly PlantContext _plantContext;
        private readonly ILogger<PlantRepository> _logger;

        public PlantRepository(PlantContext plantContext, ILogger<PlantRepository> logger)
        {
            _plantContext = plantContext;
            _logger = logger;
        }

        public async Task UpdatePlantConfigurations(List<Plantconfiguration> plantconfigurationList, CancellationToken cancellation)
        {
            if (plantconfigurationList == null || !plantconfigurationList.Any())
            {
                _logger.LogError("Plant configuration list is null or empty");
                throw new ArgumentNullException(nameof(plantconfigurationList));
            }

            // Todo: This can be refactored to use a bulk update library.
            foreach (var plantconfiguration in plantconfigurationList)
            {
                var existingConfig = _plantContext.Plantconfigurations.FirstOrDefault(p => p.TrayId == plantconfiguration.TrayId);

                if (existingConfig == null)
                {
                    // Insert new record
                    _plantContext.Plantconfigurations.Add(plantconfiguration);
                }
                else
                {
                    // Update existing record
                    existingConfig.TargetTemperature = plantconfiguration.TargetTemperature;
                    existingConfig.TargetHumidity = plantconfiguration.TargetHumidity;
                    existingConfig.TargetLight = plantconfiguration.TargetLight;
                }
            }

            await _plantContext.SaveChangesAsync();
            _logger.LogInformation("Plant configurations updated successfully");
        }

        public async Task UpdateSensorReadings(List<Sensorreading> sensorreadingList, CancellationToken cancellation)
        {
            if (sensorreadingList == null || !sensorreadingList.Any())
            {
                _logger.LogError("Sensor reading list is null");
                throw new ArgumentNullException(nameof(sensorreadingList));
            }

            // Appending to records
            _plantContext.Sensorreadings.AddRange(sensorreadingList);
            await _plantContext.SaveChangesAsync();
            _logger.LogInformation("Sensor readings updated successfully");
        }

    }
}
