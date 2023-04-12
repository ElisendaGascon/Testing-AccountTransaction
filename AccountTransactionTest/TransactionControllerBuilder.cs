using AccountTransactionLogic.Interfaces;
using AccountTransactionApi.Controllers;
using Moq;
using System.Net.Http;

namespace AccountTransactionTest
{
    internal class TransactionControllerBuilder
    {
        private IAccountTransactionService _accountTransactionService;
        private bool _defaultAccountTransactionService = true;

        public TransactionControllerBuilder WithAccountTransactionService
            (IAccountTransactionService accountTransactionService)
        {
            _defaultAccountTransactionService = false;
            _accountTransactionService = accountTransactionService;
            return this;
        }

        public TransactionController Build ()
        {
            if (_defaultAccountTransactionService)
                _accountTransactionService = DefaultAccountTransactionService().Object;
            var controller = new TransactionController(_accountTransactionService);
            controller.Configuration = new System.Web.Http.HttpConfiguration();
            controller.Request = new HttpRequestMessage();
            return controller;
        }

        public Mock<IAccountTransactionService> DefaultAccountTransactionService()
        {
            var mock = new Mock<IAccountTransactionService>();
            return mock;
        }
    }
}