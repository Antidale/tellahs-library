using DSharpPlus.Commands.Processors.SlashCommands.ArgumentModifiers;

namespace tellahs_library.RollCommand.Enums;

public enum FeHostedApi
{
#if DEBUG
    [ChoiceDisplayName("Local")]
    Local,
#endif

    [ChoiceDisplayName("Main")]
    Main,

    [ChoiceDisplayName("Galeswift")]
    Galeswift,

    [ChoiceDisplayName("5.0 Alpha")]
    Alpha
}
