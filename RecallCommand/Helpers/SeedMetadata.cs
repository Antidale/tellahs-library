using System;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using tellahs_library.Converters;

namespace tellahs_library.RecallCommand.Helpers;

public class SeedMetadata
{
    [JsonPropertyName("version")]
    public string Version { get; init; } = string.Empty;

    [JsonPropertyName("flags")]
    public string Flags { get; init; } = string.Empty;

    [JsonPropertyName("seed")]
    public string Seed { get; init; } = string.Empty;

    [JsonPropertyName("binary_flags")]
    public string BinaryFlags { get; init; } = string.Empty;

    [JsonPropertyName("verification")]
    public List<string> Verification { get; set; } = [];

    [JsonPropertyName("metadata_addr")]
    [JsonConverter(typeof(HexStringToUintConverter))]
    public uint MetadataAddress { get; init; } = 0;

    [JsonPropertyName("metadata_len")]
    [JsonConverter(typeof(HexStringToIntConverter))]
    public int MedataLength { get; init; } = 0;

    public string VerificationString => string.Join(", ", Verification);

    public override string ToString()
    {
        return @$"version: {Version}
flags: {Flags}
binary flags: {BinaryFlags}
seed: {Seed}
verification: {VerificationString}";
    }

    public DiscordMessageBuilder ToMessageBuilder()
    {
        return new DiscordMessageBuilder().EnableV2Components()
        .AddContainerComponent(new DiscordContainerComponent(components:
        [
            new DiscordTextDisplayComponent("### Seed Metadata"),
            new DiscordTextDisplayComponent($"```\r\n{Flags}\r\n```"),
            new DiscordTextDisplayComponent($"**Binary Flags**: {BinaryFlags}"),
            new DiscordTextDisplayComponent($"**Seed**: {Seed}"),
            new DiscordTextDisplayComponent($"**Version**: {Version}"),
            new DiscordTextDisplayComponent($"**Check**: {VerificationString}")
        ], color: DiscordColor.Teal));
    }
}
