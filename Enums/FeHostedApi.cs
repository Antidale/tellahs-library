using System.ComponentModel;
using DSharpPlus.Commands.Processors.SlashCommands.ArgumentModifiers;

namespace tellahs_library.Enums;

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
}
