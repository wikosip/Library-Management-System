using System.Data.Common;
using Library.DTO;
using Library.Repository.Interfaces;

namespace Library.Repository;

internal sealed class PositionRepository : RepositoryBase<Position>, IPositionRepository
{
    public PositionRepository(DbConnection connection, Func<DbTransaction?> transaction) : base(connection, transaction)
    {

    }
}