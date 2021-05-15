using Application.Core.DTOs.Pagination;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Core.Extensions
{
    public static class PaginationExtensions
    {
        public static async Task<PagedList<T>> PaginateAsync<T>(this IQueryable<T> query, int pageNumber,
            int pageSize, CancellationToken cancellationToken) where T : class
        {
            return await query.PaginateAsync(new PaginationFilter { PageNumber = pageNumber, PageSize = pageSize }, cancellationToken);
        }

        public static async Task<PagedList<T>> PaginateAsync<T>(this IQueryable<T> query,
            PaginationFilter paginationFilter, CancellationToken cancellationToken) where T : class
        {
            int startRow = (paginationFilter.PageNumber - 1) * paginationFilter.PageSize;

            var data = await query.Skip(startRow).Take(paginationFilter.PageSize)
                .ToListAsync(cancellationToken);

            var totalRecords = await query.CountAsync(cancellationToken);

            return new PagedList<T>(data, totalRecords)
            {
                PageNumber = paginationFilter.PageNumber,
                PageSize = paginationFilter.PageSize
            };
        }
    }
}
