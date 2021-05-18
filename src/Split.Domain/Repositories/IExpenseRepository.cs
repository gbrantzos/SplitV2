using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Split.Domain.Models;

namespace Split.Domain.Repositories
{
    public interface IExpenseRepository
    {
        Task<Expense> GetById(int id, CancellationToken cancellationToken = default);
        Task<IEnumerable<Expense>> All(CancellationToken cancellationToken = default);
        Task Save(Expense expense, CancellationToken cancellationToken = default);
        Task Delete(Expense expense, CancellationToken cancellationToken = default);
    }
}