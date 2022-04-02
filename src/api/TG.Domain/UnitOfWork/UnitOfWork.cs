using TG.Domain.Context;
using TG.Domain.Repository;
using TG.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Transaction = TG.Entities.Transaction;

namespace TG.Domain.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext context;

        public UnitOfWork(AppDbContext context)
        {
            this.context = context;
            transactionRepository = new EntityRepository<Transaction>(context);
            shareRepository = new EntityRepository<Share>(context);
            portfolioRepository = new EntityRepository<Portfolio>(context);
            sharePriceRepository = new EntityRepository<SharePrice>(context);
            userRepository = new EntityRepository<User>(context);
        }

        public IEntityRepository<Transaction> transactionRepository { get; set; }
        public IEntityRepository<Share> shareRepository { get; set; }
        public IEntityRepository<Portfolio> portfolioRepository { get; set; }
        public IEntityRepository<SharePrice> sharePriceRepository { get; set; }
        public IEntityRepository<User> userRepository { get; set; }

        public int Commit()
        {
            return context.SaveChanges();
        }

        public async ValueTask<int> CommitAsync()
        {
            return await context.SaveChangesAsync();
        }

        public void Dispose()
        {
            GC.SuppressFinalize(context);
            context.Dispose();
        }

        public DbContext DbContext()
        {
            return context;
        }

        public Task DisposeAsync()
        {
            GC.SuppressFinalize(context);
            context.DisposeAsync();
            return Task.CompletedTask;
        }
    }
}
