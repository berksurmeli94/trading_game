using TG.Core.Attiributes;
using System.Text.Json.Serialization;

namespace TG.Common.Models.Request.Portfolio
{
    public class PortfolioCreateRequestModel
    {
        [JsonIgnore]
        public string UserId { get; set; }
        [CustomRequired]
        public string Name { get; set; }
    }
}
