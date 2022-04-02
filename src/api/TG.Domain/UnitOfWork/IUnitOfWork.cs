using TG.Domain.Repository;
using TG.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TG.Domain.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        public IEntityRepository<Transaction> transactionRepository { get; set; }
        public IEntityRepository<Share> shareRepository { get; set; }
        public IEntityRepository<Portfolio> portfolioRepository { get; set; }
        public IEntityRepository<SharePrice> sharePriceRepository { get; set; }
        public IEntityRepository<User> userRepository { get; set; }
        public DbContext DbContext();
        public ValueTask<int> CommitAsync();
        public int Commit();
        public Task DisposeAsync();
    }
}
