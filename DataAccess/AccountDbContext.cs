using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using DataAccess.Entities;
using DataAccess.Interfaces;

namespace DataAccess
{
    public class AccountDbContext : DbContext, IAccountDbContext
    {
        public AccountDbContext()
            : base("name=AccountsDbConnection")
        {
            //No pre-fetching
            Configuration.LazyLoadingEnabled = false;
            //Don't check for migrations
            Database.SetInitializer<AccountDbContext>(null);
            //Write out tsql
            Database.Log = Log;
        }

        private void Log(string sql)
        {
            System.Diagnostics.Debug.WriteLine(sql);
        }

        public override Task<int> SaveChangesAsync()
        {
            HandleChanges();
            return base.SaveChangesAsync();
        }

        private void HandleChanges()
        {
            var changed = ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Modified || 
                            e.State == EntityState.Added);
            //Reset read-only entities so they aren't changed in the database
            foreach (var item in changed)
            {
                if (item.Entity is IReadOnlyEntity)
                {
                    item.State = EntityState.Unchanged;
                }
                else if (item.Entity is IUpdateableEntity)
                {
                    var entity = (IUpdateableEntity) item.Entity;
                    entity.LastUpdated = DateTime.UtcNow;
                }
            }
        }

        public override int SaveChanges()
        {
            HandleChanges();
            return base.SaveChanges();
        }

        public DbSet<Account> Accounts { get; set; } 
        public DbSet<AccountSummary> AccountSummaries { get; set; } 
        public DbSet<Customer> Customers { get; set; } 
        public DbSet<Log> Logs { get; set; }
        public DbSet<Transaction> Transactions { get; set; } 
        public DbSet<TransactionType> TransactionTypes { get; set; } 
    }
}
