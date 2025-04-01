using Common;
using GreenServices.Services.Plant;
using MediatR;

namespace Greenphyto.Features.Plants.GetPlantsSensor
{
    public class GetPlantSensorDataHandler : IRequestHandler<GetPlantSensorDataQuery, GenericResponse<List<GetPlantSensorDataResponse>>>
    {
        private readonly IPlantServices _plantServices;
        private readonly ILogger<GetPlantSensorDataHandler> _logger;

        public GetPlantSensorDataHandler(IPlantServices plantServices, ILogger<GetPlantSensorDataHandler> logger)
        {
            _plantServices = plantServices;
            _logger = logger;
        }

        public async Task<GenericResponse<List<GetPlantSensorDataResponse>>> Handle(GetPlantSensorDataQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _plantServices.GetPlantSensorDataAsync(cancellationToken);
                var (plantConfigurations, sensorReadings) = response.Data;


                if (!plantConfigurations.Any() && !sensorReadings.Any())
                {
                    var errorMessage = "No plant configurations or sensor readings found.";
                    _logger.LogWarning(errorMessage);
                    return GenericResponse<List<GetPlantSensorDataResponse>>.Failure(errorMessage);
                }

                // Combine the plant configurations with the sensor readings here instead at the service level. its up to the api to decide how to use the data
                var combinedData = (from pc in plantConfigurations
                                    join sr in sensorReadings
                                    on pc.TrayId equals sr.TrayId into sensorGroup
                                    from sr in sensorGroup.DefaultIfEmpty() 
                                    select new GetPlantSensorDataResponse
                                    {
                                        TrayId = pc.TrayId,
                                        PlantType = pc.PlantType,
                                        TargetTemperature = pc.TargetTemperature,
                                        TargetHumidity = pc.TargetHumidity,
                                        TargetLight = pc.TargetLight,
                                        SensorTemperature = sr?.Temperature,
                                        SensorHumidity = sr?.Humidity,
                                        SensorLight = sr?.Light
                                    }).ToList();

                
                return new GenericResponse<List<GetPlantSensorDataResponse>>(combinedData, true, "Data retrieved successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving plant sensor data"); // Remember not to swallow exceptions
                return GenericResponse<List<GetPlantSensorDataResponse>>.Failure($"An error occurred: {ex.Message}");
            }
        }
    }
}
