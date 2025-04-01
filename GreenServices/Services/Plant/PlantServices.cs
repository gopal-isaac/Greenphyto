using Common;
using GreenServices.Models;
using GreenServices.ThirdPartyIntegration.PlantConfigurationApi;
using MediatR;
using Microsoft.Extensions.Logging;
using Services.Events.UpdatePlantConfigurations;
using Services.Events.UpdateSensorReadings;

namespace GreenServices.Services.Plant
{
    public class PlantServices : IPlantServices
    {
        //Get data from API
        // inject iGet....
        // Update Database
        // Inject IPlantRepository
        // Return data to API

        public IGetPlantConfigurations _getPlantConfigurations { get; }
        public IGetSensorReadings _getSensorReadings { get; }
        public IMediator _mediator { get; }
        public ILogger<PlantServices> _logger { get; }

        public PlantServices(IGetPlantConfigurations getPlantConfigurations, IGetSensorReadings getSensorReadings, IMediator mediator, ILogger<PlantServices> logger)
        {
            _getPlantConfigurations = getPlantConfigurations;
            _getSensorReadings = getSensorReadings;
            _mediator = mediator;
            _logger = logger;
        }

        public async Task<GenericResponse<(List<Plantconfiguration>, List<Sensorreading>)>> GetPlantSensorDataAsync(CancellationToken cancellationToken)
        {
            try
            {
                // Using task when all to Async call to get data from API
                var plantConfigurationsTask = _getPlantConfigurations.GetPlantConfigurationsAsync(cancellationToken);
                var sensorReadingsTask = _getSensorReadings.GetSensorReadingsAsync(cancellationToken);

                _logger.LogInformation("Retrieving plant sensor data from the API");
                await Task.WhenAll(plantConfigurationsTask, sensorReadingsTask);


                var plantConfigurations = await plantConfigurationsTask ?? new List<Plantconfiguration>();
                var sensorReadings = await sensorReadingsTask ?? new List<Sensorreading>();


                //Updating the database using MediatR. Fire and forget
                if (plantConfigurations.Any())
                {
                    _logger.LogInformation("Updating plant configurations in the database");
                    await _mediator.Send(new UpdatePlantConfigurationsEvent(plantConfigurations), cancellationToken);
                }
                if (sensorReadings.Any())
                {
                    _logger.LogInformation("Updating sensor readings in the database");
                    await _mediator.Send(new UpdateSensorReadingsEvent(sensorReadings), cancellationToken);
                }


                //Assumation: The relevant data are the one thats being retreived instead of the one in the DB
                _logger.LogInformation("Successfully retrieved plant sensor data");
                return GenericResponse<(List<Plantconfiguration>, List<Sensorreading>)>.SuccessResponse((plantConfigurations, sensorReadings));

            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving plant sensor data");
                return GenericResponse<(List<Plantconfiguration>, List<Sensorreading>)>.Failure($"An error occurred: {ex.Message}");
            }

        }
    }
}
