using Greenphyto.Features.Plants.GetPlantsSensor;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.FeatureManagement;
using WebApi.FeatureFlags;

namespace Greenphyto.Controllers
{
    [ApiController]
    //[Route("[controller]")] //Ideally I'll keep this route as it is. 
    public class FarmMonitoringController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<FarmMonitoringController> _logger;
        private readonly IFeatureManager _featureManager;

        public FarmMonitoringController(IMediator mediator, ILogger<FarmMonitoringController> logger, IFeatureManager featureManager)
        {
            _mediator = mediator;
            _logger = logger;
            _featureManager = featureManager;
        }

        /// <summary>
        /// Get plant sensor data
        /// </summary>
        /// <returns></returns>
        [HttpGet("plant-sensor-data")]
        public async Task<IActionResult> GetPlantSensorData()
        {
            _logger.LogInformation($"Retrieving plant sensor data at {DateTimeOffset.UtcNow}");

            try
            {
                if (await _featureManager.IsEnabledAsync(FeatureFlag.AdditionalApi))
                {
                    return Ok("Featureflag is active, congrats");
                }

                var response = await _mediator.Send(new GetPlantSensorDataQuery());

                if (response.Success)
                {
                    return Ok(new
                    {
                        status = "success",
                        message = "Data retrieved successfully.",
                        data = response.Data
                    });
                }

                //TODO: Room for improvement: The response should handle the error accoringly.
                return BadRequest(new
                {
                    status = "error",
                    message = response.Message
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving plant sensor data");

                // Not throwing the exception to the client, but logging it.
                return StatusCode(500, new
                {
                    status = "error",
                    message = "An internal error occurred while processing your request."
                });
            }
        }

    }
}
