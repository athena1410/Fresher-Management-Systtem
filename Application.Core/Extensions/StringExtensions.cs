using System;
using System.Collections.Generic;
using System.Linq;

namespace Application.Core.Extensions
{
    public static class StringExtensions
    {

        public static Dictionary<string, string> ExtractMultipleSort(this string source)
        {
            if (string.IsNullOrEmpty(source))
            {
                throw new ArgumentNullException(nameof(source));
            }

            string[] sortSettings = source.Split(',');
            var sortDictionaries = new Dictionary<string, string>();
            foreach (var sortSetting in sortSettings)
            {
                if (string.IsNullOrEmpty(sortSetting))
                {
                    continue;
                }

                var sortField = sortSetting.Split(' ');
                if (sortField.Length != 2)
                {
                    continue;
                }

                sortDictionaries[sortField.First()] = sortField.Last().ToLower();
            }

            return sortDictionaries;
        }
    }
}
