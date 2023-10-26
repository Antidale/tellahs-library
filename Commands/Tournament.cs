﻿using DSharpPlus.SlashCommands;
using DSharpPlus.SlashCommands.Attributes;
using tellahs_library.Enums;

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

        [SlashCommand("SelectPB2JFlagset", "stuff")]
        public async Task SelectPB2JFlagsetAsync(InteractionContext ctx,
            [Option("VetoChoice", "Flagset that's vetoed")]Pb2jFlagetChoices pb2JFlagsetChoice)
        {
            Pb2jFlagetChoices[] flagsets = { Pb2jFlagetChoices.Ladder, Pb2jFlagetChoices.HopTillYouShop, Pb2jFlagetChoices.Pro };

            flagsets = flagsets.Where(x => x != pb2JFlagsetChoice).ToArray();
            var rand = new Random();
            var selectedFlagset = flagsets[rand.Next(flagsets.Length)];
            var flagsetDetails = string.Empty;
            switch (selectedFlagset) 
            {
                case Pb2jFlagetChoices.Ladder:
                    flagsetDetails = "O1:quest_forge/2:quest_tradepink/random:1,quest,char/req:all/win:crystal Kmain/summon/moon Pkey Cstandard/nofree/j:abilities Twildish Sstandard Bstandard/alt:gauntlet Etoggle Glife/sylph/backrow -kit:basic -kit2:trap -spoon -smith:super -pushbtojump";
                    break;

                case Pb2jFlagetChoices.HopTillYouShop:
                    flagsetDetails = "O1:quest_murasamealtar/2:quest_crystalaltar/3:quest_whitealtar/4:quest_ribbonaltar/5:quest_masamunealtar/req:4/win:game Kmain/moon/unsafe Pkey Cstandard/nofree/distinct:5/j:abilities/nekkie Twildish/money Swild/no:j Bstandard/alt:gauntlet/whichburn Etoggle/noexp/no:jdrops Gmp/warp/life/sylph/backrow -kit:better -kit2:money -kit3:money -spoon -vanilla:traps -pushbtojump";
                   break;

                case Pb2jFlagetChoices.Pro:
                    flagsetDetails = "O1:quest_forge/2:quest_tradepink/3:quest_unlocksewer/random:2,tough_quest/req:4/win:crystal Kmain/summon/moon/unsafe Pkey Cstandard/nofree/distinct:7/no:tellah,fusoya/restrict:rydia,edward,yang,palom,porom/j:abilities/nekkie/nodupes/bye/hero Tpro/no:j Sstandard/no:j Bstandard/unsafe/alt:gauntlet Etoggle/no:jdrops Glife/backrow -kit:freedom -kit2:freedom -kit3:trap -spoon -pushbtojump";
                   break;
            }

            await ctx.CreateResponseAsync(
$@"Vetoed Set: {pb2JFlagsetChoice.GetName()}
Random Set: {selectedFlagset.GetName()}
```
{flagsetDetails}
```");
        }
    }
}
