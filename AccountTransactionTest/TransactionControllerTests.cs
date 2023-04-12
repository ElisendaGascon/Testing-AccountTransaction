using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using Moq;
using System;
using AccountTransactionLogic.Interfaces;
using AccountTransactionApi.Controllers;
using AccountTransactionLogic.Models;
using System.Web.Http.Results;
using System.Threading.Tasks;
using System.Net;

namespace AccountTransactionTest
{
    [TestClass]
    public class TransactionControllerTests
    {
        private TransactionInfo GetTestableTransactionInfo()
        {
            return new TransactionInfo
            {
                AccountNumber = "1",
                Amount = 100,
                TransactionDate = new DateTime(2000, 1, 1)
            };

        }

        [TestMethod]
        public async Task PostTransaction_WithValidAccount_Returns200OK()
        {
            //Arrange
            var builder = new TransactionControllerBuilder();
            var controller = builder.Build();
            var transactionInfo = GetTestableTransactionInfo();

            //Act
            var actionResult = await controller.PostTransaction(transactionInfo);

            //Assert
            actionResult.Should()
                .BeOfType(typeof(OkNegotiatedContentResult<int>));
        }

        [TestMethod]
        public async void PostTransaction_WithInvalidAccount_ReturnsNotFound()
        {
            //Arrange
            var builder = new TransactionControllerBuilder();
            var service = builder.DefaultAccountTransactionService();
            service.Setup(x => x.CommitTransaction(It.IsAny<TransactionInfo>()))
                .Throws<ArgumentException>();
            var controller = builder
                .WithAccountTransactionService(service.Object)
                .Build();
            var transactionInfo = GetTestableTransactionInfo();

            //Act
            var actionResult = await controller.PostTransaction(transactionInfo);
            var content = actionResult as NegotiatedContentResult<string>;

            //Assert
            content.StatusCode.Should().Be(HttpStatusCode.NotFound);    
        }
    }
}
