namespace tellahs_library.RacingCommands;

public class RacetimeHttpClient : HttpClient
{
    private readonly string ClientId = string.Empty;
    private readonly string ClientSecret = string.Empty;
    private string authToken = string.Empty;

    public RacetimeHttpClient()
    {
        ClientId = Environment.GetEnvironmentVariable("Racetime_Client_Id") ?? ClientId;
        ClientSecret = Environment.GetEnvironmentVariable("Racetime_Client_Secret") ?? ClientSecret;
        var baseAddress = new Uri("https://racetime.gg/o/");

#if DEBUG
        ClientId = Environment.GetEnvironmentVariable("Local_RTGG_Client_ID") ?? ClientId;
        ClientSecret = Environment.GetEnvironmentVariable("Local_RTGG_Client_Secret") ?? ClientSecret;
        baseAddress = new Uri("http://localhost:8000/o/");
#endif

        this.BaseAddress = baseAddress;
    }

    public async Task StartAuthTimer()
    {

    }

    private async Task Authorize()
    {

    }

    public async Task<bool> CreateRace()
    {
        return false;
    }
}
