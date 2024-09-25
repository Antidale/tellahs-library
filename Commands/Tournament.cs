using DSharpPlus;
using DSharpPlus.Commands;
using DSharpPlus.Commands.ContextChecks;
using DSharpPlus.Commands.Processors.SlashCommands;
using DSharpPlus.Entities;
using FeInfo.Common.Enums;
using FeInfo.Common.Requests;
using FeInfo.Common.Responses;
using System.ComponentModel;
using System.Net.Http.Json;
using tellahs_library.Extensions;

namespace tellahs_library.Commands
{
    [Command("Tournament")]
    [Description("Commands related to tournaments")]
    public class Tournament
    {
        internal static async Task<bool> GuardHttpClientAsync(HttpClient? httpClient, SlashCommandContext ctx)
        {
            if (httpClient == null)
            {
                await ctx.FollowupAsync("Unable to communicate with remote. Contact Antidale; you shouldn't see this", ephemeral: true);
                await ctx.LogErrorAsync($"HttpClient was null for an action.\r\nGuildId: {ctx.Guild}\r\nUser: {ctx.Member?.DisplayName ?? ctx.User.Username}");
                return false;
            }

            return true;
        }

        [Command("Administration")]
        [Description("Commands related to tournament administration")]
        public class TournamentAdministration
        {
            public TournamentAdministration(HttpClient client) => HttpClient = client;

            public HttpClient? HttpClient { private get; set; }

            [Command("CreateTournament"),
            Description("Create A Tournament"),
            RequireGuild]
            public async Task CreateTournamentAsync(SlashCommandContext ctx,
                [Parameter("tournament_name")][Description("The name of your tournament")] string tournamentName,
                [Parameter("registration_start")][Description("full registration open time format as YYYY-MM-DD hh:mm:ss -hmm")] string startDateTimeOffsetString,
                [Parameter("registration_end")][Description("full registration close time format as YYYY-MM-DD hh:mm:ss -hmm")] string endDateTimeOffsetString,
                [Parameter("role_name")][Description("the name of the role to assign to registrants")] string roleName = "",
                [Parameter("rules_link")][Description("a link to the rules document")] string rulesLink = ""
            )
            {
                await CreateTournament(ctx, tournamentName, roleName, startDateTimeOffsetString, endDateTimeOffsetString, rulesLink);
            }

            [Command("CreateTournamentOverride")]
            [Description("Create A Tournament")]
            [RequireApplicationOwner]
            [RequireGuild]
            public async Task CreateTournamentOverrideAsync(SlashCommandContext ctx,
                [Parameter("tournament_name")][Description("The name of your tournament")] string tournamentName,
                [Parameter("registration_start")][Description("full registration open time format as YYYY-MM-DD hh:mm:ss -hmm")] string startDateTimeOffsetString,
                [Parameter("registration_end")][Description("full registration close time format as YYYY-MM-DD hh:mm:ss -hmm")] string endDateTimeOffsetString,
                [Parameter("role_name")][Description("the name of the role to assign to registrants")] string roleName = "",
                [Parameter("rules_link")][Description("a link to the rules document")] string rulesLink = ""
            )
            {
                await CreateTournament(ctx, tournamentName, roleName, startDateTimeOffsetString, endDateTimeOffsetString, rulesLink);
            }

