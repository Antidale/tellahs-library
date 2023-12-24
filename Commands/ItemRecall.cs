using DSharpPlus.SlashCommands;
using tellahs_library.Enums;
using static tellahs_library.Helpers.ItemHelper;

namespace tellahs_library.Commands
{
    public class ItemRecall : ApplicationCommandModule
    {
        [SlashCommand("item", "provides some information about select consumable items")]
        public async Task ItemRecallAsync(InteractionContext ctx,
            [Option("item", "get information about important consumable items")] ItemRecallOptions selectedItem,
            [Option("justme", "only show for yourself")] bool isEphemeral = true)
        {
            var embed = GetItemNotes(selectedItem);

            await ctx.CreateResponseAsync(embed, isEphemeral);
        }
    }
}
