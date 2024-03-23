using System.ComponentModel.DataAnnotations;

namespace SmartTradeAdvisor.Data.Entities;

public class MarketIndex
{
    [Key]
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required bool IsSymbol { get; set; }
}
