using DSharpPlus.Entities;
using tellahs_library.Enums;

namespace tellahs_library.Helpers
{
    public static class BossInfoEmbedHelper
    {
        public static DiscordEmbed GetBossInfoEmbed(BossName bossName)
        {
            var embedBuilder = new DiscordEmbedBuilder();

            switch (bossName)
            {
                case BossName.DMist:
                    embedBuilder
                        .WithTitle("D.Mist")
                        .WithUrl("https://wiki.ff4fe.com/doku.php?id=d.mist")
                        .WithDescription(@"**Fight Flow**
D. Mist alternates between two phases, an attack phase where she punches once a turn for three turns, and a mist phase she reacts to the Fight action with party-wide ice damage, and is immune to damage/spells.

**Strats**
Since the player can control whether or not ColdMist comes out, blink/illusion and life potion usage can often be the main way a party gets through the fight. While in the attack phase, mages should prefer casting fast spells (e.g. Virus) so that they don't fire during mist phase.  During mist phase, resurrect swooned party members, apply blink/illusion, and if you have a feel for the timing, queue up spells with a longer delay so that they land as D.Mist re-forms. Moonveils are very strong in this fight.

**Further Consideration**
Depending on the level disparity, Cover strats will have some variable utility, but can be a huge help if you have solid defensive gear (Glass Hat, Crystal Ring, Adamant Armor, etc). Spending time using cure spells is often not advised for underleveled fights, since D.Mist might just be one-shotting characters even if they had full health.")
                        .AddField("Damage Types", "Physical, Ice (ColdMist)")
                        .AddField("Resists", "Holy Absorb")
                        .AddField("Weakness","None")
                        .AddField("Boss Bit", "Yes")
                        .WithThumbnail("http://www.videogamesprites.net/FinalFantasy4/Bosses/MistDragon1.gif", 50, 50);
                    break;

                case BossName.KaipoGuards:
                    embedBuilder
                        .WithTitle("Kaipo Guards")
                        .WithUrl("https://wiki.ff4fe.com/doku.php?id=officer")
                        .WithThumbnail("https://schala-kitty.net/ff4fe-tracker/FFIVFE-Bosses-2Soldier-Color.png")
                        .WithDescription("**Fight Flow**\r\nThe officer will command the soldiers to fight, causing them to react to the command and Fight. If the officer is dead, the soldiers will Fight each other. If the soldiers are dead, the officer will Retreat (no XP from the officer)\r\n\r\n**Strats**\r\nBy default, no enemies in this fight have the boss bit, and so the fight can be easily one with either a coffin targeting the officer, or an hourglass.  \r\n\r\n**Additional Notes**\r\nWhen *Bunsafe* is turned on, all the enemies will have a boss bit. Each soldier has slightly more than 12% of the health of the officer, so if you're severely under-leveled sweeping the soldiers is the fastest way to go about the fight\r\n**Additional Link**\r\nhttps://wiki.ff4fe.com/doku.php?id=officer.0" +
                        "" +
                        "" +
                        "" +
                        "" +
                        "" +
                        "" +
                        "" +
                        "" +
                        "" +
                        "" +
                        "" +
                        "" +
                        "" +
                        "" +
                        "" +
                        "")
                        .AddField("Damage Types", "Physical")
                        .AddField("Resist", "None")
                        .AddField("Weakness", "None")
                        .AddField("Boss Bit", "No");
                    break;

                case BossName.Octomamm:
                    embedBuilder
                        .WithTitle("Octomamm")
                        .WithUrl("https://wiki.ff4fe.com/doku.php?id=octomamm")
                        .WithThumbnail("")
                        .WithDescription("")
                        .AddField("Damage Types", "Physical")
                        .AddField("Resist", "Holy absorb")
                        .AddField("Weakness", "Dark, Lightning")
                        .AddField("Boss Bit", "Yes");
                    break;

                case BossName.Antlion:
                    embedBuilder
                        .WithTitle("Antlion")
                        .WithUrl("")
                        .WithThumbnail("")
                        .WithDescription("")
                        .AddField("Damage Types", "Physical, Untyped Magic (Counter)")
                        .AddField("Resist", "None")
                        .AddField("Weakness", "None")
                        .AddField("Boss Bit", "Yes");
                    break;

                case BossName.MomBomb:
                    embedBuilder
                        .WithTitle("Kaipo Guards")
                        .WithUrl("")
                        .WithThumbnail("")
                        .WithDescription("")
                        .AddField("Damage Types", "")
                        .AddField("Resist", "")
                        .AddField("Weakness", "")
                        .AddField("Boss Bit", "");
                    break;

                case BossName.FabulGauntlet:
                    embedBuilder
                        .WithTitle("Kaipo Guards")
                        .WithUrl("")
                        .WithThumbnail("")
                        .WithDescription("")
                        .AddField("Damage Types", "")
                        .AddField("Resist", "")
                        .AddField("Weakness", "")
                        .AddField("Boss Bit", "");
                    break;

                case BossName.Milon:
                    embedBuilder
                        .WithTitle("Kaipo Guards")
                        .WithUrl("")
                        .WithThumbnail("")
                        .WithDescription("")
                        .AddField("Damage Types", "")
                        .AddField("Resist", "")
                        .AddField("Weakness", "")
                        .AddField("Boss Bit", "");
                    break;

                case BossName.MilonZ:
                    embedBuilder
                        .WithTitle("Kaipo Guards")
                        .WithUrl("")
                        .WithThumbnail("")
                        .WithDescription("")
                        .AddField("Damage Types", "")
                        .AddField("Resist", "")
                        .AddField("Weakness", "")
                        .AddField("Boss Bit", "");
                    break;

                case BossName.DarkKnightCecil:
                    embedBuilder
                        .WithTitle("Kaipo Guards")
                        .WithUrl("")
                        .WithThumbnail("")
                        .WithDescription("")
                        .AddField("Damage Types", "")
                        .AddField("Resist", "")
                        .AddField("Weakness", "")
                        .AddField("Boss Bit", "");
                    break;

                case BossName.BaronGuards:
                    embedBuilder
                        .WithTitle("Kaipo Guards")
                        .WithUrl("")
                        .WithThumbnail("")
                        .WithDescription("")
                        .AddField("Damage Types", "")
                        .AddField("Resist", "")
                        .AddField("Weakness", "")
                        .AddField("Boss Bit", "");
                    break;

                case BossName.Karate:
                    embedBuilder
                        .WithTitle("Kaipo Guards")
                        .WithUrl("")
                        .WithThumbnail("")
                        .WithDescription("")
                        .AddField("Damage Types", "")
                        .AddField("Resist", "")
                        .AddField("Weakness", "")
                        .AddField("Boss Bit", "");
                    break;

                case BossName.Baigan:
                    embedBuilder
                        .WithTitle("Kaipo Guards")
                        .WithUrl("")
                        .WithThumbnail("")
                        .WithDescription("")
                        .AddField("Damage Types", "")
                        .AddField("Resist", "")
                        .AddField("Weakness", "")
                        .AddField("Boss Bit", "");
                    break;

                case BossName.Kainazzo:
                    embedBuilder
                        .WithTitle("Kaipo Guards")
                        .WithUrl("")
                        .WithThumbnail("")
                        .WithDescription("")
                        .AddField("Damage Types", "")
                        .AddField("Resist", "")
                        .AddField("Weakness", "")
                        .AddField("Boss Bit", "");
                    break;

                case BossName.DarkElf:
                    embedBuilder
                        .WithTitle("Kaipo Guards")
                        .WithUrl("")
                        .WithThumbnail("")
                        .WithDescription("")
                        .AddField("Damage Types", "")
                        .AddField("Resist", "")
                        .AddField("Weakness", "")
                        .AddField("Boss Bit", "");
                    break;

                case BossName.MagusSisters:
                    embedBuilder
                        .WithTitle("Kaipo Guards")
                        .WithUrl("")
                        .WithThumbnail("")
                        .WithDescription("")
                        .AddField("Damage Types", "")
                        .AddField("Resist", "")
                        .AddField("Weakness", "")
                        .AddField("Boss Bit", "");
                    break;

                case BossName.Valvalis:
                    embedBuilder
                        .WithTitle("Kaipo Guards")
                        .WithUrl("")
                        .WithThumbnail("")
                        .WithDescription("")
                        .AddField("Damage Types", "")
                        .AddField("Resist", "")
                        .AddField("Weakness", "")
                        .AddField("Boss Bit", "");
                    break;

                case BossName.Calbrena:
                    embedBuilder
                        .WithTitle("Kaipo Guards")
                        .WithUrl("")
                        .WithThumbnail("")
                        .WithDescription("")
                        .AddField("Damage Types", "")
                        .AddField("Resist", "")
                        .AddField("Weakness", "")
                        .AddField("Boss Bit", "");
                    break;

                case BossName.Golbez:
                    embedBuilder
                        .WithTitle("Kaipo Guards")
                        .WithUrl("")
                        .WithThumbnail("")
                        .WithDescription("")
                        .AddField("Damage Types", "")
                        .AddField("Resist", "")
                        .AddField("Weakness", "")
                        .AddField("Boss Bit", "");
                    break;

                case BossName.DrLugae:
                    embedBuilder
                        .WithTitle("Kaipo Guards")
                        .WithUrl("")
                        .WithThumbnail("")
                        .WithDescription("")
                        .AddField("Damage Types", "")
                        .AddField("Resist", "")
                        .AddField("Weakness", "")
                        .AddField("Boss Bit", "");
                    break;

                case BossName.DarkImps:
                    embedBuilder
                        .WithTitle("Kaipo Guards")
                        .WithUrl("")
                        .WithThumbnail("")
                        .WithDescription("")
                        .AddField("Damage Types", "")
                        .AddField("Resist", "")
                        .AddField("Weakness", "")
                        .AddField("Boss Bit", "");
                    break;

                case BossName.KingQueenEblan:
                    embedBuilder
                        .WithTitle("Kaipo Guards")
                        .WithUrl("")
                        .WithThumbnail("")
                        .WithDescription("")
                        .AddField("Damage Types", "")
                        .AddField("Resist", "")
                        .AddField("Weakness", "")
                        .AddField("Boss Bit", "");
                    break;

                case BossName.Rubicant:
                    embedBuilder
                        .WithTitle("Kaipo Guards")
                        .WithUrl("")
                        .WithThumbnail("")
                        .WithDescription("")
                        .AddField("Damage Types", "")
                        .AddField("Resist", "")
                        .AddField("Weakness", "")
                        .AddField("Boss Bit", "");
                    break;

                case BossName.EvilWall:
                    embedBuilder
                        .WithTitle("Kaipo Guards")
                        .WithUrl("")
                        .WithThumbnail("")
                        .WithDescription("")
                        .AddField("Damage Types", "")
                        .AddField("Resist", "")
                        .AddField("Weakness", "")
                        .AddField("Boss Bit", "");
                    break;

                case BossName.Elements:
                    embedBuilder
                        .WithTitle("Kaipo Guards")
                        .WithUrl("")
                        .WithThumbnail("")
                        .WithDescription("")
                        .AddField("Damage Types", "")
                        .AddField("Resist", "")
                        .AddField("Weakness", "")
                        .AddField("Boss Bit", "");
                    break;

                case BossName.CPU:
                    embedBuilder
                        .WithTitle("Kaipo Guards")
                        .WithUrl("")
                        .WithThumbnail("")
                        .WithDescription("")
                        .AddField("Damage Types", "")
                        .AddField("Resist", "")
                        .AddField("Weakness", "")
                        .AddField("Boss Bit", "");
                    break;

                case BossName.Odin:
                    embedBuilder
                        .WithTitle("Kaipo Guards")
                        .WithUrl("")
                        .WithThumbnail("")
                        .WithDescription("")
                        .AddField("Damage Types", "")
                        .AddField("Resist", "")
                        .AddField("Weakness", "")
                        .AddField("Boss Bit", "");
                    break;

                case BossName.Asura:
                    embedBuilder
                        .WithTitle("Kaipo Guards")
                        .WithUrl("")
                        .WithThumbnail("")
                        .WithDescription("")
                        .AddField("Damage Types", "")
                        .AddField("Resist", "")
                        .AddField("Weakness", "")
                        .AddField("Boss Bit", "");
                    break;

                case BossName.Leviatan:
                    embedBuilder
                        .WithTitle("Kaipo Guards")
                        .WithUrl("")
                        .WithThumbnail("")
                        .WithDescription("")
                        .AddField("Damage Types", "")
                        .AddField("Resist", "")
                        .AddField("Weakness", "")
                        .AddField("Boss Bit", "");
                    break;

                case BossName.Bahamut:
                    embedBuilder
                        .WithTitle("Kaipo Guards")
                        .WithUrl("")
                        .WithThumbnail("")
                        .WithDescription("")
                        .AddField("Damage Types", "")
                        .AddField("Resist", "")
                        .AddField("Weakness", "")
                        .AddField("Boss Bit", "");
                    break;

                case BossName.PaleDim:
                    embedBuilder
                        .WithTitle("Kaipo Guards")
                        .WithUrl("")
                        .WithThumbnail("")
                        .WithDescription("")
                        .AddField("Damage Types", "")
                        .AddField("Resist", "")
                        .AddField("Weakness", "")
                        .AddField("Boss Bit", "");
                    break;

                case BossName.Wyvern:
                    embedBuilder
                        .WithTitle("Kaipo Guards")
                        .WithUrl("")
                        .WithThumbnail("")
                        .WithDescription("")
                        .AddField("Damage Types", "")
                        .AddField("Resist", "")
                        .AddField("Weakness", "")
                        .AddField("Boss Bit", "");
                    break;

                case BossName.Plague:
                    embedBuilder
                        .WithTitle("Kaipo Guards")
                        .WithUrl("")
                        .WithThumbnail("")
                        .WithDescription("")
                        .AddField("Damage Types", "")
                        .AddField("Resist", "")
                        .AddField("Weakness", "")
                        .AddField("Boss Bit", "");
                    break;

                case BossName.DLunars:
                    embedBuilder
                        .WithTitle("Kaipo Guards")
                        .WithUrl("")
                        .WithThumbnail("")
                        .WithDescription("")
                        .AddField("Damage Types", "")
                        .AddField("Resist", "")
                        .AddField("Weakness", "")
                        .AddField("Boss Bit", "");
                    break;

                case BossName.Ogopogo:
                    embedBuilder
                        .WithTitle("Kaipo Guards")
                        .WithUrl("")
                        .WithThumbnail("")
                        .WithDescription("")
                        .AddField("Damage Types", "")
                        .AddField("Resist", "")
                        .AddField("Weakness", "")
                        .AddField("Boss Bit", "");
                    break;

                case BossName.Zeromus:
                    embedBuilder
                        .WithTitle("Kaipo Guards")
                        .WithUrl("")
                        .WithThumbnail("")
                        .WithDescription("")
                        .AddField("Damage Types", "")
                        .AddField("Resist", "")
                        .AddField("Weakness", "")
                        .AddField("Boss Bit", "");
                    break;



                case BossName.Unknown:
                default:
                    break;

            }

            return embedBuilder.Build();
        }
    }
}
