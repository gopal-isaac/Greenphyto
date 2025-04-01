using Common;
using GreenServices.Models;

namespace GreenServices.Services.Plant
{
    public interface IPlantServices
    {
        Task<GenericResponse<(List<Plantconfiguration>, List<Sensorreading>)>> GetPlantSensorDataAsync(CancellationToken cancellationToken);
    }
}
