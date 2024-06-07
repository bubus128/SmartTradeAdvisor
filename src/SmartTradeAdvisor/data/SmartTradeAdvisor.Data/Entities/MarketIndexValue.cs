using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartTradeAdvisor.Data.Entities;
public class MarketIndexValue
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }
    public DateTime Date { get; set; }
    public double LowValue { get; set; }
    public double HighValue { get; set; }
    public double ClosingValue { get; set; }
    public double Volume { get; set; }
    public required string MarketIndexId { get; set; }

    [ForeignKey("MarketIndexId")]
    public virtual required MarketIndex MarketIndex { get; set; }
}