            private async Task CreateTournament(SlashCommandContext ctx, string tournamentName, string roleName, string startDateTimeOffsetString, string endDateTimeOffsetString, string rulesLink)
            {
                try
                {
                    await ctx.DeferResponseAsync();

                    tournamentName = tournamentName.Trim();
                    roleName = roleName.Trim();
                    startDateTimeOffsetString = startDateTimeOffsetString.Trim();
                    endDateTimeOffsetString = endDateTimeOffsetString.Trim();
                    rulesLink = rulesLink.Trim();

                    if (!await GuardHttpClientAsync(HttpClient, ctx)) { return; }

                    var message = await ctx.EditResponseAsync("Creating Tournament");
                    if (message is null)
                    {
                        await ctx.LogErrorAsync("Something went really poorly, contact Antidale", $"Creating tournament failed for {ctx.Guild!.Name}");
                        return;
                    }

                    var user = ctx.Member;
                    var role = ctx.Guild?.Roles.FirstOrDefault(x => x.Value.Name.Equals(roleName, StringComparison.InvariantCultureIgnoreCase));

                    var startParsed = DateTimeOffset.TryParse(startDateTimeOffsetString, out var startRegistration);
                    if (!startParsed)
                    {
                        var builder = new DiscordMessageBuilder().WithContent("Registration start must be formatted correctly");
                        await message.DeleteAsync();
                        await ctx.FollowupAsync("Registration start must be formatted correctly", ephemeral: true);
                        return;
                    }

                    var endParsed = DateTimeOffset.TryParse(endDateTimeOffsetString, out var endRegistration);
                    if (!startParsed)
                    {
                        await message.DeleteAsync();
                        await ctx.FollowupAsync("Registration end must be formatted correctly", ephemeral: true);
                        return;
                    }

                    var createRequest = new CreateTournament(ctx.Guild!.Id, ctx.Guild.Name, tournamentName, message.ChannelId, message.Id, role.HasValue ? role.Value.Key : 0, startRegistration, endRegistration);

                    var response = await HttpClient!.PostAsJsonAsync("tournament", createRequest);
                    if (response.IsSuccessStatusCode)
                    {
                        var tournamentDocString = string.IsNullOrWhiteSpace(rulesLink) || !Uri.IsWellFormedUriString(rulesLink, UriKind.Absolute)
                            ? null
                            : $"([Rules Document](<{rulesLink}>))";

                        await message.ModifyAsync(string.Join("\r\n",
                            string.Join(" ", $"**{tournamentName}**", tournamentDocString),
                            "Registration Opens: " + Formatter.Timestamp(startRegistration, TimestampFormat.LongDateTime),
                            "Registration Closes: " + Formatter.Timestamp(endRegistration, TimestampFormat.LongDateTime)
                        ));

                        await message.PinAsync();
                    }
                    else
                    {
                        var errorMessage = await response.Content.ReadAsStringAsync();
                        await message.ModifyAsync($"Tournament Creation Failed: {errorMessage}");
                    }
                }
                catch (Exception ex)
                {
                    await ctx.LogErrorAsync(ex.Message);
                }
            }

            [Command("OpenRegistrationOverride")]
            [Description("Opens registration for a tournament")]
            [RequireApplicationOwner]
            [RequireGuild]
            public async Task OpenRegistrationOverrideAsync(
                SlashCommandContext ctx,
                [Parameter("tournament_name")][Description("Only needed if a server has multiple tournaments in Announced status")]
                string tournamentName = ""
            )
            {
                await UpdateRegistrationWindow(ctx, RegistrationPeriodStatus.Opened, tournamentName);
            }

            [Command("OpenRegistration")]
            [Description("Opens registration for a tournament")]
            [RequirePermissions(DiscordPermissions.SendMessages, DiscordPermissions.Administrator)]
            [RequireGuild]
            public async Task OpenRegistrationAsync(
                SlashCommandContext ctx,
                [Parameter("tournament_name")][Description("Only needed if a server has multiple tournaments in Announced status")]
                string tournamentName = ""
            )
            {
                await UpdateRegistrationWindow(ctx, RegistrationPeriodStatus.Opened, tournamentName);
            }

            [Command("CloseRegistrationOverride")]
            [Description("Closes registration for a tournament")]
            [RequireApplicationOwner]
            [RequireGuild]
            public async Task CloseRegistrationOverrideAsync(
                SlashCommandContext ctx,
                [Parameter("tournament_name")][Description("Only needed if a server has multiple tournaments with open registration")]
                string tournamentName = ""
            )
            {
                await UpdateRegistrationWindow(ctx, RegistrationPeriodStatus.Closed, tournamentName);
            }

