using DSharpPlus.SlashCommands;

namespace tellahs_library.Commands
{
    public class Recall : ApplicationCommandModule
    {
        //[SlashCommand("ping", "pong")]
        //public async Task PingAsync(InteractionContext ctx)
        //{
        //    await ctx.CreateResponseAsync("pong");
        //}

        [SlashCommand("recall", "search the library for information")]
        public async Task RecallAsync(InteractionContext ctx)
        {
            await ctx.CreateResponseAsync("No library attendants are available to help you yet. For now, check <https://wiki.ff4fe.com>");
        }

        [SlashCommand("racing", "get information about racing Free Enterprise")]
        public async Task RacingAsync(InteractionContext ctx)
        {
            await ctx.CreateResponseAsync("you don't have a library card");
        }

    }
}
