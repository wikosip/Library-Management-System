using System.Data.Common;
using Library.DTO;
using Library.Repository.Interfaces;

namespace Library.Repository;

sealed class CustomerMembershipRepository : RepositoryBase<CustomerMembership>, ICustomerMembershipRepository
{
    public CustomerMembershipRepository(DbConnection connection, Func<DbTransaction?> getTransaction) : base(connection, getTransaction)
    {

    }
}