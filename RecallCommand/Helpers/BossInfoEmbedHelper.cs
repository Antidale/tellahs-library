using FeInfo.Common.DTOs;

namespace tellahs_library.RecallCommand.Helpers
{
    public static class BossInfoEmbedHelper
    {
        public static List<DiscordMessageBuilder> GetBossInfoEmbed(BossStrategy strategy, string thumbnailHost) => [
            new DiscordMessageBuilder().EnableV2Components().AddContainerComponent(
            new DiscordContainerComponent(components:
            [
                new DiscordSectionComponent(sections:
                [
                    new DiscordTextDisplayComponent($"## [{strategy.Name}](<{strategy.WikiUrl}>)"),
                    new DiscordTextDisplayComponent(strategy.FightFlow),

                ],
                accessory: new DiscordThumbnailComponent($"{thumbnailHost}{strategy.Thumbnail}")),
                new DiscordTextDisplayComponent(strategy.Strats),
                new DiscordTextDisplayComponent(strategy.AdditionalInfo),
                new DiscordTextDisplayComponent(string.Join("\n", strategy.Fields.Select(x => $"**{x.Key}**: {x.Value}")))
            ],

            color: DiscordColor.Azure)
        )];
    }
}
