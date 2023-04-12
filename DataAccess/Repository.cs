using DataAccess.Entities;
using DataAccess.Enums;
using DataAccess.Exceptions;
using DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess
{
    public class Repository : IRepository
    {
        private readonly IAccountDbContext _context;
        private readonly object _lock = new object();

        public Repository(IAccountDbContext accountDbContext)
        {
            _context = accountDbContext;
        }

        public async Task Log(string message, LogLevels level)
        {
            var log = new Log { Message = message, LogLevel = level };
            _context.Logs.Add(log);
            await _context.SaveChangesAsync();
        }

        private void LockAccount(string accountNumber)
        {
            lock (_lock)
            {
                if (AccountIsLocked(accountNumber))
                    throw new AccountLockedException(accountNumber);

                var account = _context.Accounts.
                    First(x => x.AccountNumber.Equals(accountNumber));

                account.Locked = true;
                _context.SaveChanges();
            }
        }

        private void UnlockAccount(string accountNumber)
        {
            lock (_lock)
            {
                var account = _context.Accounts.
                    First(x => x.AccountNumber.Equals(accountNumber));
                account.Locked = false;
                _context.SaveChanges();
            }
        }

        private bool AccountIsLocked(string accountNumber)
        {
            return _context.Accounts
                .First(x => x.AccountNumber.Equals(accountNumber))
                .Locked;
        }

        public int AddTransaction(Transaction transaction)
        {
            var accountNumber = transaction.Account.AccountNumber;

            LockAccount(accountNumber);
            _context.Transactions.Add(transaction);
            _context.SaveChanges();
            UnlockAccount(accountNumber);

            return transaction.Id;
        }

        public void UpdateTransaction(int transactionId, bool cleared)
        {
            var transaction = _context.Transactions
                .First(x => x.Id.Equals(transactionId));
            transaction.Cleared = cleared;
            _context.SaveChanges();
        }

        public int GetCustomerIdFromAccountNumber(string accountNumber)
        {
            var account = _context.Accounts
                .FirstOrDefault(x => x.AccountNumber.Equals(accountNumber));
            return account?.CustomerId ?? 0;
        }

        public Account GetAccount(string accountNumber)
        {
            return _context.Accounts
                .Include("Customer")
                .First(x => x.AccountNumber.Equals(accountNumber));
        }

        public AccountSummary UpdateAccountSummary(int customerId)
        {
            var accountSummary = GetAccountSummary(customerId);

            var totalBalance = _context.Transactions
                    .Where(x => x.Account.CustomerId.Equals(customerId))
                    .Sum(x => x.Amount);

            var lastTransaction = _context.Transactions
                .OrderByDescending(x => x.DateAdded)
                .FirstOrDefault();

            accountSummary.TotalAccountBalance = totalBalance;
            accountSummary.LastTransactionId = lastTransaction?.Id ?? 0;

            _context.SaveChanges();
            return accountSummary;
        }

        public AccountSummary GetAccountSummary(int customerId)
        {
            return _context.AccountSummaries
                .Include("Customer")
                .FirstOrDefault(x => x.CustomerId.Equals(customerId))
                ?? AddAccountSummary(customerId); //Add if not yet created
        }

        private AccountSummary AddAccountSummary(int customerId)
        {
            var accountSummary = new AccountSummary
            {
                CustomerId = customerId
            };
            _context.AccountSummaries.Add(accountSummary);
            _context.SaveChanges();
            accountSummary.Customer = GetCustomer(customerId);
            return accountSummary;
        }

        public Customer GetCustomer(int customerId)
        {
            return _context.Customers
                .FirstOrDefault(x => x.Id.Equals(customerId));
        }

        public Customer GetCustomer(Guid customerGuid)
        {
            return _context.Customers
                .FirstOrDefault(x => x.CustomerGuid.Equals(customerGuid));
        }

        public Customer GetCustomerFromTransaction(int transactionId)
        {
            return _context.Transactions
                .Include("Account.Customer")
                .First(x => x.Id.Equals(transactionId))
                .Account
                .Customer;
        }

        public IList<string> GetCustomerAccountNumbers(int customerId)
        {
            return _context.Accounts
                .Where(x => x.CustomerId.Equals(customerId))
                .Select(x => x.AccountNumber)
                .ToList();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
