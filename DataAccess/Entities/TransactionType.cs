using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entities
{
    public class TransactionType : IReadOnlyEntity
    {
        public TransactionType()
        {
            Transactions = new List<Transaction>();
        }

        [Key]
        public int Id { get; set; } 
        public string Type { get; set; } 
        public string DisplayName { get; set; }

        public virtual ICollection<Transaction> Transactions { get; set; } 
        
    }
}
