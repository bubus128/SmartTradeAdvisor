namespace SmartTradeAdvisor.Api.Mapper;

public class ResultResponse
{
    public required string Strategy { get; set; }
    public required List<TransactionResponse> Transactions { get; set; }

}
