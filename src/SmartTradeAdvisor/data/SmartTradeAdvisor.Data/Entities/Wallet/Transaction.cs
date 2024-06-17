using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartTradeAdvisor.Data.Entities.Wallet;
public class Transaction
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    [Required]
    public bool Seal { get; set; }

    [Required]
    public DateTime Date { get; set; }

    [Required]
    public required Guid WalletId { get; set; }

    [ForeignKey("WalletId")]
    public virtual required Wallet Wallet { get; set; }
}
