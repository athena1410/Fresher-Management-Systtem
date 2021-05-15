using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Application.Core.Extensions
{
    public static class SortExtensions
    {
        public static IOrderedQueryable<T> AppendOrderBy<T, TKey>(this IQueryable<T> query, Expression<Func<T, TKey>> keySelector)
            => query.Expression.Type == typeof(IOrderedQueryable<T>)
                ? ((IOrderedQueryable<T>)query).ThenBy(keySelector)
                : query.OrderBy(keySelector);

        public static IOrderedQueryable<T> AppendOrderByDescending<T, TKey>(this IQueryable<T> query, Expression<Func<T, TKey>> keySelector)
            => query.Expression.Type == typeof(IOrderedQueryable<T>)
                ? ((IOrderedQueryable<T>)query).ThenByDescending(keySelector)
                : query.OrderByDescending(keySelector);

        /// <summary>
        /// Extension method of IQueryable use to sort by multiple columns
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="multipleSorts"></param>
        /// <returns></returns>
        public static IQueryable<T> Sort<T>(this IQueryable<T> source, Dictionary<string, string> multipleSorts)
        {
            if (multipleSorts is null)
            {
                throw new ArgumentNullException(nameof(multipleSorts));
            }

            ParameterExpression param = Expression.Parameter(typeof(T), "item");

            foreach (var (field, sortDirection) in multipleSorts)
            {
                if (!typeof(T).HasPropertyName(field))
                {
                    continue;
                }

                var sortExpression = Expression.Lambda<Func<T, object>>
                    (Expression.Convert(Expression.Property(param, field), typeof(object)), param);

                source = sortDirection is "dsc" 
                    ? source.AppendOrderByDescending(sortExpression) 
                    : source.AppendOrderBy(sortExpression);
            }

            return source;
        }
    }
}
