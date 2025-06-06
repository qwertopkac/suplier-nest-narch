using Application.Features.CompanyAddresses.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Pipelines.Logging;
using MediatR;

namespace Application.Features.CompanyAddresses.Commands.Create;

public class CreateCompanyAddressCommand : IRequest<CreatedCompanyAddressResponse>, ICacheRemoverRequest, ILoggableRequest
{
    public required string Street { get; set; }
    public required int CityId { get; set; }
    public required City City { get; set; }
    public required int TownId { get; set; }
    public required Town Town { get; set; }
    public required int CountryId { get; set; }
    public required Country Country { get; set; }
    public required string PostalCode { get; set; }
    public required string Description { get; set; }
    public required int CompanyId { get; set; }

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetCompanyAddresses"];

    public class CreateCompanyAddressCommandHandler : IRequestHandler<CreateCompanyAddressCommand, CreatedCompanyAddressResponse>
    {
        private readonly IMapper _mapper;
        private readonly ICompanyAddressRepository _companyAddressRepository;
        private readonly CompanyAddressBusinessRules _companyAddressBusinessRules;

        public CreateCompanyAddressCommandHandler(IMapper mapper, ICompanyAddressRepository companyAddressRepository,
                                         CompanyAddressBusinessRules companyAddressBusinessRules)
        {
            _mapper = mapper;
            _companyAddressRepository = companyAddressRepository;
            _companyAddressBusinessRules = companyAddressBusinessRules;
        }

        public async Task<CreatedCompanyAddressResponse> Handle(CreateCompanyAddressCommand request, CancellationToken cancellationToken)
        {
            CompanyAddress companyAddress = _mapper.Map<CompanyAddress>(request);

            await _companyAddressRepository.AddAsync(companyAddress);

            CreatedCompanyAddressResponse response = _mapper.Map<CreatedCompanyAddressResponse>(companyAddress);
            return response;
        }
    }
}