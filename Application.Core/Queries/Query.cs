using Application.Core.DTOs.Pagination;
using MediatR;

namespace Application.Core.Queries
{
    public class Query<TResult> : IRequest<TResult> where TResult : class
    {
    }

    public class GetByIdQuery<TKey, TResult> : IRequest<TResult> where TResult : class
    {
        public TKey Id { get; set; }

        public GetByIdQuery(TKey id)
        {
            Id = id;
        }
    }

    public class QueryWithPagination<TResult> : PaginationFilter, IRequest<TResult> where TResult : class
    {
    }
}
