using CommonHelper.Helper.Attributes;
using System;
using System.Linq;

namespace CommonHelper.Extensions
{
    public static class StringValueExtensions
    {

        public static string ToStringValue(this Enum value)
        {
            StringValueAttribute[] attributes = (StringValueAttribute[])value.GetType().GetField(value.ToString()).GetCustomAttributes(typeof(StringValueAttribute), false);
            return ((attributes != null) && (attributes.Length > 0)) ? attributes[0].Value : value.ToString();
        }

        public static string[] ToStringValues(this Enum value)
        {
            StringValueAttribute[] attributes = (StringValueAttribute[])value.GetType().GetField(value.ToString()).GetCustomAttributes(typeof(StringValueAttribute), false);
            return ((attributes != null) && (attributes.Length > 0)) ? attributes.Select(x => x.Value).ToArray() : default(string[]);
        }

        public static T ToEnum<T>(this string value)
        {
            foreach (T item in Enum.GetValues(typeof(T)))
            {
                StringValueAttribute[] attributes = (StringValueAttribute[])item.GetType().GetField(item.ToString()).GetCustomAttributes(typeof(StringValueAttribute), false);
                if ((attributes != null) && (attributes.Length > 0) && (attributes[0].Value.Equals(value)))
                {
                    return item;
                }
            }
            return (T)Enum.Parse(typeof(T), value, true);
        }
    }
}
