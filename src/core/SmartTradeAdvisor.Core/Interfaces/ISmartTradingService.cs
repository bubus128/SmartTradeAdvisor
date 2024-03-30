using SmartTradeAdvisor.Data.Entities;

namespace SmartTradeAdvisor.Core.Interfaces;
public interface ISmartTradingService
{
    public void Calculate(TradingAlgorithm algorithm);
}
