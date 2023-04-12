using System;
using AccountSummaryLogic.Models;

namespace AccountSummaryLogic.Interfaces
{
    public interface IAccountSummaryService : IDisposable
    {
        AccountSummaryInfo RetrieveAccountSummary(Guid customerGuid);
        void UpdateCustomerSummary(int transactionId);
    }
}