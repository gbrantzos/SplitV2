using System;
using System.Collections.Generic;

namespace Split.Application.ViewModels
{
    public class ExpenseViewModel
    {
        public int Id { get; init; }
        public int RowVersion { get; init; } = 1;
        public string Description { get; init; }
        public string Category { get; init; }
        public decimal Value { get; init; }
        public DateTime EntryDate { get; init; }
        public bool ForOwner { get; init; }
    }
    
    public class ExpenseListViewModel
    {
        public IEnumerable<ExpenseViewModel> Items { get; init; }
    }
}