            [Command("CloseRegistration")]
            [Description("Closes registration for a tournament")]
            [RequirePermissions(DiscordPermissions.SendMessages, DiscordPermissions.Administrator)]
            [RequireGuild]
            public async Task CloseRegistrationAsync(
                SlashCommandContext ctx,
                [Parameter("tournament_name")][Description("Only needed if a server has multiple tournaments with open registration")] string tournamentName = ""
            )
            {
                await UpdateRegistrationWindow(ctx, RegistrationPeriodStatus.Closed, tournamentName);
            }

            private async Task UpdateRegistrationWindow(SlashCommandContext ctx, RegistrationPeriodStatus newStatus, string tournamentName)
            {
                await ctx.DeferResponseAsync();

                if (!await GuardHttpClientAsync(HttpClient, ctx)) { return; }

                tournamentName = tournamentName.Trim();

                var changeRequest = new ChangeRegistrationPeriod(ctx.Guild!.Id, tournamentName, newStatus);

                var response = await HttpClient!.PatchAsJsonAsync("tournament/UpdateRegistrationWindow", changeRequest);
                if (response.IsSuccessStatusCode)
                {
                    var responseResult = await response.Content.ReadFromJsonAsync<ChangeRegistrationPeriodResponse>();
                    if (responseResult is null)
                    {
                        await ctx.EditResponseAsync($"Update failed: cannot read response");
                        return;
                    }

                    var message = await ctx.GetMessageAsync(responseResult.TrackingChannelId, responseResult.TrackingMessageId);
                    if (message is null)
                    {
                        await ctx.EditResponseAsync($"Registration {newStatus}");
                        return;
                    }

                    //TODO: Extract this to handle more cases, and actually handle correctly in this combined method
                    var contents = message.Content.Split("\r\n");
                    for (int i = 0; i < contents.Length; i++)
                    {
                        if (contents[i].StartsWith("Registration Opens:"))
                        {
                            contents[i] = $"Registration is open!";
                        }

                        if (newStatus == RegistrationPeriodStatus.Closed && contents[i].StartsWith("Rgistration Closes:"))
                        {
                            contents[i] = "Registration Closed";
                        }
                    }

                    var newMessage = string.Join("\r\n", [.. contents]);
                    //end extract this

                    await message.ModifyAsync(newMessage);
                    await ctx.EditResponseAsync($"Registration {newStatus}");
                }
                else
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    await ctx.LogErrorAsync($"Update failed: {errorMessage}", $"Update failed: {errorMessage}");
                }
            }
        }

        [Command("Registration")]
        [Description("Commands related to registering for a tournament")]
        public class TournamentRegistration
        {
            public TournamentRegistration(HttpClient client) => HttpClient = client;
            public HttpClient? HttpClient { private get; set; }

            [Command("Register")]
            [Description("Register for a tournament")]
            [RequireGuild]
            public async Task RegisterAsync(
                SlashCommandContext ctx,
                [Parameter("pronouns")][Description("Preferred pronouns for any restreams/tournament information that displays pronouns")] string pronouns = "",
                [Parameter("alias")][Description("Preferred name for this tournament. Leave blank to use your discord username")] string desiredAlias = "",
                [Parameter("tournament_name")][Description("Only needed if a server has multiple tournaments with open registration")] string tournamentName = ""
            )
            {
                try
                {
                    await ctx.DeferResponseAsync(ephemeral: true);

                    if (!await GuardHttpClientAsync(HttpClient, ctx)) { return; }

                    pronouns = pronouns.Trim();
                    desiredAlias = desiredAlias.Trim();
                    tournamentName = tournamentName.Trim();

                    var message = await ctx.EditResponseAsync("Registration process starting");

                    var member = ctx.Member;

                    var registration = new ChangeRegistration(member!.Id, member.Username, ctx.Guild!.Id, tournamentName, desiredAlias, pronouns);

                    var response = await HttpClient!.PostAsJsonAsync("Tournament/Register", registration);

                    if (!response.IsSuccessStatusCode)
                    {
                        var errorMessage = await response.Content.ReadAsStringAsync();
                        await ctx.LogErrorAsync($"registration failed: {errorMessage}", errorMessage);
                        return;
                    }

                    var responseDto = await response.Content.ReadFromJsonAsync<ChangeRegistrationResponse>();
                    if (responseDto == null)
                    {
                        await ctx.LogErrorAsync("Registrataion failed", "Could not read response from server");
                        return;
                    }

                    await UpdateEntrantCount(ctx, responseDto.TrackingChannelId, responseDto.TrackingMessageId, responseDto.RegistrantCount);
                    await ctx.EditResponseAsync("registration complete, have fun!");

                    var role = ctx.Guild.GetRole(responseDto.TournamentRoleId);
                    if (role is not null)
                    {
                        await member.GrantRoleAsync(role, $"{member.Username} registered for a tournament");
                    }
                }
                catch (Exception ex)
                {
                    await ctx.LogErrorAsync(ex.Message, ex);
                }
            }

