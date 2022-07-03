using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace NannyModels.Extensions
{
    public static class EnumExtensions
    {
        public static string GetDisplayName(this Enum enumValue)
        {
            return enumValue.GetType()
                .GetMember(enumValue.ToString())
                .First()
                .GetCustomAttributes<DisplayAttribute>()
                .First()
                .GetName() 
                ?? string.Empty;
        }
    }
}
