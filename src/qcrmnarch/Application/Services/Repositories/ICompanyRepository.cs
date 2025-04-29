using Domain.Entities;
using NArchitecture.Core.Persistence.Repositories;

namespace Application.Services.Repositories;

public interface ICompanyRepository : IAsyncRepository<Company, int>, IRepository<Company, int>
{
}