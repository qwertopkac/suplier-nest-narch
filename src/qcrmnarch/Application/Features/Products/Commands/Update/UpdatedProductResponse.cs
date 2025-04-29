using NArchitecture.Core.Application.Responses;

namespace Application.Features.Products.Commands.Update;

public class UpdatedProductResponse : IResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
}