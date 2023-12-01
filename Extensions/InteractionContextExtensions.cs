using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;

namespace tellahs_library.Extensions
{
    public static class InteractionContextExtensions
    {
        public static async Task<DiscordMessage?> GetMessageAsync(this InteractionContext ctx, ulong channelId, ulong messageId)
        {
            try
            {
                return await ctx.Guild.GetChannel(channelId).GetMessageAsync(messageId);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static async Task<DiscordMessage?> EditResponseAsync(this InteractionContext ctx, string updatedMessage)
        {
            return await ctx.EditResponseAsync(new DiscordWebhookBuilder().WithContent(updatedMessage));
        }
    }
}
