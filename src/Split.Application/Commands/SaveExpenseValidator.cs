using FluentValidation;

namespace Split.Application.Commands
{
    public class SaveExpenseValidator : AbstractValidator<SaveExpense>
    {
        public SaveExpenseValidator()
        {
            RuleFor(m => m.Description)
                .NotEmpty()
                .WithMessage($"{nameof(SaveExpense.Description)} cannot be null or empty");
            RuleFor(m => m.Value)
                .GreaterThan(0)
                .WithMessage($"{nameof(SaveExpense.Value)} must be greater than zero");
        }
    }
}