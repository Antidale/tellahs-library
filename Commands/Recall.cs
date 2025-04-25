using System.ComponentModel;
using System.Net.Http.Json;
using DSharpPlus.Commands.ArgumentModifiers;
using DSharpPlus.Commands.Processors.SlashCommands.Metadata;
using DSharpPlus.Commands.Trees.Metadata;
using FeInfo.Common.DTOs;
using tellahs_library.Enums;
using tellahs_library.Helpers;
using static tellahs_library.Helpers.BossInfoEmbedHelper;
using static tellahs_library.Helpers.BossNameHelper;
using static tellahs_library.Helpers.FlagInteractionHelper;
using static tellahs_library.Helpers.ItemHelper;

namespace tellahs_library.Commands
{
    [Command("recall"), InteractionInstallType(DiscordApplicationIntegrationType.GuildInstall, DiscordApplicationIntegrationType.UserInstall)]
    [AllowDMUsage]

    public class Recall(FeInfoHttpClient httpClient)
    {
        [Command("boss")]
        [Description("Get boss info")]
        [AllowDMUsage]
        public static async Task BossRecallAsync(CommandContext ctx,
            [Parameter("BossName")] [Description("the boss you want info on")]
            string bossName)
        {

            await ctx.DeferResponseAsync();

            var bossEnum = GetBossName(bossName);
            var embed = GetBossInfoEmbed(bossEnum);

            await ctx.EditResponseAsync(embed);
            await ctx.LogUsageAsync();
        }

        [Command("flag_interaction")]
        [Description("provides information about some flag interactions")]
        [AllowDMUsage]
        public static async Task FlagInteractionAsync(CommandContext ctx,
            [Parameter("interaction")] [Description("flagset interaction to learn more about")]
            FlagInteractionChoices choice
        )
        {
            await ctx.DeferResponseAsync();

            var response = GetFlagInteractionAsync(choice);

            await ctx.EditResponseAsync(response);
            await ctx.LogUsageAsync();
        }

        [Command("item")]
        [Description("provides some information about select consumable items")]
        [AllowDMUsage]
        public static async Task ItemRecallAsync(SlashCommandContext ctx,
                [Parameter("item")] [Description("get information about important consumable items")]
                ItemRecallOptions selectedItem,
                [Parameter("just_me")] [Description("Only show to me")]
                bool justMe = false
        )
        {
            await ctx.DeferResponseAsync(ephemeral: justMe);

            var embed = GetItemNotes(selectedItem);

            await ctx.EditResponseAsync(embed);
            await ctx.LogUsageAsync();
        }

