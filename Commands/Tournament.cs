using FeInfo.Common.Requests;
using FeInfo.Common.Responses;
using System.ComponentModel;
using System.Net.Http.Json;
using tellahs_library.Helpers;

namespace tellahs_library.Commands
{
    [Command("Tournament")]
    [Description("Commands related to tournaments")]
    public class Tournament(HttpClient client)
    {
        private readonly HttpClient? _httpClient = client;

        [Command("Register")]
        [Description("Register for a tournament")]
        [RequireGuild]
        public async Task RegisterAsync(
                SlashCommandContext ctx,
                [Parameter("twitch_name")][Description("your username on Twitch")] string twitchName,
                [Parameter("pronouns")][Description("Preferred pronouns for any restreams/tournament information that displays pronouns")] string pronouns = "",
                [Parameter("alias")][Description("Preferred name for this tournament. Leave blank to use your discord username")] string desiredAlias = "",
                [Parameter("tournament_name")][Description("Only needed if a server has multiple tournaments with open registration")] string tournamentName = ""
            )
        {
            try
            {
                await ctx.DeferResponseAsync(ephemeral: true);

                if (!await TournamentHelper.GuardHttpClientAsync(_httpClient, ctx)) { return; }

                pronouns = pronouns.Trim();
                desiredAlias = desiredAlias.Trim();
                tournamentName = tournamentName.Trim();

                var message = await ctx.EditResponseAsync("Registration process starting");

                var member = ctx.Member;

                var registration = new ChangeRegistration(member!.Id, member.Username, ctx.Guild!.Id, twitchName, tournamentName, desiredAlias, pronouns);

                var response = await _httpClient!.PostAsJsonAsync("Tournament/Register", registration);

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

                if (responseDto.TournamentRoleId > 0)
                {
                    try
                    {
                        var role = await ctx.Guild.GetRoleAsync(responseDto.TournamentRoleId);
                        if (role is not null)
                        {
                            await member.GrantRoleAsync(role, $"{member.Username} registered for a tournament");
                        }
                    }
                    catch (Exception ex)
                    {
                        await ctx.LogErrorAsync($"Failed to grant role to {member.Username}", ex);
                    }
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
        public async Task DropAsync
        (
            SlashCommandContext ctx,
            [Parameter("tournament_name")]
            [Description("Only needed you're registered in multiple tournaments in this server")]
            string tournamentName = ""
        )
        {
            try
            {
                await ctx.DeferResponseAsync(ephemeral: true);

                if (!await TournamentHelper.GuardHttpClientAsync(_httpClient, ctx)) { return; }

                var user = ctx.User;

                var registration = new ChangeRegistration(user.Id, user.Username, ctx.Guild!.Id, TwitchName: "", TournamentName: tournamentName);

                var response = await _httpClient!.PostAsJsonAsync("Tournament/Drop", registration);

                if (response.IsSuccessStatusCode)
                {
                    var responseDto = (await response.Content.ReadFromJsonAsync<ChangeRegistrationResponse>()) ?? new ChangeRegistrationResponse(0, 0, 0, 0, "");
                    //respond to the user
                    await ctx.EditResponseAsync(
                        string.Join(" ", "You're no longer registered", string.IsNullOrEmpty(tournamentName)
                                                                        ? null
                                                                        : $"in {tournamentName}"));

                    if (responseDto.TournamentRoleId > 0)
                    {
                        try
                        {
                            var role = await ctx.Guild.GetRoleAsync(responseDto.TournamentRoleId);
                            if (role is not null)
                            {
                                await ctx.Member!.RevokeRoleAsync(role, $"{ctx.Member.Username} dropped from a tournament {tournamentName}");
                            }
                        }
                        catch (Exception ex)
                        {
                            await ctx.LogErrorAsync($"Failed removing role for {user.Username}", ex);
                        }
                    }

                    await UpdateEntrantCount(ctx, responseDto.TrackingChannelId, responseDto.TrackingMessageId, responseDto.RegistrantCount);
                }
                else
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    await ctx.EditResponseAsync($"drop failed: {errorMessage}");
                    await ctx.LogErrorAsync($"{user.Username} failed to drop from a tournament: {errorMessage}");
                }
            }
            catch (Exception ex)
            {
                await ctx.LogErrorAsync("Something MegaNuked the library, many apologies", ex.Message, ex);
            }
        }

        [Command("UpdatePronouns")]
        [Description("Update your pronouns for tournament bookkeeping")]
        public async Task UpdatePronouns
        (
            SlashCommandContext ctx,
            [Parameter("pronouns")]
            [Description("your new pronouns. Use none, n/a, or na to remove pronoun preference")]
            string newPronouns
        )
        {
            await ctx.DeferResponseAsync(ephemeral: true);

            if (!await TournamentHelper.GuardHttpClientAsync(_httpClient, ctx)) { return; }

            newPronouns = MapNoneEntries(newPronouns);

            var request = new UpdatePronouns(ctx.User.Id, newPronouns.Trim());

            var response = await _httpClient!.PatchAsJsonAsync("Entrant/updatepronouns", request);

            try
            {
                response.EnsureSuccessStatusCode();
                await ctx.EditResponseAsync("pronouns updated!");
            }
            catch (Exception ex)
            {
                var errorMessage = await response.Content.ReadAsStringAsync();
                await ctx.EditResponseAsync($"pronoun update failed; Antidale will follow up with you.");
                await ctx.LogErrorAsync(errorMessage, ex);
            }
        }

        [Command("UpdateAlias")]
        [Description("Update your alias for tournaments, or for a specific tournament")]
        public async Task UpdateAlias
        (
            SlashCommandContext ctx,

            [Parameter("alias")]
            [Description("your new alias. Use none, n/a, or na to have no alias.")]
            string newAlias,

            [Parameter("tournament_name")]
            [Description("Leave blank to update all registrations")]
            string tournamentName = ""

        )
        {
            await ctx.DeferResponseAsync(ephemeral: true);

            if (!await TournamentHelper.GuardHttpClientAsync(_httpClient, ctx)) { return; }
            newAlias = MapNoneEntries(newAlias);
            var request = new UpdateAlias(ctx.User.Id, newAlias.Trim(), tournamentName);

            var response = await _httpClient!.PatchAsJsonAsync("Entrant/updateAlias", request);

            try
            {
                response.EnsureSuccessStatusCode();
                await ctx.EditResponseAsync("Alias updated!");
            }
            catch (Exception ex)
            {
                var errorMessage = await response.Content.ReadAsStringAsync();
                await ctx.EditResponseAsync($"alias update failed; Antidale will follow up with you.");
                await ctx.LogErrorAsync(errorMessage, ex);
            }
        }

        [Command("UpdateTwitchName")]
        [Description("updates your twitch handle for tournament bookkeeping")]
        public async Task UpdateTwitchName
        (
            SlashCommandContext ctx,
            [Parameter("twitch_name")]
            [Description("your new Twitch account name.")]
            string newTwitchHandle
        )
        {
            await ctx.DeferResponseAsync(ephemeral: true);

            if (string.IsNullOrWhiteSpace(newTwitchHandle))
            {
                await ctx.EditResponseAsync("you must supply a new name");
                return;
            }

            if (!await TournamentHelper.GuardHttpClientAsync(_httpClient, ctx)) { return; }

            var request = new UpdateTwitch(ctx.User.Id, newTwitchHandle.Trim());

            var response = await _httpClient!.PatchAsJsonAsync("Entrant/updatetwitch", request);

            try
            {
                response.EnsureSuccessStatusCode();
                await ctx.EditResponseAsync("Twitch account updated!");
            }
            catch (Exception ex)
            {
                var errorMessage = await response.Content.ReadAsStringAsync();
                await ctx.EditResponseAsync($"twitch account update failed; Antidale will follow up with you.");
                await ctx.LogErrorAsync(errorMessage);
                await ctx.LogErrorAsync(ex.Message);
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

        private string MapNoneEntries(string userInput)
        {
            return userInput.ToLower() switch
            {
                "none" => "",
                "n/a" => "",
                "na" => "",
                _ => userInput
            };
        }
    }
}
