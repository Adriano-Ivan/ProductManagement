using ProductManagement.Application.Contracts;
using ProductManagement.Domain;
using ProductManagement.Persistence.DatabaseContext;

namespace ProductManagement.Persistence.Repositories;

public class ProductRepository : GenericRepository<Product>, IProductRepository
{
    public ProductRepository(ProductManagementContext context) : base(context)
    {
    }
}
