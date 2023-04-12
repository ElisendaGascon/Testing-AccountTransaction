using System;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using AccountTransactionLogic.Interfaces;
using AccountTransactionLogic.Models;

namespace AccountTransactionApi.Controllers
{
    /// <summary>
    /// Manage transactions for customers
    /// </summary>
    public class TransactionController : ApiController
    {
        private readonly IAccountTransactionService _accountTransactionService;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="accountTransactionService"></param>
        public TransactionController(
            IAccountTransactionService accountTransactionService)
        {
            _accountTransactionService = accountTransactionService;
        }

        /// <summary>
        /// Post transactions for a customer
        /// </summary>
        /// <param name="transactionInfo"></param>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(int))]
        public async Task<IHttpActionResult> PostTransaction(
            TransactionInfo transactionInfo)
        {
            try
            {
                var id = _accountTransactionService
                    .CommitTransaction(transactionInfo);
                await _accountTransactionService.Log(
                    $"Commited transaction for {transactionInfo.AccountNumber}",
                    DataAccess.Enums.LogLevels.Info);


                await _accountTransactionService
                    .SummarizeTransactions(transactionInfo.AccountNumber);
                await _accountTransactionService.Log(
                    $"Updated summary for {transactionInfo.AccountNumber}",
                    DataAccess.Enums.LogLevels.Info);


                return Ok(id);
            }
            catch (ArgumentException ae)
            {
                await _accountTransactionService
                    .Log(ae.ToString(), DataAccess.Enums.LogLevels.Error);
                return Content(HttpStatusCode.NotFound, ae.Message);
            }
            catch (HttpResponseException hre)
            {
                await _accountTransactionService
                    .Log(hre.ToString(), DataAccess.Enums.LogLevels.Error);
                return Content(HttpStatusCode.InternalServerError,
                    $"Failed to summarize account {transactionInfo.AccountNumber}, {hre.Message}");
            }
            catch (Exception ex)
            {
                await _accountTransactionService
                    .Log(ex.ToString(), DataAccess.Enums.LogLevels.Error);
                return Content(HttpStatusCode.InternalServerError,
                    $"Transaction post failed {transactionInfo.AccountNumber}, {ex.Message}");
            }
            finally
            {
                _accountTransactionService.Dispose();
            }
        }

    }
}
