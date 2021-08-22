using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Split.Domain.Models;
using Split.Domain.Repositories;

namespace Split.Infrastructure.Persistence.Repositories
{
    public class ExpenseRepository : IExpenseRepository
    {
        private readonly SplitDbContext _dbContext;

        public ExpenseRepository(SplitDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Expense> GetById(int id, CancellationToken cancellationToken = default)
        {
            var expense = await _dbContext.Expenses.FindAsync(new object[] { id }, cancellationToken);
            return expense;
        }

        public async Task<IEnumerable<Expense>> Query(ExpenseQueryParameters parameters,
            CancellationToken cancellationToken = default)
        {
            var query = _dbContext
                .Expenses
                .AsNoTracking();
            
            if (!String.IsNullOrEmpty(parameters.Description))
                query = query.Where(e => e.Description.StartsWith(parameters.Description));
            if (parameters.DateFrom != null)
                query = query.Where(e => e.EntryDate >= parameters.DateFrom);
            if (parameters.DateTo != null)
                query = query.Where(e => e.EntryDate <= parameters.DateTo);
            
            return await query.ToListAsync(cancellationToken);
        }

        public async Task Save(Expense expense, CancellationToken cancellationToken = default)
        {
            if (expense.IsNew)
                _dbContext.Add(expense);

            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task Delete(Expense expense, CancellationToken cancellationToken = default)
        {
            _dbContext.Expenses.Remove(expense);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}