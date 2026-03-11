using System.Collections.Concurrent;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using tellahs_library.Helpers;
using tellahs_library.Services.RacetimeModels;

namespace tellahs_library.Services;

public class RaceAnnouncerService(RacetimeHttpClient racetimeHttpClient, ILogger<RaceAnnouncerService> logger, ActiveRaces activeRaces) : BackgroundService
{
    private bool hasStarted = false;

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        if (stoppingToken.IsCancellationRequested)
        {
            //store data about races
            return;
        }

        using PeriodicTimer timer = new(TimeSpan.FromSeconds(15));
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
            var db = SqliteHelper.GetAsyncSqlConnection();
            var data = await db.Table<Entities.ActiveRace>().ToListAsync();
            foreach (var race in data)
            {
                activeRaces.AddOrUpdateRace(race.Url, race);
            }

            hasStarted = true;
        }

        var racedata = await racetimeHttpClient.GetRaces();
        //get the active races for FE
        //iterate through the urls
        //add missing urls
        //update existing ones (if needed due to status change [opened vs running])
        //delete ones no longer on the list
    }

}
