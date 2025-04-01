using GreenServices.Models;
using MediatR;

namespace Services.Events.UpdateSensorReadings
{
    public class UpdateSensorReadingsEvent : IRequest
    {
        public List<Sensorreading> _sensorReadings { get; }
        public UpdateSensorReadingsEvent(List<Sensorreading> sensorReadings)
        {
            _sensorReadings = sensorReadings;
        }
    }
}
