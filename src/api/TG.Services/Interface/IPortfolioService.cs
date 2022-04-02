using System.Threading.Tasks;
using TG.Common.Models.Request.Portfolio;
using TG.Common.Models.Response.Portfolio;

namespace TG.Services.Interface
{
    public interface IPortfolioService
    {
        Task Create(PortfolioCreateRequestModel model);
        Task<PortfolioGetResponseModel> Get(GetPortfolioRequestModel model);
        Task<PortfolioListResponseModel> List(string UserId);
    }
}
