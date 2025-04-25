using System;

namespace tellahs_library.DTOs;

public class SeedResponse
{
    public string Status { get; set; } = string.Empty;
    public string Version { get; set; } = string.Empty;
    public string Seed { get; set; } = string.Empty;
    public string Flags { get; set; } = string.Empty;
    public string Verification { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
    public string Error { get; set; } = string.Empty;
}
