using GreenServices.Models;

namespace GreenServices.Repository
{
    public interface IPlantRepository
    {
        Task UpdatePlantConfigurations(List<Plantconfiguration> plantconfigurationList, CancellationToken cancellation);
        Task UpdateSensorReadings(List<Sensorreading> sensorreadingList, CancellationToken cancellation);
    }
}
