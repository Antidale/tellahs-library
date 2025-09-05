namespace tellahs_library.RacingCommands;

public record class RaceMessage
{
    public string Description { get; set; } = string.Empty;
    public string Goal { get; set; } = string.Empty;
    public int EntrantCount { get; set; }
    public string Status { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
    public uint MessageIdString { get; set; }
}
