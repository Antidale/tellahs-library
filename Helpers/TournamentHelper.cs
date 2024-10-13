using System.Net.Http.Json;
using DSharpPlus;
using FeInfo.Common.Requests;
using FeInfo.Common.Responses;

namespace tellahs_library.Helpers;

public class TournamentHelper
{
    public static async Task<bool> GuardHttpClientAsync(HttpClient? httpClient, SlashCommandContext ctx)
    {
        if (httpClient == null)
        {
            await ctx.FollowupAsync("Unable to communicate with remote. Contact Antidale; you shouldn't see this", ephemeral: true);
            await ctx.LogErrorAsync($"HttpClient was null for an action.\r\nGuildId: {ctx.Guild}\r\nUser: {ctx.Member?.DisplayName ?? ctx.User.Username}");
            return false;
        }

        return true;
    }

    public static async Task CreateTournament(SlashCommandContext ctx, string tournamentName, string roleName, string startDateTimeOffsetString, string endDateTimeOffsetString, string rulesLink, string standingsLink, HttpClient? client)
    {
        try
        {
            await ctx.DeferResponseAsync();

            tournamentName = tournamentName.Trim();
            roleName = roleName.Trim();
            startDateTimeOffsetString = startDateTimeOffsetString.Trim();
            endDateTimeOffsetString = endDateTimeOffsetString.Trim();
            rulesLink = rulesLink.Trim();

            if (!await GuardHttpClientAsync(client, ctx)) { return; }

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

            var createRequest = new CreateTournament(ctx.Guild!.Id, ctx.Guild.Name, tournamentName, message.ChannelId, message.Id, role.HasValue ? role.Value.Key : 0, rulesLink, standingsLink, startRegistration, endRegistration);

            var response = await client!.PostAsJsonAsync("tournament", createRequest);
            if (response.IsSuccessStatusCode)
            {
                var rulesDocString = string.IsNullOrWhiteSpace(rulesLink) || !Uri.IsWellFormedUriString(rulesLink, UriKind.Absolute)
                    ? null
                    : $"([Rules Document](<{rulesLink}>))";

                var standingsSiteString = string.IsNullOrWhiteSpace(standingsLink) || !Uri.IsWellFormedUriString(standingsLink, UriKind.Absolute)
                    ? null
                    : $"([Standings](<{standingsLink}>))";

                await message.ModifyAsync(string.Join("\r\n",
                    $"## {tournamentName}",
                    string.Join(" ", rulesDocString, standingsSiteString),
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

    public static async Task UpdateRegistrationWindow(SlashCommandContext ctx, RegistrationPeriodStatus newStatus, string tournamentName, HttpClient? client)
    {
        await ctx.DeferResponseAsync();

        if (!await GuardHttpClientAsync(client, ctx)) { return; }

        tournamentName = tournamentName.Trim();

        var changeRequest = new ChangeRegistrationPeriod(ctx.Guild!.Id, tournamentName, newStatus);

        var response = await client!.PatchAsJsonAsync("tournament/UpdateRegistrationWindow", changeRequest);
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
