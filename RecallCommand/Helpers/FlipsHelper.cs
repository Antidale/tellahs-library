using System;

namespace tellahs_library.RecallCommand.Helpers;

public class FlipsHelper
{
    public static async Task<string> CreateBpsPatchAsync(string fileName)
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

        flips.Start();

        //consider redirecting error output, too, and capturing that, assuming flips would give error output.
        var output = await flips.StandardOutput.ReadToEndAsync();
        await flips.WaitForExitAsync();
        return outputPatchName;
    }
}
