using TG.Core.Attiributes;
using System.Text.Json.Serialization;

namespace TG.Common.Models.Request.Portfolio
{
    public class AddShareToPortfolioRequestModel
    {
        [JsonIgnore]
        public string UserId { get; set; }
        [CustomRequired]
        public string PortfolioId { get; set; }
        [CustomRequired]
        public string ShareId { get; set; }
        [CustomRequired]
        public int Amount { get; set; }
        [CustomRequired]
        public decimal TotalPrice { get; set; }
    }
}
