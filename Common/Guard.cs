using System;

namespace Common.Guard
{
    public class Guard
    {
        public static T Null<T>(T value, string parameterName = default)
        {
            return value ?? throw new ArgumentNullException(parameterName);
        }
    }
}
