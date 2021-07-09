using System;
using System.Collections.Generic;
using System.Text;

namespace Zino.Core.Utilities
{
    public static class EnumExtensions
    {
        public static T ParseEnum<T>(string value)
        {
            return (T)Enum.Parse(typeof(T), value, true);
        }
    }
}
