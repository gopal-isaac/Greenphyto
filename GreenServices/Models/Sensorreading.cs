using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace GreenServices.Models;

public partial class Sensorreading
{
    public int Id { get; set; }

    [JsonPropertyName("tray_id")]
    public int TrayId { get; set; }

    [JsonPropertyName("temperature")]
    public decimal Temperature { get; set; }

    [JsonPropertyName("humidity")]
    public decimal Humidity { get; set; }

    [JsonPropertyName("light")]
    public int Light { get; set; }

    public DateTimeOffset? CreatedAt { get; set; } = DateTimeOffset.UtcNow;

    public virtual Plantconfiguration Tray { get; set; } = null!;
}
