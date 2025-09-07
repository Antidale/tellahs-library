using System.ComponentModel;
using System.Reflection.Metadata;
using DSharpPlus.Commands.ArgumentModifiers;
using DSharpPlus.Commands.Trees.Metadata;
using FeInfo.Common.DTOs;
using FeInfo.Common.Requests;
using tellahs_library.DTOs;
using tellahs_library.RollCommand.Enums;
using tellahs_library.RollCommand.Helpers;

namespace tellahs_library.RollCommand;

[Command("roll")]
[Description("A set of commands for rolling seeds")]
[AllowDMUsage]
public class SeedRoller(FeGenerationHttpClient client, FeInfoHttpClient feInfoHttpClient)
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
        [Parameter("desired_preset")]
        [Description("The preset to use for rolling a seed")]
        FePresetChoices choice,
        [Description("seed value to use for rolling. if used, use 5 or more characters")]
        [MinMaxLength(5, 10)]
        string? seed = null
    )
    {
        await ctx.DeferResponseAsync();

        var (Api, Flagset) = PresetHelper.GetPresetDetails(choice);
        var generateRequest = new GenerateRequest
        {
            flags = Flagset,
            seed = seed
        };

        await RollSeedAndPresentResultsAsync(ctx, generateRequest, Api);
    }

    async Task RollSeedAndPresentResultsAsync(CommandContext ctx, GenerateRequest generateRequest, FeHostedApi api)
    {
        var response = await SeedRollerHelper.RollSeedAsync(client, generateRequest, api);
        if (!string.IsNullOrWhiteSpace(response.Error))
        {
            await ctx.EditResponseAsync($"Error rolling seed: {response.Error}");
            await ctx.LogErrorAsync(response.Error);
            return;
        }

        var messages = response.ToMessageBuilders(generateRequest.flags, generateRequest.seed);

        await ctx.EditResponseAsync(messages);

        var seedInfo = new LogSeedRoled(ctx.User.Id, new SeedInformation(response.Version, response.Seed, response.Flags, response.Verification, response.Url));

        await SeedRollerHelper.LogRolledSeedAsync(feInfoHttpClient, seedInfo);
    }
}
