using System;

namespace tellahs_library.RacingCommands.Services;

public class RacetimeTokenService
{
    private readonly string ClientId = string.Empty;
    private readonly string ClientSecret = string.Empty;
    private readonly RacetimeHttpClient httpClient;

    public RacetimeTokenService(RacetimeHttpClient client)
    {

    }
}
