using Newtonsoft.Json;

namespace tellahs_library.Services.RacetimeModels;

public record class RacesResponse
{

    public required string Name { get; set; }

    public List<Race> CurrentRaces { get; set; } = [];
}
