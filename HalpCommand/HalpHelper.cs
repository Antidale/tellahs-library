namespace tellahs_library.HalpCommand;

public static class HalpHelper
{
    public static string GetAgilityHalp()
    {
        /*
        determine your anchor
        slow the anchor down
        speed other people up
        reorganize the party
        */
        return
@"There are a few fights in Free Enterprise where lacking a proper agility setup can be the literal death of your party, with Wyvern, Kainazzo, and DKC are three of the mainstays. Making sure you know your anchoring options is the first thing to make sure of when you realize that you can't act in time, or enough, to get through a boss.

If you have free choice of an anchor a good first move is to take the slowest member of your party and stick them in the middle. Equip a Cursed ring if you can. A Dwarf axe can make Cid a pretty slow character all by itself. If you have the ability to get swap characters in the Tower of Wishes in Mysidia, swap out your least useful character for the slowest one in the tower. Once you have a slow anchor, you want the person who should take the first action in the battle in the very top spot, then put the person who should act after that in the bottom slow, then middle top, and finally middle bottom. Some of the party order considerations are making sure you can Cure someone to get out of DKC, or getting a zerker going early, and being ready to disperse Kainazzo's water wall.

If you're in a more restricted situation (think `Chero`, or `-vanilla:agility` when you have a Cecil in your party), a Cursed ring still gets you through a lot of things, and taking off gear that gives agility bonuses does, too. Cecil can be slowed down with a Drain sword or a Dwarf axe. Kain can be slowed with either of those, and a Drain spear is even better. Edge's Murasame weapon also reduces his agility. You _are_ more free to order your party, since if you have some sort of restricted anchoring restriction, the anchor isn't tied to being in the center slot, which means you might be able to take advantage of the accuracy boost in the slot. You still want to try and order your party, and through a combination of +Agility gear and putting them in priority order get a turn order that meets your needs.

And sometimes you just need to slow that battle speed down a notch or five, but without being anchored to give yourself a chance, the slowest battle speed won't help if Wyvern MegaNukes you before any turn could ever pop up.

To further your understanding of agility, and get into some of the details left out of thise, start on the [relative agility](<https://wiki.ff4fe.com/doku.php?id=relative_agility>) page in the wiki. To help clarify your anchor situation, use the `/recall flag_interactions` command and select the `Agility_vs_Hero` option.
";
    }

    public static string GetDkcHalp()
    {
        /*
        agility setup
battle speed
targeted healing
Gotta Survive, everyone living is a bonus
    (permadeath notes?)
        */
        return
@"On seeds without any of `Bunsafe`, `Cpermadeath`, or `Cpermadeader` enabled, your goal for a rough DKC fight (Levi spot, at K/Q on a Hook route) is to just get one person to survive the incoming damage. 

You'll want to
* slow down the battle speed
* slow your anchor down
* speed up the rest of the party (but don't equip an Avenger!)
* order the party so that you can toss Cures in a way that ensures at least one person survives. 

Sometimes creative party order can make sure you either have time to keep two people alive, get a life potion before DKC begins the speech, or both.

Reference the [Pain Main](<https://docs.google.com/spreadsheets/d/1w938cMyuKb_-MBAUNQG8L1ynIRGBntskT4I4mkuTgF4/edit?gid=0#gid=0>) spreadsheet to know what damage ranges you're looking to survive. For more detail about agility anchoring in this kind of fight, use the `/halp agility` command.
";
    }
}