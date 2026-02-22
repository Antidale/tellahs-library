using DSharpPlus.Commands.Processors.SlashCommands.ArgumentModifiers;
using tellahs_library.Attributes;

namespace tellahs_library.RacingCommands.Enums;

public enum AfcFlagset
{
    [ChoiceDisplayName("Megaflare Rally")]
    [DiscordColor(0xEF3B24)]
    Ace,
    [ChoiceDisplayName("Tsunami Open")]
    [DiscordColor(0x134A8E)]
    Fbf,
    [ChoiceDisplayName("Zantetsuken Circuit")]
    [DiscordColor(0x69BE28)]
    Zza
}
