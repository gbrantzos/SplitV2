using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Split.Application.Base;
using Split.Application.ViewModels;
using Split.Domain.Models;
using Split.Domain.Repositories;

namespace Split.Application.Commands
{
    public class SaveExpenseHandler : RequestHandler<SaveExpense, ExpenseViewModel>
    {
        private readonly IExpenseRepository _repository;
        private readonly ILogger<SaveExpenseHandler> _logger;

        public SaveExpenseHandler(ILogger<SaveExpenseHandler> logger, IExpenseRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        protected override async Task<Result<ExpenseViewModel>> HandleInternal(SaveExpense request,
            CancellationToken cancellationToken)
        {
            var expense = await _repository.GetById(request.Id, cancellationToken) ?? new Expense();
            if (expense.RowVersion != request.RowVersion)
                return Result.Failure($"Entity changed by other user/process! [ID: {request.Id} - Version: {request.RowVersion}]");
            
            expense.Description = request.Description;
            expense.Category = request.Category;
            expense.Value = Money.InEuro(request.Value);
            expense.ForOwner = request.ForOwner;
            expense.EntryDate = request.EntryDate;
            
            try
            {
                await _repository.Save(expense, cancellationToken);
                return expense.ToViewModel();
            }
            // catch (DbUpdateConcurrencyException ex)
            catch (Exception ex)
            {
                //var entry = ex.Entries[0];
                //_logger.LogError(ex, "Concurrency conflict on table {Table}", entry.Entity.GetType().Name);
                
                _logger.LogError(ex, "Concurrency conflict on expense save!");
                return Result.Failure(ex.Message);
            }
        }
    }
}