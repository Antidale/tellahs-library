using System.Collections.Concurrent;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace tellahs_library.Services;

public class RacetimeRacesService(RacetimeHttpClient racetimeHttpClient, Logger<RacetimeRacesService> logger) : BackgroundService
{
    private readonly RacetimeHttpClient client = racetimeHttpClient;
    private readonly ConcurrentDictionary<string, string> races = new();
    private bool hasStarted = false;

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        if (stoppingToken.IsCancellationRequested)
        {
            //store data about races
            return;
        }

        using PeriodicTimer timer = new(TimeSpan.FromMinutes(5));
        try
        {
            while (await timer.WaitForNextTickAsync(stoppingToken))
            {
                await DoWork();
            }
        }
        catch (Exception ex)
        {
            logger.LogError("error: {ex}", ex.Message);
        }
    }

    private async Task DoWork()
    {
        if (!hasStarted)
        {
            //go to the db to track what races we knew about before the system shut down.
        }

        var racedata = await client.GetActiveRaces();
        //get the active races for FE
        //iterate through the urls
        //add missing urls
        //update existing ones (if needed due to status change [opened vs running])
        //delete ones no longer on the list
    }

}