        [Command("racing")]
        [Description("get information about racing Free Enterprise")]
        [AllowDMUsage]
        public static async Task RacingAsync(CommandContext ctx)
        {
            await ctx.RespondAsync(@"
Non-tournament organized racing of Free Enterprise happens mainly in various Racing Clubs, sometimes called Community Clubs. These clubs are kind of like the FE equivalent of a bowling league. Generally led by community member or two, they're generally open to all to sign up for and have a good time. Players can also jump into individual races without joining the club.

See the wiki's [Racing Clubs](<https://wiki.ff4fe.com/doku.php?id=racing_clubs>) page for links and details. Check out [Fleury's site](<https://adaptable-rabbit.surge.sh/events>) to see rankings and seeds of present and past clubs. Some listed clubs on Fleury's site won't be for FE, since DarkPaladin's racebot doesn't lock clubs to a single server.
### Racing Guides
* [A general guide](<https://docs.google.com/document/d/18ab5ejhqr_iwQ0e6m04BB0Nf2dlFaO5mw6fpbWie3Q4/>) to dr-race-bot
* [Another guide](<https://bit.ly/FF4FE-Bootcamp>) that also has some stream setup help. Normal/non-tournament races don't require stream delay, so skip that part of any instructions
* [2v2 Racing](<https://docs.google.com/document/d/102eUr6DBE93AmXrIP7gZHhKHH22RXJz2KBc_aSQDITo>)
* dr-race-bot [commands](<https://gitlab.com/akw5013/discord-race-bot/blob/master/HELP.md>)
### Upcoming Races Links
* [Upcoming Restreams](<https://docs.google.com/spreadsheets/d/1dTekGBPUl0Y_eEGtIj3Is_X5rBQpy8QcxDhoHCcRVf0>) spreadsheet - also includes information on non-tournament restreamed races
");
        }


        [Command("search")]
        [Description("search the library for information")]
        [AllowDMUsage]
        public async Task SearchAsync(CommandContext ctx,
            [Parameter("search_text")] [Description("text to search for in title, descirption, or tags returns at most 10 entries.")]
            [MinMaxLength(1, 100)]
            string searchValue
        )
        {
            await ctx.DeferResponseAsync();

            if (!await GuardHttpClientAsync(httpClient, ctx)) { return; }

            try
            {
                var response = await httpClient!.GetFromJsonAsync<List<Guide>>($"Guide?searchText={searchValue}&limit=10");

                var text = response is null || response.Count == 0
                    ? "Sorry, we're unable to find anything that matches your search. If you'd like to suggest something, leave a request in the Library's discord. Here are some general resources [Enemy List](<https://wiki.ff4fe.com/doku.php?id=enemy_list>), [Algorithm FAQ](<https://gamefaqs.gamespot.com/snes/522596-final-fantasy-ii/faqs/54945>), [Magic Guide](<https://gamefaqs.gamespot.com/snes/588330-final-fantasy-iv/faqs/53021>), [FE Wiki](<https://wiki.ff4fe.com/>)"
                    : string.Join("\r\n", response.Select(x => $"[{x.Title}](<{x.Url}>) - {x.Description}"));

                //Hopefully with adding the limit query param we don't hit needing this truncation, but this should make things safe
                if (text.Length > 2000)
                {
                    text = string.Join("\r\n", response!.Select(x => $"[{x.Title}](<{x.Url}>)"));
                }

                //If we have only one result, and it's an image, just link to it
                if (response?.Count == 1 && (response.All(x => x.LinkType == LinkType.Image)))
                {
                    text = $"[{response.First().Title}]({response.First().Url})";
                }

                await ctx.EditResponseAsync(text);
            }
            catch (Exception ex)
            {
                await ctx.LogErrorAsync("Sorry, something MegaNuked the library", ex.Message, ex);
            }

            await ctx.LogUsageAsync();
        }

        [Command("pitfalls")]
        [Description("learn some of the common pitfalls in playing Free Enterprise")]
        [AllowDMUsage]
        public static async Task PitfallsAsync(CommandContext ctx)
        {
            await ctx.RespondAsync(PitfallHelper.GetPitfallsText());
            await ctx.LogUsageAsync();
        }

        [Command("resistance")]
        [Description("provides some details about resistance/weakness")]
        public async Task ResistancesAsync(CommandContext ctx,
        [Parameter("choice")] [Description("Select an option for specific advice/information")]
        ResistanceChoices resistanceChoice = ResistanceChoices.Overview)
        {
            await ctx.RespondAsync(ResistanceHelper.GetResistanceInfo(resistanceChoice));
            await ctx.LogUsageAsync();
        }

        private static async Task<bool> GuardHttpClientAsync(HttpClient? httpClient, CommandContext ctx)
        {
            if (httpClient != null)
            {
                return true;
            }

            await ctx.RespondAsync("Unable to communicate with remote. Contact Antidale; you shouldn't see this");
            await ctx.LogErrorAsync($"HttpClient was null for an action.\r\nGuildId: {ctx.Guild}\r\nUser: {ctx.Member?.Username ?? "unknown user"}");
            return false;
        }

        [Command("suggested-flagsets")]
        [Description("Some suggested flagsets for newer players")]
        [AllowDMUsage]
        public static async Task SuggestedFlagsetsAsync(CommandContext ctx)
        {
            await ctx.RespondAsync(FlagsetHelper.GetSuggestedFlagsets());
            await ctx.LogUsageAsync();
        }

        [Command("key-item-placement")]
        [Description("Explains how FE places key items")]
        [AllowDMUsage]
        public async Task KeyItemPlacementAcync(CommandContext ctx)
        {
            await ctx.RespondAsync(KeyItemPlacementHelper.GetKeyItemPlacementDescrition());
            await ctx.LogUsageAsync();
        }

        [Command("forks")]
        [Description("information about forks of the main repository")]
        [AllowDMUsage]
        public async Task RecallForksAsync(CommandContext ctx)
        {
            await ctx.RespondAsync(ForkHelper.GetForkInformation());
            await ctx.LogUsageAsync();
        }
    }
}
