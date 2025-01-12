using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartTradeAdvisor.Data.Entities.Wallet;
public class Wallet
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    [Required]
    public required string Strategy { get; set; }

    [Required]
    public required string MarketIndexId { get; set; }


    [ForeignKey("MarketIndexId")]
    public virtual required MarketIndex MarketIndex { get; set; }

    public required List<Transaction> Transactions { get; set; }
}
