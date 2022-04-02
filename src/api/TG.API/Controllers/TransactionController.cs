using Microsoft.AspNetCore.Mvc;
using TG.Services.Interface;
using TG.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using TG.Common.Models.Request.Share;
using TG.Common.Models.Response.Share;
using TG.Core.Request;
using TG.Core.Response;
using TG.Services.Concrete;
using TG.Common.Models.Request.Transaction;

namespace TG.API.Controllers
{
    public class TransactionController : BaseController<TransactionController>
    {
        private readonly ITransactionService transactionService;
        private readonly ICurrentUserService currentUser;

        public TransactionController(ITransactionService transactionService, ICurrentUserService currentUser)
        {
            this.transactionService = transactionService;
            this.currentUser = currentUser;
        }

        public async ValueTask<ActionResult<BaseAPIResponse<bool>>> Buy([FromQuery] BuyRequestModel model)
        {
            var response = new BaseAPIResponse<bool>();
            model.UserId = currentUser.UserId;
            await transactionService.Buy(model);
            response.Data = true;
            return Ok(response);
        }

        public async ValueTask<ActionResult<BaseAPIResponse<bool>>> Sell([FromQuery] SellRequestModel model)
        {
            var response = new BaseAPIResponse<bool>();
            model.UserId = currentUser.UserId;
            await transactionService.Sell(model);
            response.Data = true;
            return Ok(response);
        }
    }
}
