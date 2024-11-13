using System.ComponentModel;
using tellahs_library.Helpers;

namespace tellahs_library.Commands;

[Command("TournamentAdministration")]
[Description("Commands related to tournaments")]
[RequireGuild]
[RequirePermissions
(
    botPermissions: [DiscordPermission.ManageRoles],
    userPermissions: [DiscordPermission.ManageEvents]
)]
public class TournamentAdministration(HttpClient client)
{
    private readonly HttpClient? _httpClient = client;

    [Command("CreateTournament"),
     Description("Create A Tournament"),
     RequireGuild]
    public async Task CreateTournamentAsync(SlashCommandContext ctx,
        [Parameter("tournament_name")][Description("The name of your tournament")] string tournamentName,
        [Parameter("registration_start")][Description("full registration open time format as YYYY-MM-DD hh:mm:ss -hmm")] string startDateTimeOffsetString,
        [Parameter("registration_end")][Description("full registration close time format as YYYY-MM-DD hh:mm:ss -hmm")] string endDateTimeOffsetString,
        [Parameter("role_name")][Description("the name of the role to assign to registrants")] string roleName = "",
        [Parameter("rules_link")][Description("a link to the rules document")] string rulesLink = "",
        [Parameter("standings_link")][Description("a link to standings sheet/site")] string standingsLink = ""
    )
    {
        await TournamentHelper.CreateTournament(ctx, tournamentName, roleName, startDateTimeOffsetString, endDateTimeOffsetString, rulesLink, standingsLink, _httpClient);
    }

    [Command("CloseRegistration")]
    [Description("Closes registration for a tournament")]
    [RequireGuild]
    public async Task CloseRegistrationAsync(
        SlashCommandContext ctx,
        [Parameter("tournament_name")][Description("Only needed if a server has multiple tournaments with open registration")] string tournamentName = ""
    )
    {
        await TournamentHelper.UpdateRegistrationWindow(ctx, RegistrationPeriodStatus.Closed, tournamentName, _httpClient);
    }

    [Command("OpenRegistration")]
    [Description("Opens registration for a tournament")]
    [RequirePermissions(DiscordPermission.SendMessages, DiscordPermission.Administrator)]
    [RequireGuild]
    public async Task OpenRegistrationAsync(
        SlashCommandContext ctx,
        [Parameter("tournament_name")][Description("Only needed if a server has multiple tournaments in Announced status")]
        string tournamentName = ""
    )
    {
        await TournamentHelper.UpdateRegistrationWindow(ctx, RegistrationPeriodStatus.Opened, tournamentName, _httpClient);
    }


}
