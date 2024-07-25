using Microsoft.EntityFrameworkCore;
using ProductManagement.Application.Contracts;
using ProductManagement.Domain;
using ProductManagement.Persistence.DatabaseContext;

namespace ProductManagement.Persistence.Repositories;

public class ProviderRepository : GenericRepository<Provider>, IProviderRepository
{
    public ProviderRepository(ProductManagementContext context) : base(context)
    {
    }

    public async Task<bool> IsProviderCnpjUnique(string cnpj)
    {
        return !(await _context.Providers.AnyAsync(p => p.CNPJ.Equals(cnpj)));
    }

    public async Task<bool> IsProviderCnpjUniqueBesidesThisProvider(Guid id, string cnpj)
    {
        return !(await _context.Providers.AnyAsync(p => p.Id != id && p.CNPJ.Equals(cnpj)));
    }
}
