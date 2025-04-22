
namespace tellahs_library.Extensions;

public static class HttpClientExtensions
{
    public static HttpClient ConfigureForFeInfo(this HttpClient client)
    {
        var apiKey = Environment.GetEnvironmentVariable("FE_Info_Api_Key");
        var baseAddress = new Uri("https://free-enterprise-info-api.herokuapp.com/api/");

#if DEBUG
        apiKey = "test";
        baseAddress = new Uri("https://localhost:5001/api/");
#endif
        client.BaseAddress = baseAddress;

        client.DefaultRequestHeaders.Add("Api-Key", apiKey);

        return client;
    }
}
