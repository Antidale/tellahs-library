using System;

namespace tellahs_library.Entities;

public class ActiveRace
{
    public string MessageIdString { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Goal { get; set; } = string.Empty;
    public int EntrantCount { get; set; }
}
