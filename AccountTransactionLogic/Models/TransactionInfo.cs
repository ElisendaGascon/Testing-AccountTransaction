using System;

namespace AccountTransactionLogic.Models
{
    public class TransactionInfo
    {
        public decimal Amount { get; set; }
        public DateTime TransactionDate { get; set; }
        public string AccountNumber { get; set; }

    }
}
