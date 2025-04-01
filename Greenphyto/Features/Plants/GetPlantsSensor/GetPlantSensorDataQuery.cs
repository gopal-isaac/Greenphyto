using Common;
using MediatR;

namespace Greenphyto.Features.Plants.GetPlantsSensor
{
    public class GetPlantSensorDataQuery : IRequest<GenericResponse<List<GetPlantSensorDataResponse>>>
    {
    }
}
