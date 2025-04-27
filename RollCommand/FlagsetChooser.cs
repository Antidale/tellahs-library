using System.ComponentModel;
using DSharpPlus.Commands.Processors.SlashCommands.Metadata;
using DSharpPlus.Commands.Trees.Metadata;
using tellahs_library.Enums;
using static tellahs_library.RollCommand.Helpers.Pb2jFlagsetHelper;

namespace tellahs_library.RollCommand
{
    public class FlagsetChooser
    {
        [Command("selectpb2jflagset"), Description("Selects one non-vetoed PB2J flagset at Random"), AllowDMUsage, InteractionInstallType(DiscordApplicationIntegrationType.GuildInstall, DiscordApplicationIntegrationType.UserInstall)]
        public async Task SelectPB2JFlagsetAsync(
            SlashCommandContext ctx,
            [Parameter("VetoChoice"), Description("Flagset to veto")]
            Pb2jFlagsetChoices pb2JFlagsetChoice
        )
        {
            var (flagsetDetails, selectedFlagset) = GetFlagset(pb2JFlagsetChoice);

            await ctx.RespondAsync(
$@"Vetoed Set: {pb2JFlagsetChoice.GetDescription()}
Random Set: {selectedFlagset.GetDescription()}
```
{flagsetDetails}
```");
        }
    }
}
