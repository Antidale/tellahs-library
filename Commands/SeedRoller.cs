using System.ComponentModel;

namespace tellahs_library.Commands;


public class SeedRoller
{
    [Command("Roll")]
    [Description("Generate a seed")]
    public async Task RollAsync(
        CommandContext ctx,
        [Parameter("flags")]
        [Description("")]
        string flags
        )
    {
        await ctx.DeferResponseAsync();
        
        await ctx.EditResponseAsync("rolling seed");
        
    }
    
}