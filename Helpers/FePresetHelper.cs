using System;
using tellahs_library.Enums;

namespace tellahs_library.Helpers;

public static class PresetHelper
{
    public static (FeHostedApi Api, string Flagset) GetPresetDetails(FePresetChoices choice)
    {
        return choice switch
        {
            _ => (FeHostedApi.Main, "")
        };
    }
}
