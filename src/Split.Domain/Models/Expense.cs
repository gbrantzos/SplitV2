using System;
using Split.Domain.Base;

namespace Split.Domain.Models
{
    public class Expense : BaseEntity
    {
        public string Description { get; set; }
        public string Category { get; set; }
        public decimal Amount { get; set; }
        public DateTime EntryDate { get; set; }
        public bool ForOwner { get; set; }
        public DateTime? PaidAt { get; set; }
    }
}
