﻿using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using Microsoft.Extensions.Logging;
using System.Diagnostics.CodeAnalysis;
using tellahs_library.Constants;

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

        [SuppressMessage("Usage", "CA2254:Template should be a static expression", Justification = "No structured logging use")]
        public static async Task LogErrorAsync(this InteractionContext ctx, string message, Exception? ex = null)
        {
            if (ex != null)
            {
                ctx.Client.Logger.LogError(message: ex.ToString());
            }

            var guild = ctx.Client.Guilds[GuildIds.BotHome];
            if (guild == null) { return; }

            var channel = guild.Channels[ChannelIds.BotLogsChannelId];
            if (channel == null) { return; }

            if (message.Length > 2000)
            {
                message = message[..2000];
            }

            await channel.SendMessageAsync(string.Join("\r\n", message, ex?.GetType()));
        }

        public static async Task LogErrorAsync(this InteractionContext ctx, string responseMessage, string errorMessage, Exception? ex = null)
        {
            await ctx.EditResponseAsync(responseMessage);
            await LogErrorAsync(ctx, errorMessage, ex);
        }
    }
}