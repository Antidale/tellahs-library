using System.ComponentModel;

namespace tellahs_library.Helpers
{
    public static class EnumHelper
    {
        public static string GetDescription(this Enum enumVal)
        {
            var type = enumVal.GetType();
            var memInfo = type.GetMember(enumVal.ToString());
            if (memInfo is null)
                return string.Empty;

            var attributes = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
            return (attributes.Length > 0) ? ((DescriptionAttribute)attributes[0])?.ToString() ?? string.Empty : enumVal?.ToString() ?? string.Empty;
        }
    }
}
