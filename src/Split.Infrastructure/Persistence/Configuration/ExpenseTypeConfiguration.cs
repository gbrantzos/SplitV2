using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Split.Domain.Models;

namespace Split.Infrastructure.Persistence.Configuration
{
    public class ExpenseTypeConfiguration : BaseEntityTypeConfiguration<Expense>
    {
        public override void Configure(EntityTypeBuilder<Expense> builder)
        {
            base.Configure(builder);
            
            var value = builder.OwnsOne(m => m.Value);
            value.Property(v => v.Amount).HasColumnName("value_amount");
            value.Property(v => v.Currency).HasColumnName("value_currency");

            builder.Property(m => m.Description).HasColumnName("description");
            builder.Property(m => m.Category).HasColumnName("category");
            builder.Property(m => m.ForOwner).HasColumnName("for_owner");
            builder.Property(m => m.Description).HasColumnName("description");
            builder.Property(m => m.EntryDate).HasColumnName("entry_date");
            builder.Property(m => m.PaidAt).HasColumnName("paid_at");
        }
    }
}