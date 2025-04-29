using Application.Features.Products.Constants;
using Application.Features.Products.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using MediatR;
using static Application.Features.Products.Constants.ProductsOperationClaims;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Products.Queries.GetById;

public class GetByIdProductQuery : IRequest<GetByIdProductResponse>, ISecuredRequest
{
    public int Id { get; set; }

    public string[] Roles => [Admin, Read];

    public class GetByIdProductQueryHandler : IRequestHandler<GetByIdProductQuery, GetByIdProductResponse>
    {
        private readonly IMapper _mapper;
        private readonly IProductRepository _productRepository;
        private readonly ProductBusinessRules _productBusinessRules;

        public GetByIdProductQueryHandler(IMapper mapper, IProductRepository productRepository, ProductBusinessRules productBusinessRules)
        {
            _mapper = mapper;
            _productRepository = productRepository;
            _productBusinessRules = productBusinessRules;
        }

        public async Task<GetByIdProductResponse> Handle(GetByIdProductQuery request, CancellationToken cancellationToken)
        {
            Product? product = await _productRepository.GetAsync(
                predicate: p => p.Id == request.Id,
                include:p=>p.Include(i=>i.Items)
                .ThenInclude(ti=>ti.ItemSpecifications)
                .ThenInclude(ti => ti.SpecificationValue)
                .ThenInclude(ti=>ti.Specification)
                , 
                cancellationToken: cancellationToken
                );
            await _productBusinessRules.ProductShouldExistWhenSelected(product);

            GetByIdProductResponse response = _mapper.Map<GetByIdProductResponse>(product);
            return response;
        }
    }
}