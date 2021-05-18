using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Split.Application.Base;
using Split.Application.ViewModels;
using Split.Domain.Repositories;

namespace Split.Application.Queries
{
    public class QueryExpensesHandler : RequestHandler<QueryExpenses, ExpenseListViewModel>
    {
        private readonly IExpenseRepository _repository;

        public QueryExpensesHandler(IExpenseRepository repository)
        {
            _repository = repository;
        }

        protected override async Task<Result<ExpenseListViewModel>> HandleInternal(QueryExpenses request,
            CancellationToken cancellationToken)
        {
            var data = await _repository.All(cancellationToken);

            return new ExpenseListViewModel
            {
                Items = data.Select(e => e.ToViewModel())
            };
        }
    }
}