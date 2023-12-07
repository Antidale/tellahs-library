using DSharpPlus.SlashCommands;
using DSharpPlus.SlashCommands.Attributes;
using FeInfo.Common.Emums;
using FeInfo.Common.Requests;
using FeInfo.Common.Responses;
using System.Net.Http.Json;
using tellahs_library.Extensions;
using DSharpPlus;

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
                await ctx.LogErrorAsync($"HttpClient was null for an action.\r\nGuildId: {ctx.Guild}\r\nUser: {ctx.Member.Username}");
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
                [Option("registration_start", "full registration open time format as YYYY-MM-DD hh:mm:ss -hmm")] string startDateTimeOffsetString,
                [Option("registration_end", "full registration close time format as YYYY-MM-DD hh:mm:ss -hmm")] string endDateTimeOffsetString,
                [Option("role_name", "the name of the role to assign to registrants")] string roleName = "",

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
                [Option("registration_start", "full registration open time format as YYYY-MM-DD hh:mm:ss -hmm")] string startDateTimeOffsetString,
                [Option("registration_end", "full registration close time format as YYYY-MM-DD hh:mm:ss -hmm")] string endDateTimeOffsetString,
                [Option("role_name", "the name of the role to assign to registrants")] string roleName = "",
                [Option("rules_link", "a link to the rules document")] string rulesLink = ""
            )
            {
                await CreateTournament(ctx, tournamentName, roleName, startDateTimeOffsetString, endDateTimeOffsetString, rulesLink);
            }

            private async Task CreateTournament(InteractionContext ctx, string tournamentName, string roleName, string startDateTimeOffsetString, string endDateTimeOffsetString, string rulesLink)
            {
                try
                {
                    await ctx.DeferAsync();

                    tournamentName = tournamentName.Trim();
                    roleName = roleName.Trim();
                    startDateTimeOffsetString = startDateTimeOffsetString.Trim();
                    endDateTimeOffsetString = endDateTimeOffsetString.Trim();
                    rulesLink = rulesLink.Trim();

                    if (!await GuardHttpClientAsync(HttpClient, ctx)) { return; }

                    var message = await ctx.EditResponseAsync("Creating Tournament");
                    if (message == null)
                    {
                        await ctx.LogErrorAsync("Something went really poorly, contact Antidale", $"Creating tournament failed for {ctx.Guild.Name}");
                        return;
                    }

                    var user = ctx.Member;
                    var role = ctx.Guild.Roles.FirstOrDefault(x => x.Value.Name.Equals(roleName, StringComparison.InvariantCultureIgnoreCase));

                    var startParsed = DateTimeOffset.TryParse(startDateTimeOffsetString, out var startRegistration);
                    if (!startParsed)
                    {
                        await message.DeleteAsync();
                        await ctx.CreateResponseAsync("Registration start must be formatted correctly", ephemeral: true);
                        return;
                    }

                    var endParsed = DateTimeOffset.TryParse(endDateTimeOffsetString, out var endRegistration);
                    if (!startParsed)
                    {
                        await message.DeleteAsync();
                        await ctx.CreateResponseAsync("Registration end must be formatted correctly", ephemeral: true);
                        return;
                    }

                    var createRequest = new CreateTournament(ctx.Guild.Id, ctx.Guild.Name, tournamentName, message.ChannelId, message.Id, role.Value?.Id ?? 0, startRegistration, endRegistration);

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

            [SlashCommand("OpenRegistration", "Opens registration for a tournament")]
            [SlashRequireOwner]
            [SlashRequireGuild]
            public async Task OpenRegistrationOverrideAsync(
                InteractionContext ctx,
                [Option("tournament_name", "Only needed if a server has multiple tournaments in Announced status")] string tournamentName = "")
            {
                await UpdateRegistrationWindow(ctx, RegistrationPeriodStatus.Opened, tournamentName);
            }

            [SlashCommand("OpenRegistration", "Opens registration for a tournament")]
            [SlashRequireUserPermissions(Permissions.Administrator)]
            [SlashRequireGuild]
            public async Task OpenRegistrationAsync(
                InteractionContext ctx,
                [Option("tournament_name", "Only needed if a server has multiple tournaments in Announced status")] string tournamentName = "")
            {
                await UpdateRegistrationWindow(ctx, RegistrationPeriodStatus.Opened, tournamentName);
            }

            [SlashCommand("CloseRegistration", "Closes registration for a tournament")]
            [SlashRequireOwner]
            [SlashRequireGuild]
            public async Task CloseRegistrationOverrideAsync(
                InteractionContext ctx,
                [Option("tournament_name", "Only needed if a server has multiple tournaments with open registration")] string tournamentName = ""
            )
            {
                await UpdateRegistrationWindow(ctx, RegistrationPeriodStatus.Closed, tournamentName);
            }

            [SlashCommand("CloseRegistration", "Closes registration for a tournament")]
            [SlashRequireUserPermissions(Permissions.Administrator)]
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
                await ctx.DeferAsync(ephemeral: true);

                if (!await GuardHttpClientAsync(HttpClient, ctx)) { return; }

                tournamentName = tournamentName.Trim();

                var changeRequest = new ChangeRegistrationPeriod(ctx.Guild.Id, tournamentName, newStatus);

                var response = await HttpClient!.PatchAsJsonAsync("tournament/UpdateRegistrationWindow", changeRequest);
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
                    await ctx.LogErrorAsync($"Update failed: {errorMessage}", $"Update failed: {errorMessage}");
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
                [Option("pronouns", "Preferred pronouns for any restreams/tournament information that displays pronouns")] string pronouns = "",
                [Option("alias", "Preferred name for this tournament. Leave blank to use your discord username")] string desiredAlias = "",
                [Option("tournament_name", "Only needed if a server has multiple tournaments with open registration")] string tournamentName = ""
            )
            {
                try
                {
                    await ctx.DeferAsync(ephemeral: true);

                    if (!await GuardHttpClientAsync(HttpClient, ctx)) { return; }

                    pronouns = pronouns.Trim();
                    desiredAlias = desiredAlias.Trim();
                    tournamentName = tournamentName.Trim();

                    var message = await ctx.EditResponseAsync("Registration process starting");

                    var user = ctx.Member;

                    var registration = new ChangeRegistration(user.Id, user.Username, ctx.Guild.Id, tournamentName, desiredAlias, pronouns);

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

                    var trackingMessage = await ctx.GetMessageAsync(responseDto.TrackingChannelId, responseDto.TrackingMessageId);

                    if (trackingMessage != null)
                    {
                        var contents = trackingMessage.Content.Split("\r\n").ToList();
                        if (!contents.Any(x => x.StartsWith("Entrants:")))
                        {
                            contents.Add("Entrants:");
                        }

                        for (int i = 0; i < contents.Count; i++)
                        {
                            if (contents[i].StartsWith("Registation Opens:") || contents[i] == "Registration is open!")
                            {
                                contents[i] = string.Empty;
                            }

                            if (contents[i].StartsWith("Entrants:"))
                            {
                                contents[i] = $"Entrants: {responseDto.RegistrantCount}";
                            }
                        }

                        var newMessage = string.Join("\r\n", [.. contents.Where(x => !string.IsNullOrWhiteSpace(x))]);
                        await trackingMessage.ModifyAsync(newMessage);
                    }

                    var role = ctx.Guild.GetRole(responseDto.TournamentRoleId);
                    if (role != null)
                    {
                        await user.GrantRoleAsync(role, $"{user.Username} registered for a tournament");
                    }

                    await ctx.EditResponseAsync("registration complete, have fun!");

                }
                catch (Exception ex)
                {
                    await ctx.LogErrorAsync(ex.Message, ex);
                }
            }

            [SlashCommand("Drop", "Drop from a tournament")]
            [SlashRequireGuild]
            public async Task DropAsync(
                InteractionContext ctx,
                [Option("tournament_name", "Only needed you're registered in multiple tournaments in this server")] string tournamentName = ""
            )
            {
                try
                {
                    await ctx.DeferAsync();

                    if (!await GuardHttpClientAsync(HttpClient, ctx)) { return; }

                    var user = ctx.User;

                    var registration = new ChangeRegistration(user.Id, user.Username, ctx.Guild.Id, TournamentName: tournamentName);

                    var response = await HttpClient!.PostAsJsonAsync("/tournament/Drop", registration);
                    var responseDto = (await response.Content.ReadFromJsonAsync<ChangeRegistrationResponse>()) ?? new ChangeRegistrationResponse(0, 0, 0, 0, "");
                    if (response.IsSuccessStatusCode)
                    {
                        //respond to the user
                        await ctx.CreateResponseAsync(
                            string.Join(" ", "You're no longer registered", string.IsNullOrEmpty(tournamentName)
                                                                            ? null
                                                                            : $"from {tournamentName}"),
                            ephemeral: true);

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
                        await ctx.EditResponseAsync($"drop failed");
                        await ctx.LogErrorAsync(errorMessage);
                    }
                }
                catch (Exception ex)
                {
                    await ctx.LogErrorAsync("Something MegaNuked the library, many apologies", ex.Message, ex);
                }
            }
        }
    }
}
