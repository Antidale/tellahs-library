using tellahs_library.Enums;

namespace tellahs_library.Helpers
{
    public static class ItemHelper
    {
        public static DiscordEmbed GetItemNotes(ItemRecallOptions item)
        {
            var builder = new DiscordEmbedBuilder()
                                .WithTitle(item.ToString())
                                .WithUrl("https://wiki.ff4fe.com/doku.php?id=item_stats_tables");

            return item switch
            {
                ItemRecallOptions.Cure3 => builder
                    .WithDescription(
@"Useful as a replacement for Cure3 and Cure4 spells, Cure3 potions can appear in any non-Smithy item shop on `Sstandard`; `Spro` restricts availability to Gated item shops.

If there are no White Mages in a seed, Cure3 potions will be guaranteed to be in a Gated item shop. This guarantee is void if `Sunsafe` is enabled.

The `NotDeme` kit always comes with three Cure3 potions, and up to three can appear in the `Better` kit.
")
                    .AddField("Tier", "4", inline: true)
                    .AddField("Price", "3000", inline: true)
                    .Build(),

                ItemRecallOptions.Ether1 => builder
                    .WithDescription(
@"Ether1 potions restore a bit of MP, coming in handy for getting an extra spell cast or two. They can appear in any non-Smithy item shop under both `Sstandard` and `Spro`. There are no availability guarantees unless `Swild` is enabled.

The `Mysidia` kit always includes 70 Ether1 potions, and the `Better` kit can include up to three of them")
                    .AddField("Tier", "3", inline: true)
                    .AddField("Price", "1000", inline: true)
                    .Build(),

                ItemRecallOptions.Ether2 => builder
                    .WithDescription(
@"Ether2 potions restore roughly 100 MP and can appear in any non-Smithy item shop with `Sstandard`, and only Gated item shops can sell them under `Spro`. There are no availability guarantees unless `Swild` is enabled.

The `Loaded` kit comes with three Ether2 potions
")
                    .AddField("Tier", "4", inline: true)
                    .AddField("Price", "5000", inline: true)
                    .Build(),

                ItemRecallOptions.Exit => builder
                    .WithDescription(
@"Exits replicate the Exit spell, and can appear in any non-Smithy item shop under both `Sstandard` and `Spro`. There are no availability guarantees unless `Swild` is enabled.

The `Loaded` kit comes with five Exits, and the `Better` kit can have up to three.")
                    .AddField("Tier", "2", inline: true)
                    .AddField("Price", "2000", inline: true)
                    .Build(),

                ItemRecallOptions.Life => builder
                    .WithDescription(
@"Life potions cure the Swoon status, and can appear in any non-Smithy item shop under both `Sstandard` and `Spro`. Life potions are guaranteed to be in an Ungated shop unless `Sunsafe`, `Sno:life`, or both are enabled.

Life potions appear in the `Starter`, `Better`, `Loaded`, `Freedom`, `Mysidia`, and `Cata` kits.
")
                    .AddField("Tier", "2", inline: true)
                    .AddField("Price", "600", inline: true)
                    .Build(),

                ItemRecallOptions.SilkWeb => builder
                    .WithDescription(
@"SilkWebs apply two instances of the Slow spell on all enemies. They can appear in a non-Smithy item shop under both `Sstandard` and `Spro`. There are no availability guarantees unless `Swild` is enabled, and `Sno:j` removes any chance of them being in a shop regardless of other flags.

The `Loaded` kit can have up to three SilkWebs.
")
                    .AddField("Tier", "3", inline: true)
                    .AddField("Price", "3000", inline: true)
                    .Build(),

                ItemRecallOptions.Siren => builder
                    .WithDescription(
@"Sirens summon the rarest encounter in an area for you to fight, and they can appear in a Gated item shop with `Sstandard` acive, and might be found in the Smithy shop in `Spro`. There are no availability guarantees unless `Swild` is enabled, and both `Sno:j` and `Sno:sirens` each remove any chance of them being in a shop regardless of other flags.

The `Freedom` kit comes with one to two Sirens, and the `Better` kit can have up to three of them.")
                    .AddField("Tier", "5", inline: true)
                    .AddField("Price", "4000", inline: true)
                    .Build(),

                ItemRecallOptions.StarVeil => builder
                    .WithDescription(
@"StarVeils cast Wall on the item's user, and will be found in an Ungated item shop unless `Sunsafe` is enabled; `Sno:j` removes any chance of them being in any shop regardless of other flags.

The `Starter`, `Better`, `Loaded`, `Freedom`, and `Cata` kits all contain StarVeils.
")
                    .AddField("Tier", "2", inline: true)
                    .AddField("Price", "3000", inline: true)
                    .Build(),

                ItemRecallOptions.ThorRage => builder
                    .WithDescription(
@"Because of their facility in dispelling Kainazzo's Wave attack, ThorRages will be found in an Ungated item shop unless `Sunsafe` is enabled; `Sno:j` removes any chance of them being in a shop regardless of other flags.

The `Freedom` kit contains ten ThorRages, and the `Better` kit can have up to three of them.
")
                    .AddField("Tier", "2", inline: true)
                    .AddField("Price", "200", inline: true)
                    .Build(),

                ItemRecallOptions.Vampire => builder
                    .WithDescription(
@"Vampires are a solid damage alternative early on, and can appear in any item shop with `Sstandard`. Under `Spro` only Gated item shops might have them. There are no availability guarantees unless `Swild` is enabled. `Sno:j` removes any possibility of finding them in shops.

The `Better` and `Loaded` kits each can have up to two Vampires contained within.")
                    .AddField("Tier", "4", inline: true)
                    .AddField("Price", "4000", inline: true)
                    .Build(),

                _ => new DiscordEmbedBuilder().WithTitle("Selected item is not yet included in this library's catalog")
            };
        }
    }
}
