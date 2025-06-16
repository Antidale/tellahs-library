using tellahs_library.RecallCommand.Enums;

namespace tellahs_library.RecallCommand.Helpers
{
    public static class ItemHelper
    {
        public static DiscordMessageBuilder GetItemNotes(ItemRecallOptions item)
        {
            return new DiscordMessageBuilder()
                .EnableV2Components()
                .AddContainerComponent(new DiscordContainerComponent(
                    components:
                    [
                        new DiscordTextDisplayComponent(GetContent(item))
                    ],
                    color: DiscordColor.Teal
                )
            );
        }

        private static string GetContent(ItemRecallOptions item)
        {
            return item switch
            {
                ItemRecallOptions.Cure3 => @$"### [{item}](<https://wiki.ff4fe.com/doku.php?id=item_stats_tables>)

Useful as a replacement for Cure3 and Cure4 spells, Cure3 potions can appear in any non-Smithy item shop on `Sstandard`; `Spro` restricts availability to Gated item shops.

If there are no White Mages in a seed, Cure3 potions will be guaranteed to be in a Gated item shop. This guarantee is void if `Sunsafe` is enabled.

The `NotDeme` kit always comes with three Cure3 potions, and up to three can appear in the `Better` kit.

**Tier**: 3
**Price**: 1000
",

                ItemRecallOptions.Ether1 => @$"### [{item}](<https://wiki.ff4fe.com/doku.php?id=item_stats_tables>)
            
Ether1 potions restore a bit of MP, coming in handy for getting an extra spell cast or two. They can appear in any non-Smithy item shop under both `Sstandard` and `Spro`. There are no availability guarantees unless `Swild` is enabled.

The `Mysidia` kit always includes 70 Ether1 potions, and the `Better` kit can include up to three.

**Tier**: 4
**Price**: 3000",

                ItemRecallOptions.Ether2 => @$"### [{item}](<https://wiki.ff4fe.com/doku.php?id=item_stats_tables>)
                
Ether2 potions restore roughly 100 MP and can appear in any non-Smithy item shop with `Sstandard`, and only Gated item shops can sell them under `Spro`. There are no availability guarantees unless `Swild` is enabled.

The `Loaded` kit comes with three Ether2 potions.

**Tier**: 4
**Price**: 5000",

                ItemRecallOptions.Exit => @$"### [{item}](<https://wiki.ff4fe.com/doku.php?id=item_stats_tables>)

Exits replicate the Exit spell, and can appear in any non-Smithy item shop under both `Sstandard` and `Spro`. There are no availability guarantees unless `Swild` is enabled.

The `Loaded` kit comes with five Exits, and the `Better` kit can have up to three.

**Tier**: 2
**Price**: 2000",

                ItemRecallOptions.Life => @$"### [{item}](<https://wiki.ff4fe.com/doku.php?id=item_stats_tables>)

Life potions cure the Swoon status, and can appear in any non-Smithy item shop under both `Sstandard` and `Spro`. Life potions are guaranteed to be in an Ungated shop unless `Sunsafe`, `Sno:life`, or both are enabled.

Life potions appear in the `Starter`, `Better`, `Loaded`, `Freedom`, `Mysidia`, and `Cata` kits.

**Tier**: 2
**Price**: 600
",

                ItemRecallOptions.SilkWeb => @$"### [{item}](<https://wiki.ff4fe.com/doku.php?id=item_stats_tables>)

SilkWebs apply two instances of the Slow spell on all enemies. They can appear in a non-Smithy item shop under both `Sstandard` and `Spro`. There are no availability guarantees unless `Swild` is enabled, and `Sno:j` removes any chance of them being in a shop regardless of other flags.

The `Loaded` kit can have up to three SilkWebs.

Centepedes, Arachne, and Talantla all have SilkWeb as an item that can be obtained with the `Steal` command. A common place Centepedes can be summoned using a Siren is outside Kokkol's shop. Enabling `Eno:jdrops` will prevent this.

**Tier**: 3
**Price**: 3000
",

                ItemRecallOptions.Siren => @$"### [{item}](<https://wiki.ff4fe.com/doku.php?id=item_stats_tables>)

Sirens summon the rarest encounter in an area for you to fight, and they can appear in a Gated item shop with `Sstandard` acive, and might be found in the Smithy shop in `Spro`. There are no availability guarantees unless `Swild` is enabled, and both `Sno:j` and `Sno:sirens` each remove any chance of them being in a shop regardless of other flags.

The `Freedom` kit comes with one to two Sirens, and the `Better` kit can have up to three of them.

Sirens can also be obtained via the `Steal` command from Alerts, Searchers, and Last Arm. Enabling either `Eno:sirens` or `Eno:jdrops` prevents this.
**Tier**: 5
**Price**: 4000
",

                ItemRecallOptions.StarVeil => @$"### [{item}](<https://wiki.ff4fe.com/doku.php?id=item_stats_tables>)
StarVeils cast Wall on the item's user, and will be found in an Ungated item shop unless `Sunsafe` is enabled; `Sno:j` removes any chance of them being in any shop regardless of other flags.

The `Starter`, `Better`, `Loaded`, `Freedom`, and `Cata` kits all contain StarVeils.

**Tier**: 2
**Price**: 3000
",

                ItemRecallOptions.ThorRage =>
@$"### [{item}](<https://wiki.ff4fe.com/doku.php?id=item_stats_tables>)
Because of their facility in dispelling Kainazzo's Wave attack, ThorRages will be found in an Ungated item shop unless `Sunsafe` is enabled; `Sno:j` removes any chance of them being in a shop regardless of other flags.

The `Freedom` kit contains ten ThorRages, and the `Better` kit can have up to three of them.

**Tier**: 2
**Price**: 200
",

                ItemRecallOptions.Vampire =>
@$"### [{item}](<https://wiki.ff4fe.com/doku.php?id=item_stats_tables>)
Vampires are a solid damage alternative early on, and can appear in any item shop with `Sstandard`. Under `Spro` only Gated item shops might have them. There are no availability guarantees unless `Swild` is enabled. `Sno:j` removes any possibility of finding them in shops.

The `Better` and `Loaded` kits each can have up to two Vampires contained within.

**Tier**: 4
**Price**: 4000
",
                _ => "Item not implemented"
            };
        }
    }
}
