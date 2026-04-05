using System.Data.Common;
using System.Security;
using Library.DTO;
using Library.Repository.Interfaces;

namespace Library.Repository;

internal sealed class PermissionRepository : RepositoryBase<Permission>, IPermissionRepository
{
    public PermissionRepository(DbConnection connection, Func<DbTransaction?> transaction) : base(connection, transaction)
    {

    }
}