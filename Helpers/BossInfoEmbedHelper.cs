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
                        .WithDescription(
@"### Fight Flow
D.Mist alternates between two phases: Attack and Mist. In the Attack phase, D.Mist punches once a turn for three turns. In Mist phase D.Mist is immune to everything and reacts to the Fight action with party-wide ice damage.
### Strats
While in the attack phase, mages should prefer casting fast spells (e.g. Virus) so that they don't land during mist phase. During mist phase, resurrect swooned party members, apply Blink/Illusion, and if you have a feel for the timing, queue up spells with a longer delay so that they land as D.Mist re-forms. MoonVeils are very strong in this fight. Since the player can control whether or not ColdMist comes out, Blink/Illusion and Life potion usage can often be the main way a party gets through the fight if they’re having trouble dealing with the punch damage.

Be careful about deciding when to berserk characters. Miscalculating on if you’ll kill in the cycle can be really costly in both time and damage, as each berserked swing will trigger ColdMist.
### Additional Notes
Depending on the level disparity, Cover strats will have some variable utility, but can be a huge help if you have solid defensive gear (Glass Hat, Crystal Ring, Adamant Armor, etc). Spending time using cure spells is often not advised for underleveled fights, since D.Mist might just be one-shotting characters even if they had full health.

")
                        .AddField("Damage Types", "Physical, Ice (ColdMist)", inline: true)
                        .AddField("Resists", "Holy Absorb", inline: true)
                        .AddField("Boss Bit", "Yes", inline: true)
                        .WithThumbnail("https://ff4-fe-info.s3.us-west-2.amazonaws.com/schalaSprites/dmist.png");
                    break;

                case BossName.KaipoGuards:
                    embedBuilder
                        .WithTitle("Kaipo Guards")
                        .WithUrl("https://wiki.ff4fe.com/doku.php?id=officer")
                        .WithThumbnail("https://ff4-fe-info.s3.us-west-2.amazonaws.com/schalaSprites/Soldier.png")
                        .WithDescription(
@"### Fight Flow
The Officer will command the Soldiers, causing them to react to the command and Fight. If the Officer is dead, the Soldiers will Fight each other. If the Soldiers are dead, the Officer will Retreat (no XP from the officer).
### Strats
By default, no enemies in this fight have the boss bit, and so the fight can be easily won with either a Coffin targeting the Officer, an HrGlass, or quick AoE to clear the Soldiers.
### Additional Notes
Each soldier has roughly 9% of the location's total HP. When `Bnofree` is turned on, all the enemies will have a boss bit.

")
                        .AddField("Damage Types", "Physical", inline: true)
                        .AddField("Boss Bit", "No", inline: true)
                        .AddField("Additional Links", @"[Soldier](<https://wiki.ff4fe.com/doku.php?id=soldier>)", inline: true);

                    break;

                case BossName.Octomamm:
                    embedBuilder
                        .WithTitle("Octomamm")
                        .WithUrl("https://wiki.ff4fe.com/doku.php?id=octomamm")
                        .WithThumbnail("https://ff4-fe-info.s3.us-west-2.amazonaws.com/schalaSprites/Octo.png")
                        .WithDescription(
@"### Fight Flow
Octomamm only uses physical attacks. After the third time you deal direct damage to Octo a tentacle is removed. After the first tentacle loss, every other time you deal direct damage another tentacle is lost. Each tentacle loss slows Octomamm's speed.
### Strats
Overall a pretty basic fight. Berserk status and fast spells are all very helpful in slowing the squid. Once you’ve gotten a few tentacles removed, mages with Lit3 and no nuke probably really like switching from Virus to Lit3 when the fight is at a high HP location.
### Additional Notes
A MoonVeil used against Octomamm means the fight is entirely free, and Cover strats can really help out, especially for underleveled parties. If the fight's in a challenging spot, a SilkWeb or a double slow helps out in this fight, since the slow effect from those gets amplified by the tentacle loss. If you're having trouble getting started slowing down the battle speed a notch or two can help get in extra early attacks, which'll have some cascading benefits. Blink and Illusion can be huge in helping set up/stabilize in underleveled fights.

Yang really loves equipping a Thunder Claw here, and the more unequal Edge’s equips are, the more he wants one, too.

")
                        .AddField("Damage Types", "Physical", inline: true)
                        .AddField("Resist", "Holy absorb", inline: true)
                        .AddField("Weakness", "Dark, Lightning", inline: true)
                        .AddField("Boss Bit", "Yes", inline: true);
                    break;

                case BossName.Antlion:
                    embedBuilder
                        .WithTitle("Antlion")
                        .WithUrl("https://wiki.ff4fe.com/doku.php?id=antlion")
                        .WithThumbnail("https://ff4-fe-info.s3.us-west-2.amazonaws.com/schalaSprites/Antlion.png")
                        .WithDescription(
@"### Fight Flow
Antlion just Fights on their own turn, and will react with Counter on any character who Fights.
### Strats
Blink/Illusion/MoonVeil are the standard defensive package here, with a smattering of healing items or spells. Since Antlion will counter that command, which includes berserked characters, using other commands (e.g. Jump, Power, Aim) or magic to deal damage is generally preferred.
### Additional Notes
The Counter reaction bases its damage from the Physical stat at a location, and is stopped by neither Reflect nor Barrier status, so MoonVeils, Blink, Illusion, and Cover are useless against it. All those defensive options are still fantastic for helping handle the Fight damage.

")
                        .AddField("Damage Types", "Physical", inline: true)
                        .AddField("Boss Bit", "Yes", inline: true);
                    break;

                case BossName.Waterhag:
                    embedBuilder
                        .WithTitle("WaterHag")
                        .WithUrl("https://wiki.ff4fe.com/doku.php?id=waterhag_boss")
                        .WithThumbnail("https://ff4-fe-info.s3.us-west-2.amazonaws.com/schalaSprites/waterhag.png")
                        .WithDescription(@"
### Fight Flow
WaterHag only does normal Fight commands, and unless `Bnofree` is turned on, will die by any three instances of direct damage from the party.
### Strats
Opt for fast attacks/spells. Zerkers are great, although prefer Avenger or Bacchus over casting the spell where possible. This is a great fight to use excess j-items that don't pack much punch anymore.
### Additional Notes
As with any fight that only does physical/Fight damage, MoonVeil will fully solve this encounter, and Blink, Illusion, and Cover help tremendously if you're having trouble staying alive long enough to get the three hits in. Alternatively, RA1 parties at slow battle speeds can get all their actions in before WaterHag takes a turn, which can help you take out WaterHag at high damage spots when you're at low level.

When `Bnofree` is on you can save some time by killing WaterHag with big damage at early spots, since you’ll be skipping some/most of the interactions with Anna. Alternatively, you can get a lot of menu time to rearrange your inventory while Anna is talking to you.

")
                        .AddField("Damage Types", "Physical", inline: true)
                        .AddField("Boss Bit", "Yes", inline: true);
                    break;

                case BossName.MomBomb:
                    embedBuilder
                        .WithTitle("MomBomb")
                        .WithUrl("https://wiki.ff4fe.com/doku.php?id=mombomb")
                        .WithThumbnail("https://ff4-fe-info.s3.us-west-2.amazonaws.com/schalaSprites/Mombomb.png")
                        .WithDescription(@"
### Fight Flow
MomBomb starts the fight just doing physical damage. When below an HP threshold (see Additional Notes for details), MomBomb begins a sequence where the next turns are an empty turn with a battle message, and then using Explode, dealing damage to the party and being replaced with 3 Bombs and 3 Grey Bombs. After they each attack twice, the baby bombs will explode for their current HP to a targeted party member.
### Strats
Deal damage quickly, and when MomBomb starts the explode sequence, either stop dealing damage and set up for killing the baby bombs, or try to push through the extra HP and skip the explode/baby bomb phase of the fight entirely. For lower level strats, you’ll focus on dealing damage and surviving MomBomb’s attacks, then heal up and prep your handling of the baby bombs, which is often an HrGlass or a source of AoE damage.
### Additional Notes
MomBomb has 64% the spot's HP total, plus an additional 10,000 HP (up to a max of 65,000 HP). 
Bombs have 4% the spot's HP total.
Grey Bombs have 8% the spot's HP total, and get no magic defenses.
None of the baby bombs have the boss bit, unless `Bnofree` is turned on.

")
                        .AddField("Damage Types", "Physical, Fire (explode, MomBomb), Untyped Magic (explode, baby bombs)", inline: true)
                        .AddField("Resist", "MomBomb: None, Baby bombs: Poison, Pig, Mini, Frog", inline: true)
                        .AddField("Weakness", "MomBomb: Dark, Baby bombs: none", inline: true)
                        .AddField("Boss Bit", "Yes (MomBomb), No (baby bombs)", inline: true)
                        .AddField("Additional Links", @"[GrayBomb](<https://wiki.ff4fe.com/doku.php?id=graybomb>), [Bomb](<https://wiki.ff4fe.com/doku.php?id=bomb>)", inline: true);
                    break;

                case BossName.FabulGauntlet:
                    embedBuilder
                        .WithTitle("Fabul Gauntlet")
                        .WithUrl("https://wiki.ff4fe.com/doku.php?id=general")
                        .WithThumbnail("https://ff4-fe-info.s3.us-west-2.amazonaws.com/schalaSprites/Gauntlet.png")
                        .WithDescription(
@"### Fight Flow
The vanilla Fabul Gauntlet consists of six consecutive fights. The first, third, and sixth fights are against a General/2x Fighter composition, the second and fifth have a Weeper, WaterHag, and Imp Captain, and the fourth fight is against a solo Gargoyle.
### Strats
A vanilla Gauntlet generally poses little threat, since the total HP of the location is spread out among all six fights. Likewise, it is not rewarding to use the Life glitch for extra experience as XP is similarly split. AoE damage is fantastic in this fight, and the Tier 2 j-items carry pretty far into the game for this boss. Very high Physical damage spots can pose some trouble to low-level parties, but an HrGlass can bring you time to recover. Cover is also solid.
### Additional Notes
The `alt:gauntlet` flag replaces the fights outlined above with five fights drawn from the area around the boss location, or one of applicable power level (e.g. the Baron Basement boss draws from the Land of Summoned Monsters, like both Feymarch boss fights).

Moon locations are great XP for the Alt Gauntlet, but also can be large time sinks or highly challenging. The Super Cannon gauntlet is very rude compared to normal bosses there, and for all fights in Lower Babil and the Tower of Zot, you'll want to bring some source of magic damage to handle pudding type enemies.

See [Alt Gauntlets](<https://wiki.ff4fe.com/doku.php?id=alt_gauntlet>) for specifics on each location.

")
                        .AddField("Additional Links",
@"[Fighter](<https://wiki.ff4fe.com/doku.php?id=Fighter>)
[Weeper](<https://wiki.ff4fe.com/doku.php?id=Weeper>), [WaterHag](<https://wiki.ff4fe.com/doku.php?id=WaterHag>), [Imp Cap.](<https://wiki.ff4fe.com/doku.php?id=Imp_Cap>)
[Gargoyle](<https://wiki.ff4fe.com/doku.php?id=gargoyle>)
[Alt Gauntlets](<https://wiki.ff4fe.com/doku.php?id=alt_gauntlet>)")
                        .AddField("Damage Types", "Physical", inline: true)
                        .AddField("Boss Bit", "No", inline: true)
                        .AddField("Race", "Gargoyle: Reptile, Weeper: Spirit", inline: true)
                        .AddField("Resist", "Gargoyle: Pig, Mini, KO", inline: true)
                        .AddField("Weakness", "Gargoyle: Holy, Air (4x)", inline: true);
                    break;

                case BossName.Milon:
                    embedBuilder
                        .WithTitle("Milon")
                        .WithUrl("https://wiki.ff4fe.com/doku.php?id=milon")
                        .WithThumbnail("https://ff4-fe-info.s3.us-west-2.amazonaws.com/schalaSprites/Milon.png")
                        .WithDescription(
@"### Fight Flow
While both Milon and any Ghasts are on the field, Milon will command them to use Drain. If Milon is alone, he’ll alternate between casting single target Lit1 and using Fight. When only Ghasts are left, they’ll use Fight. Milon always reacts to damage with a Lit1 counter
### Strats
Often you’ll approach the fight by using AoE to clear the Ghasts, and relying on one/two damage dealers to kill Milon. Only having a couple of people attacking Milon results in fewer Lit1 counters, which will generally speed things up. At high physical damage spots, Blink/Illusion takes the threat out of Milon fairly well, as would Cover.
### Additional Notes
At high physical damage spots, tossing an HrGlass to stop the Ghasts, and then focus firing down Milon can result in a very clean fight. If you do this, be very aware of how hard the Ghasts will punch if the stop effect wears off.

")
                        .AddField("Damage Types", "Milon:Physical, Bolt. Ghasts: Physical, Magic", inline: true)
                        .AddField("Resist", "Ghasts: Dark Immune, Poison, Blind, Mute, Pig, Mini, Frog, KO, Calcify 1, Calcify 2, Berserk, Charm, Sleep, Stun, Float, Curse", inline: true)
                        .AddField("Weakness", "Ghast: Fire, Holy", inline: true)
                        .AddField("Boss Bit", "Yes (Milon), No (Ghasts)", inline: true)
                        .AddField("Additional Links", "[Ghast](<https://wiki.ff4fe.com/doku.php?id=Ghast>)", inline: true);
                    break;

                case BossName.MilonZ:
                    embedBuilder
                        .WithTitle("Milon Z.")
                        .WithUrl("https://wiki.ff4fe.com/doku.php?id=milon_z")
                        .WithThumbnail("https://ff4-fe-info.s3.us-west-2.amazonaws.com/schalaSprites/MilonZ.png")
                        .WithDescription(
@"### Fight Flow
Milon Z’s only damage comes from Fight, which can proc the Curse status effect.
### Strats
As standard for physical fights, Blink/Illusion/MoonVeil are all great defense options. Milon Z’s Zombie race means that Cure magic/potions cause harm. At low HP spots, Cure2 pots possibly do better damage than your party's normal attacks, and a Cure3 or two can really speed up the fight. Don't toss Elixirs.
### Additional Notes
Wizard hats, Sorcerer and White robes provide defense against Zombies, as does any part of Cecil’s Paladin or Crystal defensive gear. Generally equipping any of those on a Cecil in the back row lets Cover strats carry the day. Also, the Crystal Sword does extra damage against Zombies, as do White arrows.

")
                        .AddField("Damage Types", "Physical", inline: true)
                        .AddField("Resist", "Ice Absorb", inline: true)
                        .AddField("Weakness", "Air, Holy, Fire", inline: true)
                        .AddField("Race", "Zombie", inline: true)
                        .AddField("Boss Bit", "Yes", inline: true);
                    break;

                case BossName.DarkKnightCecil:
                    embedBuilder
                        .WithTitle("DKC")
                        .WithUrl("https://wiki.ff4fe.com/doku.php?id=d.knight")
                        .WithThumbnail("https://ff4-fe-info.s3.us-west-2.amazonaws.com/schalaSprites/DKCecil.png")
                        .WithDescription(
@"### Fight Flow
Dark Knight Cecil will use Dark Wave on the whole party three times, give a short speech, and then disappear. If you use Fight (Avenger/Berserk counts), the “three times” count resets. 
### Strats
Sit back and accept a little damage. In places where the damage outpaces your HP, fast healing goes a long way. Cure3/Elixir can be fight-savers, and slowing the battle speed can let you get more healing in between Dark Waves. Also consider who's going to survive a wave to target with healing so you can assure victory or maybe get another character XP. Properly anchored, Edward can hide before the first Dark Wave comes out and can earn you a free win. Finally, Jump can help evade a round of damage, since characters in the air won't be hit by Dark Wave, and the command doesn't count as Fight.
### Additional Notes
When `Bnofree` is on, you have to kill DKC, and DKC cannot gate intended underground access unless `Bunsafe` is also on.

")
                        .AddField("Damage Types", "Untyped, based on Physical stat", inline: true)
                        .AddField("Boss Bit", "Yes", inline: true);
                    break;

                case BossName.BaronGuards:
                    embedBuilder
                        .WithTitle("Baron Guards")
                        .WithUrl("https://wiki.ff4fe.com/doku.php?id=guard")
                        .WithThumbnail("https://ff4-fe-info.s3.us-west-2.amazonaws.com/schalaSprites/Guards.png")
                        .WithDescription(
@"### Fight Flow
The Guards only Fight on their turns. They’ll react to magic cast on them with a single target Piggy, and react to Fight with a single target Size.
### Strats
Since the guards don’t normally have the boss bit, an HrGlass, Coffin, or successful use of the Stone spell (or multiple casts of Stop) fully control the fight. Mute, or using a mutebell, removes their ability to cast. Rune Axe, Mute Knife, Mute Arrows, and Elven Bows do extra damage, and properly timed, an Assassin dagger is just as effective as a Coffin. Should Rydia have learned Cockatrice or Mage, both of them can be very useful in the fight as well. Size and Toad also negate this fight’s threat.
### Additional Notes
Equipping a Rune Ring, Ribbon, or an Aegis Shield will give a character mage defense, giving great protection from a Guard’s attack. Ribbons also block both status effect counters from the Guards, Zeus Gauntlets and Crystal Armor protect against Size, and a Ruby Ring protects against Piggy

At high XP spots, Life glitches, or even a Life2 grind, can be very welcome infusions of experience. 

With `Bnofree` on, this fight becomes much more dangerous. A focus on minimizing incoming damage and handling the Size/Piggy counters will help greatly. Equip anti-Mage defensive equipment to reduce incoming damage, and give any status protection to the most important character(s) in the party for the fight. Getting Wall status on characters is also great as a form of status prevention, just make sure you have curative potions to help handle damage. If your party relies on physical damage to clear fights, equip anti-Mage weapons (Rune Axe, Mute Knife, Mute Arrows, Elven Bow) and try to only have one or two very strong attackers dealing damage. Parties that can bring Magic damage should bounce damage spells off of a Wall.

")
                        .AddField("Damage Types", "Physical", inline: true)
                        .AddField("Race", "Mage", inline: true)
                        .AddField("Boss Bit", "No", inline: true);
                    break;

                case BossName.Karate:
                    embedBuilder
                        .WithTitle("Karate")
                        .WithUrl("https://wiki.ff4fe.com/doku.php?id=karate")
                        .WithThumbnail("https://ff4-fe-info.s3.us-west-2.amazonaws.com/schalaSprites/Yang.png")
                        .WithDescription(
@"### Fight Flow
Karate alternates between using Fight and Kick. After Karate queues the second kick, any Paladin Cecil using Fight will cause Karate to say “Ouch” and perish.
### Strats
Generally normal strategies for dealing damage and handling incoming physical attacks apply. Cover strats don’t often work to keep the whole party alive, since Kick will deal AoE damage that ignores Barrier/Blink status and can do enough to kill party members that have low HP.
### Additional Notes
`Bnofree` removes the Ouch reaction, so you’ll have to fight the fight straight up.

")
                        .AddField("Damage Types", "Physical", inline: true)
                        .AddField("Boss Bit", "Yes", inline: true);
                    break;

                case BossName.Baigan:
                    embedBuilder
                        .WithTitle("Baigan")
                        .WithUrl("https://wiki.ff4fe.com/doku.php?id=Baigan")
                        .WithThumbnail("https://ff4-fe-info.s3.us-west-2.amazonaws.com/schalaSprites/Baigan.png")
                        .WithDescription(
@"### Fight Flow
The left arm (the one closest to the party) will cycle a sequence of Fight, Entangle, Fight for its actions. The right arm will alternate between Fight and Vampire. If Baigan dies before the arms do, they’ll use Explode the next time they get to queue an action. Baigan's script has a cycle of five consecutive Fight turns, then casts Fast on any living arms, and then does another Fight while any arms are alive. When both arms are dead, Baigan will use Recover. In reaction to a magic cast, Baigan casts a self-targeted Wall.
### Strats
Sweeping spells that also pierce Wall (e.g. Quake, Summons) are a great action, preventing the arms or Baigan from dealing damage to the party; just make sure to only queue the cast when you can see that the arms are being targeted as well. Most of the damage from the enemies is from Fight, so Blink/Illusion/MoonVeil are all very useful as ways to prevent damage. Berserk, which is so often a main strategy, is a little less powerful in this fight due to the random targeting. It’s still useful, especially as a way to keep damage flowing and helping other party members get an effective speed boost.

As the fight goes on and Baigan reflects a wall onto your party, you can use that to bounce spells back. Timed well, you can get spells to mainly hit Baigan and not the arms. The walls do make it more difficult to keep Blink/Illusion up on characters, or do mass heals. This is not a bad fight to dip into the Cure3 potion supply.
### Additional Notes
Making good use of Cover in this fight can be a little difficult, since Cecil can’t cover the Vampire from the Right Arm. Keeping that enemy dead is a high priority if Cover is your best defensive plan, so you might consider single/manually targeting spells and attacks. This kind of strategy is more useful at high physical attack locations where you don’t have the AoE to clear the arms, and have good physical attackers that can reach to back row enemies without penalty (Jump, Aim, Dart, characters equipped with long range weapons, or characters that have the back row glitch applied).

")
                        .AddField("Additional Links", "[Left Arm](<https://wiki.ff4fe.com/doku.php?id=left_arm>), [Right Arm](https://wiki.ff4fe.com/doku.php?id=rightarm)", inline: true)
                        .AddField("Damage Types", "Physical, Magic (Right Arm - Vampire)", inline: true)
                        .AddField("Boss Bit", "Yes", inline: true);
                    break;

                case BossName.Kainazzo:
                    embedBuilder
                        .WithTitle("Kainazzo")
                        .WithUrl("https://wiki.ff4fe.com/doku.php?id=Kainazzo")
                        .WithThumbnail("https://ff4-fe-info.s3.us-west-2.amazonaws.com/schalaSprites/Kainazzo.png")
                        .WithDescription(
@"### Fight Flow
Kainazzo begins the fight with a punch, then enters a cycle of gathering water, using Wave, and then self-targeting a Fast cast. Using lightning based magic will cause the water to dispel, and set Kainazzo up to queue a punch on the next opportunity to queue an action. When damaged at low HP (17.5% of max or less), Kainazzo will retreat into his shell, dispelling any gathered water. After hiding, Kainazzo’ll use Remedy to heal, take a turn to bot set defenses and prepare to gather water.
### Strats
This is a fight where location significantly dictates tactics. At spots where your party can’t ever reasonably survive the first Wave (often the 2nd boss on the hook route, or the first spot in the Giant), your first turns should be working towards both setting up long term goals (getting a berserker online, getting Blink up, tossing a silkweb), and making sure that the water is dispelled before the Wave usage gets queued.
### Additional Notes
At high HP spots, or if your damage is low for the location, you can find yourself in a loop where the Remedy action outpaces your ability to do damage. If that’s happening while you only have zerked characters, you should probably reset and either try again to have more people up and fighting at the end, or come back after getting a few more levels.

The timing to dissipate the water before a Wave can be queued can be tricky, so slower battle speeds might be necessary. Given an RA1 setup, you can also time queuing up an item toss with the punch so that the item gets used just after the water is gathered.

")
                        .AddField("Damage Types", "Physical, Untyped Magic (Wave)", inline: true)
                        .AddField("Weakness", "Ice (water down) \r\nBolt (gathered water)", inline: true)
                        .AddField("Boss Bit", "Yes", inline: true);
                    break;

                case BossName.DarkElf:
                    embedBuilder
                        .WithTitle("Dark Elf")
                        .WithUrl("https://wiki.ff4fe.com/doku.php?id=dark_elf_post-harp")
                        .WithThumbnail("https://ff4-fe-info.s3.us-west-2.amazonaws.com/schalaSprites/DElf.png")
                        .WithDescription(
@"### Fight Flow
The fight begins with Dark Elf chaining together party-wide Fire2/Ice2/Lit2 attacks, then using Weak and Piggy on consecutive turns, before starting back at the top. After taking damage below a certain threshold, Dark Elf transform to the Dark Dragon form, where the script alternates between using Fight and DBreath.
### Strats
Get some damage going (preferably with someone doing Holy damage), try to make sure any necessary mages get healed of Piggy if they’re hit by it, and restore hit points to people who get hit by Weak. Once you transition to the dragon phase, Stop, Weak, or an HrGlass really solves the fight from there. If you don’t have access to that, just continue restoring HP, and get anyone with dragon-killing gear going with some Bersk. Casting Holy elemental spells at the Dragon form is a bad idea, since the transformation includes an addition of Holy absorb (physical attacks are still fine). 
### Additional Notes
The dragon form lacks the boss bit, and while it does have some status resistances, it won't resist Stop, an HrGlass, or the weak spell. The fight can be really sped up with a properly timed Weak cast.

")
                        .AddField("Damage Types", "Dark Elf: Fire, Ice, Lit\r\nDragon form: Physical, Fire (D.Breath)", inline: true)
                        .AddField("Resist", "Dragon form: Holy Absorb Blind Mute Pig Mini Frog Petrify KO Calcify 1 Calcify 2 Berserk Charm Sleep Stun Float Curse", inline: true)
                        .AddField("Weakness", "Holy", inline: true)
                        .AddField("Boss Bit", "Dark Elf: Yes, Dragon form: No", inline: true)
                        .AddField("Race", "Dragon form: Dragon", inline: true)
                        .AddField("Additional Links", "[Dragon form](<https://wiki.ff4fe.com/doku.php?id=dark_elf_dragon_form>)", inline: true);
                    break;

                case BossName.MagusSisters:
                    embedBuilder
                        .WithTitle("Magus Sisters")
                        .WithUrl("https://wiki.ff4fe.com/doku.php?id=Cindy")
                        .WithThumbnail("https://ff4-fe-info.s3.us-west-2.amazonaws.com/schalaSprites/MagusSis.png")
                        .WithDescription(
@"### Fight Flow
While Cindy is alive, Sandy will cast Wall on her and Mindy will target Cindy with a spell (cycling through Fire2, Lit2, Ice2, and Virus) intending to bounce it off of the wall Sandy provides. Cindy will punch while both her sisters are alive, if any combination of her sisters are dead, will punch once more then use Remedy to revive her dead sisters.
### Strats
Quake, Meteo, and most of Rydia’s summons do pretty well here, since they all bypass the wall that gets put on Cindy. Physical fighters generally prefer to have some sort of mage-killing weapon equipped, and be back-row glitched. Different parties might have different priorities, but if the fight is dangerous for your party, killing Cindy or Mindy first can help ease the fight along. Removing Cindy removes the possibility of Mindy/Sandy being revived, and removing Mindy reduces the magic damage coming at the party (while making Cindy no longer in a back row, so the physical damage portion of the fight does increase for a bit)

Parties with a Nuke caster can, with proper anchoring and party order, can sometimes kill Sandy first, preventing a wall from going on Cindy, which can help make the start of the fight more manageable.
### Additional Notes
While Cindy has more HP than her sisters in the fight, each sister awards one-third of the spot’s XP, which can make the sisters a good grind. And since they have a mage weakness, there can be a leeway where it’s a reasonable mini-grind.

Lower level parties might want to avoid using berserk, since it can spread out damage too much, and leave you taking both punches from Cindy, and reflected spells from Mindy. The Dragoon armor set provides good elemental protection for those that can wear it, as does the Crystal Helm and Protect rings. Also don’t discount the Lit resistance from Diamond Rings or Sorcerer robes. When you’re under leveled for the fight, making any of the spells be less dangerous is really helpful. Having a Headband for Charm resistance can also save you when Cindy’s gone and Sandy starts slinging spells at the party.
")
                        .AddField("Damage Types", "Cindy: Physical\r\nMindy: Lit, Ice, Fire, Untyped Magic", inline: true)
                        .AddField("Race", "Mage", inline: true)
                        .AddField("Boss Bit", "Yes", inline: true)
                        .AddField("Additional Links", "[Sandy](<https://wiki.ff4fe.com/doku.php?id=Sandy>), [Mindy](<https://wiki.ff4fe.com/doku.php?id=Mindy>)", inline: true);
                    break;

                case BossName.Valvalis:
                    embedBuilder
                        .WithTitle("Valvalis")
                        .WithUrl("https://wiki.ff4fe.com/doku.php?id=Valvalis")
                        .WithThumbnail("https://ff4-fe-info.s3.us-west-2.amazonaws.com/schalaSprites/Valvalis.png")
                        .WithDescription(
@"### Fight Flow
At the start of the fight, Val will assume tornado form, changing defensive values (See Tornado Defense for details, as different locations have vastly different stats). While in tornado form, Val will alternate between chaining single targets of Weak and Ray, and using Fight. If a character uses Jump (hit or miss), Val fall out of tornado form, reset defenses and punch as a reaction, and then take a turn to use Fight again, take an empty turn and set up getting back to tornado form.
### Strats
The location really dictates some of your choices in the fight, and the fight in general is one that should guide party choices if you’re mage heavy. Generally, the fight relies on physical attacks hitting through the defenses Val spins up. Blink/Illusion are most of the defense you need in the fight, along with some ways of clearing the calcify/stone status.

In a mage heavy composition, using Jump and timing the magical onslaught will likely be the fastest/easiest way through, although swapping one character over to a bow/arrow and stacking +Str gear can be an alternative path if the Jump command's not available.
### Additional Notes
It can be a good idea to carry some extra Heal potions for this fight when you have a limited party size, to help prevent your characters from fully becoming stone. You can also kill a character before they fully turn to stone and resurrect them to clear any partial calcification.

Aim, Kick, Dark, and Dart all are able to hit Valvalis in tornado form, so you can lean on them if you don’t have the multipliers to get through the defenses and also don’t have Kain to knock Valvalis out of tornado form.

Valvalis will not gate intended underground access unless `Bunsafe` is on.

")
                        .AddField("Damage Types", "Physical", inline: true)
                        .AddField("Additional Links", "[Tornado Defense](<https://docs.google.com/spreadsheets/d/1tVQFvlQ_4oWCn0EE9d7QAGrYW3w2IbZzuO2MWuUC8ww>)", inline: true)
                        .AddField("Boss Bit", "Yes", inline: true);
                    break;

                case BossName.Calbrena:
                    embedBuilder
                        .WithTitle("Calbrena")
                        .WithUrl("https://wiki.ff4fe.com/doku.php?id=calbrena")
                        .WithThumbnail("https://ff4-fe-info.s3.us-west-2.amazonaws.com/schalaSprites/Calcabrina.png")
                        .WithDescription(
@"### Fight Flow
The small dolls will all attack, unless they’re down to one type (either Cal or Brena) left, when they’ll summon Calbrena on their next action. Calbrena will Fight, use Glance, Fight on two consecutive turns, use Hold, and Fight again before restarting at the top. The Big Doll also will counter elemental Fire, Ice, or Lit magic with Fire, Wave, and Thunder, respectively. At very low HP, if Calbrena takes damage, the big doll will revert back to the 3 Cal/3 Brena formation. 
### Strats
Quake is fantastic against the dolls at many spots, and Meteo can be too, although you’ll eat a lot of attack animations waiting for rocks to fall. The back dolls (Cals) don’t get the boss bit, so a common tactic is to toss an HrGlass, clean up the Brenas, then finish off the stopped enemies. The main thing you mostly want to avoid in this fight is seeing the Big doll, so if you don’t have any way of Stopping the Cals, nor AoE damage to clear all of them together, you’ll want to kill off the dolls so that you can take out the last of each type at roughly the same time.
### Additional Notes
Each Cal has about 12% of the spot’s HP, each Brena has about 4%, and Calbrena has about 54%.

")
                        .AddField("Damage Types", "Physical, Fire, Ice, Bolt", inline: true)
                        .AddField("Boss Bit", "Yes (Brena, Calbrena) No (Cal)", inline: true)
                        .AddField("Additional Links", "[Cal](https://wiki.ff4fe.com/doku.php?id=cal), [Brena](https://wiki.ff4fe.com/doku.php?id=brena)", inline: true);
                    break;

                case BossName.Golbez:
                    embedBuilder
                        .WithTitle("Golbez")
                        .WithUrl("https://wiki.ff4fe.com/doku.php?id=golbez_dwarf_castle")
                        .WithThumbnail("https://ff4-fe-info.s3.us-west-2.amazonaws.com/schalaSprites/Golbez.png")
                        .WithDescription(
@"### Fight Flow
The fight opens with Golbez casting HoldGas on the party, then summoning Shadow. Shadow will use Demolish either three times, or until the party is down to two on-screen characters, whichever happens first. Golbez then dismisses Shadow, releases the HoldGas, and becomes vulnerable. Then Golbez will cycle through single targets of Virus, Lit3, and Fire22.

Golbez will react to unreflected damage, when he’s under an HP threshold for the location, with his plaintive “Why?” before dying.
### Strats
If you can, take advantage of the status effect system by using Size or Piggy before the battle when you know you’re facing Golbez. This will prevent the HoldGas from paralyzing the party, making it easy to get starveils up on whoever is left. You can also self-target Venom from a well-placed/anchored Black Mage for the same effect. If you lack the ability for any of that, consider getting into a random encounter and killing off all but your two most important characters for the upcoming fight, and then taking on Golbez. 

Characters wearing a Ribbon or Adamant armor are immune to Shadow’s demolish, and Adamant Armor, Crystal Rings and Heroine Robes prevent being paralyzed.

Depending on your damage output, you can sometimes just let Golbez do the damage to himself and have anyone trigger the finishing script. Cecil with a Holy weapon often speeds up finishing out the fight very quickly.
### Additional Notes
High magic spots might seem scary, but can be a great way to leverage some early XP, since Golbez will do most of the work. The Asura spot in the Feymarch, or the Ogopogo (Masamune altar) spot are two notable examples of this. Other spots, like the White Spear altar (Plague’s spot) or the top of Lower Babil (Lugae’s spot), that have very low magic damage means that you can often just forgo using any walls at all.
Goblez will not gate intended underground access unless `Bunsafe` is turned on.

")
                        .AddField("Damage Types", "Bolt, Fire, Untyped Magic", inline: true)
                        .AddField("Weakness", "Holy, Fire", inline: true)
                        .AddField("Additional Links", "[Shadow](<https://wiki.ff4fe.com/doku.php?id=shadow>)", inline: true)
                        .AddField("Boss Bit", "Yes", inline: true);
                    break;

                case BossName.DrLugae:
                    embedBuilder
                        .WithTitle("Dr. Lugae")
                        .WithUrl("https://wiki.ff4fe.com/doku.php?id=dr.lugae_first")
                        .WithThumbnail("https://ff4-fe-info.s3.us-west-2.amazonaws.com/schalaSprites/Lugae.png")
                        .WithDescription(
@"### Fight Flow
This conversation heavy fight starts with Balnab punching the Dr, who heals Balnab (which is Dr. Lugae’s only action unless he’s the only enemy left in the fight, where he’ll operate Balnab directly as Balnab-Z. If this happens, Balnab-Z fights twice, and then uses Explode. For Balnab, after punching Dr Lugae, he’ll Fight the party until he’s the only enemy left, and then he’ll use Explode.

After the first fight ends, a second begins, with another Dr Lugae form. This fight starts with more dialog, and then Lugae transforms and uses Poison on the party, after which Lugae cycles through Beam, Laser, Emission, and then a party-wide Heal. Lugae will react with any damage the party inflicts with a single target Gas counter.
### Strats
The first fight gives you a decent bit of setup time, and also has fairly limited damage potential. You do want to decide early on if you think you’re able to take down both Lugae and Balnab, or if you want to focus fire on one and just accept Balnab-Z into your life. Having good AoE (Quake, Levi, Baham) should lean you towards taking out both in the first fight, where having only one strong damage source might indicate that Balnab-Z is a good option.

Once into the second fight, lean on your big damage dealers pretty heavily. With RA1 setups, you’re unlikely to get a berserk cast off before the Poison blocks it, so don’t count on that landing. Lugae’s Laser ability can be a major issue at high HP spots. For the most part, though, even the second fight isn’t too scary if you take your time and manage resources well.

The robot race on both Balnab and the second Dr Lugae means that Thunder Claws do great work, and also that Cid can do some really heavy lifting in the fights. 
### Additional Notes
Walls will reflect both Beam and Emission, and will block Laser, which makes it very handy at high HP spots, or if the party is underleveled. Underleveled parties should also make liberal use of Heal if the Gas counters land. Limiting how many characters are dealing damage limits the Gas counters, as well as frees up characters for opportunistic potion (Heal/Cure/Life) usage to keep the party up and going.

")
                        .AddField("Damage Types", "Physical, Untyped Magic, Fire", inline: true)
                        .AddField("Weakness", "Robot (Balnab, Dr Lugae 2)", inline: true)
                        .AddField("Boss Bit", "Yes", inline: true)
                        .AddField("Additional Links", "[Balnab](<https://wiki.ff4fe.com/doku.php?id=balnab>), [Balnab-Z](<https://wiki.ff4fe.com/doku.php?id=balnab-z>), [Lugae (2nd Fight)](<https://wiki.ff4fe.com/doku.php?id=dr.lugae_second>)", inline: true);
                    break;

                case BossName.DarkImps:
                    embedBuilder
                        .WithTitle("Dark Imps")
                        .WithUrl("https://wiki.ff4fe.com/doku.php?id=darkimps")
                        .WithThumbnail("https://ff4-fe-info.s3.us-west-2.amazonaws.com/schalaSprites/DarkImps.png")
                        .WithDescription(
@"### Fight Flow
The dark imps fight. That’s it.
### Strats
At many spots, Quake is a very effective spell since it does not have any damage split for hitting multiple enemies. Berserk is generally a great ability, but sometimes you might have a need to focus fire in order to reduce incoming damage by killing one imp ASAP. For defense, the standard Blink/Illusion/MoonVeil are always applicable for this fight.
### Additional Notes
An HrGlass, or landing a Stone, Size or Toad spell will win (or effectively win) the fight with the Dark Imps while `Bnofree` is off. Since Size and Toad do persist when Life2 is used, when the fight is at high XP spots (ribbon room or either giant location especially) you can do an extended, D.Machin-like grind if you have access to those spells.

If `Bnofree` is enabled, Cover strats can become one of the best helpers for the fight when your party is underleveled.

")
                        .AddField("Damage Types", "Physical", inline: true)
                        .AddField("Boss Bit", "No", inline: true);
                    break;

                case BossName.KingQueenEblan:
                    embedBuilder
                        .WithTitle("King/Queen Eblan")
                        .WithUrl("https://wiki.ff4fe.com/doku.php?id=k.eblan")
                        .WithThumbnail("https://ff4-fe-info.s3.us-west-2.amazonaws.com/schalaSprites/Eblan.png")
                        .WithDescription(
@"### Fight Flow
K Eblan will cast Fire 2 three times before switching the music and then dying on his next turn. Q Eblan will cast Fire1 until she's alone, and then queues Vanish the next time she is able to queue an action.
### Strats
Wait out the damage from the spells, healing if necessary and watch them disappear. 
### Additional Notes
Since the Queen will disappear after the King does, you can sometimes speed up the fight by killing him. If `Bnofree` is on, they’ll skip their initial dialog at the beginning of the fight, which can be a handy tell in a mystery seed.

")
                        .AddField("Damage Types", "Fire", inline: true)
                        .AddField("Boss Bit", "Yes", inline: true)
                        .AddField("Additional Links", "[Q Eblan](<https://wiki.ff4fe.com/doku.php?id=q.eblan>)", inline: true);
                    break;

                case BossName.Rubicant:
                    embedBuilder
                        .WithTitle("Rubicant")
                        .WithUrl("https://wiki.ff4fe.com/doku.php?id=rubicant")
                        .WithThumbnail("https://ff4-fe-info.s3.us-west-2.amazonaws.com/schalaSprites/Rubicante.png")
                        .WithDescription(
@"### Fight Flow
Rubicant cycles through actions of opening his cloak (which gives an Ice weakness), casting glare, closing the cloak (changing the ice weakness to an ice absorb), and then alternating between a single punch or a double punch. The first cycle will be a single punch, the second a double, then back to single &etc.

Rubicant reacts to all times a character uses Fight with a party-wide Fire2 counter. If he’s below 4% of the spot’s health and gets hit with Fight, instead of that Fire2 counter, he’ll give a small speech and disappear. He’ll also react to usage of fire magic with using Life1 on the whole party. 
### Strats
Equipping melee characters with ice weapons (IceBrand, Ice Claw, Blizzard spear, Ice arrows, &etc) will hit a script-induced 4x weakness throughout the fight once Rubi opens his cloak. Depending on the spot’s magic power and your party's HP/amount of Fire resistance gear equipped, you might opt to just use berserk on a physical fighter equipped with an ice weapon and either tank or heal through the counters, or you can make use of the Fight alternatives (Power, Jump, Aim) in order to not draw the counters.

Casters should generally lean towards using fast spells, like Virus, Quake, and Nuke most of the fight, but timing Ice3 to land when the cloak is open is definitely a skill to learn. Boreas and Blizzard items are great to toss and land while the cloak is open, especially with high HP characters who might not have ice element weapons equipped.
### Additional Notes
On his own, Rubicant generally uses single-target damage, so underleveled parties can look to strategies that don’t involve the Fight command, and making liberal use of Life spells/potions and Blink/Illusion to keep party members up and active, and generally not risking a wipe. This strategy takes more time to execute, but if you’re in a low level situation can get you through a fight that’d otherwise not be possible to win.
")
                        .AddField("Damage Types", "Physical, Fire (Glare, Fire2)")
                        .AddField("Resist", "Fire Absorb (until first cloak opening), Berserk, Ice Absorb (cloak closed)")
                        .AddField("Weakness", "Ice (4x, cloak open)", inline: true)
                        .AddField("Boss Bit", "Yes", inline: true);
                    break;

                case BossName.EvilWall:
                    embedBuilder
                        .WithTitle("Evil Wall")
                        .WithUrl("https://wiki.ff4fe.com/doku.php?id=EvilWall")
                        .WithThumbnail("https://ff4-fe-info.s3.us-west-2.amazonaws.com/schalaSprites/EvilWall.png")
                        .WithDescription(
@"### Fight Flow
EvilWall will punch 16 times, then switch to using Crush. Since the punching phase uses chained commands to keep the “advance, punch” combination together, Evil Wall effectively acts as though it’s berserked during this phase. No atb cheating shenanigans happen during the Crush phase.

EvilWall will counter any magic usage affecting it with a Petrify counter, so mage heavy compositions can run into some trouble with the Stone condition if they’re reliant on low damage per cast spells. 
### Strats
Berserk status does wonders for helping mitigate EvilWall’s ATB shenanigans with using a chain for its actions. Good defensive gear and using the backrow glitch will help keep party members alive, and using Blink/Illusion helps a lot too. If you’ve a MoonVeil and instant death protection, one character can be immune from any of EvilWall’s actions.
### Additional Notes
EvilWall will punch itself if there are no valid PC targets to hit. In some places you can use this along with Eddy’s hide command to let Evil Wall kill itself, or with the aid of a Ribbon or Adamant armor to prevent the instant death effect from Crush, let Eddy come out of hiding once the fight is in Crush phase to finish the job.
")
                        .AddField("Damage Types", "Physical", inline: true)
                        .AddField("Additional Links", "[Eddy Vs EvilWall](<https://docs.google.com/spreadsheets/d/1TQY6hGjqkC1NQGDv_M0pzPQa2xZZlAT7rzCzCNLyX1g>)", inline: true)
                        .AddField("Boss Bit", "Yes", inline: true);
                    break;

                case BossName.Elements:
                    embedBuilder
                        .WithTitle("Elements")
                        .WithUrl("https://wiki.ff4fe.com/doku.php?id=elements_milon_rubi")
                        .WithThumbnail("https://ff4-fe-info.s3.us-west-2.amazonaws.com/schalaSprites/Fiends.png")
                        .WithDescription(
@"### Fight Flow
The Milon form will Fight on four consecutive turns, and then use Curse. Rubicant will cycle through, in order, Fire2, Fire3, and Glare. Kainazzo uses Fight four consecutive turns, then uses Big Wave. Valvalis alternates between Fight and Ray for two instances each, then uses Storm. Each boss has an HP threshold where they’ll use their reaction to transform into the next form in the cycle. 
### Strats
Generally, this is a straightforward damage race, with Rubicant’s Fire3/Glare targets being the main concerns throughout. Take advantage of elemental (or racial) weaknesses where you can to speed up the fight and reduce the threat of the encounter. 

Often the fastest path through is to reflect spells onto the Milon form and never see any other of the Fiends. Placing the wall on someone with Zombie protection (a Wizard hat, Sorcerer robe, or a part of Cecil’s Crystal armor set are common ways of getting this) means that Milon’s attacks will deal reduced damage to that character, making healing them often not required.
### Additional Notes
Since this fight is actually two separate HP pools (Milon/Rubi in one, Kainazzo/Val in another), if you can kill the first fight without hitting the HP threshold for it to transform into the other, you’ll never face the Kainazzo/Valvalis fight. Typically this happens with using a Wall and bouncing spells onto the Milon form. Since reflected spells don’t trigger reactions, you’ll never see any other shape.
")
                        .AddField("Damage Types", "Milon: Physical\r\nRubicant: Fire\r\nKainazzo: Physical, Untyped Magic (Big Wave)\r\nValvalis: Physical")
                        .AddField("Resist", "Milon: Absorbs Ice, Holy, Dark, Bolt, Air\r\nRubicant: Absorbs: Fire, Holy, Dark, Bolt, Air\r\nKainazzo: Absorbs: Fire, Ice, Dark, Holy, Air\r\nValvalis: None\r\n")
                        .AddField("Weakness", "Milon: Fire (4x)\r\nRubicant: Ice (4x)\r\nKainazzo: Thunder (4x)\r\nValvalis: Thunder (4x), Holy (4x)")
                        .AddField("Additional Links", "[Kainazzo/Val](<https://wiki.ff4fe.com/doku.php?id=elements_kainazzo_val>)", inline: true)
                        .AddField("Race", "Milon: Zombie\r\nRubicant: Mage", inline: true)
                        .AddField("Boss Bit", "Yes", inline: true);
                    break;

                case BossName.CPU:
                    embedBuilder
                        .WithTitle("CPU")
                        .WithUrl("https://wiki.ff4fe.com/doku.php?id=CPU")
                        .WithThumbnail("https://ff4-fe-info.s3.us-west-2.amazonaws.com/schalaSprites/CPU.png")//;
                        .WithDescription(
@"### Fight Flow
The attacker always uses Maser, a full party targeted effect dealing 10% of each person’s HP in damage. The Defender always uses Remedy on the CPU, which heals it for 10% of the CPU’s total health. The CPU will cast Wall when it doesn’t have a wall up. If both the Attacker and Defender are dead, the CPU will use Globe 199 twice, then resurrect the Attacker and Defender.
### Strats
Most strategies start with killing the Defender, since it can be extremely hard to keep up with the healing that it does. Teams with Jump, Aim, Dart, and backrow-glitched characters will be able to attack the CPU at full damage, and often rely on manual targeting to avoid having to deal with Globe 199 and resurrected Attacker/Defender nodes. Berserk and Blink can be, RNG willing, a good pairing when Zerk is used on one party member and you can quickly (re)apply Blink on the Attacker node. And once enough damage has been dealt, going all in on damage (e.g. stop using Blink, berzerk more, use Meteo/Quake) can help shorten the time it takes to get through this fight. Setting up Nuke/White casters with a wall on a party member and letting them be part of the barrage is also a good play

Float strats, where you get a wall up on one character, then try to reflect the float on the Attacker orb can also be very helpful, as long as the float doesn’t reflect back on the CPU. Once float is on the Attacker, you’re free to use Quake, which will really help mages get in on the damage without the RNG of reflecting single target spells and hoping they land on CPU
### Additional Notes
The attacker and defender each get 10% of the spot’s HP, but award one-third of the spot’s XP. With a combination of location and team composition, you can set up a pretty effective grind by forcing resummons of the orbs, but you’ll also have two Globe 199s per cycle, so Life potions/spells and party-wide healing will be important for an orb grind.

")
                        .AddField("Damage Types", "Magic (Globe 199), Target HP Based (Maser)", inline: true)
                        .AddField("Additional Links", "[Attacker](<https://wiki.ff4fe.com/doku.php?id=Attacker>), [Defender](<https://wiki.ff4fe.com/doku.php?id=Defender>)")
                        .AddField("Boss Bit", "Yes", inline: true);
                    break;

                case BossName.Odin:
                    embedBuilder
                        .WithTitle("Odin")
                        .WithUrl("https://wiki.ff4fe.com/doku.php?id=Odin")
                        .WithThumbnail("https://ff4-fe-info.s3.us-west-2.amazonaws.com/schalaSprites/Odin.png")
                        .WithDescription(
@"### Fight Flow
Odin will use Fight three times, then raise his sword and take an empty turn, then use Odin. He’ll then Fight twice, then raise his sword, take an empty turn, then use Odin. Then he’ll fight once, raise his sword, take an empty turn, and use Odin. Then repeat from the top.
### Strats
Getting a berserker going early is a strong start to the fight. The lightning weakness makes Thunder Claw users really shine, and Lit Arrows do well too. Defensively, Blink/Illusion will help you handle the physical attacks. If you can’t thunderstruck (see below), this can be a damage race to the first time Odin sweeps forward and does his party-wide attack
### Additional Notes
Both Edward and Kain can avoid the Odin attack by being not on screen during it, enabling the Thunderstruck strats at locations and levels where the fight might not normally be possible. The trigger for Thunderstruck requires Odin to be in the third part of the cycle (single punch, Odin attack), and is only active for a short time while the sword is raised. This generally requires your party member to survive the punch and then immediately use a lightning-based item to land the damage when the trigger is in effect.
")
                        .AddField("Damage Types", "Physical, Untyped Magic", inline: true)
                        .AddField("Weakness", "Bolt (4x)", inline: true)
                        .AddField("Boss Bit", "Yes", inline: true);
                    break;

                case BossName.Asura:
                    embedBuilder
                        .WithTitle("Asura")
                        .WithUrl("https://wiki.ff4fe.com/doku.php?id=Asura")
                        .WithThumbnail("https://ff4-fe-info.s3.us-west-2.amazonaws.com/schalaSprites/Asura.png")
                        .WithDescription(
@"### Fight Flow
On each of Asura's turns, Asura will self-target a spell based on the current active face, and whether or not a reaction script was triggered in between Asura's turns. If a reaction was triggered, no face cycling occurs. If not, she’ll cycle to the next face before casting. The orange face casts Cure4, the cream face casts Cure3, and the gray face casts Life1. When rotating the active face, the rotation goes orange => cream => gray => orange

When Asura is dealt damage, the face change pattern goes backwards (should the face change at all), so hitting Asura on while the cream face is active will make it cycle back to the orange face.
### Strats
Asura never attacks your party except as a reaction, so you have plenty of setup time to get Blink/Illusion/MoonVeil on party members before actually being attacked. 

Giving Asura the Reflect status will bounce those self-targeted spells, which then means this becomes like fights that just have physical attacks to deal with. Because those physical attacks come from a reaction to direct damage, if the location’s damage is deadly to your party, you’ll often want to have a single source of damage throughout the fight. And even if the damage isn’t high, if your party is doing lots of low damage attacks, you’ll spend a lot of time in animations in battle that aren’t necessary. Edge with a Mute knife and a decent katana is great in this fight, as is anyone swinging a Rune Axe. For bows and arrows, both mute arrows and the elven bow hit the Mage race, mix/match your best combination for maximum damage. Defensively, equipping a Ribbon, Rune Ring, or Aegis Shield gives protection from mages, reducing the threat of Asura’s counters.

Parties without much physical firepower will look to bouncing spells off of a wall to both be able to cast Virus/Nuke during the fight, and also not require any healing/preventative measures.

Lastly, the Lifelock strategy of keeping Asura on continually casting Life1 is something generally best used in the overworld. Using quick actions (Fight, Virus, Nuke, damaging j-items, Dancing Dagger, etc) just as the Life1 is cast from Asura will cycle back to that face and cast Life1 on when acting next. Depending on party speed, you might have to have every member contribute with some damage, so inaccurate characters will want to rely on item usage to do their part. This strategy is not something to do when Asura is at fast RA numbers (RA1, probably RA2 as well), since you might lose the correct ATB timing to have Asura remain on the correct face.
### Additional Notes
The description in Fight Flow for the reactions is limited, since getting into the details of how/when faces change precisely, as well as writing out examples for it, are a little beyond the scope/character limit of this application. You may well see slightly different behavior as you introduce elements in a battle that go beyond a very simple setup. Perhaps someone will write out a concise, but sufficiently detailed description and this note gets replaced. Someday!

")
                        .AddField("Damage Types", "Physical", inline: true)
                        .AddField("Race", "Mage", inline: true)
                        .AddField("Boss Bit", "Yes", inline: true);
                    break;

                case BossName.Leviatan:
                    embedBuilder
                        .WithTitle("Leviatan")
                        .WithUrl("https://wiki.ff4fe.com/doku.php?id=leviatan")
                        .WithThumbnail("https://ff4-fe-info.s3.us-west-2.amazonaws.com/schalaSprites/Leviath.png")
                        .WithDescription(
@"### Fight Flow
Levitan goes through a cycle of changing form and using Big Wave, then taking a turn to change form back, then casting Ice 2 either twice (odd times through the cycle), or once, before starting the cycle again.
### Strats
This fight is generally not much to worry about if you have decent healing. Zerkers are happy to zerk, mages can cast the standard array of spells (best of Virus, Quake, Nuke) or also choose to add in their lightning spells. At high magic locations those Ice2s can really hurt, so using a starveil is definitely a solid choice to help get through a fight. 
### Additional Notes
The lightning weakness being 2x means that Yang’ll get a bigger increase over Edge in equipping a Thunder Claw. If you have both on your team, the other equipment will decide who should get it. Edge with pretty mismatched katanas, think Masa/Long or Ninja/Short, will be very happy to replace the weaker blade with a Thunder Claw.

")
                        .AddField("Damage Types", "Untyped Magic, Ice", inline: true)
                        .AddField("Weakness", "Bolt", inline: true)
                        .AddField("Boss Bit", "Yes", inline: true);
                    break;

                case BossName.Bahamut:
                    embedBuilder
                        .WithTitle("Bahamut")
                        .WithUrl("https://wiki.ff4fe.com/doku.php?id=bahamut")
                        .WithThumbnail("https://ff4-fe-info.s3.us-west-2.amazonaws.com/schalaSprites/Bahamut.png")
                        .WithDescription(
@"### Fight Flow
Bahamut starts counting down from 5 to 1, and then casts MegaNuke on the party.
### Strats
Methods of handling range from an all out assault, often using the berserk status, to patiently reflecting all the MegaNuke damage back at Bahamut, with a large range of a hybrid approach in-between. When the part is underleveled, you’re more likely going to be successful trending toward reflecting back the damage. Good use of timing in using the item and agility anchoring can make starveils last for up to three cycles. It can also be a great idea to stagger out the veil usage by not protecting some characters and having them be revived by those with protection. This can help save some veils in your inventory, and not waste excess damage on what’s reflected, since a full party with reflect up generally easily exceeds the 9999 damage cap.
### Additional Notes
You get less time in between the 1 count and the casting of MegaNuke because Bahamut makes use of the Chain command to chain together sprite animations and the spell cast all as one action. A side effect of Chain is that the enemy essentially skips a wait in line.
")
                        .AddField("Damage Types", "Magic", inline: true)
                        .AddField("Boss Bit", "Yes", inline: true);
                    break;

                case BossName.PaleDim:
                    embedBuilder
                        .WithTitle("Pale Dim")
                        .WithUrl("https://wiki.ff4fe.com/doku.php?id=pale_dim")
                        .WithThumbnail("https://ff4-fe-info.s3.us-west-2.amazonaws.com/schalaSprites/PaleDim.png")
                        .WithDescription(
@"### Fight Flow
On its turn, Pale Dim only fights. In reaction to Fight, Pale Dim will cast Slow. In reaction to fire magic, Pale Dim uses Glare. In reaction to ice magic, Pale Dim uses Blizzard. In reaction to Bolt magic, Pale Dim uses Blitz. In reaction to the Call ability, Pale Dim uses Quake.
### Strats
Another fight where Blink/Illusion/MoonVeil do so much to protect the party and be very efficient in doing so. The Slow counters from Pale Dim can vary from being a nuisance to something that makes the fight a little risky if you fall behind in preventative measures. Black Mages should use untyped magic (Virus, Quake, Nuke) here to avoid both healing Pale Dim and suffering the counter attack. Zerkers are fine, but if you have someone equipped with dragon killing gear (dragoon spear, dragon whip, artemis arrows), you’ll want to lean on them as possibly the only attacker.
### Additional Notes
Cecil as a cover bot can be extremely strong for this fight and be essentially invulnerable when placed in the backrow and given a Crystal Ring, or any Dragoon defensive gear to pick up dragon defense (Glass headgear and Adamant Armor also do wonders). 

")
                        .AddField("Damage Types", "Physical\r\nReactions: Fire, Ice, Bolt, Untyped Magic")
                        .AddField("Resist", "Absorbs: Fire, Ice, Bolt\r\nStatuses: Poison Blind Mute Pig Mini Frog Petrify KO Sleep Stun ")
                        .AddField("Race", "Dragon", inline: true)
                        .AddField("Boss Bit", "Yes", inline: true);
                    break;

                case BossName.Wyvern:
                    embedBuilder
                        .WithTitle("Wyvern")
                        .WithUrl("https://wiki.ff4fe.com/doku.php?id=wyvern")
                        .WithThumbnail("https://ff4-fe-info.s3.us-west-2.amazonaws.com/schalaSprites/Wyvern.png")
                        .WithDescription(
@"### Fight Flow
Wyvern begins the fight with MegaNuke, takes two consecutive empty turns, then casts a self-targeted Wall. While the Wall is active, Wyvern will bounce Nuke off of the wall. When the wall runs out, Wyvern will use Remedy, then re-cast the Wall, which will resume the Nuke barrage.
### Strats
Wyvern’s opening Meganuke often requires agility setups that let you get Reflect status up on multiple characters right away, and also slowing down the battle speed to allow you the time to input the commands to do so; very practiced runners can substitute battle speed adjustments with run buffering. Early in the game you can still often outspeed the MegaNuke with fast characters anchoring the battle, but later on you’ll want a slow anchor and fast first actors in order to have a solid setup.

Once you’ve managed to survive the opening salvo, get people back up and let the damage fly. Dragon-killing weapons are greatly desired here to speed up the fight and reduce resource usage. After that first salvo, and assuming you don’t use Call to target Wyvern, Wyvern will only kill one person a turn, so there’s a good opportunity that you can use Life potions or spells to keep on even-ish footing. 
### Additional Notes
Rydia’s Call abilities will cause Wyvern to Counter with MegaNuke (at a reduced spell power from the opening MegaNuke). If you have Sylph, you can use that to help reflect more MegaNuke damage back at Wyvern.

Wyvern will not gate intended underground access unless `Bunsafe` is on. The `whyburn` flag jumps straight to the Wall cast, skipping the MegaNuke and empty turns after it at the start of the fight. The `whichburn` flag changes the opening attack to another random enemy attack, with some protections to keep the opener milder than MegaNuke. `Bunsafe` removes those protections.

")
                        .AddField("Damage Types", "Magic", inline: true)
                        .AddField("Race", "Dragon", inline: true)
                        .AddField("Boss Bit", "Yes", inline: true);
                    break;

                case BossName.Plague:
                    embedBuilder
                        .WithTitle("Plague")
                        .WithUrl("https://wiki.ff4fe.com/doku.php?id=plague")
                        .WithThumbnail("https://ff4-fe-info.s3.us-west-2.amazonaws.com/schalaSprites/Plague.png")
                        .WithDescription(
@"### Fight Flow
While any living, on-screen character lacks a Doom counter, Plage will cast Count on the party, which also resets the counter on everyone hit by it. Otherwise, Plague casts Fast on the party.
### Strats
Plague’s flying status means that spears, boomerangs, and bows/arrows deal extra damage, and that Quake is completely ineffective. In the Milon Z location (back attack on Mt Ordeals) location, you might consider starting the fight with a party member dead so that you can easily reset the counter. It’s also strongly recommended you use as slow of an agility anchor as possible, since you start the fight at significant ATB disadvantage from the forced Back Attack.

Count is an effect that can be manipulated in a couple of ways. Most basically, killing and resurrecting a character will trigger Plague to re-apply the status on all party members, which resets everyone’s counter to 10, buying you more time (functionally unlimited, as long as your Life potions hold out). Alternatively, because of how the game handles conditions, using berserk, fast spells, holding A, or a combination, you can make it so the character’s action prevents a count timer at 00 from activating.
### Additional Notes
Plague is susceptible to his own Count, which means that reflecting it back and having the count expire can be an efficient way to handle this fight. Generally you’ll need to use a wall on one character, kill another, use Life on the swooned character, let the Count reflect back on plague, either killing or letting the walled character die, resurrecting them, and waiting for the Count (which isn’t visible) to reach 00 for Plague, and then taking any action. Plague will not die to Count while at RA1 (or if enough actions are happening that makes Plague effectively function at that speed), so you might need to use Slow/Silkweb to change RA to that value. 
")
                        .AddField("Resist", "Poison Blind Mute Pig Mini Frog Charm Sleep Stun")
                        .AddField("Weakness", "Air", inline: true)
                        .AddField("Boss Bit", "Yes", inline: true);
                    break;

                case BossName.DLunars:
                    embedBuilder
                        .WithTitle("D. Lunars")
                        .WithUrl("https://wiki.ff4fe.com/doku.php?id=d._lunar")
                        .WithThumbnail("https://ff4-fe-info.s3.us-west-2.amazonaws.com/schalaSprites/LunarD.png")
                        .WithDescription(
@"### Fight Flow
The D. Lunar’s basic script is for each dragon to use Fight, then Fire, then Fight on four consecutive turns, then use Breath, then repeat the cycle. 

If a character uses Fight against them, the attacked monster reacts by casting Wall on both of them, and their script changes to targeting a Virus on the other D. Lunar, to reflect the spell back on the party.

If there is only one D. Lunar left, it will react to everything with a Fire counter.

D. Lunars counter Call with Remedy.
### Strats
Using Fight alternatives (Power, Jump, Aim, Dart) for melee-heavy parties is standard practice. For parties with heavy magic damage, Fire3, Nuke, and Quake are good spells to use to either exploit the Fire weakness, or generally deal above average damage for the turn usage. Blink/Illusion are great for preventative damage, although some recovery is good to have on hand for the Fire casts.

At many locations, Crystal Sword users can change the calculus on whether or not to use the berserk status, but this can be a highly RNG dependent strategy. Supplementing that damage with Quake, or starting off with other sources of damage to reduce the amount of Virus spells your party might face. Dragoon Spears, Artemis Arrows, and the Dragon whip also can change the calculation, although do so to a lesser degree.
### Additional Notes
Low level parties (and in locations with either high Magic attack, high HP, or both) will consider Walls to reflect the Breath spell back on the enemies. A D. Lunar who has been hit by the Breath cannot use magic, will do at most 1 point of damage with physical attacks, and suffers great defensive penalties. Should the reflected Breath only attack one of the D. Lunars, you can still take great advantage of Frog Strats by Fighting the frog, since the attacked enemy is the one that uses the wall reaction. After that initial triggering of the Virus script, any focus fire should be done to the non-frog enemy first.

")
                        .AddField("Damage Types", "Physical, Fire", inline: true)
                        .AddField("Race", "Dragon, Zombie", inline: true)
                        .AddField("Weakness", "Fire", inline: true)
                        .AddField("Boss Bit", "yes", inline: true);
                    break;

                case BossName.Ogopogo:
                    embedBuilder
                        .WithTitle("Ogopogo")
                        .WithUrl("https://wiki.ff4fe.com/doku.php?id=ogopogo")
                        .WithThumbnail("https://ff4-fe-info.s3.us-west-2.amazonaws.com/schalaSprites/Ogopogo.png")
                        .WithDescription(
@"### Fight Flow
The fight begins with two Big Waves chained together, taking doing 50% of the party’s max HP right away, then will Fight on three consecutive turns, use Big Wave a single time, and then fight on two more turns, and then returns to the beginning of the fight (the double big wave).

Ogopogo will counter most uses of magic with Blaze, a ice-based attack that deals 20% Max HP damage to the party. The exception is lightning-based magic, which elicits a single-target Weak cast.
### Strats
The counters Ogopogo uses means that most strategies lean heavily on berserked characters. Mages should look to bouncing spells off of a wall, and Rydia generally will avoid using the Call ability. Blink/Illusion is important to get going early, but you will need some healing as well if the fight goes long enough that the script rolls back around to the start of the script. You’ll also want to keep an idea of where the fight is in the script if you are thinking of using non-Life2 resurrection, since a Big Wave is almost certain to render anything less useless, unless you’ve timed other healing on the target.
### Additional Notes
This is an extremely rude boss to see at either Hook location, due to how limited your preventative/healing resources are early on, and the HP based damage is difficult to outlevel. Definitely a boss where underleveled parties very much want to manipulate agility, and slow down the battle speed.
")
                        .AddField("Damage Types", "Physical, Untyped Magic (Big Wave), Ice (Blaze)", inline: true)
                        .AddField("Boss Bit", "Yes", inline: true);
                    break;

                case BossName.Zeromus:
                    embedBuilder
                        .WithTitle("Zeromus")
                        .WithUrl("https://wiki.ff4fe.com/doku.php?id=zeromus")
                        .WithThumbnail("https://ff4-fe-info.s3.us-west-2.amazonaws.com/schalaSprites/Zeromus.png")
                        .WithDescription(
@"### Fight Flow
The fight begins with Zeromus untransformed, invulnerable, and the only action taken is to remove speed modifiers and most statuses from the party. After the Crystal is removed, Z’s first action is to remove the invulnerability, shake, and use Big Bang. After that, the Zeromus goes through a cycle of visible actions: Shake, Big Bang, Black Hole, Shake, Big Bang, Virus, Black Hole, Shake, Big Bang, Black Hole, until the party does enough damage to switch to the Nuke Phase, where the pattern changes to Shake, Big Bang, Black Hole, Nuke, Shake, Big Bang. Both of these phases have extra “Do Nothing” turns not listed here. In the last phase, Zeromus switches to alternating between using Meteo and a “Do Nothing” turn.
### Strats
There are a few main strategies for handling this fight: zerkers, reflect, hybrid, eddy strats, Fu and Friends, and 1200 strats. 

Zerkers relies on getting berserk up on your physical fighters, and letting their damage carry the fight. White mages heal, revive, or cast zerk, and black mages might either nerf incoming Big Bangs, bounce a spell off of a wall (which they probably set up on themselves, or play chemist while they’re alive. 

Reflect strats have the first character post-Crystal tossing get a wall up on themselves, and then the spell casters bounce their fast damage spells off of that wall. Either a silkweb or a direct cast of something (not White) as soon as Z is vulnerable will nerf the first Big Bang, which is often essential to make sure that the party survives the first Big Bang. If any physical fighters chip in, make sure that they don’t do any damage to Zeromus that puts your total damage at or above 45k, since that triggers the refill script.

Fu and Friends, Eddy and 1200 Strats are all variants relying on the same tactic: getting Reflect up on necessary party members, and using j-items, directly cast spells, or the Crystal to trigger Zeromus’ counter Nuke to bounce off of a wall and reflect back. All of these strategies will stop dealing direct damage at/before 45k total damage to prevent the refill.
### Additional Notes
See the links below for some videos.
")
                        .AddField("Damage Types", "Magic (Untyped, Holy)")
                        .AddField("Additional Links", "[Script Detail](<https://wiki.ff4fe.com/doku.php?id=zeromus_script>), [Eddy Strats](<https://docs.google.com/document/d/1Xw1vsN-OROShv4ZxPcStwJ1LsmFlPcZr3IIjOBSNEww/edit#heading=h.dvcyslrwgp71>), [Full party 1200 strats](<https://www.twitch.tv/videos/1051386268>), [2 character 1200 strats](<https://www.twitch.tv/videos/1051391891>)")
                        .AddField("Boss Bit", "Yes");
                    break;

                case BossName.Unknown:
                default:
                    embedBuilder.WithTitle("Interlibrary Loan Requested")
                                .WithDescription("We're waiting on getting that information from another library. Hold tight!");
                    break;

            }

            return embedBuilder.Build();
        }
    }
}
