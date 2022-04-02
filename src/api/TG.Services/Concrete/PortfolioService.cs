using TG.Common.Models.Request.Portfolio;
using TG.Common.Models.Response.Portfolio;
using TG.Core.Exceptions;
using TG.Domain.UnitOfWork;
using TG.Entities;
using TG.Services.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TG.Services.Concrete
{
    public class PortfolioService : IPortfolioService
    {
        private readonly IUnitOfWork unitOfWork;
        public PortfolioService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task Create(PortfolioCreateRequestModel model)
        {
            var user = await unitOfWork.userRepository.GetSingleAsync(model.UserId);

            if (user == null)
                throw new Exception("User not found");

            // fast operation
            await unitOfWork.portfolioRepository.CreateAsync(new Portfolio
            {
                UserId = model.UserId,
                Title = model.Name
            }).ContinueWith(x => unitOfWork.CommitAsync(), TaskContinuationOptions.OnlyOnRanToCompletion);

        }

        public async Task<PortfolioGetResponseModel> Get(GetPortfolioRequestModel model)
        {
            var user = await unitOfWork.userRepository.GetSingleAsync(model.UserId);

            if (user == null)
                throw new Exception("User not found");

            var portfolio = await unitOfWork.portfolioRepository.GetSingleAsync(model.PortfolioId);

            if (portfolio == null)
                throw new Exception("Portfolio not found");

            var portfolioShares = await unitOfWork.portfolioRepository.GetAllAsNoTrackingQueryable().Where(x => x.PortfolioId == portfolio.ID).ToListAsync();

            var shareIds = portfolioShares.Select(x => x.ShareId).ToList();

            var shares = await unitOfWork.shareRepository.GetAllAsNoTrackingQueryable().Where(x => shareIds.Contains(x.ID)).Include(x => x.SharePrices).ToListAsync();

            var shareInfos = new List<ShareInfo>();

            foreach (var item in portfolioShares)
            {
                shareInfos.Add(new ShareInfo { 
                    TotalAmount = item.Amount,
                    TotalPrice = item.TotalPrice,
                    CreatedOn = item.CreatedOn,
                    ShareCode = item.ShareCode,
                    ShareId = item.ShareId,
                    CurrentPrice = shares.FirstOrDefault(x => x.ID == item.ShareId).SharePrices.LastOrDefault().Price
                });
            }

            var response = new PortfolioGetResponseModel
            {
                Portfolio = new PortfolioInfo
                {
                    ID = portfolio.ID,
                    Title = portfolio.Title,
                    ShareInfos = shareInfos
                }
            };

            return response;
        }

        public async Task<PortfolioListResponseModel> List(string UserId)
        {
            if (!await unitOfWork.userRepository.AnyAsync(x => x.ID == UserId))
                throw new NotFoundException("User not found.");

            var response = new PortfolioListResponseModel();
            response.Portfolios = await unitOfWork.portfolioRepository.GetAllAsNoTrackingQueryable().Where(x => x.PortfolioId == null && x.UserId == UserId).Select(x => new PortfolioInfo
            { 
                ID = x.ID,
                Title = x.Title
            }).ToListAsync();

            return response;
        }
    }
}
