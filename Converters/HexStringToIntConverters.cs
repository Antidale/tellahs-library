using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace tellahs_library.Converters;

public class HexStringToUintConverter : JsonConverter<uint>
{
    public override uint Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using var jsonDoc = JsonDocument.ParseValue(ref reader);
        var hexString = jsonDoc.RootElement.Deserialize<string?>();
        if (hexString is null) return 0;

        if (uint.TryParse(hexString[2..], System.Globalization.NumberStyles.HexNumber, null, out uint result))
            return result;

        return 0;
    }

    public override void Write(Utf8JsonWriter writer, uint value, JsonSerializerOptions options)
    {
        throw new NotImplementedException();
    }
}

public class HexStringToIntConverter : JsonConverter<int>
{
    public override int Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using var jsonDoc = JsonDocument.ParseValue(ref reader);
        var hexString = jsonDoc.RootElement.Deserialize<string?>();
        if (hexString is null) return 0;

        if (int.TryParse(hexString[2..], System.Globalization.NumberStyles.HexNumber, null, out int result))
            return result;

        return 0;
    }

    public override void Write(Utf8JsonWriter writer, int value, JsonSerializerOptions options)
    {
        throw new NotImplementedException();
    }
}
