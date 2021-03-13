namespace Split.Domain.Base
{
    public abstract class BaseEntity
    {
        public int Id { get; private set; }
        public int RowVersion { get; set; } = 1;

        public bool IsNew => Id == 0;
    }
}