using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Threading;
using System.Threading.Tasks;
using DataAccess.Entities;

namespace DataAccess.Interfaces
{
    public interface IAccountDbContext
    {
        int SaveChanges();
        DbSet<Account> Accounts { get; set; }
        DbSet<AccountSummary> AccountSummaries { get; set; }
        DbSet<Customer> Customers { get; set; }
        DbSet<Log> Logs { get; set; }
        DbSet<Transaction> Transactions { get; set; }
        DbSet<TransactionType> TransactionTypes { get; set; }
        Database Database { get; }
        DbSet Set(Type entityType);
        Task<int> SaveChangesAsync();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        IEnumerable<DbEntityValidationResult> GetValidationErrors();
        DbEntityEntry Entry(object entity);
        void Dispose();
    }
}