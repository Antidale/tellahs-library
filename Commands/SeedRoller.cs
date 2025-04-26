using System.ComponentModel;
using DSharpPlus.Commands.ArgumentModifiers;
using tellahs_library.DTOs;
using tellahs_library.Enums;
using tellahs_library.Helpers;

namespace tellahs_library.Commands;

[Command("roll")]
[Description("A set of commands for rolling seeds")]
public class SeedRoller(FeGenerationHttpClient client)
{
    [Command("flags")]
    [Description("Generate a seed from an arbitrary flag string")]
    public async Task RollFlagsAsync
    (
        SlashCommandContext ctx,
        [Parameter("site")]
        [Description("The site to generate the seed from")]
        FeHostedApi selectedSite,
        [Parameter("flags")]
        [Description("flagstring to roll")]
        string flags,
        [Parameter("seed")]
        [Description("seed value to use for rolling. if used, use 5 or more characters")]
        [MinMaxLength(0, 10)]
        string? seed = null
    )
    {
        if (seed is not null && seed.Length < 5)
        {
            seed = null;
        }

        await ctx.DeferResponseAsync();

        var generateRequest = new GenerateRequest
        {
            flags = flags,
            seed = seed
        };

        var response = await SeedRollerHelper.RollSeedAsync(client, generateRequest, selectedSite);
        if (!string.IsNullOrWhiteSpace(response.Error))
        {
            await ctx.EditResponseAsync($"Error rolling seed: {response.Error}");
            await ctx.LogErrorAsync(response.Error);
        }
        else
        {
            await ctx.EditResponseAsync(response.ToEmbedList(flags));
            await ctx.LogUsageAsync();
        }

    }

    // [Command("preset")]
    // [Description("rolls a seed from a predefined preset")]
    // public async Task RollPresetAsync(CommandContext ctx,
    //     [Parameter("preset_choice")]
    //     [Description("")]
    //     string flags)
    // {
    //     await ctx.DeferResponseAsync();

    //     await ctx.EditResponseAsync("rolling seed");
    // }

}