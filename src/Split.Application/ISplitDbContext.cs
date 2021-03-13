using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Split.Domain.Models;

namespace Split.Application
{
    public interface ISplitDbContext
    {
        DbSet<Expense> Expenses { get; }
        string DatabaseSchema { get; }
        
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}