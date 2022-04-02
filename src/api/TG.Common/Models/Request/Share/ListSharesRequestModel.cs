using TG.Core.Request;

namespace TG.Common.Models.Request.Share
{
    public class ListSharesRequestModel
    {
        public BaseFilter Filter { get; set; } = new BaseFilter();
    }
}
