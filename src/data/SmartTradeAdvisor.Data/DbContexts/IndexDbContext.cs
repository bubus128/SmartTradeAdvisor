using Microsoft.EntityFrameworkCore;
using SmartTradeAdvisor.Data.Entities;

namespace SmartTradeAdvisor.Data.DbContexts;
public class IndexDbContext : DbContext
{
    public DbSet<MarketIndex> MarketIndexes { get; set; }
    public DbSet<MarketIndexValue> MarketIndexValues { get; set; }

    public IndexDbContext(DbContextOptions<IndexDbContext> dbContextOptions) : base(dbContextOptions) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<MarketIndex>().ToTable("MarketIndexes");
        modelBuilder.Entity<MarketIndexValue>().ToTable("MarketIndexValues");
    }
}
