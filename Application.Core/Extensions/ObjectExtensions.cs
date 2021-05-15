using System;
using System.Linq;
using System.Reflection;

namespace Application.Core.Extensions
{
    public static class ObjectExtensions
    {
        public static bool HasPropertyName(this object source, string propertyName)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.GetType().HasPropertyName(propertyName);
        }

        public static bool HasPropertyName(this Type type, string propertyName)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            PropertyInfo propertyInfo = type.GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .FirstOrDefault(x => x.Name.Equals(propertyName, StringComparison.OrdinalIgnoreCase));
            return propertyInfo != null;
        }
    }
}
