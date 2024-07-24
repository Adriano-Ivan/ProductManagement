using Microsoft.EntityFrameworkCore;
using ProductManagement.Application.Contracts;
using ProductManagement.Domain;
using ProductManagement.Persistence.DatabaseContext;

namespace ProductManagement.Persistence.Repositories;

public class ProductRepository : GenericRepository<Product>, IProductRepository
{
    public ProductRepository(ProductManagementContext context) : base(context)
    {
    }

    public async Task<bool> IsProductNameUniqueBesidesThisProduct(Guid id, string description)
    {
        return !(await _context.Products.AnyAsync(p => p.Id != id && p.Descricao.Equals(description)));
    }
}
