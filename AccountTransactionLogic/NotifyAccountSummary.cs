using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http;
using AccountTransactionLogic.Interfaces;
using DataAccess.Interfaces;

namespace AccountTransactionLogic
{
    public class NotifyAccountSummary : INotifyAccountSummary
    {
        private readonly IRepository _repository;

        public NotifyAccountSummary(IRepository repository)
        {
            _repository = repository;
        }

        public async Task SummarizeTransactions(string accountNumber)
        {
            var accountSummaryBaseUrl = 
                ConfigurationManager.AppSettings["AccountSummaryBaseUrl"];
            var requestUri = accountSummaryBaseUrl + 
                $"Summary?accountNumber={accountNumber}";
            var handler = new HttpClientHandler { UseDefaultCredentials = true };
            var client = new HttpClient(handler);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept
                .Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var response = await client.PostAsync(requestUri, null);

            if (!response.IsSuccessStatusCode)
            {
                await _repository
                    .Log($"Unable to notify account summary for account {accountNumber}",
                    DataAccess.Enums.LogLevels.Error);
                throw new HttpResponseException(response);
            }
            await _repository
                .Log($"Notified account summary for account {accountNumber}",
                DataAccess.Enums.LogLevels.Debug);
        }
    }
}
