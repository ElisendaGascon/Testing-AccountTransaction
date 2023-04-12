using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DataAccess.Enums;

namespace DataAccess.Interfaces
{
    public interface IRepository : IDisposable
    {
        Task Log(string message, LogLevels level);
        int AddTransaction(Transaction transaction);
        int GetCustomerIdFromAccountNumber(string accountNumber);
        Account GetAccount(string accountNumber);
        AccountSummary UpdateAccountSummary(int customerId);
        AccountSummary GetAccountSummary(int customerId);
        Customer GetCustomer(int customerId);
        Customer GetCustomer(Guid customerGuid);
        Customer GetCustomerFromTransaction(int transactionId);
        IList<string> GetCustomerAccountNumbers(int customerId);
        void UpdateTransaction(int transactionId, bool cleared);
    }
}
