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
        await ctx.DeferResponseAsync();

        if (seed is not null && seed.Length < 5)
        {
            seed = null;
        }


        var generateRequest = new GenerateRequest
        {
            flags = flags,
            seed = seed
        };

        await RollSeedAndPresentResultsAsync(ctx, generateRequest, selectedSite);
    }

    [Command("preset")]
    [Description("rolls a seed from a predefined preset")]
    public async Task RollPresetAsync(SlashCommandContext ctx,
        [Parameter("choice")]
        [Description("The preset to use for rolling a seed")]
        FePresetChoices choice,
        [Description("seed value to use for rolling. if used, use 5 or more characters")]
        [MinMaxLength(0, 10)]
        string? seed = null
        )
    {

        await ctx.DeferResponseAsync();

        if (seed is not null && seed.Length < 5)
        {
            seed = null;
        }

        var presetDetails = PresetHelper.GetPresetDetails(choice);
        var generateRequest = new GenerateRequest
        {
            flags = presetDetails.Flagset,
            seed = seed
        };

        await RollSeedAndPresentResultsAsync(ctx, generateRequest, presetDetails.Api);
    }

    async Task RollSeedAndPresentResultsAsync(CommandContext ctx, GenerateRequest generateRequest, FeHostedApi api)
    {
        var response = await SeedRollerHelper.RollSeedAsync(client, generateRequest, api);
        if (!string.IsNullOrWhiteSpace(response.Error))
        {
            await ctx.EditResponseAsync($"Error rolling seed: {response.Error}");
            await ctx.LogErrorAsync(response.Error);
        }
        else
        {
            await ctx.EditResponseAsync(response.ToEmbedList(generateRequest.flags));
            await ctx.LogUsageAsync();
        }
    }

}