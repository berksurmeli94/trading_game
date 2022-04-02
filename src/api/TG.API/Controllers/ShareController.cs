using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TG.Common.Models.Request.Portfolio;
using TG.Common.Models.Request.Share;
using TG.Common.Models.Response.Share;
using TG.Core.Request;
using TG.Core.Response;
using TG.Services;
using TG.Services.Concrete;
using TG.Services.Interface;

namespace TG.API.Controllers
{
    public class ShareController : BaseController<ShareController>
    {
        private readonly IShareService shareService;
        private readonly ICurrentUserService currentUser;

        public ShareController(IShareService shareService, ICurrentUserService currentUser)
        {
            this.shareService = shareService;
            this.currentUser = currentUser; 
        }

        [HttpPost]
        public async ValueTask<ActionResult<BaseAPIResponse<BaseResult<List<ListShareResponseModel>>>>> List([FromQuery] ListSharesRequestModel model)
        {
            var response = new BaseAPIResponse<BaseResult<List<ListShareResponseModel>>>();
            response.Data = await shareService.GetShares(model);
            return Ok(response);
        }

        [HttpGet]
        public async ValueTask<ActionResult<BaseAPIResponse<GetShareResponseModel>>> Detail([FromQuery] string Id)
        {
            var response = new BaseAPIResponse<GetShareResponseModel>();
            response.Data = await shareService.GetShare(Id);
            return Ok(response);
        }
    }
}
