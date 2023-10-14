using DSharpPlus.SlashCommands;

namespace tellahs_library.Commands
{
    public class Recall : ApplicationCommandModule
    {

        [SlashCommand("recall", "search the library for information")]
        public async Task RecallAsync(InteractionContext ctx)
        {
            await ctx.CreateResponseAsync("No library attendants are available to help you yet. For now, check <https://wiki.ff4fe.com>");
        }

        [SlashCommand("racing", "get information about racing Free Enterprise")]
        public async Task RacingAsync(InteractionContext ctx)
        {
            await ctx.CreateResponseAsync(@"
### Community Races
Community races are held on Mondays at <t:1695168000:t> and Fridays at <t:1695175200:t>, with race rooms generally open about an hour before the race starts. Races are open to all
See [Community Races](<https://wiki.ff4fe.com/doku.php?id=community_races>) on the wiki for more info and links.
### Racing Clubs
See [Fleury's site](<https://adaptable-rabbit.surge.sh/events>) for a listing of clubs both active and those past. Some clubs are not for FE. The wiki also has a [Racing Clubs](<https://wiki.ff4fe.com/doku.php?id=racing_clubs>) page, although that is currently out of date.
### Racing Guides
* [A general guide](<https://docs.google.com/document/d/18ab5ejhqr_iwQ0e6m04BB0Nf2dlFaO5mw6fpbWie3Q4/>) to dr-race-bot
* [Another guide](<http://bit.ly/FF4FE-Bootcamp>) that also has some stream setup help. Normal/non-tournament races don't require stream delay, so skip that part of any instructions
* [2v2 Racing](<https://docs.google.com/document/d/102eUr6DBE93AmXrIP7gZHhKHH22RXJz2KBc_aSQDITo>)
* dr-race-bot [commands](<https://gitlab.com/akw5013/discord-race-bot/blob/master/HELP.md>)
");
        }

    }
}
