using System.ComponentModel;

namespace tellahs_library.Commands;

[Command("roll")]
[Description("A set of commands for rolling seeds")]
public class SeedRoller
{
    [Command("flags")]
    [Description("Generate a seed from an arbitrary flag string")]
    public async Task RollFlagsAsync(
        CommandContext ctx,
        [Parameter("flags")]
        [Description("")]
        string flags
        )
    {
        await ctx.DeferResponseAsync();

        await ctx.EditResponseAsync("rolling seed");

        /* TODO: 
         * call endpoint to start requesting the seed
         * get back the seedid, a task id, or an error
            * seedId:
                *
            * taskId:
                * poll the task endpoint until you get a seed back, or you hit a timeout
            * error:
                * print out error 
         */

    }

    [Command("preset")]
    [Description("rolls a seed from a predefined preset")]
    public async Task RollPresetAsync(CommandContext ctx,
        [Parameter("preset_choice")]
        [Description("")]
        string flags)
    {
        await ctx.DeferResponseAsync();

        await ctx.EditResponseAsync("rolling seed");
    }

}