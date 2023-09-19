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
                    || antlion.Contains("googly") => BossName.Antlion,
            
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
            
            var golbez when golbez.EndsWith("bez")
                    || golbez.Contains("shadow") 
                    || golbez.Contains("haha")=> BossName.Golbez,

            var cpu when cpu.Contains("cpu")
                    || cpu.Contains("orbs")
                    || cpu.Contains("defender")
                    || cpu.Contains("attacker") => BossName.CPU,



            _ => BossName.Unknown
        };
    }
}
