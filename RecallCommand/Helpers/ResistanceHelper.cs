using tellahs_library.RecallCommand.Enums;

namespace tellahs_library.RecallCommand.Helpers
{
    public static class ResistanceHelper
    {
        public static DiscordEmbed GetResistanceInfo(ResistanceChoices resistanceChoice)
        {
            return resistanceChoice switch
            {
                ResistanceChoices.Overview => new DiscordEmbedBuilder()
                    .WithTitle("Know Your Resistances - Overview")
                    .WithDescription(GetOverviewDescription())
                    .AddField("Links", GetResistanceLinks())
                    .Build(),

                ResistanceChoices.LinksOnly => new DiscordEmbedBuilder()
                    .WithTitle("Resistance Links")
                    .WithDescription(GetResistanceLinks())
                    .Build(),

                ResistanceChoices.Mage => new DiscordEmbedBuilder()
                    .WithTitle("Trait: Mages")
                    .WithDescription(GetMageTraitInfo())
                    .Build(),

                ResistanceChoices.Giant => new DiscordEmbedBuilder()
                    .WithTitle("Trait: Giants")
                    .WithDescription(GetGiantTraitInfo())
                    .Build(),

                ResistanceChoices.Dragon => new DiscordEmbedBuilder()
                    .WithTitle("Trait: Dragons")
                    .WithDescription(GetDragonTraitInfo())
                    .Build(),

                ResistanceChoices.Robot => new DiscordEmbedBuilder()
                    .WithTitle("Trait: Robots")
                    .WithDescription(GetRobotTraitInfo())
                    .Build(),

                ResistanceChoices.Undead => new DiscordEmbedBuilder()
                    .WithTitle("Trait: Undead")
                    .WithDescription(GetZombieTraitInfo())
                    .Build(),

                ResistanceChoices.Slime => new DiscordEmbedBuilder()
                    .WithTitle("Trait: Slimes")
                    .WithDescription(GetSlimeTraitInfo())
                    .Build(),

                ResistanceChoices.Spirit => new DiscordEmbedBuilder()
                    .WithTitle("Trait: Spirits")
                    .WithDescription(GetSpiritTraitInfo())
                    .Build(),

                _ => new DiscordEmbedBuilder().WithDescription("currently unimplemented choice")

            };
        }

        static string GetOverviewDescription()
        {
            return @"FFIV has a system for resistance and weakness to damage that can feel a little complicated due to how the different parts of it can combine. It takes into account element and trait damage types being done, the damage source (Physical or Magic), and then what defensive properties the target has. Let's break that all down.  

Physical attacks can apply zero, one, or more elemental damage types and zero, one, or more trait types to the damage they do, and the entire damage carries that information. So if you have an archer equipped with Ice arrows and an Elven bow, that character is applying Ice and Air elements and the Mage trait to the damage being dealt. This is the Physical damage source.

Attacking with Spells or Items with spell effects carry no trait information, and effectively have zero or one element, as there are no spells with multiple elemental damage types. These are Magic damage sources.

Once the algorithm has finished figuring out things from the attacker's side, and then the defender's information comes into play. Most of the process is the same for any source of damage, but the key difference is the order in which elemental defensive properties are processed. For Physical sources, weaknesses are checked for first, then immunities, and finally absorb/resistance. For Magic sources, that processing order starts with immunities, then absorbs, then resistances, and ends with weaknesses. Only one each of the Elemental and trait types of damage will be applied, and the algorithm stops processing modifiers for that damage type. In other words, you can't actually apply two types of Elemental damage on the same attack, but you can hit an elemental and a trait weakness. A some practical examples of this processing are:

* Dark Elf's Dragon form - Holy-based physical damage hits a weakness, while Holy-based magic hits an absorb.

* Ice and Fire claws vs FlameDog - the attack carries both elemental damage types, but since the weakness gets checked first, the Ice weakness gets applied, and the Fire resist is ignored.

* Thunder and Charm claws vs MacGiants: two different Trait types can be hit (Robot and Giant), but only one will apply.

* Attacking your own Adamant-wearing party members: Casting Fire3 on the Adamant wearer hits the immune property, but attacking with a Fire claw, Flame spear, or Fire sword will hit a Fire weakness that the armor has.

Check out Deathlike's [Algorithm FAQ](<https://gamefaqs.gamespot.com/snes/522596-final-fantasy-ii/faqs/54945>) for more detail about the whole process of damage determination, and especially more details about Drain. Also check out the other options for this command using the `choice` parameter for some round-up information about specific traits: Dragon, Giant, Mage, Robot, Slime, Spirit, and Undead. The Undead trait has some special handling detailed in that round-up.
";
        }

