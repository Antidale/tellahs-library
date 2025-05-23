
using System.ComponentModel;
using tellahs_library.Constants;
using tellahs_library.RacingCommands.Enums;

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
        AfcFlagset flagset
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
            _ => "Beat Zeromus"
        };

        var response = await client.CreateRaceAsync(new()
        {
            Goal = goal,
            InfoUser = description
        });

        if (response is null || !response.IsSuccessStatusCode)
        {
            await ctx.EditResponseAsync("Race creation failed. Try going directly to racetime: https://racetime.gg/ff4fe/startrace");
        }

        var locationHeader = response!.Headers.FirstOrDefault(x => x.Key == "Location");
        var raceUrl = string.Join(string.Empty, urlBase, locationHeader.Value.First());

        await ctx.EditResponseAsync($"Race created! {raceUrl}");

        //Don't have the workshop's ping-to-race role ID to just hard code so we have to pull it from the context
        var pingRole = ctx.Guild?.Roles.FirstOrDefault(x => x.Value.Name.Equals("ping-to-race", StringComparison.InvariantCultureIgnoreCase)).Value;

        if (pingRole is null) { return; }

        if (!ctx.Guild!.Channels.TryGetValue(alertsChannelId, out var alertsChannel))
        {
            return;
        }

        var messageBuilder = new DiscordMessageBuilder()
            .EnableV2Components()
            .AddTextDisplayComponent(new DiscordTextDisplayComponent($"{pingRole.Mention} {description}"))
            .AddSeparatorComponent(new DiscordSeparatorComponent(divider: true, spacing: DiscordSeparatorSpacing.Small))
            .AddTextDisplayComponent(new DiscordTextDisplayComponent(raceUrl))
            .WithAllowedMention(new RoleMention(pingRole));

        await alertsChannel.SendMessageAsync(messageBuilder);
    }
}
