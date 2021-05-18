using System;
using System.Threading;
using System.Threading.Tasks;
using Split.Application.Base;
using Split.Domain.Repositories;

namespace Split.Application.Commands
{
    public class DeleteExpenseHandler : RequestHandler<DeleteExpense>
    {
        private readonly IExpenseRepository _repository;

        public DeleteExpenseHandler(IExpenseRepository repository) => _repository = repository;

        protected override async Task<Result<bool>> HandleInternal(DeleteExpense request, CancellationToken cancellationToken)
        {
            try
            {
                var expense = await _repository.GetById(request.Id, cancellationToken);
                if (expense == null)
                    return Result.Failure("Expense not found! [ID: {request.Id} - Version: {request.RowVersion}]");
                if (expense.RowVersion != request.RowVersion)
                    return Result.Failure($"Entity changed by other user/process! [ID: {request.Id} - Version: {request.RowVersion}]");

                await _repository.Delete(expense, cancellationToken);
                return true;
            }
            catch (Exception e)
            {
                return Result.Failure($"Could not delete expense with ID {request.Id}\\r\\n{e.Message}");
            }
        }
    }
}