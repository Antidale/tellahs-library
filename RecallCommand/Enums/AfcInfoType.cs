using DSharpPlus.Commands.Processors.SlashCommands.ArgumentModifiers;

namespace tellahs_library.RecallCommand.Enums;

public enum AfcInfoType
{
    [ChoiceDisplayName("Adamant Cup Experience")]
    Ace,
    [ChoiceDisplayName("Firebomb Fiesta")]
    Fbf,
    [ChoiceDisplayName("Zemus Zone: Anthology")]
    Zza,
    [ChoiceDisplayName("Fork Flags Info")]
    ForkFlags,
}
