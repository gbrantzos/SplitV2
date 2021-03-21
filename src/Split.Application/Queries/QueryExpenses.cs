using System;
using System.Collections.Generic;
using Split.Application.Base;

namespace Split.Application.Queries
{
    public class QueryExpenses : Request<ExpensesList>
    {
        public bool IncludeAll { get; init; }

        public override string ToString() 
            => IncludeAll ? "Get all expenses" : "Get unallocated expenses";
    }

    public class ExpenseItem
    {
        public int Id { get; set; }
        public int RowVersion { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public decimal Value { get; set; }
        public DateTime EntryDate { get; set; }
        public bool ForOwner { get; set; }

    }
    public class ExpensesList
    {
        public IEnumerable<ExpenseItem> Items { get; set; }
    }
}