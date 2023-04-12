using System;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entities
{
    public class AccountSummary : IUpdateableEntity
    {
        [Key]
        public int Id { get; set; } 
        public int CustomerId { get; set; } 
        public decimal TotalAccountBalance { get; set; } 
        public int LastTransactionId { get; set; } 
        public DateTime LastUpdated { get; set; } 

        public virtual Customer Customer { get; set; } 
    }
}
