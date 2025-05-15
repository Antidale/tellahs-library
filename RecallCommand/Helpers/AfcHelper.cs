using System;
using DSharpPlus;
using tellahs_library.RecallCommand.Enums;

namespace tellahs_library.RecallCommand.Helpers;

public static class AfcHelper
{
    public static DiscordEmbed GetAfcEmbeds(AfcInfoType infoType, AfcDetailOptions detailOptions)
    {
        return infoType switch
        {
            AfcInfoType.Ace => GetAceFlagsetInfo(detailOptions),
            AfcInfoType.Fbf => GetFbfFlagsetInfo(detailOptions),
            AfcInfoType.Zza => GetZzaFlagsetInfo(detailOptions),
            AfcInfoType.ForkFlags => GetForkFlagsInfo(detailOptions),
            _ => throw new NotImplementedException("I'm sorry, I fumbled it!")
        };
    }

    private static DiscordEmbed GetAceFlagsetInfo(AfcDetailOptions detailOptions)
    {
        var builder = new DiscordEmbedBuilder()
                    .WithTitle("Adamant Cup Experience")
                    .WithUrl("https://ff4fe.galeswift.com/make/bBAYBAAYAAAAAAAAAkAgBAAAAAIAAAAB4gBBQAQAAAAAAAAIQjkBhABkAIAAAAJAAFACcAACIIAAAAAAAAAAAACA")
                    .WithColor(DiscordColor.HotPink);

        builder.WithDescription(detailOptions == AfcDetailOptions.Flags
        ? baseDescription()
        : baseDescription() + summary());

        foreach (var flagField in flagFields())
        {
            builder.AddField(flagField.Key, flagField.Value);
        }

        return builder.AddField("Signups Close", $"{Formatter.Timestamp(DateTime.Parse("2025-06-07T06:59:00Z"), TimestampFormat.ShortDateTime)}")
           .AddField("Registration Form", "[AFC Registration](<https://docs.google.com/forms/d/e/1FAIpQLSe1tPCmPsLZbDRvCCjZFfDHei20cMcoL4gw_Z8duechwpbaRA/viewform>)")
           .Build();

        #region ACE Local Methods
        string baseDescription() => @"### Flagset:
```Omode:ki12/random:2,quest/random2:1,tough_quest/req:all/win:crystal 
Kmain/summon/moon/nofree:dwarf/unweighted Pkey Cstandard/nofree/restrict:cecil,fusoya/j:abilities/paladin/nekkie/party:4/treasure:free,unsafe Twildish Sprice:200/pricey:items/standard Bstandard/alt:gauntlet/whichbez Etoggle Glife/sylph/backrow -kit:better -smith:alt -fusoya:sequential_r -exp:objectivebonus25 -tweak:edwardheal
```
To see a full listing of the fork flags: <https://wiki.ff4fe.com/doku.php?id=forks>
";
        string summary() => @"
### Quick summary:
Summaries are **rough approximations**! Check the actual details on the [rules doc](<https://docs.google.com/document/d/1O3Kr5gB0niU2KCArtXzjmnCmFFEBwgVA7Nas7FIGANM/edit?tab=t.0#heading=h.12ukq9gc26q8>) and the wiki.

Get 12 Key Items, do two random quests and one random tough quest for the Crystal. 

Key items are at pretty normal spots, but Cid in Dwarf Castle has the Bedward/D.Mist item. The Pass is in the KI Pool. All KI checks have a (roughly) equal chance of being Key Items.

Cecil and Fu are restricted, and free characters are on, but are hiding on treasure. FuSoYa learns spells (by boss kills) in the order that Rydia and Rosa learn them (via leveling). Cecil starts as a Paladin, no changing required. You only get 4 characters.

Twildish for looting goodness. Shops are standard, but consumables are more expensive. 

The Golbez fight gets some randomization.  

You get more XP per objective you finish. Edward's Heal ability isn't totally awful. Adamants are on, Smith is Alt.";

        Dictionary<string, string> flagFields() => new()
        {
            ["Omode:ki12"] = "Gather any 12 key items to satisfy this objective.",
            ["Knofree:dwarf"] = "BedCid has a key item, not Bedward.",
            ["Kunweighted"] = "removes the normal key item placment weighting. [KI distribution](<https://wiki.ff4fe.com/doku.php?id=key_item_randomization#key_item_distribution>)",
            ["Cpaladin"] = "Cecil will start as a Paladin; Ordeals still good for Tellah and a KI check.",
            ["Ctreasure:free,unsafe"] = "Characters normally found in free locations will now be in any normal treasure chest (no MIABs and no chests possibly having a KI).",
            ["Sprice:200/pricey:items"] = "consumable items adjusted to double their normal value. weapons/armor are not changed.",
            ["Bwhichbez"] = "Golbez and Shadow get new spells! Shadow can sometimes use things that shouldn't be reflected, so maybe don't Reflect up.",
            ["-fusoya:sequential_r"] = "Instead of learning spells randomly, FuSoYa will learn spells in order based on the levels at which Rydia and Rosa learn their spells. He will not learn spells learned outside of level-ups (Fire1, Fire2/Ice2/Lit2, Exit).",
            ["-exp:objectivebonus25"] = "Each completed objective gives 25% extra EXP.",
            ["-tweak:edwardheal"] = "Edward's Heal command will now use the best of Cure3/Cure2/Cure1 available."
        };
        #endregion

    }

    private static DiscordEmbed GetFbfFlagsetInfo(AfcDetailOptions detailOptions)
    {
        var builder = new DiscordEmbedBuilder()
                    .WithTitle("Firebomb Fiesta")
                    .WithUrl("https://ff4fe.galeswift.com/make/bBAYBAACQCn--IwAAGAEAAAAAAIADAAAU4QRAcQAAAQEAAAMAGhBgAACARABAAfAAHQxeVeEAAAMAAAAAAAAAACA")
                    .WithColor(DiscordColor.Orange);

        builder.WithDescription(detailOptions == AfcDetailOptions.Flags
            ? baseDescription()
            : baseDescription() + summary());

        if (detailOptions != AfcDetailOptions.Summary)
        {
            foreach (var flagField in flagFields())
            {
                builder.AddField(flagField.Key, flagField.Value);
            }
        }

        return builder.AddField("Signups Close", $"{Formatter.Timestamp(DateTime.Parse("2025-06-07T06:59:00Z"), TimestampFormat.ShortDateTime)}")
           .AddField("Registration Form", "[AFC Registration](<https://docs.google.com/forms/d/e/1FAIpQLSe1tPCmPsLZbDRvCCjZFfDHei20cMcoL4gw_Z8duechwpbaRA/viewform>)")
           .Build();

        #region Fbf Local Methods
        string baseDescription() => @"### Flagset
```
O1:quest_forge/2:quest_cavebahamut/3:quest_monsterking/4:quest_monsterqueen/5:quest_masamunealtar/random:3,tough_quest/req:6/win:game Kmain/miab:above/pink/nofree:package/force:magma Pnone Cstandard/nofree/distinct:7/start:not_tellah,not_fusoya/no:fusoya/restrict:cecil/j:abilities/nekkie/nodupes/hero Twildish/mintier:2/money Swild/always:damage_items/no:j Bstandard/nofree/unsafe/alt:gauntlet/whichburn/whichbez/woahdin Etoggle/noexp/nogp Gwarp/life/sylph/backrow -kit:dwarf -kit2:miab -kit3:basic -noadamants -nocursed -spoon -exp:kicheckbonus2 -tweak:edwardheal
```
To see a full listing of the fork flags: <https://wiki.ff4fe.com/doku.php?id=forks>";

        string summary() => @"### Quick summary
Summaries are **rough approximations**! Check the actual details on the [rules doc](<https://docs.google.com/document/d/1O3Kr5gB0niU2KCArtXzjmnCmFFEBwgVA7Nas7FIGANM/edit?tab=t.0#heading=h.12ukq9gc26q8>) and the wiki.

Do six of Forge, Feymarch (king and queen), Value, Masa, and 3 other tough quests to win. That's 6 of 8, win:game.

Key Items can be at Main spots and in Above MIABS (Overworld, Hairdryers, Last Arm). Pink Tail is a Main checks, and the Package boss fight replaces D.Mist for setting up Rydia's Mom. Magma is Forced. No Pass.

Cstandard, but: there are only 7 different people, never Fu. Can't start with Tellah. Cecil is restricted, no dupes. Hero is ON.

Twildish for $$$ only. Shopping's wild, but no J-items unless they're for damage.

Bstandard but neither free nor safe, Gauntlet is Alt. Wyvern, Golbez, and Odin all have some spell randomization.

Random encounters take time, give nothing. Do MIABs and Bosses for xp/gold.

Warp Glitch! No Cursed ring or Adamants. Bonus XP for KI Checks. Spoon is on and Edward's Heal isn't totally awful.";

        Dictionary<string, string> flagFields() => new()
        {
            ["Kmiab:above"] = "Overworld MIABs, Hairdryers, and Last Arm can have Key Items. Underground access still locked to Overworld checks only.",
            ["Kpink"] = "Pink Tail is a Kmain check",
            ["Knofree:package"] = "The Package takes the place of D.Mist for letting Rydia's Mom give you a KI",
            ["Salways:damage_items"] = "Damage items are guaranteed to be in shops",
            ["Bwhoadin"] = "consumable items adjusted to double their normal value. weapons/armor are not changed.",
            ["Bwhichbez"] = "Golbez and Shadow get new spells! Shadow can sometimes use things that shouldn't be reflected, so maybe don't Reflect up.",
            ["Enogp"] = "No GP for random encounters.",
            ["-exp:kicheckbonus2"] = "Each completed KI check gives 2% extra EXP.",
            ["-tweak:edwardheal"] = "Edward's Heal command will now use the best of Cure3/Cure2/Cure1 available."
        };
        #endregion
    }

    private static DiscordEmbed GetZzaFlagsetInfo(AfcDetailOptions detailOptions)
    {
        var builder = new DiscordEmbedBuilder()
                            .WithTitle("Zemus Zone: Anthology")
                            .WithUrl("https://ff4fe.galeswift.com/make/bBAYBAACQAgAAAAAAAAAAAAAAAIAAAABYYBCUgQAAAAAAAABAEhBAgAAAWAAACBRQFQB8ASAAAQACAAAAAAAAACA")
                            .WithColor(DiscordColor.Blue);

        builder.WithDescription(detailOptions == AfcDetailOptions.Flags
            ? baseDescription()
            : baseDescription() + summary());

        if (detailOptions != AfcDetailOptions.Summary)
        {
            foreach (var flagField in flagFields())
            {
                builder.AddField(flagField.Key, flagField.Value);
            }
        }

        return builder.AddField("Signups Close", $"{Formatter.Timestamp(DateTime.Parse("2025-06-07T06:59:00Z"), TimestampFormat.ShortDateTime)}")
           .AddField("Registration Form", "[AFC Registration](<https://docs.google.com/forms/d/e/1FAIpQLSe1tPCmPsLZbDRvCCjZFfDHei20cMcoL4gw_Z8duechwpbaRA/viewform>)")
           .Build();

        #region ZZA Local Methods
        string baseDescription() =>
@"### Flagset
```
O1:quest_forge/req:all/win:crystal Kmain/moon/pink/nofree/unweighted/start:spoon Pkey Crelaxed/nofree/distinct:8/thrift:3/j:abilities/nodupes/hero Tsemipro/playable/junk Swildish/no:vampires,damage_items Bstandard/restrict:giant,package/whichburn/whichbez Etoggle Glife/sylph/backrow/64 -kit:freedom -noadamants -fusoya:maybe -exp:maxlevelbonus -tweak:edwardheal
```
To see a full listing of the fork flags: <https://wiki.ff4fe.com/doku.php?id=forks>
";


        string summary() => @"### Quick Summary
Summaries are **rough approximations**! Check the actual details on the [rules doc](<https://docs.google.com/document/d/1O3Kr5gB0niU2KCArtXzjmnCmFFEBwgVA7Nas7FIGANM/edit?tab=t.0#heading=h.12ukq9gc26q8>) and the wiki.

Forge to get a Crystal. Key items are Main, Moon, Pink and D.Mist. All KI spots are (roughly) equal chance of being rewarded. You start with a Spoon! The Pass is in the KI pool.

Characters are relaxed, there are 8 of them, none are free, no dupes, and they start with decent gear. Hero is On.

Treasure is between pro and wildish, junk items are not auto-cash. Swildish, but no damage items.

Bosses are standard, but D.Mist shouldn't be at the Giant or the Package spots. Wyvern (opening MegaNuke) and Golbez get some spell randomization.

64 floor glitch is ON. Here's some extra detail in the [wiki](<https://wiki.ff4fe.com/doku.php?id=glitches&s[]=64#performing_the_64_floor_glitch_in_fe>).

Adamants are OFF. Fu might not learn everything! Edward's Heal isn't totally trash! Level differences matter for XP gains.
";

        Dictionary<string, string> flagFields() => new()
        {
            ["Kpink"] = "The reward for trading away the Pink Tail is a key item.",
            ["Kunweighted"] = "removes the normal key item placment weighting. [KI distribution](<https://wiki.ff4fe.com/doku.php?id=key_item_randomization#key_item_distribution>)",
            ["Kstart:spoon"] = "The starting Key Item is a Spoon!",
            ["Cthrift:3"] = "Characters start with a full set of gear: weapon(s), possibly a shield, and head/body/arms, all from at most the tier specified. (Duplicate characters will have the same starting gear. Cursed Rings are excluded.) Must select a tier between two and five to limit the gear to at most that tier. ",
            ["Tsemipro"] = "Uses location-based weighting, with item quality between that provided by pro and wildish.",
            ["Tplayable"] = "Equipment in chests (including MIABs) will be usable by at least one character that you can acquire in the seed.",
            ["Swildish"] = "A basic randomization, allowing slightly stronger items than the standard randomization to place in shops.",
            ["Sno:vampires,damage_items"] = "Vampires and Damage Items are removed from shops",
            ["Brestrict:giant,package"] = "Allows the ability to force boss hunt objectives (and D.Mist, if Knofree is enabled) to not be in listed locations.",
            ["Bwhichbez"] = "Golbez and Shadow get new spells! Shadow can sometimes use things that shouldn't be reflected, so maybe don't Reflect up.",
            ["-fusoya:maybe"] = "Normally, FuSoYa will eventually learn all possible spells. This flag removes that guarantee; each possible spell will be included independently with an 85% chance. ",
            ["-exp:maxlevelbonus"] = "Normally, level does not play into EXP calculations. Under this flag, if 5 plus twice the largest level in your party is less than the smallest monster level in the encounter, then the encounter awards 20% bonus EXP (and another 20% for each additional deficit of 5). ",
            ["-tweak:edwardheal"] = "Edward's Heal command will now use the best of Cure3/Cure2/Cure1 available."
        };
        #endregion

    }

    private static DiscordEmbed GetForkFlagsInfo(AfcDetailOptions detailOptions)
    {
        return new DiscordEmbedBuilder().WithTitle("Fork Flags")
                        .WithUrl("https://wiki.ff4fe.com/doku.php?id=forks")
                        .WithColor(DiscordColor.PhthaloGreen)
                        .WithDescription(
@"The AFC is a team event for Free Enterprise and uses many flags introduced by the community in forks of the main game. To help people get a sense of all the changes here is a list of each flag that is in use among the three flagsets for the tournamnet, with a brief description.  

To see a full listing of the fork flags: <https://wiki.ff4fe.com/doku.php?id=forks>

### Objective Flags
 * **Omode:ki12** - Gather any 12 key items to satisfy this objective.
### Key Item Flags
 * **Knofree:dwarf** -BedCid has a key item, not Bedward.
 * **Knofree:package** -The Package takes the place of D.Mist for letting Rydia's Mom give you a KI
 * **Kpink** -The reward for trading away the Pink Tail is a key item.
 * **Kmiab:above** -Overworld MIABs, Hairdryers, and Last Arm can have Key Items. Underground access still locked to Overworld checks only.
 * **Kunweighted** -removes the normal key item placment weighting. [KI distribution](<https://wiki.ff4fe.com/doku.php?id=key_item_randomization#key_item_distribution>)
 * **Kstart:spoon** -The starting Key Item is a Spoon!
### Characters
 * **Cpaladin** - Cecil will start as a Paladin; Ordeals still good for Tellah and a KI check.
 * **Ctreasure:free,unsafe** - Characters normally found in free locations will now be in any normal treasure chest (no MIABs and no chests possibly having a KI).
 * **Cthrift:3** - Characters start with a full set of gear: weapon(s), possibly a shield, and head/body/arms, all from at most the tier specified. (Duplicate characters will have the same starting gear. Cursed Rings are excluded.) Must select a tier between two and five to limit the gear to at most that tier.
### Treasures
 * **Tsemipro** - Uses location-based weighting, with item quality between that provided by pro and wildish.
 * **Tplayable** - Equipment in chests (including MIABs) will be usable by at least one character that you can acquire in the seed.
### Shops
 * **Sprice:200/pricey:items** - consumable items adjusted to double their normal value. weapons/armor are not changed.
 * **Salways:damage_items** - Damage items are guaranteed to be in shops
 * **Swildish** - A basic randomization, allowing slightly stronger items than the standard randomization to place in shops.
 * **Sno:vampires,damage_items** - Vampires and Damage Items are removed from shops.
### Bosses
 * **Bwhichbez** - Golbez and Shadow get new spells! Shadow can sometimes use things that shouldn't be reflected, so maybe don't Reflect up.
 * **Bwhoadin** - consumable items adjusted to double their normal value. weapons/armor are not changed.
 * **Brestrict:giant,package** - Allows the ability to force boss hunt objectives (and D.Mist, if Knofree is enabled) to not be in listed locations.
### Fu related
 * **-fusoya:sequential_r** - Instead of learning spells randomly, FuSoYa will learn spells in order based on the levels at which Rydia and Rosa learn their spells. He will not learn spells learned outside of level-ups (Fire1, Fire2/Ice2/Lit2, Exit).
 * **-fusoya:maybe** - Normally, FuSoYa will eventually learn all possible spells. This flag removes that guarantee; each possible spell will be included independently with an 85% chance.
### XP & Encounter flags
 * **Enogp** - No GP for random encounters.
 * **-exp:objectivebonus25** - Each completed objective gives 25% extra EXP.
 * **-exp:kicheckbonus2** - Each completed KI check gives 2% extra EXP.
 * **-exp:maxlevelbonus** - Normally, level does not play into EXP calculations. Under this flag, if 5 plus twice the largest level in your party is less than the smallest monster level in the encounter, then the encounter awards 20% bonus EXP (and another 20% for each additional deficit of 5).
### Misc
 * **-tweak:edwardheal** - Edward's Heal command will now use the best of Cure3/Cure2/Cure1 available")
        .AddField("Signups Close", $"{Formatter.Timestamp(DateTime.Parse("2025-06-07T06:59:00Z"), TimestampFormat.ShortDateTime)}")
           .AddField("Registration Form", "[AFC Registration](<https://docs.google.com/forms/d/e/1FAIpQLSe1tPCmPsLZbDRvCCjZFfDHei20cMcoL4gw_Z8duechwpbaRA/viewform>)")
           .Build();
    }
}
