namespace Greenphyto.Features.Plants.GetPlantsSensor
{
    public class GetPlantSensorDataResponse
    {
        public int TrayId { get; set; }
        public string PlantType { get; set; }
        public decimal TargetTemperature { get; set; }
        public decimal TargetHumidity { get; set; }
        public int TargetLight { get; set; }
        public decimal? SensorTemperature { get; set; }
        public decimal? SensorHumidity { get; set; }
        public int? SensorLight { get; set; }

    }
}
