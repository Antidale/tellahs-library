using Microsoft.Extensions.Logging;
using tellahs_library.Constants;

namespace tellahs_library.Extensions
{
    public static class CommandContextExtensions
    {
        extension(CommandContext ctx)
        {
            public async Task<DiscordMessage?> GetMessageAsync(ulong channelId, ulong messageId)
            {
                try
                {
                    if (ctx.Guild is not null)
                    {
                        var channel = await ctx.Guild.GetChannelAsync(channelId);
                        return await channel.GetMessageAsync(messageId);
                    }
                    else
                    {
                        return null;
                    }

                }
                catch (Exception)
                {
                    return null;
                }
            }

            public async Task<DiscordMessage?> EditResponseAsync(string updatedMessage)
            {
                return await ctx.EditResponseAsync(new DiscordWebhookBuilder().WithContent(updatedMessage));
            }

            public async Task<DiscordMessage?> EditResponseAsync(DiscordEmbed embed)
            {
                return await ctx.EditResponseAsync(new DiscordWebhookBuilder().AddEmbed(embed));
            }

            public async Task<DiscordMessage?> EditResponseAsync(List<DiscordEmbed> embeds)
            {
                var builder = new DiscordWebhookBuilder();
                embeds.ForEach(embed => builder.AddEmbed(embed));
                return await ctx.EditResponseAsync(builder);
            }

            public async Task<List<DiscordMessage>> EditResponseAsync(List<DiscordMessageBuilder> messages)
            {
                //Maybe should not hide this, to prevent the person feeling like the application is hanging in case we generate no messages.
                if (messages.Count == 0)
                {
                    return [];
                }

                var returnMessages = new List<DiscordMessage>(capacity: messages.Count);

                var firstResponse = await ctx.EditResponseAsync(messages.First());
                returnMessages.Add(firstResponse);
                messages.RemoveAt(0);
                foreach (var message in messages)
                {
                    var followup = await ctx.FollowupAsync(message);
                    returnMessages.Add(followup);
                }

                return returnMessages;
            }

            public async Task LogErrorAsync(string message, Exception? ex = null)
            {
                if (ex != null)
                {
                    ctx.Client.Logger.LogError("Exception: {ex}", ex.ToString());
                }

                var guild = ctx.Client.Guilds[GuildIds.BotHome];
                if (guild is null) { return; }

                var channel = guild.Channels[ChannelIds.BotLogsChannelId];
                if (channel is null) { return; }

                //Odds are good we don't need the whole message here, and this feels like a good enough arbitrary number to know what's going on.
                if (message.Length > 1700)
                {
                    message = message[..1700];
                }

                await channel.SendMessageAsync(string.Join("\r\n", ctx.User.Username, message, ex?.GetType()));
            }

            public async Task LogErrorAsync(string responseMessage, string errorMessage, Exception? ex = null)
            {
                await ctx.EditResponseAsync(responseMessage);
                await LogErrorAsync(ctx, errorMessage, ex);
            }

            public async Task LogUsageAsync()
            {
                var guild = ctx.Client.Guilds[GuildIds.BotHome];
                if (guild is null) { return; }

                var channel = guild.Channels[ChannelIds.BotUsageChannelId];
                if (channel is null) { return; }

                var invokingGuild = ctx.Guild;

                var guildString = invokingGuild is null
                    ? "a DM"
                    : $"{invokingGuild.Name}";

                var message = $"{ctx.Command.Name} was invoked in {guildString}";

                await channel.SendMessageAsync(message);
            }
        }
    }
}
