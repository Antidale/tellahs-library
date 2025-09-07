using System;

namespace tellahs_library;

public class BoundUrlSettings
{
    public string ThumbnailHost { get; set; } = string.Empty;

    public UrlSettings ToUrlSettings() => new(ThumbnailHost);
}

public record UrlSettings(string ThumbnailHost) { }