            [Command("Drop")]
            [Description("Drop from a tournament")]
            [RequireGuild]
            public async Task DropAsync(
                SlashCommandContext ctx,
                [Parameter("tournament_name")][Description("Only needed you're registered in multiple tournaments in this server")] string tournamentName = ""
            )
            {
                try
                {
                    await ctx.DeferResponseAsync(ephemeral: true);

                    if (!await GuardHttpClientAsync(HttpClient, ctx)) { return; }

                    var user = ctx.User;

                    var registration = new ChangeRegistration(user.Id, user.Username, ctx.Guild!.Id, TournamentName: tournamentName);

                    var response = await HttpClient!.PostAsJsonAsync("Tournament/Drop", registration);

                    if (response.IsSuccessStatusCode)
                    {
                        var responseDto = (await response.Content.ReadFromJsonAsync<ChangeRegistrationResponse>()) ?? new ChangeRegistrationResponse(0, 0, 0, 0, "");
                        //respond to the user
                        await ctx.EditResponseAsync(
                            string.Join(" ", "You're no longer registered", string.IsNullOrEmpty(tournamentName)
                                                                            ? null
                                                                            : $"in {tournamentName}"));

                        var role = ctx.Guild.GetRole(responseDto.TournamentRoleId);
                        if (role is not null)
                        {
                            await ctx.Member!.RevokeRoleAsync(role, $"{ctx.Member.Username} dropped from a tournament {tournamentName}");
                        }

                        await UpdateEntrantCount(ctx, responseDto.TrackingChannelId, responseDto.TrackingMessageId, responseDto.RegistrantCount);
                    }
                    else
                    {
                        var errorMessage = await response.Content.ReadAsStringAsync();
                        await ctx.EditResponseAsync($"drop failed");
                        await ctx.LogErrorAsync(errorMessage);
                    }
                }
                catch (Exception ex)
                {
                    await ctx.LogErrorAsync("Something MegaNuked the library, many apologies", ex.Message, ex);
                }
            }

            private async Task UpdateEntrantCount(CommandContext ctx, ulong channelId, ulong messageId, int registrantCount)
            {
                var trackingMessage = await ctx.GetMessageAsync(channelId, messageId);
                if (trackingMessage is null) { return; }

                var contents = trackingMessage.Content.Split("\r\n").ToList();
                if (!contents.Any(x => x.StartsWith("Entrants:")))
                {
                    contents.Add("Entrants:");
                }

                for (int i = 0; i < contents.Count; i++)
                {
                    if (contents[i].StartsWith("Entrants:"))
                    {
                        contents[i] = $"Entrants: {registrantCount}";
                    }
                }

                var newMessage = string.Join("\r\n", [.. contents.Where(x => !string.IsNullOrWhiteSpace(x))]);
                try
                {
                    await trackingMessage.ModifyAsync(newMessage);
                }
                catch (Exception ex)
                {
                    await ctx.LogErrorAsync("Something MegaNuked the library while updated entrant count, many apologies", ex.Message, ex);
                }

            }
        }
    }
}
