using GreenServices.Repository;
using MediatR;

namespace Services.Events.UpdatePlantConfigurations
{
    public class UpdatePlantConfigurationsEventHandler : IRequestHandler<UpdatePlantConfigurationsEvent>
    {
        public IPlantRepository _plantRepository { get; }

        public UpdatePlantConfigurationsEventHandler(IPlantRepository plantRepository)
        {
            _plantRepository = plantRepository;
        }

        public async Task Handle(UpdatePlantConfigurationsEvent request, CancellationToken cancellationToken)
        {
            await _plantRepository.UpdatePlantConfigurations(request._plantConfigurations, cancellationToken);
        }
    }
}
