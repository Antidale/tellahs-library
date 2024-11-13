using System.ComponentModel;
using tellahs_library.Helpers;

namespace tellahs_library.Commands;

[Command("TournamentOverrides")]
[RequireApplicationOwner]
[Description("Commands related to tournaments")]
[RequirePermissions
(
    botPermissions: [DiscordPermission.ManageRoles],
    userPermissions: [DiscordPermission.ManageMessages]
)]
public class TournamentOverrides(HttpClient client)
{
    private readonly HttpClient? _httpClient = client;

    [Command("CreateTournamentOverride")]
    [Description("Create A Tournament")]
    [RequireGuild]
    [RequireApplicationOwner]
    public async Task CreateTournamentOverrideAsync(SlashCommandContext ctx,
        [Parameter("tournament_name")][Description("The name of your tournament")] string tournamentName,
        [Parameter("registration_start")][Description("full registration open time format as YYYY-MM-DD hh:mm:ss -hmm")] string startDateTimeOffsetString,
        [Parameter("registration_end")][Description("full registration close time format as YYYY-MM-DD hh:mm:ss -hmm")] string endDateTimeOffsetString,
        [Parameter("role_name")][Description("the name of the role to assign to registrants")] string roleName = "",
        [Parameter("rules_link")][Description("a link to the rules document")] string rulesLink = "",
        [Parameter("standings_link")][Description("a link to the standings document")] string standingsLink = ""
    )
    {
        await TournamentHelper.CreateTournament(ctx, tournamentName, roleName, startDateTimeOffsetString, endDateTimeOffsetString, rulesLink, standingsLink, _httpClient);
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
        await TournamentHelper.UpdateRegistrationWindow(ctx, RegistrationPeriodStatus.Opened, tournamentName, _httpClient);
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
        await TournamentHelper.UpdateRegistrationWindow(ctx, RegistrationPeriodStatus.Closed, tournamentName, _httpClient);
    }
}
