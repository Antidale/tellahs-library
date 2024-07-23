using DSharpPlus.Commands;
using DSharpPlus.Commands.Processors.SlashCommands;
using DSharpPlus.Commands.Trees.Metadata;
using System.ComponentModel;
using tellahs_library.Enums;
using tellahs_library.Extensions;
using static tellahs_library.Helpers.Pb2jFlagsetHelper;

namespace tellahs_library.Commands
{
    public class FlagsetChooser
    {
        [Command("selectpb2jflagset"), Description("Selects one non-vetoed PB2J flagset at Random"), AllowDMUsage]
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
