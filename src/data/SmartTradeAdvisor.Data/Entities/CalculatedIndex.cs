using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartTradeAdvisor.Data.Entities;

public abstract class CalculatedIndex(string name)
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    public string Name { get; set; } = name;

    [Required]
    public double Value { get; set; }

    [Required]
    public Guid MarketIndexId { get; set; }

    [ForeignKey("MarketIndexId")]
    public virtual MarketIndex MarketIndex { get; set; }
}
