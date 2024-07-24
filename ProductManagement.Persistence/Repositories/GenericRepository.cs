using Microsoft.EntityFrameworkCore;
using ProductManagement.Application.Contracts;
using ProductManagement.Domain.Common;
using ProductManagement.Persistence.DatabaseContext;

namespace ProductManagement.Persistence.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
{
    protected readonly ProductManagementContext _context;

    public GenericRepository(ProductManagementContext context)
    {
        this._context = context;
    }

    public async Task<T> CreateAsync(T entity)
    {
        entity.CreatedAt = DateTime.Now;

        var entry = await _context.AddAsync(entity);
        await _context.SaveChangesAsync();

        return entry.Entity;
    }

    public async Task DeleteAsync(T entity)
    {
        _context.Remove(entity);
        await _context.SaveChangesAsync();
    }

    public async Task<IReadOnlyList<T>> GetAllAsync()
    {
        return await _context.Set<T>().AsNoTracking().ToListAsync();
    }

    public async Task<bool> AnyAsync(Guid id)
    {
        return await _context.Set<T>().AnyAsync(e => e.Id == id);
    }

    public async Task<T?> GetByIdAsync(Guid id)
    {
        return await _context.Set<T>()
                        .AsNoTracking()
                        .FirstOrDefaultAsync(q => q.Id == id);
    }

    public async Task UpdateAsync(T entity)
    {
        entity.UpdatedAt = DateTime.Now;
        _context.Update(entity);
        _context.Entry(entity).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }
}
