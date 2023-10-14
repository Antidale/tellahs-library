using DSharpPlus.SlashCommands;
using DSharpPlus.SlashCommands.Attributes;

namespace tellahs_library.Commands
{
    public class Tournament : ApplicationCommandModule
    {
        //[SlashCommand("ScheduleRace", "Creates an event")]
        //[GuildOnly]
        //public async Task SetRace(InteractionContext ctx,
        //    [Option("raceName", "who is racing who")] string raceName,
        //    [Option("Time", "")] string timeString,
        //    [Option("day", "")] int day,
        //    //month
        //    [Choice("Jan", 1)]
            
        //    [Option("month", "")] int month = 0,
            
        //    [Option("year", "")] int year = 0
        //)
        //{
            
        //}

        
        [SlashCommand("StartPoisonPicking", "Initiates the flag draft")]
        public async Task StartDraftAsync(InteractionContext ctx,
            [Option("player1", "player one")] string player1,
            [Option("player2", "player two")] string player2,
            [Option("player3", "player three (optional)")] string player3 = "",
            [Option("player4", "player four (optional)")] string player4 = "",
            [Option("player5", "player five (optional)")] string player5 = ""
            )
        {
            await ctx.CreateResponseAsync("okay");
            var playerString = string.Join("|", player1, player2, player3, player4, player5);

        }
    }
}
