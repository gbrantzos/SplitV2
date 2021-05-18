using System.Data.Common;

namespace Split.Domain.Repositories
{
    public interface IDbConnectionProvider
    {
        DbConnection Get();
    }
}