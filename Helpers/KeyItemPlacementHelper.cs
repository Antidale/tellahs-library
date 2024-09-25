using System;
using DSharpPlus.Entities;

namespace tellahs_library.Helpers;

public class KeyItemPlacementHelper
{
    public static DiscordEmbed GetKeyItemPlacementDescrition()
    {
        return new DiscordEmbedBuilder().WithTitle("Key Item Placement").
        WithDescription(@"
## Overview
When randomizing Key Item (KI) placement, Free Enterprise has five different buckets of locations it can pull from: Main, Summon, Quest, Monsters! boxes (miabs) outside of the Lunar Subteranne (LST), and miabs in the LST. Anything other than Main checks will have a random amount of locations from their bucket added to the overall pool of checks that can have a KI. Main checks are always in the pool.
### Ksummon
Enabling the `Ksummon` flag adds between 3 and 5 possible locations to where Key Items can go: Defeating the bosses at the vanilla Leviatan, Asura, Odin, and Bahamut locations, as well as bonking Yang with the Pan in the house in Sylph Cave.

When `Kmoon` is also enabled, the two buckets are combined when adding locations to the pool, which makes it possible that no `Ksummon` locations are actually in the KI pool. The combined buckets add between 6 and 11 locations to the pool.
### Kmoon
Enabling the `Kmoon` flag adds between 3 and 6 possible locations to where Key Items can be found: the Murasame altar (vanilla Pale Dim), Crystal Sword altar (vanilla Wyvern), White Spear altar (vanilla Plague), each chest in the Ribbon room (vanilla D.Lunars), and the Masamune altar (vanilla Ogopogo). 

When `Kmiab` is enabled with `Kmoon`, the combination allows the miabs in the LST to also have Key Items placed in them. 

When `Ksummon` is enabled with `Kmoon`, the two buckets are combined when adding locations to the pool, adding a total of between 6 and 11 locations to the pool. 
### Kmiab
Enabling the `Kmiab` flag adds between 9 and 17 possible locations to where Key Items can be found (see [FF4 Miab Info](<https://docs.google.com/document/d/1F93iM_F73UW_uFIB7R_ioVHxJVvlpKmjG41dGLIBsnU/edit>) from xPankraz for detailed info). Also enabling either `Kmoon` or `Kunsafe` adds in the 9 miabs from the LST, for a total of between 11 and 26 possibilities.
## Placement Details
When determining where Key Items will be placed, the randomizer constructs a pool of locations. `Kmain` locations are always on and always in the pool. `Kmoon`, `Ksummon`, and `Kmiab` locations will have some of their locations in that KI pool, where they have a chance of awarding a Key Item, and some locations that were never in the pool for a given seed.
### Ksummon & Kmoon
When either/both of `Ksummon` and `Kmoon` are enabled, half of those locations (rounded up) are added to the pool to start with. So if only one of those flags is on, three of the locations from that group are added to the pool, and if both are on, six locations are added. Then to add some randomness, the randomizer uses a coinflip mechanism, described below.
### Kmiab
When `Kmiab` is added, each [miab area](<https://wiki.ff4fe.com/doku.php?id=treasure_probability_curves#miabs>) gets two of the miab boxes added to the pool, with additional boxes added in a coinflip mechanism, described below. After all miab groups have locations chosen, the randomizer then adds all but a random three of those locations to the KI pool.
### Coinflip Mechanism
In order to add some randomness to the amount of potential Key Item containing slots, when the randomizer adds additional locations, the algorithm effectively flips a coin and adds an extra location every time it gets a head, until it gets a tails, where it stops.

This process is done once to handle the Kmiab areas, where the single result applies to each area separately. The process is done separately to handle the combined Kmoon and/or Ksummon determination. Should a `Kmiab` area be given more slots than it has boxes, all the boxes are added to the pool."
        )
        .AddField("Links", "[Miab Areas](<https://wiki.ff4fe.com/doku.php?id=treasure_probability_curves#miabs>)\r\n[Key Item Distribution](<https://wiki.ff4fe.com/doku.php?id=key_item_randomization#key_item_distribution>)\r\n[Kmoon/Ksummon probability tracking](<https://docs.google.com/spreadsheets/d/1L8MKeFXnbyV1w5MRsvgRseAH_nvsrI1GiQUPz3bf1cM/edit?gid=160243468#gid=160243468>) by TwistedFlax\r\n[Miab Info](<https://docs.google.com/document/d/1F93iM_F73UW_uFIB7R_ioVHxJVvlpKmjG41dGLIBsnU/edit>) by xPankraz")
        .Build();
    }
}
