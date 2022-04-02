using TG.Common.Models.Request.Transaction;
using System.Threading.Tasks;

namespace TG.Services.Interface
{
    public interface ITransactionService
    {
        public Task Buy(BuyRequestModel model);
        public Task Sell(SellRequestModel model);
    }
}
