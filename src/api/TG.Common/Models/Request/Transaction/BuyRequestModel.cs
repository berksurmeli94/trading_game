using TG.Core.Attiributes;
using System.Text.Json.Serialization;
using static TG.Core.Enums.Enums;

namespace TG.Common.Models.Request.Transaction
{
    public class BuyRequestModel
    {
        [JsonIgnore]
        public string UserId { get; set; }
        [CustomRequired]
        public int BuyAmount { get; set; }
        [CustomRequired]
        public string PortfolioId { get; set; }
        [CustomRequired]
        public string ShareId { get; set; }
        [JsonIgnore]
        public TransactionType Type { get; set; } = TransactionType.BUY;
    }
}
