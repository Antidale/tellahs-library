
using System.ComponentModel;
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

        await ctx.EditResponseAsync($"Race created! https://racetime.gg{locationHeader.Value.First()}");
    }
}
