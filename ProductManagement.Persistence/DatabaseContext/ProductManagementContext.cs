using Microsoft.EntityFrameworkCore;
using ProductManagement.Domain;
using ProductManagement.Domain.Common;

namespace ProductManagement.Persistence.DatabaseContext;

public class ProductManagementContext : DbContext
{
    public ProductManagementContext(DbContextOptions<ProductManagementContext> options) : base(options)
    {

    }

    public DbSet<Product> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProductManagementContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        foreach (var entry in base.ChangeTracker.Entries<BaseEntity>()
             .Where(q => q.State == EntityState.Added || q.State == EntityState.Modified))
        {
            entry.Entity.UpdatedAt = DateTime.Now;

            if (entry.State == EntityState.Added)
            {
                entry.Entity.CreatedAt = DateTime.Now;
            }
        }

        return base.SaveChangesAsync(cancellationToken);
    }
}
