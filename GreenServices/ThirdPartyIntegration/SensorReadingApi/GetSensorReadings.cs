using GreenServices.Models;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace GreenServices.ThirdPartyIntegration.SensorReadingApi
{
    /// <summary>
    /// 
    /// </summary>
    public class GetSensorReadings : IGetSensorReadings
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<GetSensorReadings> _logger;

        public GetSensorReadings(HttpClient httpClient, ILogger<GetSensorReadings> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<List<Sensorreading>> GetSensorReadingsAsync(CancellationToken cancellationToken)
        {


            //Setting up cancellation timeout
            using var cancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
            cancellationTokenSource.CancelAfter(TimeSpan.FromSeconds(5)); //Maybe have a global timeout policy  

            _logger.LogInformation("Retrieving sensor readings from the Sensor Reading API");

            try
            {
                var response = await _httpClient.GetAsync("http://3.0.148.231:8010/sensor-readings", cancellationTokenSource.Token); //TODO: Move to appsettings

                _logger.LogInformation($"Response Status is {response.StatusCode}");
                response.EnsureSuccessStatusCode(); // Instead of checking the status code manually, throw the ex

                var content = await response.Content.ReadAsStringAsync();
                var sensorreadings = JsonSerializer.Deserialize<List<Sensorreading>>(content); // Potential bug: when deserializing.

                return sensorreadings ?? new List<Sensorreading>(); // catch for null 

            }
            catch (TaskCanceledException ex) when (!cancellationToken.IsCancellationRequested)
            {
                _logger.LogError(ex, "Timeout occurred while calling the Sensor Reading API.");
                return new List<Sensorreading>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error while retrieving Sensor Readings.");
                return new List<Sensorreading>();
            }
        }
    }
}
