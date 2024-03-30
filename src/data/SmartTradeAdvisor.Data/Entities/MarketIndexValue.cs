using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartTradeAdvisor.Data.Entities;
public class MarketIndexValue
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }
    public DateTime Date { get; set; }
    public decimal Value { get; set; }
    public Guid IndexId { get; set; }
    [ForeignKey("IndexId")]
    public required MarketIndex Index { get; set; }
}
