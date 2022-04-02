using TG.Core.Attiributes;
using System.Text.Json.Serialization;

namespace TG.Common.Models.Request.Portfolio
{
    public class GetPortfolioRequestModel
    {
        [CustomRequired]
        public string PortfolioId { get; set; }
        [JsonIgnore]
        public string UserId { get; set; }
    }
}
