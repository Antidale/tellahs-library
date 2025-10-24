using System.ComponentModel;
using System.IO.Pipelines;
using System.Net.Http.Json;
using System.Text;
using DSharpPlus.Commands.ArgumentModifiers;
using DSharpPlus.Commands.Processors.SlashCommands.Metadata;
using DSharpPlus.Commands.Trees.Metadata;
using DSharpPlus.EventArgs;
using DSharpPlus.Interactivity;
using FeInfo.Common.DTOs;
using tellahs_library.RecallCommand.Enums;
using tellahs_library.RecallCommand.Helpers;
using static tellahs_library.RecallCommand.Helpers.BossInfoEmbedHelper;
using static tellahs_library.RecallCommand.Helpers.BossNameHelper;
using static tellahs_library.RecallCommand.Helpers.FlagInteractionHelper;
using static tellahs_library.RecallCommand.Helpers.ItemHelper;

namespace tellahs_library.RecallCommand
{
    [Command("recall"), InteractionInstallType(DiscordApplicationIntegrationType.GuildInstall, DiscordApplicationIntegrationType.UserInstall)]
    [AllowDMUsage]

    public class Recall(FeInfoHttpClient httpClient, UrlSettings urlSettings, InteractivityExtension interactivity, IHttpClientFactory httpClientFactory)
    {
        [Command("boss")]
        [Description("Get boss info")]
        [AllowDMUsage]
        public async Task BossRecallAsync(CommandContext ctx,
            [Parameter("BossName")] [Description("the boss you want info on")]
            string bossName)
        {
            await ctx.DeferResponseAsync();

            if (!await GuardHttpClientAsync(httpClient, ctx)) { return; }

            var boss = GetBossName(bossName);
            try
            {
                var bossStrategy = await httpClient!.GetFromJsonAsync<BossStrategy>($"Guide/boss-strategies/{(int)boss}");
                if (bossStrategy is null)
                {
                    await ctx.EditResponseAsync("Sorry, something MegaNuked the library");
                    return;
                }
                var embed = GetBossInfoEmbed(bossStrategy, urlSettings.ThumbnailHost);

                await ctx.EditResponseAsync(embed);
            }
            catch (Exception ex)
            {
                await ctx.LogErrorAsync("Sorry, something MegaNuked the library", ex.Message, ex);
            }

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

            var messageBuilder = GetItemNotes(selectedItem);

            await ctx.EditResponseAsync(messageBuilder);
        }

