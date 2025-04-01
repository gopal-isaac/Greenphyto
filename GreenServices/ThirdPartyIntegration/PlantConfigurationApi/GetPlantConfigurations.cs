using GreenServices.Models;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace GreenServices.ThirdPartyIntegration.PlantConfigurationApi
{
    /// <summary>
    /// 
    /// </summary>
    public class GetPlantConfigurations : IGetPlantConfigurations
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<GetPlantConfigurations> _logger;

        public GetPlantConfigurations(HttpClient httpClient, ILogger<GetPlantConfigurations> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<List<Plantconfiguration>> GetPlantConfigurationsAsync(CancellationToken cancellationToken)
        {

            //Setting up cancellation timeout
            using var cancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
            cancellationTokenSource.CancelAfter(TimeSpan.FromSeconds(5)); //Maybe have a global timeout policy  

            _logger.LogInformation("Retrieving plant configurations from the Plant Configuration API");

            try
            {
                var response = await _httpClient.GetAsync("http://3.0.148.231:8020/plant-configurations", cancellationTokenSource.Token); //TODO: Move to appsettings

                _logger.LogInformation($"Response Status is {response.StatusCode}");
                response.EnsureSuccessStatusCode(); // Instead of checking the status code manually, throw the ex

                var content = await response.Content.ReadAsStringAsync();
                var plantConfigurations = JsonSerializer.Deserialize<List<Plantconfiguration>>(content); // Potential bug: when deserializing.

                return plantConfigurations ?? new List<Plantconfiguration>(); // catch for null 

            }
            catch (TaskCanceledException ex) when (!cancellationToken.IsCancellationRequested)
            {
                _logger.LogError(ex, "Timeout occurred while calling the Plant Configuration API.");
                return new List<Plantconfiguration>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error while retrieving plant configurations.");
                return new List<Plantconfiguration>();
            }


        }

    }
}
