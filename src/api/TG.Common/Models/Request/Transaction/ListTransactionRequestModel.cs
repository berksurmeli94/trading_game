using TG.Core.Request;
using System.Text.Json.Serialization;

namespace TG.Common.Models.Request.Transaction
{
    public class ListTransactionRequestModel
    {
        [JsonIgnore]
        public string UserId { get; set; }
        public BaseFilter Filter { get; set; } = new BaseFilter();
    }
}
