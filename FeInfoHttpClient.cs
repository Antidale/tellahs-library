using System;

namespace tellahs_library;

public class FeInfoHttpClient : HttpClient
{
    public FeInfoHttpClient()
    {
        var apiKey = Environment.GetEnvironmentVariable("FE_Info_Api_Key");
        var baseAddress = new Uri("https://free-enterprise-info-api.herokuapp.com/api/");

#if DEBUG
        apiKey = "test";
        baseAddress = new Uri("https://localhost:5001/api/");
#endif

        this.BaseAddress = baseAddress;
        this.DefaultRequestHeaders.Add("Api-Key", apiKey);
    }

}
