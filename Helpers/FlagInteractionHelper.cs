﻿using DSharpPlus.Entities;
using tellahs_library.Enums;

namespace tellahs_library.Helpers
{
    public static class FlagInteractionHelper
    {
        public static DiscordEmbed GetFlagInteractionAsync(FlagInteractionChoices flagInteractionChoice)
        {
            var embedBuilder = new DiscordEmbedBuilder();

            return flagInteractionChoice switch
            {
                FlagInteractionChoices.Kmiab_UnderworldAccess => embedBuilder.WithTitle("Kmiab vs Underworld Access")
                    .WithDescription(
@"When `Kmiab` is enabled, the monster boxes in the following areas can be part of the logical path to the Underworld:
* Eblan Castle
* Eblan Cave
* Tower of Zot
* Upper Bab-il

Monster boxes on the Giant of Bab-il and the Lunar Path can have Key Items, but cannot gate Underworld access.

If `Kmoon` has also been enabled, the nine monster boxes in the Lunar Subterrane can have Key Items, but cannot gate Underworld access.

If `Kunsafe` has also been enabled, the eleven monster boxes gated by the Darkness crystal can have Key Items and can gate Underworld access.
")
                    .Build(),

                FlagInteractionChoices.Agility_vs_Hero => embedBuilder.WithTitle("Chero vs Vanilla:Agility")
                    .WithDescription(
@"`Chero` and `-vanilla:agility` both change who the randomizer uses as anchor for determining Relative Agility as a fight begins. 

When both are enabled, the anchoring changes that are part of `Chero` do not apply, and only `-vanilla:agility` is in effect.")
                    .WithImageUrl("https://ff4-fe-info.s3.us-west-2.amazonaws.com/library-images/agility-flowchart-p.jpg")
                    .Build(),

                FlagInteractionChoices.ForceMagma_DMist => embedBuilder.WithTitle("Kforce:magma vs D.Mist")
                    .WithDescription(
@"When both `Kforce:magma` and `Knofree` are enabled, D.Mist can have (or gate) logical Underworld access at any location that does not require falling down the pitfall and which follows Underworld access modifications from other flags. 

As an example, this means that a D.Mist at the vanilla Rubicant position can provide access to the Magma key.")
                    .Build(),

                FlagInteractionChoices.BVanilla_OdinSpot => embedBuilder.WithTitle("Bvanilla vs Odin's spot")
                    .WithDescription(
@"When `Ksummon` and `Bvanilla` are enabled, the Odin spot can gate your logical Underworld access. This is not intended behavior, but do not expect this to change before 5.0.

See [this](<https://discord.com/channels/411615349579186178/411616251836628993/942203585486274570>) explanation in #bug-reports in the Free Enterprise Workshop for the official explanation")
                    .Build(),

                _ => embedBuilder.WithTitle("Reference Material Missing")
                    .WithDescription("You've requested information not contained within the Library. Many apologies.")
                    .Build()
            };
        }
    }
}
