using DSharpPlus.SlashCommands;
using DSharpPlus.SlashCommands.Attributes;
using FeInfo.Common.Emums;
using FeInfo.Common.Requests;
using FeInfo.Common.Responses;
using System.Net.Http.Json;
using tellahs_library.Extensions;

namespace tellahs_library.Commands
{
    [SlashCommandGroup("Tournament", "Commands related to tournaments")]
    public class Tournament : ApplicationCommandModule
    {
        internal static async Task<bool> GuardHttpClientAsync(HttpClient? httpClient, InteractionContext ctx)
        {
            if (httpClient == null)
            {
                await ctx.CreateResponseAsync("Unable to communicate with remote. Contact Antidale; you shouldn't see this", ephemeral: true);
                return false;
            }

            return true;
        }

        [SlashCommandGroup("Administration", "Commands related to tournament administration")]
        public class TournamentAdministration : ApplicationCommandModule
        {
            public HttpClient? HttpClient { private get; set; }

            [SlashCommand("CreateTournament", "Create A Tournament")]
            [SlashRequireUserPermissions(DSharpPlus.Permissions.Administrator)]
            [SlashRequireGuild]
            public async Task CreateTournamentAsync(InteractionContext ctx,
                [Option("tournament_name", "The name of your tournament")] string tournamentName,
                [Option("role_name", "the name of the role to assign to registrants")] string roleName = "",
                [Option("registration_start", "full registration open time format as YYYY-MM-DD hh:mm:ss -hmm")] string startDateTimeOffsetString = "",
                [Option("registration_end", "full registration close time format as YYYY-MM-DD hh:mm:ss -hmm")] string endDateTimeOffsetString = "",
                [Option("rules_link", "a link to the rules document")] string rulesLink = ""
            )
            {
                await CreateTournament(ctx, tournamentName, roleName, startDateTimeOffsetString, endDateTimeOffsetString, rulesLink);
            }

            [SlashCommand("CreateTournamentOverride", "Create A Tournament")]
            [SlashRequireOwner]
            [SlashRequireGuild]
            public async Task CreateTournamentOverrideAsync(InteractionContext ctx,
                [Option("tournament_name", "The name of your tournament")] string tournamentName,
                [Option("role_name", "the name of the role to assign to registrants")] string roleName = "",
                [Option("registration_start", "full registration open time format as YYYY-MM-DD hh:mm:ss -hmm")] string startDateTimeOffsetString = "",
                [Option("registration_end", "full registration close time format as YYYY-MM-DD hh:mm:ss -hmm")] string endDateTimeOffsetString = "",
                [Option("rules_link", "a link to the rules document")] string rulesLink = ""
            )
            {
                await CreateTournament(ctx, tournamentName, roleName, startDateTimeOffsetString, endDateTimeOffsetString, rulesLink);
            }

            private async Task CreateTournament(InteractionContext ctx, string tournamentName, string roleName, string startDateTimeOffsetString, string endDateTimeOffsetString, string rulesLink)
            {
                if (!await GuardHttpClientAsync(HttpClient, ctx)) { return; }

                await ctx.DeferAsync();

                var message = await ctx.EditResponseAsync("Creating Tournament");
                if (message == null)
                {
                    await ctx.EditResponseAsync("Something went really poorly, contact Antidale");
                    return;
                }

                var user = ctx.Member;
                var role = ctx.Guild.Roles.FirstOrDefault(x => x.Value.Name.Equals(roleName, StringComparison.InvariantCultureIgnoreCase));

                var startParsed = DateTimeOffset.TryParse(startDateTimeOffsetString, out var startRegistration);

                var endParsed = DateTimeOffset.TryParse(endDateTimeOffsetString, out var endRegistration);

                var createRequest = new CreateTournament(ctx.Guild.Id, ctx.Guild.Name, tournamentName, message.ChannelId, message.Id, role.Value?.Id ?? 0, startRegistration, endRegistration);

                var response = await HttpClient!.PostAsJsonAsync("tournament", createRequest);
                if (response.IsSuccessStatusCode)
                {
                    var tournamentDocString = string.IsNullOrWhiteSpace(rulesLink) || !Uri.IsWellFormedUriString(rulesLink, UriKind.Absolute)
                        ? null
                        : $"([Rules Document](<{rulesLink}>))";

                    await message.ModifyAsync(string.Join("\r\n",
                        string.Join(" ", $"**{tournamentName}**", tournamentDocString),
                        startParsed ? "Registration Opens: " + DSharpPlus.Formatter.Timestamp(startRegistration, DSharpPlus.TimestampFormat.LongDateTime) : null,
                        endParsed ? "Registration Closes: " + DSharpPlus.Formatter.Timestamp(endRegistration, DSharpPlus.TimestampFormat.LongDateTime) : null)); ;

                    await message.PinAsync();
                }
                else
                {
                    await message.ModifyAsync("Unable to create tournament, server error");
                }
            }

            [SlashCommand("OpenRegistration", "Opens registration for a tournament")]
            [SlashRequireUserPermissions(DSharpPlus.Permissions.Administrator)]
            [SlashRequireGuild]
            public async Task OpenRegistrationAsync(
                InteractionContext ctx,
                [Option("tournament_name", "Only needed if a server has multiple tournaments in Announced status")] string tournamentName = "")
            {
                await UpdateRegistrationWindow(ctx, RegistrationPeriodStatus.Opened, tournamentName);
            }

