using System.ComponentModel;
using DSharpPlus.Commands.Processors.SlashCommands.ArgumentModifiers;

namespace tellahs_library.RacingCommands.Enums;

public enum RtggGoal
{
    [ChoiceDisplayName("Beat Zeromus")]
    BeatZeromus,
    [ChoiceDisplayName("Complete Objectives")]
    WinGame,
    [ChoiceDisplayName("Door Rando - Season -1")]
    DoorRando,
    [ChoiceDisplayName("5.0 alpha/beta")]
    Alpha,
    [ChoiceDisplayName("Entrance Rando - Season -1")]
    EntranceRando,
}
