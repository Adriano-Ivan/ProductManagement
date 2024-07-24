using ProductManagement.Application.Contracts;
using ProductManagement.Domain;
using ProductManagement.Persistence.DatabaseContext;

namespace ProductManagement.Persistence.Repositories;

public class ProviderRepository : GenericRepository<Provider>, IProviderRepository
{
    public ProviderRepository(ProductManagementContext context) : base(context)
    {
    }
}
