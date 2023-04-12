using AccountSummaryLogic.Interfaces;
using System;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using AccountSummaryLogic.Models;

namespace AccountSummaryApi.Controllers
{
    /// <summary>
    /// Find information about a customer and their accounts
    /// </summary>
    public class SummaryController : ApiController
    {
        private readonly IAccountSummaryService _accountSummaryService;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="accountSummaryService"></param>
        public SummaryController(IAccountSummaryService accountSummaryService)
        {
            _accountSummaryService = accountSummaryService;
        }

        /// <summary>
        /// Get a customer summary
        /// </summary>
        /// <param name="customerGuid"></param>
        /// <returns></returns>
        [HttpGet]
        [ResponseType(typeof(AccountSummaryInfo))]
        public IHttpActionResult CustomerSummary(Guid customerGuid)
        {
            try
            {
                var summary = _accountSummaryService
                    .RetrieveAccountSummary(customerGuid);

                return Content(HttpStatusCode.OK, summary);
            }
            catch
            {
                return Content(HttpStatusCode.InternalServerError,
                    "Customer Summary Failed");
            }

        }

        /// <summary>
        /// Updates a customer's account summary after a transaction is posted
        /// </summary>
        /// <param name="transactionId"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult UpdateCustomer(int transactionId)
        {
            try
            {
                _accountSummaryService
                    .UpdateCustomerSummary(transactionId);

                return Ok();
            }
            catch
            {
                return Content(HttpStatusCode.InternalServerError,
                    "Customer Summary Update Failed");
            }
            finally
            {
                _accountSummaryService.Dispose();
            }

        }
    }
}
