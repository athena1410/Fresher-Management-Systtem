namespace Application.Core.DTOs.Pagination
{
    public class PaginationFilter
    {
        public const int DEFAULT_PAGE_NUMBER = 1;
        public const int DEFAULT_PAGE_SIZE = 10;

        private int _pageNumber;
        private int _pageSize;

        public int PageNumber
        {
            get => this._pageNumber;
            set => this._pageNumber = value == default ? DEFAULT_PAGE_NUMBER : value;
        }

        public int PageSize
        {
            get => this._pageSize;
            set => this._pageSize = value == default ? DEFAULT_PAGE_SIZE : value;
        }
    }
}