        [Command("racing")]
        [Description("get information about racing Free Enterprise")]
        [AllowDMUsage]
        public static async Task RacingAsync(CommandContext ctx)
        {
            await ctx.RespondAsync(@"
Non-tournament organized racing of Free Enterprise happens mainly in various, Community Races, Racing Clubs, sometimes called Community Clubs, or pickup races. The Community races happen every Friday, with the race room opening at around 8pm Eastern, and the race starting at 9pm Eastern. The races are hosted on [racetime.gg](<https://racetime.gg/>) (see their [help and support page](<https://racetime.gg/about/help>) for some basic instructions for creating accounts and using the site).

The Racing Clubs clubs are kind of like the FE equivalent of a bowling league. Generally led by community member or two, they're generally open to all to sign up for and have a good time. Players can also jump into individual races without joining the club. See the wiki's [Racing Clubs](<https://wiki.ff4fe.com/doku.php?id=racing_clubs>) page for links and details.
### Racing Guides
* [A general guide](<https://docs.google.com/document/d/18ab5ejhqr_iwQ0e6m04BB0Nf2dlFaO5mw6fpbWie3Q4/>) to prof-race-bot
* [Another guide](<https://bit.ly/FF4FE-Bootcamp>) that also has stream setup help. Normal/non-tournament races don't require stream delay, so skip that part of any instructions since that has been part of past tournament requirements.
* [2v2 Racing](<https://docs.google.com/document/d/102eUr6DBE93AmXrIP7gZHhKHH22RXJz2KBc_aSQDITo>)
* prof-race-bot [commands](<https://gitlab.com/wylem/discord-race-bot/blob/master/HELP.md>)
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

                //TODO: figure out the best way to handle this in a switch expression, or similar.
                var text = response is null || response.Count == 0
                    ? "Sorry, we're unable to find anything that matches your search. If you'd like to suggest something, leave a request in the Library's discord. Here are some general resources [Enemy List](<https://wiki.ff4fe.com/doku.php?id=enemy_list>), [Algorithm FAQ](<https://gamefaqs.gamespot.com/snes/522596-final-fantasy-ii/faqs/54945>), [Magic Guide](<https://gamefaqs.gamespot.com/snes/588330-final-fantasy-iv/faqs/53021>), [FE Wiki](<https://wiki.ff4fe.com/>)"
                    : string.Join("\r\n", response.Select(x => $"[{x.Title}](<{x.Url}>) - {x.Description}"));

                //Hopefully with adding the limit query param we don't hit needing this truncation, but this should make things safe
                if (text.Length > 2000)
                {
                    text = string.Join("\r\n", response!.Select(x => $"[{x.Title}](<{x.Url}>)"));
                }

                //If we have only one result, and it's an image, just link to it
                if (response?.Count == 1 && response.All(x => x.LinkType == LinkType.Image || x.LinkType == LinkType.Video))
                {
                    text = $"[{response.First().Title}]({response.First().Url}) - {response.First().Description}";
                }

                await ctx.EditResponseAsync(text);
            }
            catch (Exception ex)
            {
                await ctx.LogErrorAsync("Sorry, something MegaNuked the library", ex.Message, ex);
            }

        }

        [Command("pitfalls")]
        [Description("learn some of the common pitfalls in playing Free Enterprise")]
        [AllowDMUsage]
        public static async Task PitfallsAsync(CommandContext ctx)
        {
            await ctx.RespondAsync(PitfallHelper.GetPitfallsText());
        }

        [Command("resistance")]
        [Description("provides some details about resistance/weakness")]
        public async Task ResistancesAsync(CommandContext ctx,
        [Parameter("choice")] [Description("Select an option for specific advice/information")]
        ResistanceChoices resistanceChoice = ResistanceChoices.Overview)
        {
            await ctx.RespondAsync(ResistanceHelper.GetResistanceInfo(resistanceChoice));
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
        }

        [Command("key-item-placement")]
        [Description("Explains how FE places key items")]
        [AllowDMUsage]
        public async Task KeyItemPlacementAcync(CommandContext ctx)
        {
            await ctx.RespondAsync(KeyItemPlacementHelper.GetKeyItemPlacementDescrition());
        }

        [Command("forks")]
        [Description("information about forks of the main repository")]
        [AllowDMUsage]
        public async Task RecallForksAsync(CommandContext ctx)
        {
            await ctx.RespondAsync(ForkHelper.GetForkInformation());
        }

        [Command("learningway")]
        [Description("information and links about the Learningway project")]
        [AllowDMUsage]
        public async Task RecallLearningwayAsync(CommandContext ctx)
        {
            await ctx.RespondAsync(LearningwayHelper.GetInfo());
        }

        [Command("afc-breakdown")]
        [Description("information about the AFC tournament flagsets")]
        [AllowDMUsage]
        public async Task RecallAfcFlagsetsAsync(
            SlashCommandContext ctx,
            [Parameter("info"), Description("what info you want")]
            AfcInfoType infoType,
            [Parameter("detail_level"), Description("The amount of detail you want")]
            AfcDetailOptions detailLevel
        )
        {
            await ctx.RespondAsync(AfcHelper.GetAfcMessages(infoType, detailLevel));
        }

        [Command("metadata")]
        [Description("get a seed's metadata")]
        [AllowDMUsage]
        public async Task RecallMetadata(SlashCommandContext ctx)
        {
            var componentId = Guid.NewGuid().ToString();
            var modalId = Guid.NewGuid().ToString();
            var modalSubmission = await WaitForUploadModalAsync(modalId, componentId, ctx);

            if (modalSubmission.TimedOut)
            {
                await modalSubmission.Result.Interaction.DeferAsync(ephemeral: true);
                await modalSubmission.Result.Interaction.CreateFollowupMessageAsync(new DiscordFollowupMessageBuilder().WithContent("must supply a SNES rom"));
                return;
            }

            modalSubmission.Result.Values.TryGetValue(componentId, out var submittedFile);
            await modalSubmission.Result.Interaction.DeferAsync(ephemeral: true);

            var fileStream = await GetFileStreamAsync(submittedFile);
            if (fileStream.stream is null)
            {
                await modalSubmission.Result.Interaction.CreateFollowupMessageAsync(new DiscordFollowupMessageBuilder().WithContent("must supply a valid patched FE ROM."));
                return;
            }

            if (MetadataHelper.TryGetSeedMetadata(fileStream.stream, out var metadata))
            {
                await modalSubmission.Result.Interaction.CreateFollowupMessageAsync(new DiscordFollowupMessageBuilder(metadata.ToMessageBuilder()));
            }
            else
            {
                await modalSubmission.Result.Interaction.CreateFollowupMessageAsync(new DiscordFollowupMessageBuilder().WithContent("could not parse metadata. Must use an FE ROM from version 0.3.0 to 5.0").AsEphemeral());
                return;
            }

        }

        [Command("patch")]
        [Description("generate a patch page from an FE seed")]
        [AllowDMUsage]
        public async Task RecallPatchPage(SlashCommandContext ctx)
        {
            var componentId = Guid.NewGuid().ToString();
            var modalId = Guid.NewGuid().ToString();
            var modalSubmission = await WaitForUploadModalAsync(modalId, componentId, ctx);

            if (modalSubmission.TimedOut)
            {
                await modalSubmission.Result.Interaction.DeferAsync(ephemeral: true);
                await modalSubmission.Result.Interaction.CreateFollowupMessageAsync(new DiscordFollowupMessageBuilder().WithContent("must supply a SNES rom"));
                return;
            }

            modalSubmission.Result.Values.TryGetValue(componentId, out var submittedFile);
            await modalSubmission.Result.Interaction.DeferAsync();

            var fileStream = await GetFileStreamAsync(submittedFile);
            if (fileStream.stream is null)
            {
                await modalSubmission.Result.Interaction.CreateFollowupMessageAsync(new DiscordFollowupMessageBuilder().WithContent("must supply a valid patched FE ROM."));
                return;
            }

            using var file = File.Create(fileStream.fileName);
            await fileStream.stream.CopyToAsync(file);
            file.Close();

            if (MetadataHelper.TryGetSeedMetadata(fileStream.fileName, out var metadata))
            {
                var patchFile = await FlipsHelper.CreateBpsPatchAsync(fileStream.fileName);
                var patchData = await File.ReadAllBytesAsync(patchFile);
                var patchString = Convert.ToBase64String(patchData);
                var patchPage = HtmlTemplate.BaseTemplate(metadata, patchString);

                using var memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(patchPage));

                await modalSubmission.Result.Interaction.CreateFollowupMessageAsync(new DiscordFollowupMessageBuilder(metadata.ToMessageBuilder()));
                await modalSubmission.Result.Interaction.CreateFollowupMessageAsync(new DiscordFollowupMessageBuilder().WithContent("patch page").AddFile($"{fileStream.fileName}.html", memoryStream, AddFileOptions.CloseStream));
                File.Delete(fileStream.fileName);
                File.Delete(patchFile);
            }
            else
            {
                await modalSubmission.Result.Interaction.CreateFollowupMessageAsync(new DiscordFollowupMessageBuilder().WithContent("could not parse metadata. Must use an FE ROM from version 0.3.0 to 5.0"));
                return;
            }
        }

        private async Task<InteractivityResult<ModalSubmittedEventArgs>> WaitForUploadModalAsync(string modalId, string uploadComponentId, SlashCommandContext ctx)
        {
            var modal = new DiscordModalBuilder()
                .WithCustomId(modalId)
                .WithTitle("File Upload")
                .AddFileUpload(new DiscordFileUploadComponent(uploadComponentId, isRequired: true), "Upload your seed here.");
            await ctx.RespondWithModalAsync(modal);

            return await interactivity.WaitForModalAsync(modalId, TimeSpan.FromMinutes(2));
        }

        private async Task<(Stream? stream, string fileName)> GetFileStreamAsync(IModalSubmission? modalSubmission)
        {
            if (modalSubmission is null) return (null, string.Empty);
            if (modalSubmission is not FileUploadModalSubmission fileSubmission || !fileSubmission.UploadedFiles.Any())
                return (null, string.Empty);

            var attachment = fileSubmission?.UploadedFiles[0];
            if (attachment is null) { return (null, string.Empty); }
            if (!attachment.FileName?.IsSnesRom() ?? false) { return (null, string.Empty); }
            var url = attachment.Url;
            var client = httpClientFactory.CreateClient();
            var getResponse = await client.GetAsync(url);
            try
            {
                getResponse.EnsureSuccessStatusCode();
                var stream = await getResponse.Content.ReadAsStreamAsync();
                return (stream, attachment.FileName!);
            }
            catch (Exception)
            {
                return (null, string.Empty);
            }
        }
    }
}
