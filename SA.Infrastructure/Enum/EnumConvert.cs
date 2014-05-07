using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SA.Infrastructure
{
    public static class EnumConvert<T> where T : struct
    {
        public static T FromInt(int value)
        {
            if (Enum.IsDefined(typeof(T), value))
                return (T)Enum.ToObject(typeof(T), value);

            return default(T);
        }
        public static T FromChar(char value)
        {
            return FromInt(Convert.ToInt32(value));
        }

        public static T ToEnum(int value)
        {
            if (Enum.IsDefined(typeof(T), value))
                return (T)Enum.ToObject(typeof(T), value);
            return default(T);
        }
        public static T ToEnum(char value)
        {
            return ToEnum(Convert.ToInt32(value));
        }

    }
}
