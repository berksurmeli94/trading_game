using TG.Common.Models.Request.Share;
using TG.Common.Models.Response.Share;
using TG.Core.Request;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TG.Services.Interface
{
    public interface IShareService
    {
        public Task<BaseResult<List<ListShareResponseModel>>> GetShares(ListSharesRequestModel model);
        public Task<GetShareResponseModel> GetShare(string Id);
    }
}
