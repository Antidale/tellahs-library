
using DSharpPlus.Commands.Processors.SlashCommands.ArgumentModifiers;
using tellahs_library.RacingCommands.Enums;

namespace tellahs_library.RacingCommands.Helpers;

public static class AlertMessageHelper
{
    public static DiscordMessageBuilder CreateAlertMessage(SlashCommandContext ctx, string description, string raceUrl, bool shouldPing, RtggGoal goal)
    {
        var raceDetailsText =
@$"**Goal**: {goal.GetAttribute<ChoiceDisplayNameAttribute>()?.DisplayName ?? goal.ToString()}
**URL**: {raceUrl}
-# **Created by**: {ctx.Member!.DisplayName}";

        var messageBuilder = new DiscordMessageBuilder()
            .EnableV2Components()
            .AddContainerComponent(new DiscordContainerComponent(
                [
                    new DiscordTextDisplayComponent($"### {description}"),
                    new DiscordTextDisplayComponent(raceDetailsText)
                ],
                color: DiscordColor.Grayple
            ));

        AddRolePing(ctx, messageBuilder, shouldPing);

        return messageBuilder;
    }

    public static DiscordMessageBuilder Create1v1AlertMessage(SlashCommandContext ctx, string description, string raceUrl, List<DiscordUser> pingUsers, AfcFlagset flagset)
    {
        var mentions = string.Join(" ", pingUsers.Select(x => x.Mention));

        var flagsetName = flagset.GetAttribute<ChoiceDisplayNameAttribute>()?.DisplayName ?? flagset.GetDescription();

        var color = flagset switch
        {
            AfcFlagset.Ace => new DiscordColor(0x125740),
            AfcFlagset.Fbf => new DiscordColor(0x002C5F),
            AfcFlagset.Zza => new DiscordColor(0xA71930),
            _ => DiscordColor.Brown
        };

        var raceDescriptionText = $"### {description}";
        var receDetailText = @$"**URL**: {raceUrl}
**Flagset**: `{flagsetName}`
**Racers**: {mentions}
-# **Created by**: {ctx.Member!.DisplayName}";

        var messageBuilder = new DiscordMessageBuilder()
            .EnableV2Components()
            .AddContainerComponent(
                new DiscordContainerComponent(
                components:
                [
                    new DiscordTextDisplayComponent(raceDescriptionText),
                    new DiscordTextDisplayComponent(receDetailText)
                ],
                color: color)
            );

        pingUsers.ForEach(user =>
        {
            messageBuilder.WithAllowedMention(new UserMention(user));
        });

        return messageBuilder;
    }

    private static void AddRolePing(SlashCommandContext ctx, DiscordMessageBuilder messageBuilder, bool shouldPing)
    {
        //Don't have the workshop's ping-to-race role ID to just hard code so we have to pull it from the context
        var pingRole = ctx.Guild?.Roles.FirstOrDefault(x =>
            x.Value.Name
                .Replace("-", string.Empty)
                .Replace("_", string.Empty)
                .Equals("pingtorace", StringComparison.InvariantCultureIgnoreCase))
            .Value;

        if (pingRole is null) { shouldPing = false; }

        if (shouldPing)
        {
            messageBuilder.AddTextDisplayComponent(new DiscordTextDisplayComponent($"{pingRole!.Mention}"));
            messageBuilder.WithAllowedMention(new RoleMention(pingRole!));
        }
    }
}
