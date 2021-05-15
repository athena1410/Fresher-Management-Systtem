using System;
using System.IO;

namespace Common.Guard
{
    public class Guard
    {
        public static T Null<T>(T value, string parameterName = default)
        {
            return value ?? throw new ArgumentNullException(parameterName);
        }
        
        public static string IsNotNullOrWhitespace(string value, string parameterName = default)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException($"Value can't be null or white space.");
            }

            return value;
        }


        public static void FileExists(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"File with path {filePath} does not existed.");
            }
        }
    }
}
