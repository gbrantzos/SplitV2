using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Split.Application.Base;
using Split.Application.ViewModels;

namespace Split.Application.Queries
{
    public class QueryExpensesHandler : RequestHandler<QueryExpenses, ExpenseListViewModel>
    {
        private readonly ISplitDbContext _dbContext;

        public QueryExpensesHandler(ISplitDbContext dbContext)
            => _dbContext = dbContext;

        protected override async Task<Result<ExpenseListViewModel>> HandleInternal(QueryExpenses request,
            CancellationToken cancellationToken)
        {
            var data = await _dbContext
                .Expenses
                .AsNoTracking()
                .ToListAsync(cancellationToken: cancellationToken);

            return new ExpenseListViewModel
            {
                Items = data.Select(e => e.ToViewModel())
            };
        }
    }
}