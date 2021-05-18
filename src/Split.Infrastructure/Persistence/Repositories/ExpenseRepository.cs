using System;
using System.Collections.Generic;
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
            var expense = await _dbContext.Expenses.FindAsync(new object[] {id}, cancellationToken);
            return expense;
        }

        public async Task<IEnumerable<Expense>> All(CancellationToken cancellationToken = default)
        {
            return await _dbContext
                .Expenses
                .AsNoTracking()
                .ToListAsync(cancellationToken);
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