using SmartTradeAdvisor.Data.Entities;

namespace SmartTradeAdvisor.Core.WalletCalculator;
public interface IWalletService
{
    public Task CalculateAll(MarketIndexValue value);

}
