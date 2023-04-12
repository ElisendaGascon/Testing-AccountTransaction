using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entities
{
    public class Customer : IReadOnlyEntity
    {
        public Customer()
        {
            Accounts = new List<Account>();
            AccountSummaries = new List<AccountSummary>();
        }

        [Key]
        public int Id { get; set; } 
        public Guid CustomerGuid { get; set; }
        public string FullName { get; set; } 
        public string Zipcode { get; set; } 

        public virtual ICollection<Account> Accounts { get; set; } 
        public virtual ICollection<AccountSummary> AccountSummaries { get; set; } 
    }
}
