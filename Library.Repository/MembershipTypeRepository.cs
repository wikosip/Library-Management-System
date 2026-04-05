using System.Data.Common;
using Library.DTO;
using Library.Repository.Interfaces;

namespace Library.Repository;

internal sealed class MembershipTypeRepository : RepositoryBase<MembershipType>, IMembershipTypeRepository
{
    public MembershipTypeRepository(DbConnection connection, Func<DbTransaction?> transaction) : base(connection, transaction)
    {

    }
}