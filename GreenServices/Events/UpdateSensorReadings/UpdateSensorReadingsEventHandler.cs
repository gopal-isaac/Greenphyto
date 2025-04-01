using GreenServices.Repository;
using MediatR;

namespace Services.Events.UpdateSensorReadings
{
    public class UpdateSensorReadingsEventHandler : IRequestHandler<UpdateSensorReadingsEvent>
    {
        public IPlantRepository _plantRepository { get; }
        public UpdateSensorReadingsEventHandler(IPlantRepository plantRepository)
        {
            _plantRepository = plantRepository;
        }

        public async Task Handle(UpdateSensorReadingsEvent request, CancellationToken cancellationToken)
        {
            await _plantRepository.UpdateSensorReadings(request._sensorReadings, cancellationToken);
        }
    }
}
