using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entities
{
    public class Account : IUpdateableEntity
    {
        public Account()
        {
            Locked = false;
            Transactions = new List<Transaction>();
        }

        [Key]
        public int Id { get; set; } 
        public string AccountNumber { get; set; }
        public DateTime? DateOpened { get; set; }
        public DateTime? DateClosed { get; set; } 
        public DateTime LastUpdated { get; set; } 
        public int CustomerId { get; set; } 
        public bool Locked { get; set; } 

        
        public virtual ICollection<Transaction> Transactions { get; set; } 
        public virtual Customer Customer { get; set; } 
    }
}
