using System.ComponentModel;
using tellahs_library.Helpers;

namespace tellahs_library.Commands;

[Command("TournamentAdministration")]
[Description("Commands related to tournaments")]
[RequireGuild]
[RequirePermissions(DiscordPermissions.ManageRoles, DiscordPermissions.ManageEvents)]
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
        [Parameter("rules_link")][Description("a link to the rules document")] string rulesLink = "",
        [Parameter("standings_link")][Description("a link to standings sheet/site")] string standingsLink = ""
    )
    {
        await TournamentHelper.CreateTournament(ctx, tournamentName, roleName, startDateTimeOffsetString, endDateTimeOffsetString, rulesLink, standingsLink, HttpClient);
    }

    [Command("CloseRegistration")]
    [Description("Closes registration for a tournament")]
    [RequireGuild]
    public async Task CloseRegistrationAsync(
        SlashCommandContext ctx,
        [Parameter("tournament_name")][Description("Only needed if a server has multiple tournaments with open registration")] string tournamentName = ""
    )
    {
        await TournamentHelper.UpdateRegistrationWindow(ctx, RegistrationPeriodStatus.Closed, tournamentName, HttpClient);
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
        await TournamentHelper.UpdateRegistrationWindow(ctx, RegistrationPeriodStatus.Opened, tournamentName, HttpClient);
    }


}
