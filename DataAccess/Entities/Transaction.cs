using System;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entities
{
    public class Transaction : IUpdateableEntity
    {
        [Key]
        public int Id { get; set; } 
        public decimal Amount { get; set; } 
        public DateTime TransactionDate { get; set; } 
        public bool? Cleared { get; set; } 
        public DateTime DateAdded { get; set; } 
        public DateTime LastUpdated { get; set; }
        public int AccountId { get; set; }
        public int TransactionTypeId { get; set; } 

        
        public virtual Account Account { get; set; } 
        public virtual TransactionType TransactionType { get; set; }
    }
}
