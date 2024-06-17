namespace SmartTradeAdvisor.ChartDrawer.Models;
public class TransactionDto
{
    public required string Id { get; set; }
    public DateTime Date { get; set; }
    public bool Seal { get; set; }
}

