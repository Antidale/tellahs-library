using System.Text;
using System.Text.Json;

namespace tellahs_library.RecallCommand.Helpers;

public class MetadataHelper
{
    const long MetadataDocLengthStart = 0x1FF000;
    const long EndScreenTextStart = 0x10d500;
    const long ChecksumStart = 0x007FDE;
    const int MetadataDocLengthByteSize = 4;

    public static bool TryGetSeedMetadata(string filePath, out SeedMetadata seedMetadata)
    {
        seedMetadata = new();

        try
        {
            using var br = new BinaryReader(File.Open(filePath, FileMode.Open));
            var (version, seed) = CheckVersion(br);

            br.BaseStream.Seek(0x1FF000, SeekOrigin.Begin);
            var docLength = BitConverter.ToInt32(br.ReadBytes(4));

            if (version.StartsWith("v0.1") || version.StartsWith("v0.2"))
            {
                var flags = "(unknown)";
                var pathSegments = (Path.GetFileName(filePath) ?? string.Empty).Split('-');
                if (pathSegments.Length == 3)
                {
                    flags = pathSegments.Skip(1).First();
                }

                seedMetadata = new SeedMetadata
                {
                    Version = version,
                    Seed = seed,
                    Flags = flags,
                    BinaryFlags = "N/A"
                };

                return true;
            }
            else
            {
                var jsonbDocBytes = br.ReadBytes(docLength);
                var jsonDocString = Encoding.UTF8.GetString(jsonbDocBytes);

                try
                {
                    seedMetadata = JsonSerializer.Deserialize<SeedMetadata>(jsonDocString) ?? seedMetadata;
                }
                catch (Exception)
                {
                    seedMetadata = JsonSerializer.Deserialize<LegacySeedMetadata>(jsonDocString)?.ToSeedMetadata() ?? seedMetadata;
                }

                if (seedMetadata.Flags == string.Empty)
                {
                    return false;
                }

                if (seedMetadata.Verification.Count == 0)
                {
                    br.BaseStream.Seek(0x007FDE, SeekOrigin.Begin);
                    var first = br.ReadByte();
                    var second = br.ReadByte();
                    var byteArray = new List<ushort>(capacity: (int)br.BaseStream.Length);

                    var romChecksum = first | (second << 8);

                    var iconNames = Enumerable.Range(0, 4)
                                         .Select(iterator => (romChecksum >> (iterator * 4)) & 0xf)
                                         .Select(nibble => Data.ChecksumTiles[nibble])
                                         .ToList();

                    if (!iconNames.Any(x => string.IsNullOrWhiteSpace(x)))
                    {
                        seedMetadata.Verification = iconNames;
                    }
                }
            }

            return seedMetadata.ToString() != new SeedMetadata().ToString();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.StackTrace);
            return false;
        }
    }

    public static bool TryGetSeedMetadata(Stream romFile, out SeedMetadata seedMetadata)
    {
        seedMetadata = new();

        try
        {
            using var br = new BinaryReader(romFile);

            br.BaseStream.Seek(0x1FF000, SeekOrigin.Begin);
            var docLength = BitConverter.ToInt32(br.ReadBytes(4));

            if (docLength <= 0) { return false; }

            var jsonbDocBytes = br.ReadBytes(docLength);
            var jsonDocString = Encoding.UTF8.GetString(jsonbDocBytes);

            try
            {
                seedMetadata = JsonSerializer.Deserialize<SeedMetadata>(jsonDocString) ?? seedMetadata;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                seedMetadata = JsonSerializer.Deserialize<LegacySeedMetadata>(jsonDocString)?.ToSeedMetadata() ?? seedMetadata;
            }

            if (seedMetadata.Flags == string.Empty)
            {
                return false;
            }

            if (seedMetadata.Verification.Count == 0)
            {
                br.BaseStream.Seek(0x007FDE, SeekOrigin.Begin);
                var first = br.ReadByte();
                var second = br.ReadByte();
                var byteArray = new List<ushort>(capacity: (int)br.BaseStream.Length);

                var romChecksum = first | (second << 8);

                var iconNames = Enumerable.Range(0, 4)
                                     .Select(iterator => (romChecksum >> (iterator * 4)) & 0xf)
                                     .Select(nibble => Data.ChecksumTiles[nibble])
                                     .ToList();

                if (!iconNames.Any(x => string.IsNullOrWhiteSpace(x)))
                {
                    seedMetadata.Verification = iconNames;
                }
            }

            return seedMetadata.ToString() != new SeedMetadata().ToString();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.StackTrace);
            return false;
        }
    }

    private static (string version, string seed) CheckVersion(BinaryReader reader)
    {
        try
        {
            reader.BaseStream.Seek(EndScreenTextStart, SeekOrigin.Begin);
            var prefixCount = 0;
            var bytes = Enumerable.Empty<byte>();
            while (prefixCount < 7 && bytes.Count() < 512)
            {
                var nextByte = reader.ReadByte();
                if (nextByte == 0x02) prefixCount++;
                bytes = bytes.Append(nextByte);
            }

            var byteArray = bytes.ToArray();
            return ParseEndScreenBytes(byteArray);
        }
        catch (Exception)
        {
            return (string.Empty, string.Empty);
        }
    }

    private static (string version, string seed) ParseEndScreenBytes(byte[] seedInfoBytes)
    {
        List<string> results = [];
        List<string> characters = [];

        var i = 0;
        while (i < seedInfoBytes.Length)
        {
            var b = seedInfoBytes[i];
            if (b == 0x00)
            {
                var line = string.Join(string.Empty, characters);
                results.Add(line);
                characters = [];
            }
            else if (b == 0x02)
            {
                i++;
                // text.py adds in some ~ based on the value of the next byte, but we don't really care about that and can just skip to the next value in the byte array
                var line = string.Join(string.Empty, characters);
                if (line != Environment.NewLine)
                {
                    results.Add(line);
                }

                characters = [];
            }
            else if (b == 0x09)
            {
                characters.Add(Environment.NewLine);
            }
            else if (Data.RawCharacterMap.ContainsKey(b))
            {
                characters.Add(Data.RawCharacterMap[b]);
            }
            else if (Data.Symbols.ContainsKey(b))
            {
                characters.Add(Data.Symbols[b]);

            }
            i++;
        }

        var version = results.Skip(4).First();
        var seed = results.Skip(6).First();
        return (version, seed);
    }

}
