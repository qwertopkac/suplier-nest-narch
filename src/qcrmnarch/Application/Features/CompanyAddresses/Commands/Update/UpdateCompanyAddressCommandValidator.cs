using FluentValidation;

namespace Application.Features.CompanyAddresses.Commands.Update;

public class UpdateCompanyAddressCommandValidator : AbstractValidator<UpdateCompanyAddressCommand>
{
    public UpdateCompanyAddressCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.Street).NotEmpty();
        RuleFor(c => c.CityId).NotEmpty();
        RuleFor(c => c.City).NotEmpty();
        RuleFor(c => c.TownId).NotEmpty();
        RuleFor(c => c.Town).NotEmpty();
        RuleFor(c => c.CountryId).NotEmpty();
        RuleFor(c => c.Country).NotEmpty();
        RuleFor(c => c.PostalCode).NotEmpty();
        RuleFor(c => c.Description).NotEmpty();
        RuleFor(c => c.CompanyId).NotEmpty();
    }
}