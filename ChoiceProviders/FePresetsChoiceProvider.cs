
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using DSharpPlus.Commands.Processors.SlashCommands.ArgumentModifiers;
using tellahs_library.Enums;

namespace tellahs_library.Commands;

public class PresetsChoiceProvider : IAutoCompleteProvider
{
    static readonly DiscordAutoCompleteChoice[] choices;

    static PresetsChoiceProvider()
    {
        List<DiscordAutoCompleteChoice> choiceList = [];
        foreach (FieldInfo fieldInfo in typeof(FePresetChoices).GetFields())
        {
            if (fieldInfo.IsSpecialName || !fieldInfo.IsStatic)
            {
                continue;
            }

            // Add support for ChoiceDisplayNameAttribute
            string displayName = fieldInfo.GetCustomAttribute<ChoiceDisplayNameAttribute>() is ChoiceDisplayNameAttribute displayNameAttribute
                ? displayNameAttribute.DisplayName
                : fieldInfo.Name;

            object? obj = fieldInfo.GetValue(null);
            if (obj is not FePresetChoices)
            {
                // Hey what the fuck
                continue;
            }

            if (obj is null || (FePresetChoices)obj < 0)
            {
                continue;
            }

            // Put ulong as a string, bool, byte, short and int as int, uint and long as long.
            choiceList.Add(Convert.ChangeType(obj, Enum.GetUnderlyingType(typeof(FePresetChoices)), CultureInfo.InvariantCulture) switch
            {
                bool value => new DiscordAutoCompleteChoice(displayName, value ? 1 : 0),
                byte value => new DiscordAutoCompleteChoice(displayName, value),
                sbyte value => new DiscordAutoCompleteChoice(displayName, value),
                short value => new DiscordAutoCompleteChoice(displayName, value),
                ushort value => new DiscordAutoCompleteChoice(displayName, value),
                int value => new DiscordAutoCompleteChoice(displayName, value),
                uint value => new DiscordAutoCompleteChoice(displayName, value),
                long value => new DiscordAutoCompleteChoice(displayName, value),
                ulong value => new DiscordAutoCompleteChoice(displayName, value),
                double value => new DiscordAutoCompleteChoice(displayName, value),
                float value => new DiscordAutoCompleteChoice(displayName, value),
                _ => throw new UnreachableException($"Unknown enum base type encountered: {obj.GetType()}")
            });
        }

        choices = [.. choiceList];
    }

    public ValueTask<IEnumerable<DiscordAutoCompleteChoice>> AutoCompleteAsync(AutoCompleteContext context)
    {
        return ValueTask.FromResult(choices.Where(x => x.Name.Contains(context.UserInput ?? "", StringComparison.InvariantCultureIgnoreCase)));
    }
}
