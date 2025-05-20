using DSharpPlus;
using tellahs_library.Constants;
using tellahs_library.EventHandlers;
using tellahs_library.RacingCommands;
using tellahs_library.RecallCommand;
using tellahs_library.RollCommand;
using tellahs_library.TournamentCommands;

namespace tellahs_library.Extensions;

public static class DiscordConfiguration
{
    public static DiscordClientBuilder AddCommands(this DiscordClientBuilder builder)
    {
        var commandsConfig = new CommandsConfiguration
        {
            RegisterDefaultCommandProcessors = false
        };

        builder.UseCommands((ServiceProvider, commands) =>
        {
            commands.AddProcessor(new SlashCommandProcessor());
            commands.AddCommands<FlagsetChooser>();
            commands.AddCommands<Recall>();
            commands.AddCommands<SeedRoller>();

            commands.CommandExecuted += CommandsEventHanlders.OnCommandInvokedAsync;

#if DEBUG
            commands.AddCommands<Tournament>(GuildIds.AntiServer);
            commands.AddCommands<TournamentAdministration>(GuildIds.AntiServer);
            commands.AddCommands<TournamentOverrides>(GuildIds.AntiServer);
            commands.AddCommands<CreateRacetimeRace>(GuildIds.AntiServer);
#else
            commands.AddCommands<Tournament>(GuildIds.AntiServer, GuildIds.SideTourneyServer);
            commands.AddCommands<TournamentAdministration>(GuildIds.AntiServer, GuildIds.SideTourneyServer);
            commands.AddCommands<TournamentOverrides>(GuildIds.AntiServer, GuildIds.SideTourneyServer);
            commands.AddCommands<CreateRacetimeRace>(GuildIds.FreeenWorkshop);
#endif
        },
        commandsConfig);


        return builder;
    }

}
