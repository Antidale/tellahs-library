using System.ComponentModel;

namespace tellahs_library.HalpCommand;

[Command("halp")]
public class Halp
{
    [Command("wyvern")]
    [Description("common techniques for dealing with Wyvern")]
    public async Task WyvernAsync(SlashCommandContext ctx)
    {
        /*
            agility setup
            battle speed
            starveils (note about whyburn, and maybe whichburn?)
                then zerking (bacchus, avenger)
        */
    }

    [Command("kainazzo")]
    [Description("common techniques for dealing with Kainazzo")]
    public async Task KainazzoAsync(SlashCommandContext ctx)
    {
        /*
            agility setup
            thor rage (or other lit items, then spells)
            battle speed
            zerk buffering
        */
    }

    [Command("valvalis")]
    [Description("common techniques for dealing with Valvalis")]
    public async Task ValvalisAsync(SlashCommandContext ctx)
    {
        /*
            nuke-able spots
            +str gear on a single person
            dart/kick/aim/dark to pierce spin
            jump to stop the spin
        */
    }

    [Command("agility")]
    [Description("common techniques for dealing with agility issues")]
    public async Task AgilityAsync(SlashCommandContext ctx)
    {
        /*
            determine your anchor
            slow the anchor down
            speed other people up
            reorganize the party
        */
    }

    [Command("dkc")]
    [Description("common techniques for dealing with DKC")]
    public async Task DkcAsync(SlashCommandContext ctx)
    {
        /*
            agility setup
            battle speed
            targeted healing
            Gotta Survive, everyone living is a bonus
                (permadeath notes?)
        */
    }
}
