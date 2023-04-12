using System.Threading.Tasks;

namespace AccountTransactionLogic.Interfaces
{
    public interface INotifyAccountSummary
    {
        Task SummarizeTransactions(string accountNumber);
    }
}