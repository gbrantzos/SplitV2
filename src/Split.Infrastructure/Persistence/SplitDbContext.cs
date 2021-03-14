using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Split.Application;
using Split.Domain.Base;
using Split.Domain.Models;

namespace Split.Infrastructure.Persistence
{
    public class SplitDbContext : DbContext, ISplitDbContext
    {
        public DbSet<Expense> Expenses { get; protected set; }

        public SplitDbContext(DbContextOptions<SplitDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(SplitDbContext).Assembly);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
        {
            UpdateCommonColumns();
            return base.SaveChangesAsync(cancellationToken);
        }

        public string DatabaseSchema => Database.GenerateCreateScript();

        private void UpdateCommonColumns()
        {
            var newOrAdded = ChangeTracker
                .Entries()
                .Where(e => e.Entity is BaseEntity &&
                            (e.State == EntityState.Added || e.State == EntityState.Modified));
            foreach (var entry in newOrAdded)
            {
                // Change modification date
                entry.Property(SplitDbConstants.EditedAt).CurrentValue = DateTime.Now;

                // Change created date for new records
                if (entry.State == EntityState.Added)
                    entry.Property(SplitDbConstants.CreatedAt).CurrentValue = DateTime.Now;

                // Proper ROW_VERSION number for modified entries
                if (entry.State == EntityState.Modified)
                {
                    // Force original value of ROW_VERSION to be the value of the request
                    // This way the proper where clause will be constructed.
                    entry.Property(nameof(BaseEntity.RowVersion)).OriginalValue =
                        entry.Property(nameof(BaseEntity.RowVersion)).CurrentValue;

                    // Increase value of ROW_VERSION by 1
                    entry.Property(nameof(BaseEntity.RowVersion)).CurrentValue =
                        (int) entry.Property(nameof(BaseEntity.RowVersion)).CurrentValue + 1;
                }
            }
        }
    }
}