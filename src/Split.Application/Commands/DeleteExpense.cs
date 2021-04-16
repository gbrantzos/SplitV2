using Split.Application.Base;

namespace Split.Application.Commands
{
    public class DeleteExpense : Request
    {
        public int Id { get; init; }
        public int RowVersion { get; init; }
    }
}