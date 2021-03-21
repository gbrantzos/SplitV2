using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Split.Application.Base;

namespace Split.Application.Queries
{
    public class QueryExpensesHandler : RequestHandler<QueryExpenses, ExpensesList>
    {
        private readonly ISplitDbContext _dbContext;

        public QueryExpensesHandler(ISplitDbContext dbContext) 
            => _dbContext = dbContext;

        protected override async Task<Result<ExpensesList>> HandleInternal(QueryExpenses request,
            CancellationToken cancellationToken)
        {
            var data = await _dbContext
                .Expenses
                .AsNoTracking()
                .ToListAsync(cancellationToken: cancellationToken);

            return new ExpensesList()
            {
                Items = data.Select(e => new ExpenseItem
                {
                    Id = e.Id,
                    RowVersion = e.RowVersion,
                    Description = e.Description,
                    Category = e.Category,
                    EntryDate = e.EntryDate,
                    ForOwner = e.ForOwner,
                    Value = e.Value.Amount
                })
            };
        }
    }
}