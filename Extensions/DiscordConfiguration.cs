using DSharpPlus;
using tellahs_library.Commands;
using tellahs_library.Constants;

namespace tellahs_library.Extensions;

public static class DiscordConfiguration
{
    public static DiscordClientBuilder AddCommands(this DiscordClientBuilder builder)
    {
        var commandsConfig = new CommandsConfiguration
        {
            RegisterDefaultCommandProcessors = false
        };

        builder.UseCommands((IServiceProvider ServiceProvider, CommandsExtension commands) =>
        {
            commands.AddProcessor(new SlashCommandProcessor());
            commands.AddCommands<FlagsetChooser>();
            commands.AddCommands<Recall>();

#if DEBUG
            commands.AddCommands<Tournament>(GuildIds.AntiServer);
            commands.AddCommands<TournamentAdministration>(GuildIds.AntiServer);
            commands.AddCommands<TournamentOverrides>(GuildIds.AntiServer);
#else
            commands.AddCommands<Tournament>(GuildIds.AntiServer, GuildIds.SideTourneyServer);
            commands.AddCommands<TournamentAdministration>(GuildIds.AntiServer, GuildIds.SideTourneyServer);
            commands.AddCommands<TournamentOverrides>(GuildIds.AntiServer, GuildIds.SideTourneyServer);
#endif
        },
        commandsConfig);

        return builder;
    }

}
