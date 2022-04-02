using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TG.Common.Models.Request;
using TG.Common.Models.Request.Portfolio;
using TG.Core.Response;
using TG.Services;
using TG.Services.Concrete;

namespace TG.API.Controllers
{
    [Authorize(Roles = "User")]
    public class PortfolioController : BaseController<PortfolioController>
    {
        private readonly PortfolioService portfolioService;
        private readonly ICurrentUserService currentUser;

        public PortfolioController(PortfolioService portfolioService, ICurrentUserService currentUser)
        {
            this.portfolioService = portfolioService;
            this.currentUser = currentUser;
        }

        [HttpPost]
        public async ValueTask<ActionResult<BaseAPIResponse<string>>> Create([FromQuery] PortfolioCreateRequestModel model)
        {
            var response = new BaseAPIResponse<string>();

            model.UserId = currentUser.UserId;
            await portfolioService.Create(model);
            response.Message = "Your portfolio created successfully!";
            return Ok(response);
        }
    }
}
