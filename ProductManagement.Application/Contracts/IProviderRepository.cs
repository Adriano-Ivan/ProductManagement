using ProductManagement.Domain;

namespace ProductManagement.Application.Contracts;

public interface IProviderRepository : IGenericRepository<Provider>
{
    Task<bool> IsProviderCnpjUnique(string cnpj);
    Task<bool> IsProviderCnpjUniqueBesidesThisProvider(Guid id, string cnpj);
}
