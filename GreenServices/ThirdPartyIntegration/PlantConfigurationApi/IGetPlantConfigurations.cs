using GreenServices.Models;

namespace GreenServices.ThirdPartyIntegration.PlantConfigurationApi
{
    public interface IGetPlantConfigurations
    {
        Task<List<Plantconfiguration>> GetPlantConfigurationsAsync(CancellationToken cancellationToken);
    }
}
