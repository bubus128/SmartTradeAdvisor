using System.ComponentModel.DataAnnotations;

namespace SmartTradeAdvisor.Data.Entities;
public class MarketIndexValue
{
    [Key]
    public Guid Id { get; set; }
    public DateTime Date { get; set; }
    public decimal Value { get; set; }
    public required Index Index { get; set; }
}
