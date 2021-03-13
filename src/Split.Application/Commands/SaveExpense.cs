using Split.Application.Base;

namespace Split.Application.Commands
{
    public class SaveExpense : BaseRequest
    {
        public int Id { get; set; }
        public int RowVersion { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public decimal Amount { get; set; }
        public bool ForOwner { get; set; }
    }
}