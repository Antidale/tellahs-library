
using System.ComponentModel;
using DSharpPlus.Commands.Processors.SlashCommands.ArgumentModifiers;
using tellahs_library.Constants;
using tellahs_library.RacingCommands.Enums;
using tellahs_library.RacingCommands.Helpers;

namespace tellahs_library.RacingCommands;

public class CreateRacetimeRace(RacetimeHttpClient client)
{
    [Command("CreateRace")]
    [Description("Creates a race at racetime.gg")]
    [RequireGuild]
    public async Task CreateRaceAsync(
        SlashCommandContext ctx,
        [Parameter("description")]
        [Description("a brief description of the race")]
        string description,
        [Parameter("flagset")]
        [Description("flagset")]
        AfcFlagset flagset,
        [Parameter("ping-alert-role")]
        [Description("include a ping to ping-to-race")]
        bool includePing = true
    )
    {
        await ctx.DeferResponseAsync(ephemeral: true);

        var alertsChannelId = ChannelIds.WorkshopRaceAlertsId;
        var restrictedGuildId = GuildIds.FreeenWorkshop;
        var urlBase = "https://racetime.gg";
#if DEBUG
        alertsChannelId = ChannelIds.AntiServerRaceAlertsId;
        restrictedGuildId = GuildIds.AntiServer;
        urlBase = "http://localhost:8000";
#endif

        if (ctx.Guild?.Id != restrictedGuildId)
        {
            await ctx.EditResponseAsync("Invalid Guild");
            return;
        }

        var goal = flagset switch
        {
            AfcFlagset.Fbf => "Complete Objectives",
            AfcFlagset.Alpha => "5.0 alpha/beta",
            _ => "Beat Zeromus"
        };

        var response = await client.CreateRaceAsync(new()
        {
            Goal = goal,
            InfoUser = description,
            AllowNonEntrantChat = flagset == AfcFlagset.Alpha,
            StreamingRequired = !(flagset == AfcFlagset.Alpha)
        });

        if (response is null || !response.IsSuccessStatusCode)
        {
            await ctx.EditResponseAsync("Race creation failed. Try going directly to racetime: https://racetime.gg/ff4fe/startrace");
            return;
        }

        var raceUrl = GetRaceLocation(response, urlBase);
        await ctx.EditResponseAsync($"Race Created: {raceUrl}");

        if (!ctx.Guild!.Channels.TryGetValue(alertsChannelId, out var alertsChannel))
        {
            return;
        }

        var alertMessage = AlertMessageHelper.CreateAlertMessage(ctx, description, raceUrl, includePing);

        await alertsChannel.SendMessageAsync(alertMessage);
    }

    private static string GetRaceLocation(HttpResponseMessage response, string urlBase)
    {
        var locationHeader = response.Headers.FirstOrDefault(x => x.Key == "Location");
        return string.Join(string.Empty, urlBase, locationHeader.Value.First());
    }
}
