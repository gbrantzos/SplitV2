using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Split.Application.Base;
using Split.Domain.Models;

namespace Split.Application.Commands
{
    public class SaveExpenseHandler : BaseHandler<SaveExpense>
    {
        private readonly ISplitDbContext _dbContext;
        private readonly ILogger<SaveExpenseHandler> _logger;

        public SaveExpenseHandler(ISplitDbContext dbContext, ILogger<SaveExpenseHandler> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public override async Task<Result<bool>> Handle(SaveExpense request, CancellationToken cancellationToken)
        {
            var expense = request.Id > 0
                ? await _dbContext.Expenses.FindAsync(new[] {request.Id}, cancellationToken)
                : new Expense();

            expense.Description = request.Description;
            expense.Category = request.Category;
            expense.Amount = request.Amount;
            expense.ForOwner = request.ForOwner;

            if (expense.IsNew)
            {
                expense.EntryDate = DateTime.Now.Date;
                _dbContext.Expenses.Add(expense);
            }
            else
            {
                expense.RowVersion = request.RowVersion;
            }

            try
            {
                await _dbContext.SaveChangesAsync(cancellationToken);
                return true;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                var entry = ex.Entries.First();
                _logger.LogError(ex, "Concurrency conflict on table {Table}", entry.Entity.GetType().Name);

                return Result.Failure(ex.Message);
            }
        }
    }
}