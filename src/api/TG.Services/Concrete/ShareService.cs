using TG.Common.Models.Request.Share;
using TG.Common.Models.Response.Share;
using TG.Core.Exceptions;
using TG.Core.Request;
using TG.Domain.UnitOfWork;
using TG.Services.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TG.Services.Concrete
{
    public class ShareService : IShareService
    {
        private readonly IUnitOfWork unitOfWork;

        public ShareService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<GetShareResponseModel> GetShare(string Id)
        {
            var entity = await unitOfWork.shareRepository.GetSingleAsync(Id);

            if (entity == null)
                throw new NotFoundException("The share you are looking for was not found.");

            var result = new GetShareResponseModel();
            result.CompanyName = entity.Title;
            result.ShareCode = entity.ShareCode;
            var prices = await unitOfWork.sharePriceRepository.GetAllAsQueryable().Where(x => x.ShareId == entity.ID).ToListAsync();

            foreach(var item in prices)
            {
                result.Prices.Add(new SharePriceMM { 
                    Price = item.Price,
                    CreatedOn = item.CreatedOn
                });
            }

            return result;

        }

        public async Task<BaseResult<List<ListShareResponseModel>>> GetShares(ListSharesRequestModel model)
        {
            var result = new BaseResult<List<ListShareResponseModel>>();

            var temp = unitOfWork.shareRepository.GetAllAsQueryable();

            if(String.IsNullOrEmpty(model.Filter.SearchTerm))
            {
                var lower = model.Filter.SearchTerm.ToLower();

                temp = temp.Where(x => x.Email.Contains(model.Filter.SearchTerm) ||
                                       x.Address.Contains(model.Filter.SearchTerm) ||
                                       x.Title.Contains(model.Filter.SearchTerm) ||
                                       x.PhoneNumber.Contains(model.Filter.SearchTerm) ||
                                       x.Email.ToLower().Contains(lower) ||
                                       x.Address.ToLower().Contains(lower) ||
                                       x.Title.ToLower().Contains(lower));
            }

            var items = await temp.Take(model.Filter.Take).Skip(model.Filter.Skip*model.Filter.Take).ToListAsync();

            var shareIds = items.Select(x => x.ID).ToList();
            var prices = await unitOfWork.sharePriceRepository.GetAllAsQueryable().Where(x => shareIds.Contains(x.ID)).ToListAsync();

            foreach(var item in items)
            {
                result.Items.Add(new ListShareResponseModel { 
                    ID = item.ID,
                    Amount = item.TotalAmountOfShare,
                    CompanyName = item.Title,
                    Price = prices.Where(x => x.ShareId == item.ID).LastOrDefault().Price,
                    CreatedOn = item.CreatedOn,
                    ShareCode = item.ShareCode
                });
            }

            result.TotalCount = await unitOfWork.shareRepository.CountAsync(x => true);
            result.FilteredCount = temp.Count();

            result.PageCount = result.FilteredCount / model.Filter.Take;
            if (result.FilteredCount % model.Filter.Take > 0)
                result.PageCount += 1;

            if (model.Filter.Skip == 0)
                result.CurrentPage = 1;
            else
                result.CurrentPage = (model.Filter.Skip / model.Filter.Take) + 1;

            return result;

        }
    }
}
