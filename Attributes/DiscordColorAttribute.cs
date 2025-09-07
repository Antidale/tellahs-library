namespace tellahs_library.Attributes;

[AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
public class DiscordColorAttribute(int hexColor) : Attribute
{
    readonly DiscordColor _color = hexColor;

    public DiscordColor Color => _color;

}
