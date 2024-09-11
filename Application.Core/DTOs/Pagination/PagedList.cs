using System;
using System.Collections.Generic;

namespace Application.Core.DTOs.Pagination
{
    public class PagedList<T>(IEnumerable<T> data, int totalRecords)
        where T : class
    {
        public bool HasNextPage => TotalPages > PageNumber;
        public bool HasPreviousPage => PageNumber > PaginationFilter.DEFAULT_PAGE_NUMBER;
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalRecords { get; set; } = totalRecords;
        public int TotalPages => (int)Math.Ceiling(TotalRecords / (double)PageSize);
        public IEnumerable<T> Data { get; set; } = data;
    }
}
