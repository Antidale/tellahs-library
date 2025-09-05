using System.Text.Json.Serialization;

namespace tellahs_library.Services.RacetimeModels;

public record class Race
{
    public required Status Status { get; set; }
    public required string Url { get; set; }
    public required Goal Goal { get; set; }
    public required string Info { get; set; }

    [JsonPropertyName("entrants_count")]
    public int EntrantsCount { get; set; }

    public required Category Category { get; set; }


}
