using DSharpPlus.SlashCommands;
using static tellahs_library.Helpers.BossInfoEmbedHelper;
using static tellahs_library.Helpers.BossNameHelper;

namespace tellahs_library.Commands
{
    public class BossRecall : ApplicationCommandModule
    {
        [SlashCommand("boss", "Get boss info")]
        public async Task BossRecallAsync(InteractionContext ctx,
            [Option("BossName", "the boss you want info on")] string bossName,
            [Option("justme", "makes the response only visible to you")] bool isEphemeral = false)
        {
            var bossEnum = GetBossName(bossName);
            var embed = GetBossInfoEmbed(bossEnum);
            await ctx.CreateResponseAsync(embed, isEphemeral);
        }
    }
}
