using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Split.Application.Base;

namespace Split.Application.Queries
{
    public class QueryExpensesHandler : BaseHandler<QueryExpenses, ExpensesList>
    {
        private readonly ISplitDbContext _dbContext;

        public QueryExpensesHandler(ISplitDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public override async Task<Result<ExpensesList>> Handle(QueryExpenses request, CancellationToken cancellationToken)
        {
            var data = await _dbContext
                .Expenses
                .AsNoTracking()
                .ToListAsync(cancellationToken: cancellationToken);

            return new ExpensesList()
            {
                Items = data
            };
        }
    }
}