using System;
using AccountSummaryLogic.Interfaces;
using AccountSummaryLogic.Models;
using DataAccess.Interfaces;

namespace AccountSummaryLogic
{
    public class AccountSummaryService : IAccountSummaryService
    {
        private readonly IRepository _repository;

        public AccountSummaryService(IRepository repository)
        {
            _repository = repository;
        }

        public AccountSummaryInfo RetrieveAccountSummary(Guid customerGuid)
        {
            var customer = _repository.GetCustomer(customerGuid);
            var accountSummary = _repository.UpdateAccountSummary(customer.Id);
            var accountNumbers = _repository.GetCustomerAccountNumbers(customer.Id);
            return new AccountSummaryInfo
            {
                AccountNumbers = accountNumbers,
                CustomerGuid = customerGuid,
                CustomerId = customer.Id,
                FullName = accountSummary.Customer.FullName,
                TotalAccountBalance = accountSummary.TotalAccountBalance,
                Zipcode = accountSummary.Customer.Zipcode
            };
        }

        public void UpdateCustomerSummary(int transactionId)
        {
            var customer = _repository.GetCustomerFromTransaction(transactionId);
            _repository.UpdateAccountSummary(customer.Id);
        }

        public void Dispose()
        {
            _repository.Dispose();
        }
    }
}
