using System.Net.Http.Json;
using tellahs_library.Constants;
using tellahs_library.DTOs;
using tellahs_library.Enums;
using static tellahs_library.DTOs.FeApiResponse;
using static tellahs_library.Helpers.EndpointHelper;

namespace tellahs_library.Helpers;

public static class SeedRollerHelper
{
    private const int MAX_TRIES = 20;
    private const int STANDARD_DELAY = 2;
    public static async Task<SeedResponse> RollSeedAsync(HttpClient client, GenerateRequest generateRequest, FeHostedApi api)
    {
        var apiKey = GetApiKey(api);
        if (string.IsNullOrWhiteSpace(apiKey))
        {
            return SetError<SeedResponse>($"Configuration incomplete, cannot create seeds for {api}");
        }

        return await GenerateSeedAsync(client, generateRequest, api, apiKey) switch
        {
            var error when error.Status == FeStatusConstants.Error => SetError<SeedResponse>(error.Error),

            ProgressResponse pr when pr.seed_id.HasContent()
                => await GetGeneratedSeedAsync(client, api, apiKey, pr.seed_id),

            ProgressResponse pr when pr.task_id.HasContent()
                => await PollForSeedAsync(client, api, apiKey, pr.task_id),

            _ => SetError<SeedResponse>("Unknown Issue preventing seed generation")
        };
    }

    private static async Task<FeApiResponse> GenerateSeedAsync(HttpClient client, GenerateRequest generateRequest, FeHostedApi api, string apiKey)
    {
        var postUrl = GenerateUrl(api, apiKey);
        var generateResponse = await client.PostAsJsonAsync(
            postUrl,
            generateRequest);

        if (!generateResponse.IsSuccessStatusCode)
        {
            var message = await generateResponse.Content.ReadAsStringAsync();
            return SetError<ProgressResponse>(message);
        }

        return await generateResponse.Content.ReadFromJsonAsync<ProgressResponse>()
            ?? SetError<ProgressResponse>("Failed to get content");
    }

    private static async Task<SeedResponse> PollForSeedAsync(HttpClient client, FeHostedApi api, string apiKey, string taskId)
    {
        var tries = 0;
        var progressResponse = new ProgressResponse();
        List<string> stopProcessingStatuses = [FeStatusConstants.Done, FeStatusConstants.Error];

        while (!stopProcessingStatuses.Contains(progressResponse.Status) && tries < MAX_TRIES)
        {
            await Task.Delay(TimeSpan.FromSeconds(STANDARD_DELAY).Add(TimeSpan.FromMilliseconds(tries * 100)));
            var taskResponse = await client.GetAsync(TaskUrl(api, apiKey, taskId));

            if (!taskResponse.IsSuccessStatusCode)
            {
                return SetError<SeedResponse>(await taskResponse.Content.ReadAsStringAsync());
            }

            progressResponse = await taskResponse.Content.ReadFromJsonAsync<ProgressResponse>()
                ?? progressResponse;

            tries++;
        }

        if (progressResponse.Error.HasContent())
        {
            return SetError<SeedResponse>(progressResponse.Error);
        }

        if (string.IsNullOrWhiteSpace(progressResponse.seed_id))
        {
            return SetError<SeedResponse>("API seems to be not responding");
        }

        return await GetGeneratedSeedAsync(client, api, apiKey, progressResponse.seed_id);
    }

    private static async Task<SeedResponse> GetGeneratedSeedAsync(HttpClient client, FeHostedApi api, string apiKey, string seedId)
    {
        var getSeedResponse = await client.GetAsync(SeedUrl(api, apiKey, seedId));

        if (getSeedResponse.IsSuccessStatusCode)
        {
            return await getSeedResponse.Content.ReadFromJsonAsync<SeedResponse>() ?? SetError<SeedResponse>("Failed to get seed content");
        }
        else
        {
            var errorMessage = await getSeedResponse.Content.ReadAsStringAsync();
            return SetError<SeedResponse>(errorMessage);
        }
    }

    private static string GetApiKey(FeHostedApi url)
    {
        return Environment.GetEnvironmentVariable($"{url}_API_KEY".ToUpperInvariant()) ?? string.Empty;
    }
}
