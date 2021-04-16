using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Split.Application.Base;

namespace Split.Application.Commands
{
    public class DeleteExpenseHandler : RequestHandler<DeleteExpense>
    {
        private readonly ISplitDbContext _dbContext;

        public DeleteExpenseHandler(ISplitDbContext dbContext) => _dbContext = dbContext;

        protected override async Task<Result<bool>> HandleInternal(DeleteExpense request, CancellationToken cancellationToken)
        {
            var expense = await _dbContext
                .Expenses
                .FirstOrDefaultAsync(ex => ex.Id == request.Id && ex.RowVersion == request.RowVersion,
                    cancellationToken);
            if (expense == null)
                return Result.Failure($"Could not find expense with ID {request.Id}");

            _dbContext.Expenses.Remove(expense);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}