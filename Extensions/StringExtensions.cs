using System;

namespace tellahs_library.Extensions;

public static class StringExtensions
{
    public static bool HasContent(this string value) => !string.IsNullOrWhiteSpace(value);
}
