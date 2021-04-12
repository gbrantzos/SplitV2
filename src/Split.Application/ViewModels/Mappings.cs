using Split.Application.Commands;
using Split.Domain.Models;

namespace Split.Application.ViewModels
{
    public static class Mappings
    {
        public static ExpenseViewModel ToViewModel(this Expense expense)
        {
            return new ExpenseViewModel
            {
                Id = expense.Id,
                RowVersion = expense.RowVersion,
                Description = expense.Description,
                Category = expense.Category,
                EntryDate = expense.EntryDate,
                ForOwner = expense.ForOwner,
                Value = expense.Value.Amount
            };
        }
        
        public static SaveExpense ToCommand(this ExpenseViewModel vm)
        {
            return new SaveExpense
            {
                Id = vm.Id,
                RowVersion = vm.RowVersion,
                Description = vm.Description,
                EntryDate = vm.EntryDate,
                Category = vm.Category,
                ForOwner = vm.ForOwner,
                Value = vm.Value
            };
        }
    }
}