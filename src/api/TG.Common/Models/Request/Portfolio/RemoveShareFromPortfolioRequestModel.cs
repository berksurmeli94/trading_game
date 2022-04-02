using TG.Core.Attiributes;

namespace TG.Common.Models.Request.Portfolio
{
    public class RemoveShareFromPortfolioRequestModel
    {
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
