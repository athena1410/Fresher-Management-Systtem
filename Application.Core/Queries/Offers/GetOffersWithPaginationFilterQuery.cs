﻿using Application.Core.DTOs.Offers;
using Application.Core.DTOs.Pagination;
using Application.Core.Extensions;
using Application.Core.Interfaces;
using AutoMapper;
using Common.Guard;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Core.Queries.Offers
{
    public class GetOffersWithPaginationFilterQuery : QueryWithPagination<PagedList<OfferDto>>
    {
        public int Technology { get; set; }
        public uint OfferSalary { get; set; }
        public Dictionary<string, string> Sort { get; set; }
    }

    public class GetOffersWithPaginationFilterQueryHandler : IRequestHandler<GetOffersWithPaginationFilterQuery, PagedList<OfferDto>>
    {
        private readonly IApplicationContext _context;
        private readonly IMapper _mapper;

        public GetOffersWithPaginationFilterQueryHandler(
            IApplicationContext context,
            IMapper mapper)
        {
            _context = Guard.Null(context, nameof(context));
            _mapper = Guard.Null(mapper, nameof(mapper));
        }

        public async Task<PagedList<OfferDto>> Handle(GetOffersWithPaginationFilterQuery request, CancellationToken cancellationToken)
        {
            var query = _context.Offers.Where(x => !x.IsDeleted).Sort(request.Sort);

            if (request.Technology != 0)
            {
                query = query.Where(x => x.Technology == request.Technology);
            }

            if (request.OfferSalary != 0)
            {
                query = query.Where(x => x.OfferSalary == request.OfferSalary);
            }

            if (request.Sort != null && request.Sort.Any())
            {
                query = query.Sort(request.Sort);
            }

            return await _mapper.ProjectTo<OfferDto>(query).PaginateAsync(request, cancellationToken);
        }
    }
}