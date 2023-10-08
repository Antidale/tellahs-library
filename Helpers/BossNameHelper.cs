using System.Diagnostics.CodeAnalysis;
using tellahs_library.Enums;

namespace tellahs_library.Helpers
{
    public static class BossNameHelper
    {
        public static BossName GetBossName(string enteredValue) => enteredValue
                .Replace(".", string.Empty)
                .Replace(" ", string.Empty)
                .ToLower() switch
        {
            var mist when mist.Contains("mist") => BossName.DMist,

            var kaipo when kaipo.Contains("kaipo")
                    || kaipo.Contains("officer")
                    || kaipo.Contains("soldier") => BossName.KaipoGuards,

            var octo when octo.Contains("octo")
                    || octo.Contains("mamm") => BossName.Octomamm,

            var antlion when antlion.Contains("antlion")
                    || antlion.StartsWith("ant")
                    || antlion.EndsWith("lion")
                    || antlion.Contains("googly") => BossName.Antlion,

            var waterhag when waterhag.Contains("water")
                    || waterhag.Contains("hag") => BossName.Waterhag,

            var mombomb when mombomb.Contains("bomb") => BossName.MomBomb,

            var gauntlet when gauntlet.Contains("fabul")
                    || gauntlet.Contains("gauntlet") => BossName.FabulGauntlet,

            var milon when milon.Equals("milon")
                    || milon.Contains("friends") => BossName.Milon,

            var milonz when milonz.Contains("milonz")
                    || milonz.Contains("hades") => BossName.MilonZ,

            var dkc when dkc.Contains("cecil")
                    || dkc.Contains("dkc")
                    || dkc.Contains("mirror") => BossName.DarkKnightCecil,

            var baron when baron.Contains("baron") => BossName.BaronGuards,

            var karate when karate.Contains("karate")
                    || karate.Contains("yang") => BossName.Karate,

            var baigan when baigan.Contains("baigan")
                    || baigan.Contains("monster")
                    || baigan.Contains("arms") => BossName.Baigan,

            var kainazzo when kainazzo.Contains("turtle")
                    || kainazzo.EndsWith("azzo") => BossName.Kainazzo,

            var sisters when sisters.Contains("sandy")
                    || sisters.Contains("mindy")
                    || sisters.Contains("cindy")
                    || sisters.Contains("sisters")
                    || sisters.Contains("magus") => BossName.MagusSisters,

            var val when val.Contains("val")
                    || val.Contains("iccia")
                    || val.Contains("tornado") => BossName.Valvalis,

            var dolls when dolls.Contains("dolls")
                    || dolls.Contains("calbrena") => BossName.Calbrena,

            var golbez when golbez.EndsWith("bez")
                    || golbez.Contains("shadow")
                    || golbez.Contains("haha") => BossName.Golbez,

            var kqEbalan when kqEbalan.Contains("eblan")
                    || kqEbalan.Contains("parents")
                    || kqEbalan.Contains("kq")
                    || kqEbalan.Contains("k/q") => BossName.KingQueenEblan,

            var leg when leg.Contains("leg")
                    || leg.Contains("rubi")
                    || leg.Contains("cant") => BossName.Rubicant,

            var cheater when cheater.Contains("cheater")
                    || cheater.Contains("wall") => BossName.EvilWall,

            var elements when elements.Contains("elements") => BossName.Elements,

            var cpu when cpu.Contains("cpu")
                    || cpu.Contains("orbs")
                    || cpu.Contains("defender")
                    || cpu.Contains("attacker") => BossName.CPU,

            var odin when odin.Contains("odin")
                    || odin.Contains("baronking") => BossName.Odin,

            var asura when asura.Contains("asura")
                    || asura.Contains("ashura") => BossName.Asura,

            var levi when levi.Contains("levi") => BossName.Leviatan,

            var bahamut when bahamut.Contains("baha") => BossName.Bahamut,

            var paleDim when paleDim.Contains("pale") => BossName.PaleDim,

            var vern when vern.Contains("blargh")
                    || vern.Contains("vern") => BossName.Wyvern,

            var plague when plague.Contains("plague")
                    || plague.Contains("bloo") => BossName.Plague,

            var lunars when lunars.Contains("lunar") => BossName.DLunars,

            var ogopogo when ogopogo.Contains("ogo")
                    || ogopogo.Contains("noperope")
                    || ogopogo.Contains("pogo") => BossName.Ogopogo,

            var zeromus when zeromus.Contains("cosplay")
                    || zeromus.Contains("z?")
                    || zeromus.Contains("zero") => BossName.Zeromus,

            _ => BossName.Unknown
        };
    }
}
