using GreenServices.Models;

public interface IGetSensorReadings
{
    Task<List<Sensorreading>> GetSensorReadingsAsync(CancellationToken cancellationToken);
}