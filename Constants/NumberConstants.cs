using System;

namespace tellahs_library.Constants;

public class NumberConstants
{
    /// <summary>
    /// ROM size for most/all (?) FE roms
    /// </summary>
    public const int EXPECTED_FE_ROM_SIZE = 2_097_152;
    /// <summary>
    /// Using the FxPak Pro's Max file size as listed on https://stoneagegamer.com/nintendo/snes/everdrives-flash-carts/
    /// </summary>
    public const int MAX_SNES_ROM_SIZE = 12_000_000;
}
