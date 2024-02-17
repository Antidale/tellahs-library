using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using tellahs_library.Enums;
using tellahs_library.Extensions;
using static tellahs_library.Helpers.BossInfoEmbedHelper;
using static tellahs_library.Helpers.BossNameHelper;
using static tellahs_library.Helpers.FlagInteractionHelper;
using static tellahs_library.Helpers.ItemHelper;
using System.Text.Json;
using System.Net.Http.Json;
using FeInfo.Common.DTOs;
using tellahs_library.Helpers;

namespace tellahs_library.Commands
{
    [SlashCommandGroup("recall", "command group for FE information")]
    public partial class Recall : ApplicationCommandModule
    {
        public HttpClient? HttpClient { private get; set; }

        [SlashCommand("boss", "Get boss info")]
        public async Task BossRecallAsync(InteractionContext ctx,
            [Option("BossName", "the boss you want info on")] string bossName,
            [Option("justme", "makes the response only visible to you")] bool isEphemeral = false)
        {
            await ctx.DeferAsync(isEphemeral);

            var bossEnum = GetBossName(bossName);
            var embed = GetBossInfoEmbed(bossEnum);

            await ctx.EditResponseAsync(new DiscordWebhookBuilder().AddEmbed(embed));
            await ctx.LogUsageAsync();
        }

        [SlashCommand("flag_interaction", "provides information about some flag interactions")]
        public async Task FlagInteractionAsync(InteractionContext ctx,
            [Option("interaction", "flagset interaction to learn more about")] FlagInteractionChoices choice,
            [Option("justme", "only show for yourself")] bool isEphemeral = true)
        {
            await ctx.DeferAsync(isEphemeral);

            var response = GetFlagInteractionAsync(choice);

            var builder = ctx.EditResponseAsync(response);
            await ctx.LogUsageAsync();
        }

        [SlashCommand("item", "provides some information about select consumable items")]
        public async Task ItemRecallAsync(InteractionContext ctx,
                [Option("item", "get information about important consumable items")] ItemRecallOptions selectedItem,
                [Option("justme", "only show for yourself")] bool isEphemeral = true)
        {
            await ctx.DeferAsync(isEphemeral);

            var embed = GetItemNotes(selectedItem);

            await ctx.EditResponseAsync(embed);
            await ctx.LogUsageAsync();
        }

        [SlashCommand("racing", "get information about racing Free Enterprise")]
        public async Task RacingAsync(InteractionContext ctx)
        {
            await ctx.CreateResponseAsync(@"
Non-tournament organized racing of Free Enterprise happens mainly in various Racing Clubs, sometimes called Community Clubs. These clubs are kind of like the FE equivalent of a bowling league. Generally led by community member or two, they're generally open to all to sign up for and have a good time. Players can also jump into individual races without joining the club.

See the wiki's [Racing Clubs](<https://wiki.ff4fe.com/doku.php?id=racing_clubs>) page for links and details. Check out [Fleury's site](<https://adaptable-rabbit.surge.sh/events>) to see rankings and seeds of present and past clubs. Some listed clubs on Fleury's site won't be for FE, since DarkPaladin's racebot doesn't lock clubs to a single server.
### Racing Guides
* [A general guide](<https://docs.google.com/document/d/18ab5ejhqr_iwQ0e6m04BB0Nf2dlFaO5mw6fpbWie3Q4/>) to dr-race-bot
* [Another guide](<http://bit.ly/FF4FE-Bootcamp>) that also has some stream setup help. Normal/non-tournament races don't require stream delay, so skip that part of any instructions
* [2v2 Racing](<https://docs.google.com/document/d/102eUr6DBE93AmXrIP7gZHhKHH22RXJz2KBc_aSQDITo>)
* dr-race-bot [commands](<https://gitlab.com/akw5013/discord-race-bot/blob/master/HELP.md>)
### Upcoming Races Links
* [Upcoming Restreams](<https://docs.google.com/spreadsheets/d/1dTekGBPUl0Y_eEGtIj3Is_X5rBQpy8QcxDhoHCcRVf0>) spreadsheet - also includes information on non-tournament restreamed races
* [Community Events Google Calendar](<https://calendar.google.com/calendar/u/0/embed?src=vmu93f6hqqbagatt78trjr0fgs@group.calendar.google.com&ctz=America/New_York>) (displays Eastern Time)
");
        }


        [SlashCommand("search", "search the library for information")]
        public async Task SearchAsync(InteractionContext ctx,
            [Option("search_text", "text to search for in title, descirption, or tags returns at most 10 entries.")]
            [MinimumLength(1)]
            [MaximumLength(100)]
            string searchValue,
            [Option("just_me", "send results as a standard message (false) or just yourself (true)")]
            bool justMe = false)
        {
            await ctx.DeferAsync(ephemeral: justMe);

            if (!await GuardHttpClientAsync(HttpClient, ctx)) { return; }

            try
            {
                var response = await HttpClient!.GetFromJsonAsync<List<Guide>>($"Guide?searchText={searchValue}&limit=10");

                var text = response is null || response.Count == 0
                    ? "Sorry, we're unable to find anything that matches your search. If you'd like to suggest something, leave a request in the Library's discord. Here are some general resources [Enemy List](<https://wiki.ff4fe.com/doku.php?id=enemy_list>), [Algorithm FAQ](<https://gamefaqs.gamespot.com/snes/522596-final-fantasy-ii/faqs/54945>), [Magic Guide](<https://gamefaqs.gamespot.com/snes/588330-final-fantasy-iv/faqs/53021>), [FE Wiki](<https://wiki.ff4fe.com/>)"
                    : string.Join("\r\n", response.Select(x => $"[{x.Title}](<{x.Url}>) - {x.Description}"));

                //Hopefully with adding the limit query param we don't hit needing this truncation, but this should make things safe
                if (text.Length > 2000)
                {
                    text = string.Join("\r\n", response!.Select(x => $"[{x.Title}](<{x.Url}>)"));
                }

                //If we have only one result, and it's an image, just link to it
                if (response?.Count == 1 && (response?.All(x => x.LinkType == FeInfo.Common.Enums.LinkType.Image) ?? false))
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

        [SlashCommand("pitfalls", "learn some of the common pitfalls in playing Free Enterprise")]
        public async Task PitfallsAsync(InteractionContext ctx, 
            [Option("justme", "makes the response only visible to you")] bool isEphemeral = false)
        {
            await ctx.CreateResponseAsync(PitfallHelper.GetPitfallsText(), ephemeral: isEphemeral);
            await ctx.LogUsageAsync();
        }

        internal static async Task<bool> GuardHttpClientAsync(HttpClient? httpClient, InteractionContext ctx)
        {
            if (httpClient == null)
            {
                await ctx.CreateResponseAsync("Unable to communicate with remote. Contact Antidale; you shouldn't see this", ephemeral: true);
                await ctx.LogErrorAsync($"HttpClient was null for an action.\r\nGuildId: {ctx.Guild}\r\nUser: {ctx.Member.Username}");
                return false;
            }

            return true;
        }
    }
}
