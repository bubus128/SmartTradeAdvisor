using Microsoft.EntityFrameworkCore;
using SmartTradeAdvisor.Data.Entities;

namespace SmartTradeAdvisor.Data.DbContexts;
public class IndexDbContext : DbContext
{
    public DbSet<MarketIndex> Indexes { get; set; }
    public DbSet<MarketIndexValue> IndexesValues { get; set; }

    public IndexDbContext(DbContextOptions<IndexDbContext> dbContextOptions)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<MarketIndex>().ToTable("Indexes");
        modelBuilder.Entity<MarketIndexValue>().ToTable("IndexesValues");
    }
}
