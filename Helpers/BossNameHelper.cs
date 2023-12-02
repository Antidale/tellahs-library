using tellahs_library.Enums;

namespace tellahs_library.Helpers
{
    public static class BossNameHelper
    {
        public static BossName GetBossName(string enteredValue) => enteredValue
                .Replace(".", string.Empty)
                .Replace(" ", string.Empty)
                .Replace("_", string.Empty)
                .Replace("-", string.Empty)
                .ToLower() switch
        {
            var mist when mist.Contains("mist") 
                || mist.Contains("derek") => BossName.DMist,

            var kaipo when kaipo.Contains("kaipo")
                || kaipo.Contains("officer")
                || kaipo.Contains("soldier") 
                || kaipo.Contains("package") => BossName.KaipoGuards,

            var octo when octo.Contains("octo")
                || octo.Contains("mamm")
                || octo.Contains("maam") => BossName.Octomamm,

            var antlion when antlion.Contains("antlion")
                || antlion.StartsWith("ant")
                || antlion.EndsWith("lion")
                || antlion.Contains("googly") => BossName.Antlion,

            var waterhag when waterhag.Contains("water")
                || waterhag.Contains("hag") => BossName.Waterhag,

            var mombomb when mombomb.Contains("bomb") 
                || mombomb.Contains("mom") => BossName.MomBomb,

            var gauntlet when gauntlet.Contains("fabul")
                || gauntlet.Contains("gauntlet") => BossName.FabulGauntlet,

            var milon when milon.Equals("milon")
                || milon.Contains("friends") 
                || milon.Contains("ordeals1") => BossName.Milon,

            var milonz when milonz.Contains("milonz")
                || milonz.Contains("hades")
                || milonz.Contains("back")
                || milonz.Contains("ordeals2") => BossName.MilonZ,

            var dkc when dkc.Contains("cecil")
                || dkc.Contains("dkc")
                || dkc.Contains("mirror") 
                || dkc.Contains("ordeals3") => BossName.DarkKnightCecil,

            var baron when baron.Contains("baron") 
                || baron.Contains("inn1") => BossName.BaronGuards,

            var karate when karate.Contains("karate")
                || karate.Contains("yang")
                || karate.Contains("inn2") => BossName.Karate,

            var baigan when baigan.Contains("baigan")
                || baigan.Contains("monster")
                || baigan.Contains("arms") => BossName.Baigan,

            var kainazzo when kainazzo.Contains("turtle")
                || kainazzo.StartsWith("cagn")
                || kainazzo.Contains("kain")
                || kainazzo.EndsWith("azzo") => BossName.Kainazzo,

            var darkElf when darkElf.Contains("elf") 
                || darkElf.Contains("music")
                || darkElf.Contains("harp")
                || darkElf.Contains("mechange") => BossName.DarkElf,

            var sisters when sisters.Contains("sandy")
                || sisters.Contains("mindy")
                || sisters.Contains("cindy")
                || sisters.Contains("sisters")
                || sisters.Contains("magus") => BossName.MagusSisters,

            var val when val.Contains("val")
                || val.Contains("barb")
                || val.Contains("iccia")
                || val.Contains("tornado") => BossName.Valvalis,

            var dolls when dolls.Contains("dolls")
                || dolls.Contains("cal")
                || dolls.Contains("calbrena") => BossName.Calbrena,

            var golbez when golbez.EndsWith("bez")
                || golbez.Contains("shadow")
                || golbez.StartsWith("lol")
                || golbez.Contains("haha") => BossName.Golbez,

            var dimps when dimps.Contains("imps") => BossName.DarkImps,

            var lugae when lugae.Contains("dr") 
                || lugae.Contains("lugae")
                || lugae.Contains("Balnab")
                || lugae.Contains("my baby") => BossName.DrLugae,

            var kqEbalan when kqEbalan.Contains("eblan")
                || kqEbalan.Contains("parents")
                || kqEbalan.Contains("kq")
                || kqEbalan.Contains("k/q") => BossName.KingQueenEblan,

            var leg when leg.Contains("leg")
                || leg.Contains("rubi")
                || leg.Contains("cant") => BossName.Rubicant,

            var cheater when cheater.Contains("cheater")
                || cheater.Contains("evil")
                || cheater.Contains("wall") => BossName.EvilWall,

            var elements when elements.Contains("elements") 
                || elements.Contains("four")
                || elements.Contains("fiends") => BossName.Elements,

            var cpu when cpu.Contains("cpu")
                || cpu.Contains("orbs")
                || cpu.Contains("defender")
                || cpu.Contains("attacker") => BossName.CPU,

            var odin when odin.Contains("odin")
                || odin.Contains("baronking") => BossName.Odin,

            var asura when asura.Contains("asura")
                || asura.Contains("ashura") => BossName.Asura,

            var levi when levi.Contains("levi") => BossName.Leviatan,

            var bahamut when bahamut.Contains("baha")
                || bahamut.Contains("value") => BossName.Bahamut,

            var paleDim when paleDim.Contains("pale") 
                || paleDim.Contains("mura")
                || paleDim.Contains("dim") => BossName.PaleDim,

            var vern when vern.Contains("blargh")
                || vern.Contains("crystal")
                || vern.Contains("sword")
                || vern.Contains("vern") => BossName.Wyvern,

            var plague when plague.Contains("plague")
                || plague.Contains("white")
                || plague.Contains("spear")
                || plague.Contains("bloo") => BossName.Plague,

            var lunars when lunars.Contains("lunar") 
                || lunars.Contains("ribbon") 
                || lunars.Contains("deez") => BossName.DLunars,

            var ogopogo when ogopogo.Contains("ogo")
                || ogopogo.Contains("noperope")
                || ogopogo.Contains("pogo") 
                || ogopogo.Contains("masa") => BossName.Ogopogo,

            var zeromus when zeromus.Contains("cosplay")
                || zeromus.Equals("z")
                || zeromus.Contains("z?")
                || zeromus.Contains("zero") 
                || zeromus.Contains("butt") => BossName.Zeromus,

            _ => BossName.Unknown
        };
    }
}
