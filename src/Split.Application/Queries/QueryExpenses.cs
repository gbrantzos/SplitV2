using System;
using Split.Application.Base;
using Split.Application.ViewModels;

namespace Split.Application.Queries
{
    public class QueryExpenses : Request<ExpenseListViewModel>
    {
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public string Description { get; set; }
        public bool IncludeAll { get; init; }

        // TODO Cleanup this mess
        public override string ToString() 
            => IncludeAll ? "Get all expenses" : "Get unallocated expenses";
    }
}