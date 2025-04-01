using GreenServices.Models;
using MediatR;

namespace Services.Events.UpdatePlantConfigurations
{
    public class UpdatePlantConfigurationsEvent : IRequest
    {
        public List<Plantconfiguration> _plantConfigurations { get; }

        public UpdatePlantConfigurationsEvent(List<Plantconfiguration> plantConfigurations)
        {
            _plantConfigurations = plantConfigurations;
        }
    }
}
