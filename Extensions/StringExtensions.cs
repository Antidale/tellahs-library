using System;

namespace tellahs_library.Extensions;

public static class StringExtensions
{
    public static void ExitIfEmpty(this string value, string propertyName = "value")
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            Console.WriteLine($"{propertyName} not found. Check environment variables");
            Environment.Exit(exitCode: 1);
        }
    }
    public static bool HasContent(this string value) => !string.IsNullOrWhiteSpace(value);
    public static string SortFlags(this string value) => string.Join(' ', value.Split(' ').OrderBy(x => x)).Trim();
}
