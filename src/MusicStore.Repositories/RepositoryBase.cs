using Microsoft.EntityFrameworkCore;
using MusicStore.Entities;

namespace MusicStore.Repositories;
/*
public abstract class RepositoryBase<TEntity, TKey>
    : IRepositoryBase<TEntity, TKey>
    where TEntity : EntityBase<TKey>
{
    protected readonly DbContext _context;

    protected RepositoryBase(DbContext context)
    {
        _context = context;
    }

    public IQueryable<TEntity> Query()
        => _context.Set<TEntity>().AsNoTracking();

    public async Task<TEntity?> GetByIdAsync(TKey id)
        => await _context.Set<TEntity>().FindAsync(id);

    public async Task<TEntity> AddAsync(TEntity entity)
    {
        await _context.Set<TEntity>().AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<TEntity> UpdateAsync(TEntity entity)
    {
        _context.Set<TEntity>().Update(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task DeleteAsync(TKey id)
    {
        var entity = await GetByIdAsync(id);

        if (entity is null)
            return;

        entity.Status = false; // soft delete
        _context.Set<TEntity>().Update(entity);

        await _context.SaveChangesAsync();
    }
}*/


public abstract class RepositoryBase<TEntity, TKey>
    : IRepositoryBase<TEntity, TKey>
    where TEntity : EntityBase<TKey>
{
    protected readonly DbContext _context;
    protected readonly DbSet<TEntity> _dbSet;

    protected RepositoryBase(DbContext context)
    {
        _context = context;
        _dbSet = _context.Set<TEntity>();
    }

    public IQueryable<TEntity> Query()
        => _dbSet.AsNoTracking();

    public async Task<TEntity?> GetByIdAsync(TKey id)
        => await _dbSet.FindAsync(id);

    public async Task<TEntity> AddAsync(TEntity entity)
    {
        await _dbSet.AddAsync(entity);
        return entity; //ya NO guarda
    }

    public Task<TEntity> UpdateAsync(TEntity entity)
    {
        _dbSet.Update(entity);
        return Task.FromResult(entity); //ya NO guarda
    }

    public async Task DeleteAsync(TKey id)
    {
        var entity = await GetByIdAsync(id);

        if (entity is null)
            return;

        entity.Status = false; // soft delete
        _dbSet.Update(entity);

        //ya NO guarda
    }
}