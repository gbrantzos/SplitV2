using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Split.Domain.Models;

namespace Split.Domain.Repositories
{
    public interface IExpenseRepository
    {
        Task<Expense> GetById(int id, CancellationToken cancellationToken = default);
        Task<IEnumerable<Expense>> Query(ExpenseQueryParameters parameters, CancellationToken cancellationToken = default);
        Task Save(Expense expense, CancellationToken cancellationToken = default);
        Task Delete(Expense expense, CancellationToken cancellationToken = default);
    }

    public class ExpenseQueryParameters
    {
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public string Description { get; set; }
        public bool IncludeAll { get; set; }
    }
}