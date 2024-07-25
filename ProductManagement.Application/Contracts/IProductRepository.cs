using ProductManagement.Domain;

namespace ProductManagement.Application.Contracts;

public interface IProductRepository : IGenericRepository<Product>
{
    Task<bool> IsProductNameUniqueBesidesThisProduct(Guid id, string description);
    Task<bool> IsProductNameUnique(string description);
}
