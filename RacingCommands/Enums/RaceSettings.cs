using DSharpPlus.Commands.Processors.SlashCommands.ArgumentModifiers;

namespace tellahs_library.RacingCommands.Enums;

public enum RaceSettings
{
    [ChoiceDisplayName("Strict (streaming required)")]
    Strict,
    [ChoiceDisplayName("Casual (no streaming required)")]
    Casual,
}
