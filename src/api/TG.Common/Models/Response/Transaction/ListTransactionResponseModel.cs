using static TG.Core.Enums.Enums;

namespace TG.Common.Models.Response.Transaction
{
    public class ListTransactionResponseModel
    {
        public string ShareCode { get; set; }
        public int Amount { get; set; }
        public decimal TotalPrice { get; set; }
        public TransactionType Type { get; set; }
    }
}
