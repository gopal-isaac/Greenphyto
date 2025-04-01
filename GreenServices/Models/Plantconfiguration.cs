using System.Text.Json.Serialization;

namespace GreenServices.Models;

public partial class Plantconfiguration
{
    [JsonPropertyName("tray_id")]
    public int TrayId { get; set; }

    [JsonPropertyName("plant_type")]
    public string PlantType { get; set; } = null!;

    [JsonPropertyName("target_temperature")]
    public decimal TargetTemperature { get; set; }

    [JsonPropertyName("target_humidity")]
    public decimal TargetHumidity { get; set; }

    [JsonPropertyName("target_light")]
    public int TargetLight { get; set; }

    public virtual ICollection<Sensorreading> Sensorreadings { get; set; } = new List<Sensorreading>();
}
