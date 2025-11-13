using System.ComponentModel;

namespace tellahs_library.Extensions
{
    public static class EnumExtensions
    {
        extension(Enum enumVal)
        {
            public string GetDescription()
            {
                var type = enumVal.GetType();
                var memInfo = type.GetMember(enumVal.ToString());
                if (memInfo is null)
                    return string.Empty;

                var attributes = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
                return attributes.Length > 0
                    ? ((DescriptionAttribute)attributes[0])?.Description ?? string.Empty
                    : enumVal?.ToString() ?? string.Empty;
            }

            public T? GetAttribute<T>() where T : Attribute
            {
                var field = enumVal.GetType().GetField(enumVal.ToString());
                if (field == null)
                    return null;

                var attributes = (T[])field.GetCustomAttributes(typeof(T), false);

                return attributes != null && attributes.Length > 0
                    ? attributes[0]
                    : null;
            }
        }
    }
}
