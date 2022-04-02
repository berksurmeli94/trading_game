using TG.Common.Models.Request.Transaction;
using TG.Core.Exceptions;
using TG.Domain.UnitOfWork;
using TG.Entities;
using TG.Services.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace TG.Services.Concrete
{
    public class TransactionService : ITransactionService
    {
        private readonly IUnitOfWork unitOfWork;
        public TransactionService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public Task Buy(BuyRequestModel model)
{
            if (!unitOfWork.userRepository.Any(x => x.ID == model.UserId))
                throw new NotFoundException("User not found.");

            var portfolio = unitOfWork.portfolioRepository.GetAllAsQueryable().FirstOrDefault(x => x.ID == model.PortfolioId);

            if (portfolio == null)
                throw new NotFoundException("Portfolio not found.");

            if(!unitOfWork.shareRepository.Any(x => x.ID == model.ShareId))
                throw new NotFoundException("Share not found.");

            var context = unitOfWork.DbContext();

            using (var transaction = context.Database.BeginTransaction(IsolationLevel.RepeatableRead))
            {
                try
                {
                    lock (transaction)
                    {
                        var share = context.Set<Share>().AsQueryable().Where(x => x.ID == model.ShareId).FirstOrDefault();

                        if (share.TotalAmountOfShare < model.BuyAmount)
                            throw new ValidationException("You cannot buy this much.");

                        var currentPrice = context.Set<SharePrice>().AsQueryable().Where(x => x.ID == model.ShareId).OrderByDescending(x => x.CreatedOn).LastOrDefault();

                        decimal cost = share.TotalAmountOfShare * currentPrice.Price;

                        var user = context.Set<User>().AsQueryable().FirstOrDefault(x => x.ID == model.UserId);

                        if(cost < user.TotalMoney)
                            throw new ValidationException("You do not have enough to buy this much.");

                        share.TotalAmountOfShare -= model.BuyAmount;
                        user.TotalMoney -= cost;

                        if (share.TotalAmountOfShare < 0 || user.TotalMoney < 0)
                            throw new Exception();

                        context.Set<Portfolio>().Add(new Portfolio { 
                            Amount = model.BuyAmount,
                            PortfolioId = portfolio.ID,
                            ShareCode = share.ShareCode,
                            ShareId = share.ID,
                            Title = portfolio.Title,
                            TotalPrice = cost,
                            UserId = model.UserId
                        });

                        context.SaveChanges();
                        transaction.Commit();
                    }
                }
                catch (ValidationException ex)
                {
                    throw ex;
                }
                catch (NotFoundException ex)
                {
                    throw ex;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                }
            }

            return Task.CompletedTask;
        }

        public Task Sell(SellRequestModel model)
        {
            throw new NotImplementedException();
        }
    }
}
