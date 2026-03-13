using DSharpPlus;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace tellahs_library.Services;

public partial class RaceAnnouncerService(RacetimeHttpClient racetimeHttpClient, ILogger<RaceAnnouncerService> logger, ActiveRaces activeRaces, DiscordClient discordClient) : BackgroundService
{
    ILogger Logger = logger;

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using PeriodicTimer timer = new(TimeSpan.FromMinutes(10));
        try
        {
            while (await timer.WaitForNextTickAsync(stoppingToken))
            {
                await DoWork();
            }
        }
        catch (Exception ex)
        {
            LogError(ex.Message);
        }
    }

    private async Task DoWork()
    {
        try
        {
            //get the active races for FE
            var rtRaces = await racetimeHttpClient.GetRaceUrls();

            //remove cancelled/finished races from ActiveRaces
            var trackingUrls = activeRaces.Races.Select(x => x.Url);
            var closedRaces = trackingUrls.Except(rtRaces);
            var recentMessages = await GetRecentMessages();

            foreach (var race in closedRaces)
            {
                var raceInfo = activeRaces.GetRace(race);
                await activeRaces.RemoveRaceAsync(race);

                if (raceInfo is not null &&
                    ulong.TryParse(raceInfo.MessageIdString,
                    out var messageId))
                {
                    var message = recentMessages.FirstOrDefault(x => x.Id == messageId);
                    message?.DeleteAsync("Race is no longer active");
                }

            }
        }
        catch (Exception ex)
        {
            LogError(ex.Message);
        }
        //TODO - when tracking new races created on rt.gg
        //Add racealert message and store race data in activeRaces

        //TODO later: don't just add or remove, but allow a mechanism for updating race data
    }

    private async Task<List<DiscordMessage>> GetRecentMessages()
    {
        var channelId = Constants.ChannelIds.WorkshopRaceAlertsId;
#if DEBUG
        channelId = Constants.ChannelIds.AntiServerRaceAlertsId;
#endif
        var channel = await discordClient.GetChannelAsync(channelId);
        var messages = channel.GetMessagesAsync(50);
        List<DiscordMessage> messageIds = new(await messages.CountAsync());
        await foreach (var item in messages) messageIds.Add(item);

        return messageIds;
    }

    [LoggerMessage(Level = LogLevel.Error, Message = "RaceAnnouncerService: {messageDetail}")]
    private partial void LogError(string messageDetail);

}