            [SlashCommand("CloseRegistration", "Closes registration for a tournament")]
            [SlashRequireUserPermissions(DSharpPlus.Permissions.Administrator)]
            [SlashRequireGuild]
            public async Task CloseRegistrationAsync(
                InteractionContext ctx,
                [Option("tournament_name", "Only needed if a server has multiple tournaments with open registration")] string tournamentName = ""
            )
            {
                await UpdateRegistrationWindow(ctx, RegistrationPeriodStatus.Closed, tournamentName);
            }

            private async Task UpdateRegistrationWindow(InteractionContext ctx, RegistrationPeriodStatus newStatus, string tournamentName)
            {
                if (HttpClient == null)
                {
                    await ctx.CreateResponseAsync("Unable to communicate with remote.");
                    return;
                }

                await ctx.DeferAsync(ephemeral: true);

                var changeRequest = new ChangeRegistrationPeriod(ctx.Guild.Id, tournamentName, newStatus);

                var response = await HttpClient.PatchAsJsonAsync("tournament/UpdateRegistrationWindow", changeRequest);
                if (response.IsSuccessStatusCode)
                {
                    var responseResult = await response.Content.ReadFromJsonAsync<ChangeRegistrationPeriodResponse>();
                    if (responseResult == null)
                    {
                        await ctx.EditResponseAsync($"Update failed: cannot read response");
                        return;
                    }

                    var message = await ctx.GetMessageAsync(responseResult.TrackingChannelId, responseResult.TrackingMessageId);
                    if (message == null)
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
                    }

                    var newMessage = string.Join("\r\n", [.. contents]);
                    //end extract this

                    await message.ModifyAsync(newMessage);
                    await ctx.EditResponseAsync($"Registration {newStatus}");
                }
                else
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    await ctx.EditResponseAsync($"Update failed: {errorMessage}");
                }
            }
        }

        [SlashCommandGroup("Registration", "Commands related to registering for a tournament")]
        public class TournamentRegistration : ApplicationCommandModule
        {
            public HttpClient? HttpClient { private get; set; }

            [SlashCommand("Register", "Register for a tournament")]
            [SlashRequireGuild]
            public async Task RegisterAsync(
                InteractionContext ctx,
                [Option("tournament_name", "Only needed if a server has multiple tournaments with open registration")] string tournamentName = "",
                [Option("alias", "Preferred name for this tournament. Leave blank to use your discord username")] string desiredAlias = "",
                [Option("pronouns", "Preferred pronouns for any restreams/tournament information that displays pronouns")] string pronouns = "")
            {
                if (HttpClient == null)
                {
                    await ctx.CreateResponseAsync("Unable to communicate with remote.");
                    return;
                }

                await ctx.DeferAsync(ephemeral: true);

                var user = ctx.Member;

                var registration = new ChangeRegistration(user.Id, user.Username, ctx.Guild.Id, tournamentName, desiredAlias, pronouns);

                var response = await HttpClient.PostAsJsonAsync("Tournament/Register", registration);


                if (response.IsSuccessStatusCode)
                {
                    var responseDto = await response.Content.ReadFromJsonAsync<ChangeRegistrationResponse>();
                    if (responseDto == null)
                    {
                        await ctx.EditResponseAsync("Registrataion failed: could not read response from server");
                        return;
                    }

                    var message = await ctx.GetMessageAsync(responseDto.TrackingChannelId, responseDto.TrackingMessageId);
                    if (message != null)
                    {
                        await message.ModifyAsync("updated message");
                    }

                    var role = ctx.Guild.GetRole(responseDto.TournamentRoleId);
                    if (role != null)
                    {
                        await user.GrantRoleAsync(role, $"{user.Username} registered for a tournament");
                    }

                    await ctx.EditResponseAsync("registration complete, have fun!");
                }
                else
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    await ctx.EditResponseAsync($"registration failed: {errorMessage}");
                }
            }

            [SlashCommand("Drop", "Drop from a tournament")]
            [SlashRequireGuild]
            public async Task DropAsync(InteractionContext ctx)
            {
                if (HttpClient == null)
                {
                    await ctx.CreateResponseAsync("Unable to communicate with remote. Contact Antidale since you shouldn't see this", ephemeral: true);
                    return;
                }

                var user = ctx.User;

                var registration = new ChangeRegistration(user.Id, user.Username, ctx.Guild.Id);

                var response = await HttpClient.PostAsJsonAsync("/tournament/Drop", registration);
                var responseDto = (await response.Content.ReadFromJsonAsync<ChangeRegistrationResponse>()) ?? new ChangeRegistrationResponse(0, 0, 0, 0);
                if (response.IsSuccessStatusCode)
                {
                    //respond to the user
                    await ctx.CreateResponseAsync("you're no longer registered", ephemeral: true);

                    //update "x registrants" message for this tournament
                    var message = await ctx.GetMessageAsync(responseDto.TrackingChannelId, responseDto.TrackingMessageId);
                    if (message != null)
                    {
                        await message.ModifyAsync("updated message");
                    }

                    var role = ctx.Guild.GetRole(responseDto.TournamentRoleId);
                    if (role != null)
                    {
                        await ctx.Member.RevokeRoleAsync(role, $"{ctx.Member.Username} dropped from a tournament");
                    }
                }
                else
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    await ctx.EditResponseAsync($"drop failed: {errorMessage}");
                }

                return;
            }
        }
    }
}
