using System;

namespace Application.Core.Extensions
{
    public static class TypeNameExtensions
    {
        public static string GetGenericTypeName(this Type type)
        {
            string genericTypeName = type.Name;
            if (type.IsGenericType)
            {
                int iBacktick = genericTypeName.IndexOf('`');
                if (iBacktick > 0)
                {
                    genericTypeName = genericTypeName.Remove(iBacktick);
                }
                genericTypeName += "<";
                Type[] typeParameters = type.GetGenericArguments();
                for (int i = 0; i < typeParameters.Length; ++i)
                {
                    string typeParamName = GetGenericTypeName(typeParameters[i]);
                    genericTypeName += (i == 0 ? typeParamName : "," + typeParamName);
                }
                genericTypeName += ">";
            }

            return genericTypeName;
        }
    }
}
