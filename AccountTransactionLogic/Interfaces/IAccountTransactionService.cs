using System;
using System.Threading.Tasks;
using AccountTransactionLogic.Models;
using DataAccess.Enums;

namespace AccountTransactionLogic.Interfaces
{
    public interface IAccountTransactionService : IDisposable
    {
        Task Log(string message, LogLevels level);
        int CommitTransaction(TransactionInfo transactionInfo);
        void TransactionCleared(int transactionId);
        void TransactionFailed(int transactionId);
        Task SummarizeTransactions(string accountNumber);
    }
}