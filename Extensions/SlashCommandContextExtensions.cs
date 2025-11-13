
namespace tellahs_library.Extensions;

public static class SlashCommandContextExtensions
{
    extension(SlashCommandContext ctx)
    {
        public async ValueTask RespondAsync(List<DiscordEmbed> embeds, bool ephemeral = false)
        {
            await ctx.RespondAsync(embeds.First(), ephemeral);

            var extraEmbeds = embeds.Skip(1).ToList();

            foreach (var embed in extraEmbeds)
            {
                await ctx.FollowupAsync(embed, ephemeral);
            }
        }
    }
}
