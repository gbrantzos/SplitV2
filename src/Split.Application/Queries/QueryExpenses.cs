using Split.Application.Base;
using Split.Application.ViewModels;

namespace Split.Application.Queries
{
    public class QueryExpenses : Request<ExpenseListViewModel>
    {
        public bool IncludeAll { get; init; }

        public override string ToString() 
            => IncludeAll ? "Get all expenses" : "Get unallocated expenses";
    }
}