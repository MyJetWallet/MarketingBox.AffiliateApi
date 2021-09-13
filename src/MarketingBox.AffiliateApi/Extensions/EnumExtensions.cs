using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketingBox.AffiliateApi.Extensions
{
    public static class EnumExtensions
    {
        public static T MapEnum<T>(this object value)
            where T : struct, IConvertible
        {
            if (value == null)
            {
                throw new ArgumentException($"Value was null while mapping {typeof(T)}");
            }

            var sourceType = value.GetType();
            if (!sourceType.IsEnum)
                throw new ArgumentException($"Source type is not enum, while mapping {typeof(T)}");
            if (!typeof(T).IsEnum)
                throw new ArgumentException($"Destination type is not enum, while mapping {typeof(T)}");
            return (T)Enum.Parse(typeof(T), value.ToString()!);
        }
    }
}
