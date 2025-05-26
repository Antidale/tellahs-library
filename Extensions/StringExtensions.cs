using System;

namespace tellahs_library.Extensions;

public static class StringExtensions
{
    public static bool HasContent(this string value) => !string.IsNullOrWhiteSpace(value);
    public static string SortFlags(this string value) => string.Join(' ', value.Split(' ').OrderBy(x => x)).Trim();
}
