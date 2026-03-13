using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using tellahs_library.RacingCommands.Requests;
using tellahs_library.Services.RacetimeModels;

namespace tellahs_library.HttpClients;

public partial class RacetimeHttpClient : HttpClient
{
    private readonly string ClientId = string.Empty;
    private readonly string ClientSecret = string.Empty;
    private string authToken = string.Empty;
    private readonly ILogger Logger;
    private DateTime TokenExpiresAt = DateTime.UtcNow.AddMinutes(-10);
    private readonly JsonSerializerOptions serializerOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
    };

    public RacetimeHttpClient(ILogger<RacetimeHttpClient> logger)
    {
        ClientId = Environment.GetEnvironmentVariable("Racetime_Client_Id") ?? ClientId;
        ClientSecret = Environment.GetEnvironmentVariable("Racetime_Client_Secret") ?? ClientSecret;
        var baseAddress = new Uri("https://racetime.gg");

#if DEBUG
        ClientId = Environment.GetEnvironmentVariable("Local_RTGG_Client_ID") ?? ClientId;
        ClientSecret = Environment.GetEnvironmentVariable("Local_RTGG_Client_Secret") ?? ClientSecret;
        baseAddress = new Uri("http://localhost:8000");
#endif

        BaseAddress = baseAddress;
        Logger = logger;
    }

    private async Task AuthorizeAsync()
    {
        DefaultRequestHeaders.Remove("Authorization");
        var stuff = new StringContent($"client_id={ClientId}&client_secret={ClientSecret}&grant_type=client_credentials", Encoding.UTF8, "application/x-www-form-urlencoded");

        var response = await this.PostAsync("/o/token", stuff);

        if (!response.IsSuccessStatusCode)
        {
            LogAuthError("unsuccessful status code");
            return;
        }

        var responseDto = await response.Content.ReadFromJsonAsync<AuthResponse>(serializerOptions);
        if (responseDto == null)
        {
            LogAuthError("null auth response dto");
            return;
        }

        TokenExpiresAt = DateTime.UtcNow.AddSeconds(responseDto.ExpiresIn - 100);

        authToken = responseDto.AccessToken;

        DefaultRequestHeaders.Add("Authorization", $"Bearer {authToken}");
    }

    public async Task<HttpResponseMessage?> CreateRaceAsync(CreateRace request)
    {
        if (!DefaultRequestHeaders.Contains("Authorization") || TokenExpiresAt < DateTime.UtcNow)
        {
            await AuthorizeAsync();
        }

        var content = request.ToStringContent();

        return await this.PostAsync("/o/ff4fe/startrace", content);
    }

    public async Task<List<string>> GetRaceUrls()
    {
        var response = await this.GetAsync("/ff4fe/data");
        try
        {
            var racesData = await response.Content.ReadFromJsonAsync<RacesResponse>(serializerOptions);

            if (racesData is null)
            { return []; }

            return [.. racesData.CurrentRaces.Select(x => string.Concat(BaseAddress!.AbsoluteUri, x.Url[1..]))];
        }
        catch (Exception ex)
        {
            LogFetchRacesError(ex.Message);
            return [];
        }
    }

    [LoggerMessage(Level = LogLevel.Error, Message = "Error fetcing races from rtgg: {ex}")]
    private partial void LogFetchRacesError(string ex);

    [LoggerMessage(Level = LogLevel.Error, Message = "Failed to authorize with rt.gg {messageDetail}")]
    private partial void LogAuthError(string messageDetail);


    record AuthResponse(string AccessToken, int ExpiresIn, string TokenType, string Scope) { }

}
