using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartTradeAdvisor.Data.Entities;

public class MarketIndex
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    [Required]
    public required string Name { get; set; } // e.g., "Bitcoin", "Ethereum", "Intel", "Apple"

    [Required]
    public required string Description { get; set; }

    // Navigation property for raw values
    public virtual List<MarketIndexValue> MarketIndexValues { get; set; } = [];

    // Navigation property for derived indices related to this market index
    public virtual List<CalculatedIndex> CalculatedIndices { get; set; } = [];

    [NotMapped]
    public required LimitedList<double> Highs;

    [NotMapped]
    public required LimitedList<double> Lows;
}
