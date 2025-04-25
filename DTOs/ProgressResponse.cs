using System;

namespace tellahs_library.DTOs;

/// <summary>
/// A class encapsulating the data that is returned from hitting the /generate or /task endpoints for Free Enterprise seed generation
/// </summary>
public class ProgressResponse
{
    public string Status { get; set; } = string.Empty;
    public string task_id { get; set; } = string.Empty;
    public string seed_id { get; set; } = string.Empty;
    public string Error { get; set; } = string.Empty;
}
