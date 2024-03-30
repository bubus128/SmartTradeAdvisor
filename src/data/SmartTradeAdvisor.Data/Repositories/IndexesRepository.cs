using Microsoft.EntityFrameworkCore;
using SmartTradeAdvisor.Data.DbContexts;
using SmartTradeAdvisor.Data.Entities;
using SmartTradeAdvisor.Data.Interfaces;

namespace SmartTradeAdvisor.Data.Repositories;

public class IndexesRepository : IIndexesRepository
{
    private readonly DbSet<MarketIndex> _dbSet;
    private readonly IndexDbContext _context;

    public IndexesRepository(IndexDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _dbSet = _context.Set<MarketIndex>();
    }

    public void Add(MarketIndex entity)
    {
        ArgumentNullException.ThrowIfNull(entity);

        _dbSet.Add(entity);
    }

    public void Delete(MarketIndex entity)
    {
        ArgumentNullException.ThrowIfNull(entity);

        _dbSet.Remove(entity);
    }

    public IEnumerable<MarketIndex> GetAll()
    {
        return _dbSet.ToList();
    }

    public MarketIndex GetById(Guid id)
    {
        // Validate arguments
        ArgumentNullException.ThrowIfNull(id);

        return _dbSet.Find(id) ?? throw new KeyNotFoundException($"index with id {id} was not found.");
    }

    public void Save()
    {
        _context.SaveChanges();
    }

    public void Update(MarketIndex entity)
    {
        ArgumentNullException.ThrowIfNull(entity);

        _dbSet.Attach(entity);
        _context.Entry(entity).State = EntityState.Modified;
    }
}
