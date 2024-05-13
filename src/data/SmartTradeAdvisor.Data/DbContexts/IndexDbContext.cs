using Microsoft.EntityFrameworkCore;
using SmartTradeAdvisor.Data.Entities;

namespace SmartTradeAdvisor.Data.DbContexts;
public class IndexDbContext(DbContextOptions<IndexDbContext> dbContextOptions) : DbContext(dbContextOptions)
{
    public DbSet<MarketIndex> MarketIndexes { get; set; }
    public DbSet<MarketIndexValue> MarketIndexValues { get; set; }
    public DbSet<CalculatedIndex> CalculatedIndexes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}
