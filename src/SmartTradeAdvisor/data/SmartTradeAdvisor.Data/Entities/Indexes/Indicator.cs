using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartTradeAdvisor.Data.Entities.Indexes;
public abstract class Indicator
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    [Required]
    public double Value { get; set; }

    [Required]
    public required string MarketIndexId { get; set; }

    [ForeignKey("MarketIndexId")]
    public virtual required MarketIndex MarketIndex { get; set; }

}
