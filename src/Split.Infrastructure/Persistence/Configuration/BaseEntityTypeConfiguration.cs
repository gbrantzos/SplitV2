using System;
using System.Reflection;
using CaseExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Split.Domain.Base;

namespace Split.Infrastructure.Persistence.Configuration
{
    public abstract class BaseEntityTypeConfiguration<TEntity> : IEntityTypeConfiguration<TEntity>
        where TEntity : BaseEntity
    {
        private readonly Type _entityType;

        protected BaseEntityTypeConfiguration()
            => _entityType = GetType().BaseType?.GetGenericArguments()[0];

        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            BaseConfigure(builder);
        }

        private void BaseConfigure(EntityTypeBuilder<TEntity> builder)
        {
            var tableName = _entityType.Name.ToSnakeCase();

            builder.ToTable(tableName);
            builder.HasKey(m => m.Id);
            builder.Property(m => m.Id).HasColumnName(SplitDbConstants.Id);
            builder.Property<DateTime>(SplitDbConstants.CreatedAt);
            builder.Property<DateTime>(SplitDbConstants.EditedAt);

            builder
                .Property(m => m.RowVersion)
                .HasColumnName(SplitDbConstants.RowVersion)
                .IsConcurrencyToken();
            
            // AddDeclaredFields(builder);
        }

        private void AddDeclaredFields(EntityTypeBuilder<TEntity> builder)
        {
            var declaredFields =_entityType.GetProperties(BindingFlags.Public
                                                          | BindingFlags.Instance
                                                          | BindingFlags.DeclaredOnly); 
            foreach (var property in declaredFields)
            {
                builder.Property(property.Name).HasColumnName(property.Name.ToSnakeCase());
            }
        }
    }
}