using System;
using Split.Application.Base;
using Split.Application.ViewModels;

namespace Split.Application.Commands
{
    public class SaveExpense : Request<ExpenseViewModel>
    {
        public int Id { get; init; }
        public int RowVersion { get; init; } = 1;
        public string Description { get; init; }
        public string Category { get; init; }
        public decimal Value { get; init; }
        public DateTime EntryDate { get; init; }
        public bool ForOwner { get; init; }

        public override string ToString() => $"{Description}, {Value}";
    }
}