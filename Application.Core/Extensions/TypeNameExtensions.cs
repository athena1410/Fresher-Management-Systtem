using System;
using System.Text;

namespace Application.Core.Extensions
{
    public static class TypeNameExtensions
    {
        public static string GetGenericTypeName(this Type type)
        {
            if (!type.IsGenericType)
            {
                return type.Name;
            }

            var typeNameBuilder = new StringBuilder(type.Name);
            int iBacktick = type.Name.IndexOf('`');
            if (iBacktick > 0)
            {
                typeNameBuilder.Remove(iBacktick, typeNameBuilder.Length - iBacktick);
            }

            typeNameBuilder.Append('<');

            Type[] typeParameters = type.GetGenericArguments();
            for (int i = 0; i < typeParameters.Length; ++i)
            {
                string typeParamName = GetGenericTypeName(typeParameters[i]);
                typeNameBuilder.Append(i == 0 ? typeParamName : $",{typeParamName}");
            }
            typeNameBuilder.Append('>');

            return typeNameBuilder.ToString();
        }
    }
}
