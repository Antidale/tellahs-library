using DSharpPlus.Commands.EventArgs;

namespace tellahs_library.EventHandlers
{
    public class CommandsEventHanlders
    {
        public static async Task OnCommandInvokedAsync(CommandsExtension _, CommandExecutedEventArgs eventArgs)
        {
            await eventArgs.Context.LogUsageAsync();
        }

        public static async Task OnCommandErroredAsync(CommandsExtension _, CommandErroredEventArgs eventArgs)
        {
            await eventArgs.Context.LogErrorAsync("Whoops. Not fully implemented yet", eventArgs.Exception);
        }
    }
}
