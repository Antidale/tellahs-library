namespace tellahs_library.Services.RacetimeModels;

public record class RacesResponse
{
    public List<Race> Races { get; set; } = [];
}
