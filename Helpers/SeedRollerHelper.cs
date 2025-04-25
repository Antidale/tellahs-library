using System.Net.Http.Json;
using tellahs_library.DTOs;
using tellahs_library.Enums;
using static tellahs_library.Helpers.EndpointHelper;

namespace tellahs_library.Helpers;

public static class SeedRollerHelper
{
    private const int MAX_TRIES = 20;
    public static async Task<SeedResponse> RollSeedAsync(HttpClient client, GenerateRequest generateRequest, FeHostedApi api)
    {
        var seedResponse = new SeedResponse();
        var apiKey = GetApiKey(api);
        if (string.IsNullOrWhiteSpace(apiKey))
        {
            seedResponse.Error = $"Configuration incomplete, cannot create seeds for {api}";
            return seedResponse;
        }

        var postUrl = GenerateUrl(api, apiKey);
        var generateResponse = await client.PostAsJsonAsync(
            postUrl,
            generateRequest);

        if (generateResponse.IsSuccessStatusCode)
        {
            var progressResponse = await generateResponse.Content.ReadFromJsonAsync<ProgressResponse>();

            if (progressResponse is null)
            {
                seedResponse.Status = "error";
                seedResponse.Error = "Somehow we're reading nothing from the endpoint";
            }
            else if (!string.IsNullOrWhiteSpace(progressResponse.seed_id))
            {
                var getSeedResponse = await client.GetAsync(SeedUrl(api, apiKey, progressResponse.seed_id));

                if (getSeedResponse.IsSuccessStatusCode)
                {
                    seedResponse = await getSeedResponse.Content.ReadFromJsonAsync<SeedResponse>() ?? seedResponse;
                }
                else
                {
                    seedResponse.Error = await getSeedResponse.Content.ReadAsStringAsync();

                    return seedResponse;
                }
            }
            else if (progressResponse.task_id == string.Empty)
            {
                seedResponse.Status = "error";
                seedResponse.Error = "task_id came back wrong. halp";
            }
            else
            {
                //poll the /Task endpoint until we get a response, or until too much time has elapsed and we just kinda give up
                var retries = 0;
                while (progressResponse.seed_id
                == string.Empty && retries < MAX_TRIES)
                {
                    //Retry every second, plus a bit
                    var taskId = progressResponse.task_id;
                    await Task.Delay(TimeSpan.FromSeconds(2) + TimeSpan.FromMilliseconds(retries * 100));
                    var taskResponse = await client.GetAsync(TaskUrl(api, apiKey, progressResponse.task_id));

                    if (!taskResponse.IsSuccessStatusCode)
                    {
                        seedResponse.Error = await taskResponse.Content.ReadAsStringAsync();

                        return seedResponse;
                    }

                    progressResponse = await taskResponse.Content.ReadFromJsonAsync<ProgressResponse>() ?? progressResponse;
                    retries++;
                    //put it back.
                    progressResponse.task_id = taskId;
                }

                if (string.IsNullOrWhiteSpace(progressResponse.seed_id))
                {
                    seedResponse.Status = "error";
                    seedResponse.Error = "API seems to be not responding";
                    return seedResponse;
                }

                if (!string.IsNullOrWhiteSpace(progressResponse.Error))
                {
                    seedResponse.Status = "error";
                    seedResponse.Error = progressResponse.Error;
                    return seedResponse;
                }

                //now get the seed
                var getSeedResponse = await client.GetAsync(SeedUrl(api, apiKey, progressResponse.seed_id));

                if (getSeedResponse.IsSuccessStatusCode)
                {
                    seedResponse = await getSeedResponse.Content.ReadFromJsonAsync<SeedResponse>() ?? seedResponse;
                }
                else
                {
                    seedResponse.Error = await getSeedResponse.Content.ReadAsStringAsync();

                    return seedResponse;
                }
            }
        }
        else
        {
            seedResponse.Status = "error";
            seedResponse.Error = generateResponse.StatusCode.ToString();
        }

        return seedResponse;
    }

    private static string GetApiKey(FeHostedApi url)
    {
        return Environment.GetEnvironmentVariable($"{url}_API_KEY".ToUpperInvariant()) ?? string.Empty;
    }
}
