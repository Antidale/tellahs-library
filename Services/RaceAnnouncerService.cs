using DSharpPlus;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using tellahs_library.Helpers;

namespace tellahs_library.Services;

public class RaceAnnouncerService(RacetimeHttpClient racetimeHttpClient, ILogger<RaceAnnouncerService> logger, ActiveRaces activeRaces, DiscordClient discordClient) : BackgroundService
{
    private bool hasStarted = false;

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        if (stoppingToken.IsCancellationRequested)
        {
            //clear existing data and then
            //store data about currently active races to pull back when the bot restarts
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

        //get the active races for FE
        var racedata = await racetimeHttpClient.GetRaces();
        //compare urls in raceData and in activeRaces
        //if in raceData, but not active races, create a message in the #race-alerts channel

        //If in activeRaces but not in raceData, delete the message currently posted in #race-alerts
        // var channel = await discordClient.GetChannelAsync(Constants.ChannelIds.WorkshopRaceAlertsId);
        // var message = await junk.GetMessageAsync();
        // await message.DeleteAsync();
        //TODO later: don't just add or remove, but allow a mechanism for updating race data
    }

}
