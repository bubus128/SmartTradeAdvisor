using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartTradeAdvisor.Data.Entities;

public class MarketIndex
{
    [Key]
    public required string Name { get; set; }

    [Required]
    public required string Description { get; set; }

    [Required]
    public required List<MarketIndexValue> MarketIndexValues { get; set; } = [];

    [NotMapped]
    public string Id => Name;
}
