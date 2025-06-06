using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using NArchitecture.Core.Persistence.Paging;
using MediatR;

namespace Application.Features.Countries.Queries.GetList;

public class GetListCountryQuery : IRequest<GetListResponse<GetListCountryListItemDto>>, ICachableRequest
{
    public PageRequest PageRequest { get; set; }

    public bool BypassCache { get; }
    public string? CacheKey => $"GetListCountries({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string? CacheGroupKey => "GetCountries";
    public TimeSpan? SlidingExpiration { get; }

    public class GetListCountryQueryHandler : IRequestHandler<GetListCountryQuery, GetListResponse<GetListCountryListItemDto>>
    {
        private readonly ICountryRepository _countryRepository;
        private readonly IMapper _mapper;

        public GetListCountryQueryHandler(ICountryRepository countryRepository, IMapper mapper)
        {
            _countryRepository = countryRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListCountryListItemDto>> Handle(GetListCountryQuery request, CancellationToken cancellationToken)
        {
            IPaginate<Country> countries = await _countryRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize, 
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListCountryListItemDto> response = _mapper.Map<GetListResponse<GetListCountryListItemDto>>(countries);
            return response;
        }
    }
}