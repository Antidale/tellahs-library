using System;

namespace tellahs_library.RacingCommands.Requests;

public class CreateRace
{
    public string Goal { get; set; } = string.Empty;
    public bool TeamRace { get; set; } = false;
    public bool Invidational { get; set; } = false;
    public bool Unlisted { get; set; } = false;
    public bool Partitionable { get; set; } = false;
    public bool HideEntrants { get; set; } = false;
    public bool Ranked { get; set; } = true;
    public string InfoUser { get; set; } = string.Empty;
    public string InfoBot { get; set; } = string.Empty;
    public bool RequireEvenTeams { get; set; } = false;
    public int StartDelay { get; set; } = 15;
    public int TimeLimit { get; set; } = 24;
    public bool TimeLimitAutoComplete { get; set; } = false;
    public bool StreamingRequired { get; set; } = true;
    public bool AutoStart { get; set; } = true;
    public bool AllowComments { get; set; } = true;
    public bool HideComments { get; set; } = true;
    public bool AllowPreraceChat { get; set; } = true;
    public bool AllowMidraceChat { get; set; } = true;
    public bool AllowNonEntrantChat { get; set; } = false;
    public int ChatMessageDelay { get; set; } = 0;
}
