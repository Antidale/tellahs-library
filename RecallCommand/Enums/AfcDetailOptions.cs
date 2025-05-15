using System;
using DSharpPlus.Commands.Processors.SlashCommands.ArgumentModifiers;

namespace tellahs_library.RecallCommand.Enums;

public enum AfcDetailOptions
{
    [ChoiceDisplayName("Flag Details")]
    Flags,
    [ChoiceDisplayName("Summary")]
    Summary,
    [ChoiceDisplayName("Both")]
    Both

}
