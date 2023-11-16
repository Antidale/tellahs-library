﻿using tellahs_library.Enums;
using tellahs_library.Services;

namespace tellahs_library.Helpers
{
    public static class Pb2jFlagsetHelper
    {
        public static (string flagsetDetails, Pb2jFlagsetChoices flagsetChoice) GetFlagset(Pb2jFlagsetChoices flagsetChoices, RandomService randomService)
        {
            Pb2jFlagsetChoices[] flagsets = { Pb2jFlagsetChoices.Ladder, Pb2jFlagsetChoices.HopTillYouShop, Pb2jFlagsetChoices.Pro };

            flagsets = flagsets.Where(x => x != flagsetChoices).ToArray();
            var selectedFlagset = flagsets[randomService.Next(flagsets.Length)];
            var flagsetDetails = string.Empty;

            switch (selectedFlagset)
            {
                case Pb2jFlagsetChoices.Ladder:
                    flagsetDetails = "O1:quest_forge/2:quest_tradepink/random:1,quest,char/req:all/win:crystal Kmain/summon/moon Pkey Cstandard/nofree/j:abilities Twildish Sstandard Bstandard/alt:gauntlet Etoggle Glife/sylph/backrow -kit:basic -kit2:trap -spoon -smith:super -pushbtojump";
                    break;

                case Pb2jFlagsetChoices.HopTillYouShop:
                    flagsetDetails = "O1:quest_murasamealtar/2:quest_crystalaltar/3:quest_whitealtar/4:quest_ribbonaltar/5:quest_masamunealtar/req:4/win:game Kmain/moon/unsafe Pkey Cstandard/nofree/distinct:5/j:abilities/nekkie Twildish/money Swild/no:j Bstandard/alt:gauntlet/whichburn Etoggle/noexp/no:jdrops Gmp/warp/life/sylph/backrow -kit:better -kit2:money -kit3:money -spoon -vanilla:traps -pushbtojump";
                    break;

                case Pb2jFlagsetChoices.Pro:
                    flagsetDetails = "O1:quest_forge/2:quest_tradepink/3:quest_unlocksewer/random:2,tough_quest/req:4/win:crystal Kmain/summon/moon/unsafe Pkey Cstandard/nofree/distinct:7/no:tellah,fusoya/restrict:rydia,edward,yang,palom,porom/j:abilities/nekkie/nodupes/bye/hero Tpro/no:j Sstandard/no:j Bstandard/unsafe/alt:gauntlet Etoggle/no:jdrops Glife/backrow -kit:freedom -kit2:freedom -kit3:trap -spoon -pushbtojump";
                    break;
            }

            return (flagsetDetails, selectedFlagset);
        }
    }
}