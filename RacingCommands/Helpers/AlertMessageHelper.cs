using System;

namespace tellahs_library.RacingCommands.Helpers;

public static class AlertMessageHelper
{
    public static DiscordMessageBuilder CreateAlertMessage(SlashCommandContext ctx, string description, string raceUrl, bool shouldPing)
    {
        //Don't have the workshop's ping-to-race role ID to just hard code so we have to pull it from the context
        var pingRole = ctx.Guild?.Roles.FirstOrDefault(x =>
            x.Value.Name
                .Replace("-", string.Empty)
                .Replace("_", string.Empty)
                .Equals("pingtorace", StringComparison.InvariantCultureIgnoreCase))
            .Value;

        if (pingRole is null) { shouldPing = false; }

        var raceDescriptionText = shouldPing
            ? $"{pingRole!.Mention}\n### {description}"
            : description;

        var messageBuilder = new DiscordMessageBuilder()
            .EnableV2Components()
            .AddTextDisplayComponent(new DiscordTextDisplayComponent(raceDescriptionText))
            .AddSeparatorComponent(new DiscordSeparatorComponent(divider: true, spacing: DiscordSeparatorSpacing.Small))
            .AddTextDisplayComponent(new DiscordTextDisplayComponent(@$"### Race Info:
URL: {raceUrl}
Created by: {ctx.Member!.DisplayName}"));

        if (shouldPing)
        {
            messageBuilder.WithAllowedMention(new RoleMention(pingRole!));
        }

        return messageBuilder;
    }
}
