using System.Collections.Generic;
using Split.Application.Base;
using Split.Domain.Models;

namespace Split.Application.Queries
{
    public class QueryExpenses : BaseRequest<ExpensesList>
    {
        public bool IncludeAll { get; set; }

        public override string ToString() 
            => IncludeAll ? "Get all expenses" : "Get unallocated expenses";
    }

    public class ExpensesList
    {
        public IEnumerable<Expense> Items { get; set; }
    }
}