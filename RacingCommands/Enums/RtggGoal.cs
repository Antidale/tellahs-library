using DSharpPlus.Commands.Processors.SlashCommands.ArgumentModifiers;
using tellahs_library.Attributes;

namespace tellahs_library.RacingCommands.Enums;

public enum RtggGoal
{
    [ChoiceDisplayName("Beat Zeromus")]
    [DiscordColor(0xCE1141)]
    BeatZeromus,
    [ChoiceDisplayName("Complete Objectives")]
    [DiscordColor(0xff6baa)]
    WinGame,
    [ChoiceDisplayName("5.0 alpha/beta")]
    [DiscordColor(0x7332a8)]
    Alpha,
}
