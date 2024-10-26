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

                FlagInteractionChoices.SafetyChecks => embedBuilder.WithTitle("Safety Checks")
                .WithDescription(
@"The randomizer guarantees that there will be a path to the underworld that does not require defeating any of these bosses:
* Golbez
* Wyvern
* Valvalis
* The boss in the vanilla Odin position (Baron Basement)
* D.Knight (only if `Bnofree` is enabled)

Additionally, under `Knofree`, if one of the above bosses is in the Karate position and D.Mist is in the Baron Guards position, then the reward for defeating D.Mist will not be part of the logical path to the underworld.

If you are playing with `Bwhichburn`, the attack replacing Wyvern's MegaNuke will be less devastating than MegaNuke.

The above safety checks can be disabled by applying the `Bunsafe` flag.

Additionally, there will be a path to the underworld that does not require any checks gated by the Darkness Crystal. This check can be disabled using `Kunsafe`.

To prevent unwinnable/painful roadblocks some items are forced to be available:
* Cure2 and Life potions are guaranteed to be available in an ungated shop.
* StarVeils and ThorRages are guaranteed to be available in an ungated shop (unless excluded with `Sno:j`).
* Cure3 potions are guaranteed to be in a gated shop if there are no white mages available in the seed

Setting `Sunsafe` will remove all of these guarantees. Setting `Bunsafe` will remove the ThorRage guarantee. Setting `Scabins` or `Sempty` also removes these guarantees.")
                .AddField("Additional Links", "[KI Safeties](<https://wiki.ff4fe.com/doku.php?id=key_item_randomization#safety_checks>), [Boss Safeties](<https://wiki.ff4fe.com/doku.php?id=boss_randomization#safety_checks>), [Shop Guarantees](<https://wiki.ff4fe.com/doku.php?id=shop_randomization#safety_guarantees>)"),

                _ => embedBuilder.WithTitle("Reference Material Missing")
                    .WithDescription("You've requested information not contained within the Library. Many apologies.")
                    .Build()
            };
        }
    }
}
