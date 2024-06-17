using Microsoft.EntityFrameworkCore;
using SmartTradeAdvisor.Data.Entities;
using SmartTradeAdvisor.Data.Entities.Indexes;
using SmartTradeAdvisor.Data.Entities.Wallet;

namespace SmartTradeAdvisor.Data.DbContexts;
public class IndexDbContext(DbContextOptions<IndexDbContext> options) : DbContext(options)
{
    public DbSet<MarketIndex> MarketIndexes { get; set; }
    public DbSet<MarketIndexValue> MarketIndexValues { get; set; }
    public DbSet<Adx> Adx { get; set; }
    public DbSet<PositiveDi> PositiveDis { get; set; }
    public DbSet<NegativeDi> NegativeDis { get; set; }
    public DbSet<Cmo> Cmo { get; set; }
    public DbSet<Macd> Macd { get; set; }
    public DbSet<MacdSignal> MacdSignal { get; set; }
    public DbSet<Mfi> Mfi { get; set; }
    public DbSet<Rsi> Rsi { get; set; }
    public DbSet<Ult> Ult { get; set; }
    public DbSet<Wallet> Wallets { get; set; }
    public DbSet<Transaction> Transactions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<MarketIndex>()
            .HasMany(x => x.MarketIndexValues)
            .WithOne(x => x.MarketIndex)
            .HasForeignKey(x => x.MarketIndexId);
        modelBuilder.Entity<MarketIndexValue>();
        modelBuilder.Entity<PositiveDi>();
        modelBuilder.Entity<NegativeDi>();
        modelBuilder.Entity<Adx>();
        modelBuilder.Entity<Cmo>();
        modelBuilder.Entity<Macd>();
        modelBuilder.Entity<MacdSignal>();
        modelBuilder.Entity<Mfi>();
        modelBuilder.Entity<Rsi>();
        modelBuilder.Entity<Ult>();
        modelBuilder.Entity<Wallet>()
            .HasMany(x => x.Transactions)
            .WithOne(x => x.Wallet)
            .HasForeignKey(x => x.WalletId);
        modelBuilder.Entity<Transaction>();
        base.OnModelCreating(modelBuilder);
    }
}
