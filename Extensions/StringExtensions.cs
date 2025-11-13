
namespace tellahs_library.Extensions;

public static class StringExtensions
{
    extension(string value)
    {
        public void ExitIfEmpty(string propertyName = "value")
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                Console.WriteLine($"{propertyName} not found. Check environment variables");
                Environment.Exit(exitCode: 1);
            }
        }
        public string SortFlags() => string.Join(' ', value.Split(' ').OrderBy(x => x)).Trim();

        public bool IsSnesRom() => value.EndsWith(".smc") || value.EndsWith(".sfc");
        public bool HasContent() => !string.IsNullOrWhiteSpace(value);
    }
}
