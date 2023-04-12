using System;
using System.Collections.Generic;

namespace AccountSummaryLogic.Models
{
    public class AccountSummaryInfo
    {
        public int CustomerId { get; set; }
        public Guid CustomerGuid { get; set; }
        public string FullName { get; set; }
        public string Zipcode { get; set; }
        public IList<string> AccountNumbers { get; set; }
        public decimal TotalAccountBalance { get; set; }
    }
}
