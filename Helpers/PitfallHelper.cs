namespace tellahs_library.Helpers
{
    public static class PitfallHelper
    {
        public static string GetPitfallsText()
        {
            return
"""
### Know Your `Knofree` Flag.
If the `K` flags do not include `nofree`, NPC Edward in Troia has a Key Item for you. But if `Knofree` is enabled, Bedward has nothing for you; instead, defeat D.Mist and talk to Rydia's mom in Mist Village. If `Knofree` is enabled, consider making a note when you defeat D.Mist to help remind you to visit Rydia's mom for your reward.
### D.Mist Hiding Places
When `Knofree` is on and D.Mist is the gate to a Key Item you might have to think a little outside the box to find your path to a Key Item. Especially tricky is a D.Mist taking the Magus Sisters spot in Zot. You can get there even without the Earth Crystal. Also, checking Mist Cave, the Waterfall, or the end of the Package sequence might be helpful.
### Keeping a Cursed Ring
If you've equipped DKC with a cursed ring, make sure to unequip it **before** entering the chamber on Mt.Ordeals. Likewise, take care when dismissing characters when `Cbye` is on.
### Route Ordering
If you have a Package objective, consider waiting to fulfill it until after you've visited Kaipo for the last time. That way if you don't want the Package character, you don't have to deal with the cutscene.

Sometimes you can get pretty strong early, and might think about skipping Antlion for a while. If you do that, it can be really easy to forget that you haven't done it, so make a (mental) note for yourself.
### Safely `Gwarp` Your Plans
Since the [Warp Glitch](<https://wiki.ff4fe.com/doku.php?id=glitches#dwarf_castle_warp>) isn't on very often, it can be easy to forget. Making a note at the start of a seed, or doing a quick double-check before starting a Dwarf Castle check are recommended. Rydia learns Warp at level 12, Palom at level 29, Tellah learns it from Ordeals, and it isn't normally a spell FuSoYa learns early on.
""";
        }
    }
}
