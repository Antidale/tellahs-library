using DSharpPlus.SlashCommands;
using tellahs_library.Enums;
using tellahs_library.Services;
using static tellahs_library.Helpers.Pb2jFlagsetHelper;

namespace tellahs_library.Commands
{
    [SlashCommandGroup("FlagsetChoice", "Ways to select flagsets")]
    public class FlagsetChoice : ApplicationCommandModule
    {
        public RandomService? RandomService { get; set; }

        [SlashCommand("SelectPB2JFlagset", "Selects one non-vetoed PB2J flagset at Random")]
        public async Task SelectPB2JFlagsetAsync(
            InteractionContext ctx,
            [Option("VetoChoice", "Flagset that's vetoed")] Pb2jFlagsetChoices pb2JFlagsetChoice
        )
        {
            if (RandomService == null)
            {
                await ctx.CreateResponseAsync("I'm sorry, something's wrong");
                return;
            }

            var (flagsetDetails, selectedFlagset) = GetFlagset(pb2JFlagsetChoice, RandomService);

            await ctx.CreateResponseAsync(
$@"Vetoed Set: {pb2JFlagsetChoice.GetName()}
Random Set: {selectedFlagset.GetName()}
```
{flagsetDetails}
```");
        }
    }
}
