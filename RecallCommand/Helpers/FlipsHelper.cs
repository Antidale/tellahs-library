using System;
using System.Data;
using Microsoft.Extensions.Logging;

namespace tellahs_library.RecallCommand.Helpers;

public class FlipsHelper
{
    public static async Task<string> CreateBpsPatchAsync(string fileName, CommandContext ctx)
    {
        var flipsPath = Environment.GetEnvironmentVariable("TL_FLIPS_PATH");
        var romPath = Environment.GetEnvironmentVariable("TL_FE_ROM_PATH");
        var outputPatchName = $"{Guid.NewGuid()}.bps";

        using var flips = new System.Diagnostics.Process();
        flips.StartInfo.FileName = flipsPath;
        flips.StartInfo.Arguments = $"{romPath} {fileName} {outputPatchName}";
        flips.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
        flips.StartInfo.UseShellExecute = false;
        flips.StartInfo.CreateNoWindow = true;
        flips.StartInfo.RedirectStandardOutput = true;
        flips.StartInfo.RedirectStandardError = true;

        flips.Start();

        //consider redirecting error output, too, and capturing that, assuming flips would give error output.
        var output = await flips.StandardOutput.ReadToEndAsync();
        var error = await flips.StandardError.ReadToEndAsync();
        if (!output.Contains("successfully!"))
        {
            throw new InvalidOperationException($"output: {output}\r\nerror: {error}");
        }
        await flips.WaitForExitAsync();
        return outputPatchName;
    }
}