        static string GetResistanceLinks()
        {
            return @"[Algorithm FAQ](<https://gamefaqs.gamespot.com/snes/522596-final-fantasy-ii/faqs/54945>)
            [Boss Weaknesses](<https://wiki.ff4fe.com/doku.php?id=boss_weaknesses>)
            [Enemy Lookup](<https://docs.google.com/spreadsheets/d/1_4ZXg5ZvOiIHP7WAmHFnk6jQEeEXyxO8mY8cn3qbR2M/edit?gid=108544148#gid=108544148>) - to use this, make your own copy of the sheet.";
        }

        static string GetMageTraitInfo()
        {
            return @"
**Key Enemies**
[Warlock](<https://wiki.ff4fe.com/doku.php?id=warlock>), [Conjurer](<https://wiki.ff4fe.com/doku.php?id=Conjurer>), [Magus Sisters](<https://wiki.ff4fe.com/doku.php?id=Cindy>), [Asura](<https://wiki.ff4fe.com/doku.php?id=Asura>), [Baron Guards](<https://wiki.ff4fe.com/doku.php?id=Guard>), [ToadLady](<https://wiki.ff4fe.com/doku.php?id=ToadLady>), [DarkTree](<https://wiki.ff4fe.com/doku.php?id=DarkTree>), [Rubicant (Elements)](<https://wiki.ff4fe.com/doku.php?id=elements_milon_rubi>)

**Weapons**
Elven bow, Rune axe, Mute knife, Mute arrows, Loki lute

**Armor**
Ribbon, Rune ring, Aegis shield

**Extra Notes**
[Alt Gauntlet](<https://wiki.ff4fe.com/doku.php?id=alt_gauntlet>)";
        }

        static string GetDragonTraitInfo()
        {
            return @"**Key Enemies**
            [D.Machin](<https://wiki.ff4fe.com/doku.php?id=d.machin>), [D.Fossil](<https://wiki.ff4fe.com/doku.php?id=d.fossil>), [D.Bone](<https://wiki.ff4fe.com/doku.php?id=d.bone>), [Blue D.](<https://wiki.ff4fe.com/doku.php?id=blue_d>), [Green D.](<https://wiki.ff4fe.com/doku.php?id=Green_d>), [Red D.](<https://wiki.ff4fe.com/doku.php?id=red_d>), [Yellow D](<https://wiki.ff4fe.com/doku.php?id=yellow_d>), [King Ryu](<https://wiki.ff4fe.com/doku.php?id=king-ryu>), [Ging Ryu](<https://wiki.ff4fe.com/doku.php?id=ging-ryu>), [D. Lunar](<https://wiki.ff4fe.com/doku.php?id=d._lunar>), [Wyvern](<https://wiki.ff4fe.com/doku.php?id=wyvern>), [Pale Dim](<https://wiki.ff4fe.com/doku.php?id=pale_dim>), [Clapper](<https://wiki.ff4fe.com/doku.php?id=clapper>)

**Weapons**
Dragoon spear, Artemis arrows, Dragon whip, Apollo harp, Loki lute, Dragon claw

**Armor**
Dragoon (all), Crystal ring, Tiara

**Extra Notes**
D.Mist and Bahamut are not Dragons. [Blue D.](<https://wiki.ff4fe.com/doku.php?id=blue_d>) has an extensive resistance list, and will take less damage than you expect most of the time.
[Alt Gauntlet](<https://wiki.ff4fe.com/doku.php?id=alt_gauntlet>)";
        }

        static string GetGiantTraitInfo()
        {
            return @"**Key Enemies**
            [Ogre](<https://wiki.ff4fe.com/doku.php?id=ogre>), [Mad Ogre](<https://wiki.ff4fe.com/doku.php?id=mad_ogre>), [MacGiant](<https://wiki.ff4fe.com/doku.php?id=MacGiant>), [RedGiant](<https://wiki.ff4fe.com/doku.php?id=RedGiant>), [Staleman](<https://wiki.ff4fe.com/doku.php?id=staleman>), [Stoneman](<https://wiki.ff4fe.com/doku.php?id=stoneman>)

**Weapons**
Poison axe, Ogre axe, Charm arrows, Charm claw, Drain sword, Drain spear, Perseus arrows, Loki lute

**Armor**
Zeus gauntlets

**Extra Notes**
The poor accuracy and stat debuffs on the Drain weapons make notably poor choices. If you can handle the wild inconsistency, though, they can work in a pinch.
[Alt Gauntlet](<https://wiki.ff4fe.com/doku.php?id=alt_gauntlet>)";
        }

        static string GetRobotTraitInfo()
        {
            return @"**Key Enemies**
            [MacGiant](<https://wiki.ff4fe.com/doku.php?id=MacGiant>), [EvilMask](<https://wiki.ff4fe.com/doku.php?id=EvilMask>), [Alert](<https://wiki.ff4fe.com/doku.php?id=Alert>), [Searcher](<https://wiki.ff4fe.com/doku.php?id=Searcher>), [Balnab](<https://wiki.ff4fe.com/doku.php?id=Balnab>), [Dr. Lugae (second form)](<https://wiki.ff4fe.com/doku.php?id=dr.lugae_second>), [Beamer](<https://wiki.ff4fe.com/doku.php?id=beamer>), [Last Arm](<https://wiki.ff4fe.com/doku.php?id=last_arm>)

**Weapons**
Earth hammer, Silver hammer, Wooden hammer, Thunder claw, Lit arrows, Loki lute, Thor hammer, Fiery hammer

**Armor**
None

**Extra Notes**
`Machine` is another common way to refer to this trait. Notable things that look like a Robot but aren't: RedGiant, D.Machin, Balnab-Z, CPU, Attacker, Defender.
[Alt Gauntlet](<https://wiki.ff4fe.com/doku.php?id=alt_gauntlet>)";
        }

        static string GetZombieTraitInfo()
        {
            return @"**Key Enemies**
            [Milon Z](<https://wiki.ff4fe.com/doku.php?id=milon_z>), [Milon Z (Elements)](<https://wiki.ff4fe.com/doku.php?id=elements_milon_rubi>), [D. Lunar](<https://wiki.ff4fe.com/doku.php?id=d._lunar>), [D.Fossil](<https://wiki.ff4fe.com/doku.php?id=d.fossil>), [D.Bone](<https://wiki.ff4fe.com/doku.php?id=d.bone>)

**Weapons**
Crystal sword, White arrows, Loki lute, Bringer sword, Silver staff

**Armor**
Crystal defensive gear (but not the Crystal ring), Paladin, Wizard hat, Sorcerer robe, White shirt

**Extra Notes**
The Undead trait provides a Darkness element immunity, allows dealing damage with Cure spells/items, and has special processing for the White spell: overriding any Holy element absorbtion. Weapons, spells, and items, with a drain effect do extra 'damage' but have the effect inverted, meaning they take HP or MP from your party member instead of the monster.
[Alt Gauntlet](<https://wiki.ff4fe.com/doku.php?id=alt_gauntlet>)";
        }

        static string GetSlimeTraitInfo()
        {
            return @"**Key Enemies**
            [Cream](<https://wiki.ff4fe.com/doku.php?id=cream>), [Jelly](<https://wiki.ff4fe.com/doku.php?id=Jelly>), [Slime](<https://wiki.ff4fe.com/doku.php?id=slime>), [Tofu](<https://wiki.ff4fe.com/doku.php?id=Tofu>), [Pink Puff](<https://wiki.ff4fe.com/doku.php?id=pinkpuff>)

**Weapons**
Drain spear, Drain sword, Loki lute, Flan sword

**Armor**
None

**Extra Notes**
All Slimes, except for Pink Puffs, have 254 Defense. That high defense means you're often going to rely on magic damage, weapons that hit the trait weakness, skills that raise attack power (Jump, Power), or Dart to get through the defense.
[Alt Gauntlet](<https://wiki.ff4fe.com/doku.php?id=alt_gauntlet>)";
        }

        static string GetSpiritTraitInfo()
        {
            return @"**Key Enemies**
            [BladeMan](<https://wiki.ff4fe.com/doku.php?id=BladeMan>), [Hooligan](<https://wiki.ff4fe.com/doku.php?id=Hooligan>), [Soul](<https://wiki.ff4fe.com/doku.php?id=soul>), [Spirit](<https://wiki.ff4fe.com/doku.php?id=spirit>), [Weeper](<https://wiki.ff4fe.com/doku.php?id=Weeper>)

**Weapons**
Silver dagger, Silver sword, Silver hammer, Ancient sword, White arrows, White spear, Bringer sword, Loki lute

**Armor**
all Silver defensive gear, White shirt

**Extra Notes**
The Silver staff is strong vs Undead, not Spirits. All Spirits are part of an [Alt Gauntlet](<https://wiki.ff4fe.com/doku.php?id=alt_gauntlet>). Most Spirits are weak to Holy damage.";
        }
    }
}
